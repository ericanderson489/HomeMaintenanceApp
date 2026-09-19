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
    public partial class CompleteTaskControl : UserControl
    {
        private Profile? m_profile;
        public CompleteTaskControl()
        {
            InitializeComponent();
        }

        public CompleteTaskControl(Profile profile) : this()
        {
            m_profile = profile;
        }

        private void CheckIfValid_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            foreach (var task in m_profile.GetTaskList())
            {
                if (task.GetName().ToLower().Trim() == taskNameBox.Text.ToLower().Trim())
                {
                    task.SetStatus(Status.Complete);
                    isValid = true;
                }
            }

            if (isValid)
            {
                MessageBox.Show("Task marked as complete.");
            }
            else
            {
                MessageBox.Show("No task with that name in profile!");
            }

            taskNameBox.Text = "";
        }
    }
}
