using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace WindowsApplication1
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button HardIdButton;
        private System.Windows.Forms.TextBox HardIdEdit;
        private TextBox CustomEdit;
        private GroupBox groupBox3;
        private Button button1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

		#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.HardIdEdit = new System.Windows.Forms.TextBox();
            this.HardIdButton = new System.Windows.Forms.Button();
            this.CustomEdit = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.HardIdEdit);
            this.groupBox4.Controls.Add(this.HardIdButton);
            this.groupBox4.Location = new System.Drawing.Point(16, 198);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(412, 88);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PC Hardware ID";
            // 
            // HardIdEdit
            // 
            this.HardIdEdit.Location = new System.Drawing.Point(160, 40);
            this.HardIdEdit.Name = "HardIdEdit";
            this.HardIdEdit.Size = new System.Drawing.Size(241, 20);
            this.HardIdEdit.TabIndex = 1;
            // 
            // HardIdButton
            // 
            this.HardIdButton.Location = new System.Drawing.Point(24, 40);
            this.HardIdButton.Name = "HardIdButton";
            this.HardIdButton.Size = new System.Drawing.Size(112, 23);
            this.HardIdButton.TabIndex = 0;
            this.HardIdButton.Text = "Get Hardware ID";
            this.HardIdButton.Click += new System.EventHandler(this.HardIdButton_Click);
            // 
            // CustomEdit
            // 
            this.CustomEdit.Location = new System.Drawing.Point(24, 58);
            this.CustomEdit.Multiline = true;
            this.CustomEdit.Name = "CustomEdit";
            this.CustomEdit.Size = new System.Drawing.Size(377, 104);
            this.CustomEdit.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.CustomEdit);
            this.groupBox3.Location = new System.Drawing.Point(16, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(412, 170);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "USB Hardware ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Get USB Hardware ID";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 305);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get Hardware ID";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Form1());
        }
        
        private void HardIdButton_Click(object sender, System.EventArgs e)
        {
            StringBuilder HardwareId = new StringBuilder(100);
            WinlicenseSDK.WLHardwareGetIDW(HardwareId);
            this.HardIdEdit.Text = HardwareId.ToString();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int NumberUsbIds = WinlicenseSDK.WLHardwareGetNumberUsbDrives();
            StringBuilder CurrentUsbId = new StringBuilder(100);
            StringBuilder CurrentUsbName = new StringBuilder(100);

            this.CustomEdit.Text = "";

            for (int i = 0; i < NumberUsbIds; i++)
            {
                WinlicenseSDK.WLHardwareGetUsbIdAt(i, CurrentUsbId);
                WinlicenseSDK.WLHardwareGetUsbNameAt(i, CurrentUsbName);

                this.CustomEdit.Text = this.CustomEdit.Text + 
                                       CurrentUsbName.ToString() + "=" +
                                       CurrentUsbId.ToString() + "\r\n";
            }
        }
    }
}