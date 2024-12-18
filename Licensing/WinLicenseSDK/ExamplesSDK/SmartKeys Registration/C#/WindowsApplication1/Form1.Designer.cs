namespace WindowsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ActivateButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SmartKeyEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CustomEdit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CompanyEdit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NameEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ActivateButton
            // 
            this.ActivateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ActivateButton.Location = new System.Drawing.Point(152, 242);
            this.ActivateButton.Name = "ActivateButton";
            this.ActivateButton.Size = new System.Drawing.Size(145, 31);
            this.ActivateButton.TabIndex = 10;
            this.ActivateButton.Text = "Activate";
            this.ActivateButton.UseVisualStyleBackColor = true;
            this.ActivateButton.Click += new System.EventHandler(this.ActivateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "Please, insert your personal Activation Information";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(57, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "This application needs to be activated";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SmartKeyEdit);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.CustomEdit);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.CompanyEdit);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.NameEdit);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(36, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(368, 144);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "License Information";
            // 
            // SmartKeyEdit
            // 
            this.SmartKeyEdit.Location = new System.Drawing.Point(93, 98);
            this.SmartKeyEdit.Name = "SmartKeyEdit";
            this.SmartKeyEdit.Size = new System.Drawing.Size(240, 20);
            this.SmartKeyEdit.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Smart Key:";
            // 
            // CustomEdit
            // 
            this.CustomEdit.Location = new System.Drawing.Point(93, 62);
            this.CustomEdit.Name = "CustomEdit";
            this.CustomEdit.Size = new System.Drawing.Size(240, 20);
            this.CustomEdit.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Custom Data:";
            // 
            // CompanyEdit
            // 
            this.CompanyEdit.Location = new System.Drawing.Point(233, 23);
            this.CompanyEdit.Name = "CompanyEdit";
            this.CompanyEdit.Size = new System.Drawing.Size(100, 20);
            this.CompanyEdit.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(177, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 23);
            this.label6.TabIndex = 2;
            this.label6.Text = "Company:";
            // 
            // NameEdit
            // 
            this.NameEdit.Location = new System.Drawing.Point(64, 24);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.Size = new System.Drawing.Size(100, 20);
            this.NameEdit.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(22, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 308);
            this.Controls.Add(this.ActivateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ActivateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox SmartKeyEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CustomEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CompanyEdit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox NameEdit;
        private System.Windows.Forms.Label label5;
    }
}

