// Program: Durable account and task storage with validated JSON and atomic writes.
// Author: Murdock MacAskill
// Date: 09/29/2026
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HomeMaintenanceApp;

internal sealed record PasswordRecord(string Salt, string Hash, int Iterations);
internal sealed record TaskRecord(Guid Id, string Name, string Description, string Type, DateTime DueDate, Status Status,
    RepeatSchedule Repeat = RepeatSchedule.None, DateTime? ScheduleAnchor = null, int Occurrence = 0,
    Guid? PreviousOccurrenceId = null, DateTime? CompletedAt = null, bool IsArchived = false);
internal sealed record AccountRecord(int Schema, int Revision, string UserName, string FirstName,
    string LastName, PasswordRecord Password, List<TaskRecord> Tasks);

internal static class PasswordSecurity
{
    private const int Iterations = 600_000;

    // A fresh salt keeps equal passwords from producing equal stored hashes.
    internal static PasswordRecord Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        return new(Convert.ToBase64String(salt), Convert.ToBase64String(hash), Iterations);
    }

    internal static bool IsValid(PasswordRecord? record)
    {
        if (record is null || record.Iterations is < 600_000 or > 2_000_000) return false;
        try { return Convert.FromBase64String(record.Salt).Length == 16 && Convert.FromBase64String(record.Hash).Length == 32; }
        catch (Exception ex) when (ex is FormatException or ArgumentNullException) { return false; }
    }

    internal static bool Verify(string password, PasswordRecord record)
    {
        if (!IsValid(record)) return false;
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromBase64String(record.Salt), record.Iterations, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(actual, Convert.FromBase64String(record.Hash));
    }
}

internal sealed class AccountStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    internal string DirectoryPath { get; }

    internal AccountStore()
    {
        // The override supports isolated tests; normal use stores data outside the installation directory.
        DirectoryPath = Environment.GetEnvironmentVariable("HMM_DATA_DIRECTORY")
            ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HomeMaintenanceManager", "Accounts");
    }

    internal List<Account> Load()
    {
        if (!Directory.Exists(DirectoryPath))
        {
            if (File.Exists(DirectoryPath)) throw new IOException("The account storage location is not a folder.");
            return new();
        }
        return Directory.EnumerateFiles(DirectoryPath, "*.json").Select(path =>
        {
            var record = Read(path);
            if (!string.Equals(Path.GetFullPath(path), Path.GetFullPath(PathFor(record.UserName)), StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("An account file has an unexpected name. Existing files have not been changed.");
            return Account.Restore(record, this);
        }).ToList();
    }

    // A revision check rejects stale sessions instead of silently overwriting newer task changes.
    internal AccountRecord Save(AccountRecord record, bool create)
    {
        Directory.CreateDirectory(DirectoryPath);
        using var writeLock = new FileStream(Path.Combine(DirectoryPath, ".write.lock"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        string path = PathFor(record.UserName);
        if (create && File.Exists(path)) throw new ArgumentException("That username already exists.");
        if (!create && (!File.Exists(path) || Read(path).Revision != record.Revision))
            throw new IOException("This account changed in another session. Reopen the app before saving again.");
        var updated = record with { Revision = checked(record.Revision + 1) };
        Validate(updated);
        string temporary = Path.Combine(DirectoryPath, Guid.NewGuid().ToString("N") + ".tmp");
        try
        {
            using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                JsonSerializer.Serialize(stream, updated, JsonOptions);
                stream.Flush(true);
            }
            if (File.Exists(path)) File.Replace(temporary, path, null);
            else File.Move(temporary, path);
            return updated;
        }
        finally
        {
            try { if (File.Exists(temporary)) File.Delete(temporary); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }

    private string PathFor(string username) => Path.Combine(DirectoryPath,
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(username.Trim().ToUpperInvariant()))) + ".json");

    private static AccountRecord Read(string path)
    {
        try
        {
            using var stream = File.OpenRead(path);
            var record = JsonSerializer.Deserialize<AccountRecord>(stream)
                ?? throw new InvalidDataException("An account file is empty.");
            Validate(record);
            return record;
        }
        catch (JsonException ex) { throw new InvalidDataException("An account file is damaged. Existing files have not been changed.", ex); }
    }

    private static void Validate(AccountRecord record)
    {
        if (record.Schema is not (1 or 2 or 3) || record.Revision < 1 || string.IsNullOrWhiteSpace(record.UserName)
            || string.IsNullOrWhiteSpace(record.FirstName) || string.IsNullOrWhiteSpace(record.LastName)
            || !PasswordSecurity.IsValid(record.Password) || record.Tasks is null
            || record.Tasks.Any(t => t is null || t.Id == Guid.Empty || string.IsNullOrWhiteSpace(t.Name)
                || string.IsNullOrWhiteSpace(t.Description) || !TaskRules.Types.Contains(t.Type)
                || !Enum.IsDefined(t.Status) || t.DueDate < new DateTime(1753, 1, 1) || t.DueDate > new DateTime(9998, 12, 31)
                || !Recurrence.IsValid(t) || (record.Schema < 3 && t.IsArchived)
                || (record.Schema == 1 && (t.Repeat != RepeatSchedule.None || t.PreviousOccurrenceId is not null)))
            || record.Tasks.Select(t => t.Id).Distinct().Count() != record.Tasks.Count)
            throw new InvalidDataException("An account file contains invalid or unsupported data. Existing files have not been changed.");
        var byId = record.Tasks.ToDictionary(t => t.Id);
        var successors = record.Tasks.Where(t => t.PreviousOccurrenceId is not null).ToArray();
        if (successors.Select(t => t.PreviousOccurrenceId).Distinct().Count() != successors.Length
            || successors.Any(t => t.PreviousOccurrenceId == t.Id
                || !byId.TryGetValue(t.PreviousOccurrenceId!.Value, out var previous)
                || previous.Status != Status.Complete))
            throw new InvalidDataException("An account contains invalid repeat history. Existing files have not been changed.");
    }
}
