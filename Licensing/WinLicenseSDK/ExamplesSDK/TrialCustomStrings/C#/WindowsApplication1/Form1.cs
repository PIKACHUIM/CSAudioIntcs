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
            StringBuilder BufferVarValue = new StringBuilder(100);

            InitializeComponent();
        }
        
        private void btReadCustomStr_Click(object sender, EventArgs e)
        {
            StringBuilder StringValue = new StringBuilder(500);

            // Get value for "Our String"
            WinlicenseSDK.WLTrialStringRead("Our String", StringValue);
            MessageBox.Show(StringValue.ToString(), "Our String:", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btWriteCustomStr_Click(object sender, EventArgs e)
        {
            WinlicenseSDK.WLTrialStringWrite("Our String", "Test 123");
        }
    }
}