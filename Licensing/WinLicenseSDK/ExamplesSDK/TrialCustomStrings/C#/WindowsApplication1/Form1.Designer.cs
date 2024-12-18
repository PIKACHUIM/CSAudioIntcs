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
            this.btReadCustomStr = new System.Windows.Forms.Button();
            this.btWriteCustomStr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btReadCustomStr
            // 
            this.btReadCustomStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btReadCustomStr.Location = new System.Drawing.Point(50, 105);
            this.btReadCustomStr.Name = "btReadCustomStr";
            this.btReadCustomStr.Size = new System.Drawing.Size(230, 31);
            this.btReadCustomStr.TabIndex = 10;
            this.btReadCustomStr.Text = "Read Custom String";
            this.btReadCustomStr.UseVisualStyleBackColor = true;
            this.btReadCustomStr.Click += new System.EventHandler(this.btReadCustomStr_Click);
            // 
            // btWriteCustomStr
            // 
            this.btWriteCustomStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btWriteCustomStr.Location = new System.Drawing.Point(50, 45);
            this.btWriteCustomStr.Name = "btWriteCustomStr";
            this.btWriteCustomStr.Size = new System.Drawing.Size(230, 31);
            this.btWriteCustomStr.TabIndex = 11;
            this.btWriteCustomStr.Text = "Write Custom String";
            this.btWriteCustomStr.UseVisualStyleBackColor = true;
            this.btWriteCustomStr.Click += new System.EventHandler(this.btWriteCustomStr_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 184);
            this.Controls.Add(this.btWriteCustomStr);
            this.Controls.Add(this.btReadCustomStr);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btReadCustomStr;
        private System.Windows.Forms.Button btWriteCustomStr;
    }
}

