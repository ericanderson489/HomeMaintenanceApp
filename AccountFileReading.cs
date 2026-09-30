namespace HomeMaintenanceApp;

// Compatibility boundary for the existing account screens.
internal class AccountFileReading
{
    private readonly AccountStore store = new();
    public List<Account> GetAccountList() => store.Load();

    public void AddAccount(Account account)
    {
        // Refuse to add accounts to a store that cannot be read safely.
        _ = store.Load();
        account.CreateIn(store);
    }
}
