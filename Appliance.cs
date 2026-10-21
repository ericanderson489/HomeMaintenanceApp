using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMaintenanceApp
{
    enum ApplianceStatus
    {
        InUse,
        InRepair,
        Removed
    }
    internal class Appliance
    {
        private string m_name;
        private string m_description;
        private string m_manufacturer;
        private string m_modelNumber;
        private ApplianceStatus m_status;

        public Appliance(string name, string description, string manufacturer, string modelNumber, ApplianceStatus status)
        {
            m_name = name;
            m_description = description;
            m_manufacturer = manufacturer;
            m_modelNumber = modelNumber;
            m_status = status;
        }

        public string GetName() { return m_name; }
        public void SetName(string name) { m_name = name; }
        public string GetDescription() { return m_description; } 
        public void SetDescription(string description) { m_description = description; }
        public string GetManufacturer() { return m_manufacturer; }
        public void SetManufacturer(string manufacturer) { m_manufacturer = manufacturer; }
        public ApplianceStatus GetStatus() { return m_status; }
        public void SetStatus(ApplianceStatus status) { m_status = status;  }
    }
}
