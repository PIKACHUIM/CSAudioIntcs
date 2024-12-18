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
                StringBuilder UserName = new StringBuilder(256);
                StringBuilder CompanyName = new StringBuilder(256);
                StringBuilder CustomData = new StringBuilder(1024);

                WinlicenseSDK.WLRegGetLicenseInfo(UserName, CompanyName, CustomData);
                MessageBox.Show("The application is already REGISTERED.\nRegistered to: " +
                                UserName, "Text Keys");
                System.Environment.Exit(0);
            }
        }
        
        private void ActivateButton_Click(object sender, EventArgs e)
        {
            // Check if the inserted text key is OK
            bool IsTextKeyCorrect = WinlicenseSDK.WLRegNormalKeyCheck(this.TextKeyEdit.Text);


            // the return value is in "SmartKeyInfo"!!!
            if (!IsTextKeyCorrect)
            {
                MessageBox.Show("Inserted key is INVALID", "ERROR");
            }
            else
            {
                // activation data is correct. Now, we are going to install the Text key as File key 
                WinlicenseSDK.WLRegNormalKeyInstallToFile(this.TextKeyEdit.Text);
                MessageBox.Show("Activation code is CORRECT. Product Registered\nYou must RESTART" +
                                "the application to finish the registration", "Text Keys");
            }
        }
    }
  

}