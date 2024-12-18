using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

             // Check if application is Registered
            int ExtendedInfo = 0;
            int ApplicationStatus = WinlicenseSDK.WLRegGetStatus(ref ExtendedInfo);

            if (ApplicationStatus == 1)
            {
                MessageBox.Show("The application is already REGISTERED", "SmartKeys");
                System.Environment.Exit(0);
            }
        }        
        private void ActivateButton_Click(object sender, EventArgs e)
        {
            bool IsSmartKeyCorrect = WinlicenseSDK.WLRegSmartKeyCheck(
                                            this.NameEdit.Text, this.CompanyEdit.Text,
                                            this.CustomEdit.Text, this.SmartKeyEdit.Text);
            if (!IsSmartKeyCorrect)
            {
                MessageBox.Show("Activation code is INVALID", "ERROR");
            }
            else
            {
                // activation data is correct. Now, we are going to install the smart key as File key 
                // (via WLRegSmartKeyInstallToFile)
                WinlicenseSDK.WLRegSmartKeyInstallToFile(
                        this.NameEdit.Text, this.CompanyEdit.Text,
                        this.CustomEdit.Text, this.SmartKeyEdit.Text);
                MessageBox.Show("Activation code is CORRECT. Product Registered\nYou must RESTART" +
                                "he application to finish the registration", "SmartKeys");
            }
        }
    }    
}