<%@ Page Language="C#" %>
<%@ import Namespace="System.Web.Mail" %>
<%@ import Namespace="System.Runtime.InteropServices" %>
<script runat="server">
    /******************************************************************
     Sample code contributed by Sanjay Kanade, AvniTech.com in 2008. You are 
     responsible for making any changes, testing and using it as per your 
     requirement. The author assumes no responsibility for any bugs or problems 
     arising from the use of the code.

     This is an ASP.NET example of WinLicense SmartKey generation for SWREG.ORG. 
     It has been tested with ASP.NET 1.1 and WinLicense 1.9, 2.0.
      
     HOSTING REQUIREMENT:
     Since this uses InteropServices, it needs asp.net accounts in full trust.
     If your hosting provider does not allow full trust, you will need to use
     a VPS type account. Please test it first and then consult your hosting
     company if it doesn't work. 


     CUSTOMIZE THESE BEFORE USE:
     You will need to customize the following items before using this script. 
     Use Search to find the items in this source.
     * Actual hosted path of WinLicnseSDK.DLL in DllImport statement.
     * LicenseHash variable: put the License Unique Key as string constant from 
       your WinLicense project. The string constant may need to be split on
       multiple lines.


     TESTING: 
     You can always test it locally by using the excellent Cassini Web Server 
     from utildev.com. In your web browser, enter a url something like the 
     following. Replace localhost:49363 with your local web site or the 
     actual online web site.

     http://localhost:49363/test/keygenswreg.aspx?initals=Sanjay&name=Kanade&co_name=AvniTech

     Although the browser will show the codes on one line, click on View Source 
     in the browser to see how it actually goes to swreg.


     HOW IT WORKS:     
     This simple example generates a smart key on Customer Name and Company Name
     from swreg order. The code is returned so that it is received by Swreg
     properly and put in the order notification. You will need to put the logic
     in the actual application to let the user enter the Name, Company Name
     and Code and install the smart key using WinLicenseSDK API. 
     

     IDEAS for future expansion:
     * Put additional checks for calling ip if needed.
     * Allow the customer to generate a key on the delivery name/company name.
       For this, also check those query variables and use them if they are
       present in the order.
     * If you want to display number of copies bought in the application, you
       can probably use the customData parameter to pass it from the corresponding
       swreg field and then display the customdata as copies in the application.
     * For use with stores other than swreg, change the parameter names. If the store
       allows Posting of form variables, for example, Plimus, you need to pass the
       required variables from there as POST and change the code here from 
       Request.QueryString[] to Request.Form[].
     * Send yourself a mail when a key is generated or when invalid requests
       are made.
     * Add the record to a user database after catching more swreg fields like
       email, country, etc.

    ******************************************************************/

    class WinlicenseSDK
    {
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
      [DllImport( "e:/hshome/auser/adir/WinlicenseSDK.dll", 
                 EntryPoint="WLGenLicenseSmartKey", CallingConvention = CallingConvention.StdCall )]
        public static extern int WLGenLicenseSmartKey(string LicenseHash, string UserName, 
                                string Organization, string                                 CustomData, string MachineID,
                               int NumDays, int NumExec, SystemTime NewDate, StringBuilder LicenseKeyBuff);  
     }

   string getCodes(string name, string company) {
            string LicenseHash = 
"7JsMgNl1n2a4tgC96TT8J5rV5Yll8E560p8R742lFgMJWuEY8LjR6wXz9gsR9Jb9Iq326MoL2i1MbcvA87jXKVAaC8RRaTdF7AhP4Mh9JP3Y936YO6EL3J75xIKL2k3xLeY3w1PeQUDdxT6p9m6iS*eccsGAZQGAQHAABACFACCRDXDNSKCKQHQD6MXURXDJEUT5MUI2Y3RABBIPGL7RSZE2NNGUX6XV2VTWX52EAL5QDJK*eccpGBFAGAQHQABACFACCRDXDNSKCKQHQD6MXURXDJEUT5MUI2Y3RABBIPGL7RSZE2NNGUX6XV2VTWX52EAL5QDJKAQVACRESFKZLTAUMN3GFQVFCGCVUSW7T4BX7A*eccefffbfffb0201000281810060490cba825c2c67708ef5cc0f423e109a6d7bda3461a66cb65a99c17f754c778a7f9893f70e120697133bf27b83a632226e4df53949d5c225df395f979f0a13eacceab9fdefb6c405673d9ac5c594198d24b7c6e39435daa4377ea1febd6b4973433b6f43c538b0ec5e71b31f6998c4e8c26c27ee1cef766c479b2bf7f34685020301000102818013c745ae5543665aac382d9d088060bbebe0f4094f6bc4b23da63041db7306ad629d91c3c942d26898a57a91b596063073baa95867777789ad5a64907296988cdcaafd2ab6e0611958d7638120fc2f507cad8535d0e0e684b554fb3870d6c4030b5cda726b8e4b5c422502f22316173cba0332437fa3e54371a1bfce1e47cb890240a89b967ec6fc71de8d268b59c5eb2189425af4bcd31a9e8c55d02c50a6b83ab943d661ea18daa67fb2439e0d92df6e70fe55cff33fd9f18a5e7a7015155d7c4d0240923110e1bcf6f7de00959da513a9836767207565107c304ec2c00f326fccfff62a9b46c5af41494756801123252c542d20a7adb61047129aeda56f6f98002f190241004eecf617c661e993ba132eec091fb60520909f3bbcbe1b35a9fdd6a0b24a4f02e664765534e9354355591da550f95223868af8daef2ac04c7b49692a15643b89024016c272c9621ce4ebf81552ecaf49f5be346cf639dcbe28db2a30b26cfcc0e29c9577852623a9bb59d2daaf50546a3954a93fd1a885923b1049ad36d31dcbf18902408abdc96a5e1fe9b38106"+
"fa1da57c09104e90e93422310f7e96b3fa353d91e248c6d0f2b1b952c31fd1180c1e3795cc0983dfe98411939ff0d482258d619dfb82fffefffe02010002820101007acecd3a76b20d27e619bed333238944b2512c43192a1b56580ea73b386ecaceff495ff5b6a4b76fffc35744e75c5bf2f7dfadfcad94fe2dfd76341c3227a963831daa52748d9707f567ca29fad3e3800508840e81f9202d7d6875ece269c2391d1f9ee7c8fb6bd44568fdeea8cab84887094079ab9b27b75e334695bb635d0a093f0a46f6f406188556482c6cc02d963c6491bfcfd8097265d3f9b8216c81a2c23c58950e0afc50023b48250248af16258e5aeef6dbdfcb45c039f45c416eb65fd09b01aa3951b0a2b0a5b337d5ae6abd21b5323a334675fee7027f207cc3fefbc91a0ca018c815d0246de8c41c4a32ec471d5b4dda1d8ad404a717f91cf57b020301000102820100338aaeecbe1b4d55350812244edeb7c25abaa1c28981b8167d7face538729b70d409061ab8ac9a068a08008cfd3330ae2125423efdd53df6ac5d9b524824883da92e5ea91b9cbf4a0a4fa4b408bf3c48a3142764755d4b097c8c468e049a9176ea07b6e81ddc93ad73f2343d219f6f479551273bc96fa5741ab79784c930534009a1f0b0ba25b0a63711c1a6e1016bfe53a9f80404756110ec7113988250d470b0f397aa9ae7edfd08b60385d879ad5b8166f79e1daa4d5585a15f3aa518586b9ba01b59cd69860ee45aaa742691a975d24febe5596e749bf1e8d045175d7dff5f41c3ebbd2159e705b33340ebaeb08e3666eea9cbe030c2c5650926df3242b1028180b7d7395c1714c3cb364"+
"011f8381f6fb7bf09696cb5ffc1431234b4972b52dbf85f98f68d7f9060b2004ed6c701eaba55e8a8c11ed36395f72a1f75c4d81283c5683d91b641180240546d9237946a2c1c5d013193da7f9d7d6fdcd422e9ef97caea6c960aa52ab525fa2d02cd3a04a356c03c21ca103b664819b681e09837820b028180ab02d6c529c221170bb645501dd287dc90f6b2c17d55df9d8630360f3eef76e1f0f03ede9f689119b2e079e99c21f1d14487707cf89ec2845bf84895dfcdc138d8ad50c400825906a30bb1fa3c2e66f16bf1e6ced43161b869c43d81033b09a339f1bc9fa287715e8303837907f8bd8295f070177bb0a51ca682b52024f8705102818021d255ba2c80e47889558638cd52734231c230841083a9f185f2ee5a755cfc1d18b15282bbba3b1fe4509cd0e5f8622995f11dfe66d3866e7f7064413fa3405cdc0d9fb61109b2b85510077b74318845fb9251a5aae819784f512831f829dd1a36c09d65fdf6fa76af2d02bbf0f5937b4d4882cae2eb9aa89f1a6590855088fb028180342eb3968a3d809765ad82cd05ac9e64819fa5c0411c99159a42cfdc2b1a2b2e9ab4d49a5e0e2c68a91ab258c499df0e3a5911cc30995145ce429ef38ccd5e2e987f9a6b94b431b242c268d6b0d27ffb1f333b78d56b14248fb1a00ae40f79cb62fd0ecfaf6aa0ff73fef879502368c0963c3e180c5844a9cf6d87137df48961028180b3042b8de0a902f10510188e8cc0ccd8a8ca7ec8e390a8e90bc9f8fdc8427e3508b45bfa6de0a8a044dd57587464122a4448d939cb9fb21550a742c5262cda086018e8079388ee0f626a96cb3950171ec722e0ed6257966dc2a1b2fc774cd665bc5eed1b8bb3bfd5ed848ce60cb2b6672dd4978392762fa6c560b255e332b01d";                        
            string          pNullStr = null;
            string          pName = null;
            string          pCompany = null;
            WinlicenseSDK.SystemTime NullExpDateSysTime = null;
            StringBuilder LicenseKeyBuff = new StringBuilder(4000);

            if (name!="")
              pName = name;
            if (company!="")
              pCompany = company;

            WinlicenseSDK.WLGenLicenseSmartKey(LicenseHash, pName, pCompany, pNullStr, pNullStr, 0, 0, NullExpDateSysTime, LicenseKeyBuff);

        return "<softshop>Name: "+name+"\r\nCompany: "+company+"\r\nCode: "+LicenseKeyBuff.ToString()+"</softshop>"; 
   }


   //***********************************************
   void Page_Load (object sender, EventArgs e) {
     //initals is actually "initials". It is not a typo here, it has been like 
     //that in swreg from the beginning.
     
     string fname = Request.QueryString["initals"];
     string lname = Request.QueryString["name"];
     string cname = Request.QueryString["co_name"];
     
     if (lname == null || lname == "")
       Response.Write("Invalid request");
     else 
       Response.Write(getCodes(fname + " " + lname, cname));

     Response.End();
   }
</script>