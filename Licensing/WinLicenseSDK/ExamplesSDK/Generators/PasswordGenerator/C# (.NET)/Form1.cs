/****************************************************************************** 
/* Module: Example
/* Description: Shows how to generate Password in C# language
/*
/* Authors: Rafael Ahucha  
/* (c) 2004 Winlicense Professional
/*****************************************************************************/ 


/****************************************************************************** 
/*                   Libraries used by this module
/*****************************************************************************/ 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;


/****************************************************************************** 
/* Class: WindowsApplication1
/*
/* Description: Main application class
/*
/******************************************************************************/


namespace WindowsApplication1
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameEdit;
        private System.Windows.Forms.TextBox PasswordEdit;
        private System.Windows.Forms.Button GenPassButton;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private const string PasswordHash = "kd7dkcdsflkjdhd63urbcmchGFFJ6sJFDBXKLAIO673jdehf7HFTGdfndj73jfkalasdj8thfgh73k394746sdfjhds6364odfjkhdsf86346kjfhf3";


        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            this.label1 = new System.Windows.Forms.Label();
            this.NameEdit = new System.Windows.Forms.TextBox();
            this.PasswordEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GenPassButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // NameEdit
            // 
            this.NameEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.NameEdit.Location = new System.Drawing.Point(72, 48);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.Size = new System.Drawing.Size(272, 21);
            this.NameEdit.TabIndex = 1;
            this.NameEdit.Text = "";
            // 
            // PasswordEdit
            // 
            this.PasswordEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.PasswordEdit.Location = new System.Drawing.Point(72, 88);
            this.PasswordEdit.Name = "PasswordEdit";
            this.PasswordEdit.ReadOnly = true;
            this.PasswordEdit.Size = new System.Drawing.Size(272, 21);
            this.PasswordEdit.TabIndex = 3;
            this.PasswordEdit.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // GenPassButton
            // 
            this.GenPassButton.Location = new System.Drawing.Point(80, 144);
            this.GenPassButton.Name = "GenPassButton";
            this.GenPassButton.Size = new System.Drawing.Size(208, 23);
            this.GenPassButton.TabIndex = 4;
            this.GenPassButton.Text = "Generate Password";
            this.GenPassButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(360, 205);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.GenPassButton,
                this.PasswordEdit,
                this.label2,
                this.NameEdit,
                this.label1});
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Generator SDK";
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            StringBuilder PasswordBuffer = new StringBuilder(100);

            // generate password for specific user

            WinlicenseSDK.WLGenPassword(PasswordHash, this.NameEdit.Text, PasswordBuffer);

            // show password

            this.PasswordEdit.Text = PasswordBuffer.ToString();
        }
    }


    /****************************************************************************** 
    /* Class: WinlicenseSDK
    /*
    /* Description: Wrapper for the Winlicense SDK APIs
    /*
    /******************************************************************************/

    class WinlicenseSDK
    {
        [DllImport( "WinlicenseSDK.dll", EntryPoint="WLGenPassword", CallingConvention = CallingConvention.StdCall )]
        public static extern int WLGenPassword(string PassHash, string Name, StringBuilder PasswordBuffer);
    }
}
