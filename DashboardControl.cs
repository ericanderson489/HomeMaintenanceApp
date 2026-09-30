using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMaintenanceApp
{
    internal partial class DashboardControl : UserControl
    {
        internal Account account;
        public DashboardControl(Account account)
        {
            InitializeComponent();
            this.account = account;
            greetingLabel.Text = $"Hello {account.GetAccountFirstName()}";
        } 
    }
}
