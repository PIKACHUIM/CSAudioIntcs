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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button GenerateSmartButton;
        private System.Windows.Forms.Button GenerateRegButton;
        private System.Windows.Forms.Button GenerateTextButton;
        private System.Windows.Forms.Button GenerateFileButton;
        private System.Windows.Forms.TextBox CustomEdit;
        private System.Windows.Forms.TextBox HardIdEdit;
        private System.Windows.Forms.TextBox OrgEdit;
        private System.Windows.Forms.TextBox NameEdit;
        private System.Windows.Forms.TextBox NumExecEdit;
        private System.Windows.Forms.TextBox NumDaysEdit;
        private System.Windows.Forms.TextBox SmartEdit;
        private System.Windows.Forms.TextBox RegFileNameEdit;
        private System.Windows.Forms.TextBox RegValueNameEdit;
        private System.Windows.Forms.TextBox RegNameEdit;
        private System.Windows.Forms.TextBox TextEdit;
        private System.Windows.Forms.TextBox FileNameEdit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.CheckBox ExpDateEnabledCheck;
        private const string LicenseHash = "jpEV9ravOc8c18N0EL8sUGMZAUCV4185544B0gm9j12igCnyK3wK8W4bqb1488jtlqcKChjU6gd2GTN4v2D7qJcQjRfI2TM568XA" + 
              "t909Yu9kk2aNAzMfG86yjaxB2h3SUP11Uvr4c1AU8v928k3Oi*eccsGA2AGAQHAABACFACCQLALNL4YVGB6N5DUB7U7AACLSJCMD" + 
              "OTNEBBKAGNB7FZDEW6XTO2FFZ3HNRS7OEB3UGJYII*eccpGBFQGAQHQABACFACCQLALNL4YVGB6N5DUB7U7AACLSJCMDOTNEBBKA" +
              "GNB7FZDEW6XTO2FFZ3HNRS7OEB3UGJYIICCUALM526QDW3SCAFJOATCHW3LXVKMXNCC6KQ*eccefffbfffb020100028180d46dd" +
              "d8d68f53f9c60462d61442dfeccc3a52f5ad58c4b5eb588efd7368e269a19b3c30da7ad89799cb985060c4f44b0f7f56172f" +
              "411f0e04d8162da6117d8560b1471a78dceb4149bcfe11669c80fb40cec7f16a8315287b7e1ebbac7d6d3efb71ae965a0ade" +
              "24b917dea250b7b79389d00c0c58b3e2bdd4bfa252a747407f30203010001028181004e3f19b898cc9d5db068a91686d29a6" +
              "43a3c8230e730022324a3081531d73288d8a6ca5621495eb618f08cfb2955996bc629f0773347d87e068e7e270a548a2e9b6" +
              "29c2151b72b0aac0200d3aa2574d53320a96fd96e6bb725541ff9b896c2c0499a6a346840605087ab27c93a6e22e084747a2" +
              "b92e46862ada8345e722add610240f616e197a9bdb7b8e81e2c361677463286820844e9dfc6aba7d275426bd7c22b8a2fc55" +
              "1d03b2f5644cf25543327421707f3be2439af696c463f2b19d670e87d0240dcfbf4b94052af63e61f9b1c309ad1cabdb5f6e" +
              "0a5033a70c601119ebe263ab600f18c5b38e0382ff72b3b8b0abad0476284dbf17aca11bb92def820c0870d2f024021ee0db" +
              "4a786acea25b6e349a1d7582a67394e8291ec74abfe3b9a474a2f396a5f672ec19500540a84fa3a9f5d5e3052949a1ac14be" +
              "2cf9a4a253380207cf8d102401f3262a6147297d81e90afb1171b842a7f05c54889831f28f48b0a15c2fa5aa77f129aa1f63" +
              "1913d1443c2302adf9cafb734d26810d19e1740f0c14ebe5f52790240f2d64b7c7230f754fe170a85f45c6119671dcad6808" +
              "7bcc51591124a60b954bff641d92716d1d9352eeff330ba3632fa33cb41ec3c2985c47680be391a81e812fffefffe0201000" +
              "2820100b8e0db94eefaa9d34422a47d68bcbd46b2871846c18dd40a84d372c60c1f9a534ceed35e7c2e9564d13ccdb89ef67" +
              "2446c2ab17ce27b22fd504cf07dd29dd708c4a2e46c18ec3bf4979fb855a69783b602117f96a4c45aa65408e26ccfa101ff9" +
              "af3d3f8a70f499056c669b43b90b5368048070a15c86d36b8028d10efd7fd136bbf8b2d31e337ac5bf842b97950e985fd34d" +
              "f00d457c259def8032405a236f949d5f3910f23e2cb48f390c07c8b98b5bd0d9c60991e4898c7a54881f6c7cade25fd6ce4e" +
              "3bef4a837e8771ea7bd6977b0b51f453831aa44e5d93b77b96214285ce751076b9a6d4f19eba3a4caff1b2c529a2389a99ac" +
              "664aad673e43879dfff020301000102820100257a0769106ca5a25aec7ed5a136a953bc7ae033efa675765068a09a7ef8a50" +
              "affcc65f2804fdd1edbbf6ca06130ea06eeba54d25102cf1bf47c343ffd42dbc53e3f1af8c2b09892c36db02b44a2f023241" +
              "631fbf2988f1baa6b46a2f20fdc5cd927562fd085abcee39842f33323e9680be70dee406e299d6efdad64e289b1163713a57" +
              "88cd84dedb7e3e6f39e018de8719b9b113af05502296637afb7416206a884b4633be6a7717a9351640a106b6368372a78d97" +
              "7d71c16a0a1a6657fe1362b76562830a90267f9c848bc7d9c0757159a0de31b9636e0165bb6ede70711b49376ebf3b88b6da" +
              "00af7f503738d75c1e2e36868441f54e64fdb8d02ffcf4341028180c426bb9ee61f123e717f0477d3d9a766ece9e8f5c9ad2" +
              "ee2620b51ba88499b9b50321d26b6738fc977ed05c2dcf8bdd5acbd92b7830487310fab1ef3fb9fcd47727e7ac061129279e" +
              "42d9ba2123b7ba9f0831769b1004cb72413270b19b10ecf9bf6a84cf1a67d8119c97a6077aaed2e54ce89ec5c41b4ff9059d" +
              "5333f6de0e1028180f14999e2b11bf1ab8b5d6baa5c0b5e3781d4704175a9357a68bde2883ed5334a319ca6d6de629a2ff7a" +
              "a603959e290ce26801f3e100e4c4dba8ba4f54d9b49a55550796d1590496b63bcf7913233317a2db40de2c8a0863a0171f61" +
              "9d761c4e557df09d60abe61ee52eaaf02d5ec1dd16d52d48757f43f66212335d55b967cdf0281803b756a463e8788a6390d4" +
              "1b3791a6f2e39fea5c96bbafd8d4e1f144530eeef266366ed8340a7431cd5279a29798645353249bce1b7eb75779fc2457db" +
              "bf6b2e94781a380116ce9cddd271e1840e29b17e67a366ca964e7dc5429221ab14d04aa5622acddb61abbcc989e84641da24" +
              "88754d9cee1410e01e9be3e1cec8dfba6c10281801099ccc1a107d827c24dea236d73c6c6b0e309da271b3e37aecf29afd00" +
              "ae11d80ae53784312aba3133a20a6b17d7a1304e919bd2e639d72579c55fc1e9bc9845b8b4edf677566e7c193f5c082a0ad0" + 
              "dc7e9346f5ef85463d7479cf408328cd66ea644c9fa314111a198cdf3b6fe28cb08e78301d0d6582a51ab577fd76e5227028" +
              "180993a9f45c6fdf5419e7b1691733e86ada7fa40487c26e298bdcaf552617e10ae1b906276cbf31eca0dc9d29781663932e" +
              "ecbd1a2fa118bf2fccdd746e1fdecb10695063945e884feb93d5b06f6156dea489f82e5203cb93fead7aebb7d3c40329101b" +
              "8218f3d1cd46bdf1f7837ccd68873f854fcb600423948f9c9d956298e2a";


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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CustomEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HardIdEdit = new System.Windows.Forms.TextBox();
            this.OrgEdit = new System.Windows.Forms.TextBox();
            this.NameEdit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ExpDateEnabledCheck = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.NumExecEdit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NumDaysEdit = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.SmartEdit = new System.Windows.Forms.TextBox();
            this.GenerateSmartButton = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.GenerateRegButton = new System.Windows.Forms.Button();
            this.RegFileNameEdit = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.RegValueNameEdit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.RegNameEdit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.GenerateTextButton = new System.Windows.Forms.Button();
            this.TextEdit = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GenerateFileButton = new System.Windows.Forms.Button();
            this.FileNameEdit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.CustomEdit,
                                                                                    this.label4,
                                                                                    this.HardIdEdit,
                                                                                    this.OrgEdit,
                                                                                    this.NameEdit,
                                                                                    this.label3,
                                                                                    this.label2,
                                                                                    this.label1});
            this.groupBox1.Location = new System.Drawing.Point(32, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User\'s Information";
            // 
            // CustomEdit
            // 
            this.CustomEdit.Location = new System.Drawing.Point(328, 24);
            this.CustomEdit.Multiline = true;
            this.CustomEdit.Name = "CustomEdit";
            this.CustomEdit.Size = new System.Drawing.Size(100, 88);
            this.CustomEdit.TabIndex = 7;
            this.CustomEdit.Text = "";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(232, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 32);
            this.label4.TabIndex = 6;
            this.label4.Text = "Custom Data:";
            // 
            // HardIdEdit
            // 
            this.HardIdEdit.Location = new System.Drawing.Point(96, 88);
            this.HardIdEdit.Name = "HardIdEdit";
            this.HardIdEdit.TabIndex = 5;
            this.HardIdEdit.Text = "";
            // 
            // OrgEdit
            // 
            this.OrgEdit.Location = new System.Drawing.Point(96, 56);
            this.OrgEdit.Name = "OrgEdit";
            this.OrgEdit.TabIndex = 4;
            this.OrgEdit.Text = "";
            // 
            // NameEdit
            // 
            this.NameEdit.Location = new System.Drawing.Point(96, 24);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.TabIndex = 3;
            this.NameEdit.Text = "";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hardware ID:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Organization:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(48, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.ExpDateEnabledCheck,
                                                                                    this.label7,
                                                                                    this.dateTimePicker1,
                                                                                    this.NumExecEdit,
                                                                                    this.label6,
                                                                                    this.label5,
                                                                                    this.NumDaysEdit});
            this.groupBox2.Location = new System.Drawing.Point(32, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Key Expiration Information";
            // 
            // ExpDateEnabledCheck
            // 
            this.ExpDateEnabledCheck.Location = new System.Drawing.Point(112, 112);
            this.ExpDateEnabledCheck.Name = "ExpDateEnabledCheck";
            this.ExpDateEnabledCheck.Size = new System.Drawing.Size(176, 24);
            this.ExpDateEnabledCheck.TabIndex = 6;
            this.ExpDateEnabledCheck.Text = "Enable Expiration Date";
            this.ExpDateEnabledCheck.CheckedChanged += new System.EventHandler(this.ExpDateEnabledCheck_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 23);
            this.label7.TabIndex = 5;
            this.label7.Text = "Expiration Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(112, 88);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(208, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // NumExecEdit
            // 
            this.NumExecEdit.Location = new System.Drawing.Point(112, 56);
            this.NumExecEdit.Name = "NumExecEdit";
            this.NumExecEdit.TabIndex = 3;
            this.NumExecEdit.Text = "";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "Max. Executions:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "Number Days:";
            // 
            // NumDaysEdit
            // 
            this.NumDaysEdit.Location = new System.Drawing.Point(112, 24);
            this.NumDaysEdit.Name = "NumDaysEdit";
            this.NumDaysEdit.TabIndex = 0;
            this.NumDaysEdit.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.groupBox7,
                                                                                    this.groupBox6,
                                                                                    this.groupBox5,
                                                                                    this.groupBox4});
            this.groupBox3.Location = new System.Drawing.Point(32, 352);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(472, 280);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Type Generated Key";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.SmartEdit,
                                                                                    this.GenerateSmartButton});
            this.groupBox7.Location = new System.Drawing.Point(240, 176);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(216, 88);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "SmartActivate Key";
            // 
            // SmartEdit
            // 
            this.SmartEdit.Location = new System.Drawing.Point(16, 56);
            this.SmartEdit.Name = "SmartEdit";
            this.SmartEdit.ReadOnly = true;
            this.SmartEdit.Size = new System.Drawing.Size(184, 20);
            this.SmartEdit.TabIndex = 9;
            this.SmartEdit.Text = "";
            // 
            // GenerateSmartButton
            // 
            this.GenerateSmartButton.Location = new System.Drawing.Point(40, 24);
            this.GenerateSmartButton.Name = "GenerateSmartButton";
            this.GenerateSmartButton.Size = new System.Drawing.Size(128, 20);
            this.GenerateSmartButton.TabIndex = 8;
            this.GenerateSmartButton.Text = "Generate Smart Key";
            this.GenerateSmartButton.Click += new System.EventHandler(this.GenerateSmartButton_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.GenerateRegButton,
                                                                                    this.RegFileNameEdit,
                                                                                    this.label11,
                                                                                    this.RegValueNameEdit,
                                                                                    this.label10,
                                                                                    this.RegNameEdit,
                                                                                    this.label9,
                                                                                    this.textBox9});
            this.groupBox6.Location = new System.Drawing.Point(232, 24);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(224, 144);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Registry key";
            // 
            // GenerateRegButton
            // 
            this.GenerateRegButton.Location = new System.Drawing.Point(40, 104);
            this.GenerateRegButton.Name = "GenerateRegButton";
            this.GenerateRegButton.Size = new System.Drawing.Size(152, 24);
            this.GenerateRegButton.TabIndex = 7;
            this.GenerateRegButton.Text = "Generate Reg File Key";
            this.GenerateRegButton.Click += new System.EventHandler(this.GenerateRegButton_Click);
            // 
            // RegFileNameEdit
            // 
            this.RegFileNameEdit.Location = new System.Drawing.Point(112, 72);
            this.RegFileNameEdit.Name = "RegFileNameEdit";
            this.RegFileNameEdit.Size = new System.Drawing.Size(96, 20);
            this.RegFileNameEdit.TabIndex = 6;
            this.RegFileNameEdit.Text = "License.reg";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(16, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 23);
            this.label11.TabIndex = 5;
            this.label11.Text = "File Name  (*.reg)";
            // 
            // RegValueNameEdit
            // 
            this.RegValueNameEdit.Location = new System.Drawing.Point(112, 48);
            this.RegValueNameEdit.Name = "RegValueNameEdit";
            this.RegValueNameEdit.Size = new System.Drawing.Size(96, 20);
            this.RegValueNameEdit.TabIndex = 4;
            this.RegValueNameEdit.Text = "reg_value";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 23);
            this.label10.TabIndex = 3;
            this.label10.Text = "Key Value Name:";
            // 
            // RegNameEdit
            // 
            this.RegNameEdit.Location = new System.Drawing.Point(120, 24);
            this.RegNameEdit.Name = "RegNameEdit";
            this.RegNameEdit.Size = new System.Drawing.Size(88, 20);
            this.RegNameEdit.TabIndex = 2;
            this.RegNameEdit.Text = "Software\\Mysoft";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 23);
            this.label9.TabIndex = 1;
            this.label9.Text = "Key Name:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(72, 24);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(48, 20);
            this.textBox9.TabIndex = 0;
            this.textBox9.Text = "HKLM\\";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.GenerateTextButton,
                                                                                    this.TextEdit});
            this.groupBox5.Location = new System.Drawing.Point(24, 128);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 136);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Text Key";
            // 
            // GenerateTextButton
            // 
            this.GenerateTextButton.Location = new System.Drawing.Point(32, 24);
            this.GenerateTextButton.Name = "GenerateTextButton";
            this.GenerateTextButton.Size = new System.Drawing.Size(120, 24);
            this.GenerateTextButton.TabIndex = 3;
            this.GenerateTextButton.Text = "Generate Text Key";
            this.GenerateTextButton.Click += new System.EventHandler(this.GenerateTextButton_Click);
            // 
            // TextEdit
            // 
            this.TextEdit.Location = new System.Drawing.Point(24, 64);
            this.TextEdit.Multiline = true;
            this.TextEdit.Name = "TextEdit";
            this.TextEdit.ReadOnly = true;
            this.TextEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextEdit.Size = new System.Drawing.Size(152, 64);
            this.TextEdit.TabIndex = 1;
            this.TextEdit.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.GenerateFileButton,
                                                                                    this.FileNameEdit,
                                                                                    this.label8});
            this.groupBox4.Location = new System.Drawing.Point(24, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 96);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "File Key";
            // 
            // GenerateFileButton
            // 
            this.GenerateFileButton.Location = new System.Drawing.Point(24, 56);
            this.GenerateFileButton.Name = "GenerateFileButton";
            this.GenerateFileButton.Size = new System.Drawing.Size(120, 24);
            this.GenerateFileButton.TabIndex = 2;
            this.GenerateFileButton.Text = "Generate File Key";
            this.GenerateFileButton.Click += new System.EventHandler(this.GenerateFileButton_Click);
            // 
            // FileNameEdit
            // 
            this.FileNameEdit.Location = new System.Drawing.Point(72, 16);
            this.FileNameEdit.Name = "FileNameEdit";
            this.FileNameEdit.TabIndex = 1;
            this.FileNameEdit.Text = "License.dat";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "File Name:";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(536, 645);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.groupBox3,
                                                                          this.groupBox2,
                                                                          this.groupBox1});
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "License Key Generator SDK";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
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


        private void ExpDateEnabledCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ExpDateEnabledCheck.Checked == false)
            {
                this.dateTimePicker1.Enabled = false;
            }
            else
            {
                this.dateTimePicker1.Enabled = true;
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
        
        }

        private void GenerateFileButton_Click(object sender, System.EventArgs e)
        {

            byte[]          LicenseKeyBuff = new byte [4000];
            string          pNameEdit = (this.NameEdit.Text.Length == 0)? null : this.NameEdit.Text;
            string          pOrgEdit = (this.OrgEdit.Text.Length == 0)? null : this.OrgEdit.Text;
            string          pCustomEdit = (this.CustomEdit.Text.Length == 0)? null : this.CustomEdit.Text;
            string          pHardIdEdit = (this.HardIdEdit.Text.Length == 0)? null : this.HardIdEdit.Text;
            int             mNumDays = (this.NumDaysEdit.Text.Length == 0)? 0 : Convert.ToInt32(NumDaysEdit.Text); 
            int             mNumExec = (this.NumExecEdit.Text.Length == 0)? 0 :  Convert.ToInt32(NumExecEdit.Text);
            SystemTime      ExpDateSysTime = new SystemTime();
            SystemTime      NullExpDateSysTime = null;
            int             SizeKey;

            // get the current date from dateTimePicker1

            ExpDateSysTime.wYear = (short)dateTimePicker1.Value.Year;
            ExpDateSysTime.wMonth = (short)dateTimePicker1.Value.Month;
            ExpDateSysTime.wDay = (short)dateTimePicker1.Value.Day;

            // generate license file

            if (this.ExpDateEnabledCheck.Checked == false)
            {
                SizeKey = WinlicenseSDK.WLGenLicenseFileKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, NullExpDateSysTime, 0, 0, 0, LicenseKeyBuff);
            }
            else
            {
                SizeKey = WinlicenseSDK.WLGenLicenseFileKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, ExpDateSysTime, 0, 0, 0, LicenseKeyBuff);
            }

            // Create file to store the generated key

            FileStream fs = new FileStream(this.FileNameEdit.Text, FileMode.Create);
        
            BinaryWriter w = new BinaryWriter(fs);
        
            // Write data to file

            for (int i = 0; i < SizeKey; i++)
            {
                w.Write((byte)LicenseKeyBuff[i]);
            }

            // Close file
        
            w.Close();
            fs.Close();

            MessageBox.Show("A License key has been generated", "Winlicense");
        }

        private void GenerateTextButton_Click(object sender, System.EventArgs e)
        {
            StringBuilder   LicenseKeyBuff = new StringBuilder(4000);
            string          pNameEdit = (this.NameEdit.Text.Length == 0)? null : this.NameEdit.Text;
            string          pOrgEdit = (this.OrgEdit.Text.Length == 0)? null : this.OrgEdit.Text;
            string          pCustomEdit = (this.CustomEdit.Text.Length == 0)? null : this.CustomEdit.Text;
            string          pHardIdEdit = (this.HardIdEdit.Text.Length == 0)? null : this.HardIdEdit.Text;
            int             mNumDays = (this.NumDaysEdit.Text.Length == 0)? 0 : Convert.ToInt32(NumDaysEdit.Text); 
            int             mNumExec = (this.NumExecEdit.Text.Length == 0)? 0 :  Convert.ToInt32(NumExecEdit.Text);
            SystemTime      ExpDateSysTime = new SystemTime();
            SystemTime      NullExpDateSysTime = null;
            int             SizeKey;

            // get the current date from dateTimePicker1

            ExpDateSysTime.wYear = (short)dateTimePicker1.Value.Year;
            ExpDateSysTime.wMonth = (short)dateTimePicker1.Value.Month;
            ExpDateSysTime.wDay = (short)dateTimePicker1.Value.Day;

            // generate license file

            if (this.ExpDateEnabledCheck.Checked == false)
            {
                SizeKey = WinlicenseSDK.WLGenLicenseTextKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, NullExpDateSysTime, 0, 0, 0, LicenseKeyBuff);
            }
            else
            {
                SizeKey = WinlicenseSDK.WLGenLicenseTextKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, ExpDateSysTime, 0, 0, 0, LicenseKeyBuff);
            }

            // show the generated key on the GUI
                        
            TextEdit.Text = LicenseKeyBuff.ToString();
        }

        private void GenerateSmartButton_Click(object sender, System.EventArgs e)
        {

            StringBuilder   LicenseKeyBuff = new StringBuilder(1000);
            string          pNameEdit = (this.NameEdit.Text.Length == 0)? null : this.NameEdit.Text;
            string          pOrgEdit = (this.OrgEdit.Text.Length == 0)? null : this.OrgEdit.Text;
            string          pCustomEdit = (this.CustomEdit.Text.Length == 0)? null : this.CustomEdit.Text;
            string          pHardIdEdit = (this.HardIdEdit.Text.Length == 0)? null : this.HardIdEdit.Text;
            int             mNumDays = (this.NumDaysEdit.Text.Length == 0)? 0 : Convert.ToInt32(NumDaysEdit.Text); 
            int             mNumExec = (this.NumExecEdit.Text.Length == 0)? 0 :  Convert.ToInt32(NumExecEdit.Text);
            SystemTime      ExpDateSysTime = new SystemTime();
            SystemTime      NullExpDateSysTime = null;
            int             SizeKey;

            // get the current date from dateTimePicker1

            ExpDateSysTime.wYear = (short)dateTimePicker1.Value.Year;
            ExpDateSysTime.wMonth = (short)dateTimePicker1.Value.Month;
            ExpDateSysTime.wDay = (short)dateTimePicker1.Value.Day;

            // generate license file

            if (this.ExpDateEnabledCheck.Checked == false)
            {
                SizeKey = WinlicenseSDK.WLGenLicenseSmartKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, NullExpDateSysTime, LicenseKeyBuff);
            }
            else
            {
                SizeKey = WinlicenseSDK.WLGenLicenseSmartKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, ExpDateSysTime, LicenseKeyBuff);
            }

            // show the generated key on the GUI
                        
            SmartEdit.Text = LicenseKeyBuff.ToString();

        }

        private void GenerateRegButton_Click(object sender, System.EventArgs e)
        {
            byte[]          LicenseKeyBuff = new byte [4000];
            string          pNameEdit = (this.NameEdit.Text.Length == 0)? null : this.NameEdit.Text;
            string          pOrgEdit = (this.OrgEdit.Text.Length == 0)? null : this.OrgEdit.Text;
            string          pCustomEdit = (this.CustomEdit.Text.Length == 0)? null : this.CustomEdit.Text;
            string          pHardIdEdit = (this.HardIdEdit.Text.Length == 0)? null : this.HardIdEdit.Text;
            int             mNumDays = (this.NumDaysEdit.Text.Length == 0)? 0 : Convert.ToInt32(NumDaysEdit.Text); 
            int             mNumExec = (this.NumExecEdit.Text.Length == 0)? 0 :  Convert.ToInt32(NumExecEdit.Text);
            SystemTime      ExpDateSysTime = new SystemTime();
            SystemTime      NullExpDateSysTime = null;
            int             SizeKey;
            string          RegName = "HKEY_LOCAL_MACHINE\\" + this.RegNameEdit.Text;

            // get the current date from dateTimePicker1

            ExpDateSysTime.wYear = (short)dateTimePicker1.Value.Year;
            ExpDateSysTime.wMonth = (short)dateTimePicker1.Value.Month;
            ExpDateSysTime.wDay = (short)dateTimePicker1.Value.Day;

            // generate license file

            if (this.ExpDateEnabledCheck.Checked == false)
            {
                SizeKey = WinlicenseSDK.WLGenLicenseRegistryKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, NullExpDateSysTime, 0, 0, 0, RegName, this.RegValueNameEdit.Text, LicenseKeyBuff);
            }
            else
            {
                SizeKey = WinlicenseSDK.WLGenLicenseRegistryKeyW(LicenseHash, pNameEdit, pOrgEdit, pCustomEdit, pHardIdEdit, mNumDays, mNumExec, ExpDateSysTime, 0, 0, 0, RegName, this.RegValueNameEdit.Text, LicenseKeyBuff);
            }

            // Create file to store the generated key

            FileStream fs = new FileStream(this.RegFileNameEdit.Text, FileMode.Create);
        
            BinaryWriter w = new BinaryWriter(fs);
        
            // Write data to file

            for (int i = 0; i < SizeKey; i++)
            {
                w.Write((byte)LicenseKeyBuff[i]);
            }

            // Close file
        
            w.Close();
            fs.Close();

            MessageBox.Show("A Registry file key has been generated", "Winlicense");
    
        }

  
 	}

    /****************************************************************************** 
    /* Required structures for WinLicense SDK functions
    /******************************************************************************/
    
    [StructLayout(LayoutKind.Sequential)]
    public class SystemTime
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class sLicenseFeatures
    {
        public int cb;
        public int NumDays;
        public int NumExec;
        public SystemTime ExpDate;
        public int CountryId;
        public int Runtime;
        public int GlobalMinutes;
        public SystemTime InstallDate;
        public int NetInstances;
        public int EmbedLicenseInfoInKey;
        public int EmbedCreationDate;
    }

    /****************************************************************************** 
    /* Class: WinlicenseSDK
    /*
    /* Description: Wrapper for the Winlicense SDK APIs
    /*
    /******************************************************************************/
 
    class WinlicenseSDK
    {
        #if WIN32
            const string WINLICENSE_DLL = "WinlicenseSDK.dll";
        #else
            const string WINLICENSE_DLL = "WinlicenseSDK64.dll";
        #endif

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenPassword", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenPassword(string PassHash, string Name, StringBuilder PasswordBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenTrialExtensionFileKey", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenTrialExtensionFileKey(string TrialHash, int Level, int NumDays, int NumExec, int NewDate, int NumMinutes, int TimeRuntime, byte[] BufferOut);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseFileKey", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseFileKey(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
                             int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, int GlobalTime, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseFileKeyW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseFileKeyW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
                             int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, int GlobalTime, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseRegistryKey", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseRegistryKey(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, int GlobalTime, string RegName, string RegValueName, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseRegistryKeyW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseRegistryKeyW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, int GlobalTime, string RegName, string RegValueName, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseTextKey", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseTextKey(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, int GlobalTime, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseTextKeyW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseTextKeyW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, int GlobalTime, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseSmartKey", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseSmartKey(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseSmartKeyW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseSmartKeyW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseFileKeyEx", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseFileKeyEx(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseFileKeyExW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseFileKeyExW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseTextKeyEx", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseTextKeyEx(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseTextKeyExW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseTextKeyExW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseRegistryKeyEx", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseRegistryKey(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, string RegName, string RegValueName, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseRegistryKeyExW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseRegistryKeyW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, string RegName, string RegValueName, byte[] LicenseBuffer);

        [DllImport(WINLICENSE_DLL, EntryPoint = "WLGenLicenseDynSmartKey", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseDynSmartKey(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

        [DllImport(WINLICENSE_DLL, CharSet = CharSet.Unicode, EntryPoint = "WLGenLicenseDynSmartKeyW", CallingConvention = CallingConvention.StdCall)]
        public static extern int WLGenLicenseDynSmartKeyW(string LicenseHash, string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);
    }


}


