unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ComCtrls, WinlicenseSDK;

const

LicenseHash = '<main_hash>6J913mE6P32E1KtW7oK9dAlb8Q76Jtuiq248B84Jfm2ex6Ova9t84V8krB4z8Rx8EWEJoCFEcx5lY40bD1uDn55AJ' +
              '3Fq5n76bp0H9B2iXDd50Ai2jF7rgm8Y04STW6Fwsdhj2t46IPq277D6kKh1lE</main_hash><ecc_public>GA2AGAQHAABACFA' +
              'CCRENBPB7XC3FFGLXTCQK6VXBDFSTAWRD7ABBKAF4XI6XUXG7RHZ6DAZ2SD5QKLPEPBG235Q</ecc_public><ecc_private>GB' +
              'FAGAQHQABACFACCRENBPB7XC3FFGLXTCQK6VXBDFSTAWRD7ABBKAF4XI6XUXG7RHZ6DAZ2SD5QKLPEPBG235QCCQ2JSJAIHSWYA3' +
              'J4DVLSCLIRHESD3LSLVE</ecc_private><rsa_public_1>028201010069481dd49cf843fdc423f97e4c3d4e7f802b98c880' +
              '68556c7211793e4617341bd97465f7a9c2910c8aab1c71ab6e4045c23b2485f80d474a8afa32f874d1deb02ac2b500a9010b' +
              'c6f196b253245fd613d11b7e49563ebcf56a54836aae706a2e7ff2cba1718f87e0ba8db6a5eb0f9b1990565cc37b26f75286' +
              '0df4d0fb8eada6a388722bff208f42b893868a47446f65e4d14e1ec4870da192a363cdb979905de94fc2c65461de128da775' +
              '5162f496de1143b307c943c6e2fa67897da704d0dc76d377b5db2b93bf295f7be7eb1e6283c4451b93d8549c661bfbfea802' +
              '62296f24afc96337ded0e4724cc3470abf40ca6951bc8af2f792b4e0cf23b26f5974330203010001</rsa_public_1><rsa_' +
              'private_1>020100028201010069481dd49cf843fdc423f97e4c3d4e7f802b98c88068556c7211793e4617341bd97465f7a9' +
              'c2910c8aab1c71ab6e4045c23b2485f80d474a8afa32f874d1deb02ac2b500a9010bc6f196b253245fd613d11b7e49563ebc' +
              'f56a54836aae706a2e7ff2cba1718f87e0ba8db6a5eb0f9b1990565cc37b26f752860df4d0fb8eada6a388722bff208f42b8' +
              '93868a47446f65e4d14e1ec4870da192a363cdb979905de94fc2c65461de128da7755162f496de1143b307c943c6e2fa6789' +
              '7da704d0dc76d377b5db2b93bf295f7be7eb1e6283c4451b93d8549c661bfbfea80262296f24afc96337ded0e4724cc3470a' +
              'bf40ca6951bc8af2f792b4e0cf23b26f597433020301000102820100326a4556a5e040f6c9310ced4ce6e2ab2d9e32e8788c' +
              'fdb313a08875a8acbdd844367809b3f226f16189f4f1fdccaadc7cd943711fca84040f2f26af51899a60b3e94f31c4bc6a7e' +
              '56fbf9e2d47d4c0f6b48061e6a3d5ad10e9cefdd6b310bb7beaf6e919dc6c4a379d218cfb513610b49d20153deaa572ede60' +
              'da8624a03b48fd5861976478d64acba9708f3f6170c088d53d743b9812510d247c512f91f48e70db03597654c23ce1203d46' +
              '5757eef492339216db6a85a6505868b8b74a7475e97a13a44f15bad9a13f459a0abf0aacda50792e920c0481fd9e90fb2697' +
              '68b6db0174ffd8d6c2061b2cbf7346c63d49bc95fce640b595073ab1a6c3efd3e6f9028180bb4010dff6446f1b8dbeda1dce' +
              '58860af7e6bd277c2a9559c9f2a3a4023c2209d2cbb883cd4028dc4db6634395821ac552bb45101e300df123c7f7fcc96431' +
              '2625b7e0cee89447b8ba90b84140544dd27675d7e179c564430c52b6d3a0fcfd07efb88e55dce68673373d1930f4ff253c50' +
              'b541e59797301f34409808e3704abd0281808fefb3e85c39b536c9f3f4747fe607707f275a1cce46debe373de043b8dcb36c' +
              'f2af6f65ecc71bc29397f931a613f293baf78fdfb09cdcb521c53cf7ebd1d8d17d3f361acc2b7da8c5273f5eb7923760d303' +
              'f0abec5a4a51c8f94e898649395efeab172e9f3fe36cd2fbcdf20b545119836685da212cf6b6dc32422b250421af0281800b' +
              'd7cc796dc8fc4200e00e498e507901de960066abeac191713d606e3219fd892384f1019d0caec8e83d5ddfdb67e42b90c4f9' +
              '644094877d35e722df2119ae261c8299bc5c676c75d98ee7a870da2425e88a218248425cca04d08271b61fea1d036367d534' +
              '687c1e7362f0e1f0cce65cff39c7962ed5481ce12028bd7f2d0dfd02818010ca58854984ddd3c75932f2081d704eba367ff7' +
              '8e448c254a52687fdde7b8f28398dd78d9571cb0e500bc6c03409e728743ebbf9f5d9d0d70240f09a8b3b104370511adac95' +
              '823216d58e39e15628299e53fe563fdb048267dc55420bad2d091cd21e812cc88a221eb68afeaa3daabc3c2cd07e72924931' +
              '699c5f45a32ac937028180b43936e069f0edaee763d98890a32b48af30ccadebf183db95f684f27a478af88ccbedeb9761c4' +
              '7cbf89cba4d2542fcb7b0b0d6b86e0f38b3c6a3c07c30d064663a9e284e2e4387c5e0793ab13fb6926d6e08e0bca7b87fb90' +
              'ac03458de986076f3e85b1f37bd012ddb8fc248b3996d85f187c54c0aa4669628a914dbc39ce0e</rsa_private_1><rsa_p' +
              'ublic_2>02818096438f41bc78b6b1a28af6c92051a2ba7f976985e19c741f08b70b74a710406e4307018a6e71181b2c3007' +
              '96f925b8aea3f00f03691dcf3be061a0ea491f72c55844a1c57a0717cb65f0efa2c5bb1ff83cecb18876f9db5cef7356d85b' +
              'ab628c8cd3aaf92e6e6ab7fdaf81055687d21f5f943d6dc168a083dd29793f215eb22d0203010001</rsa_public_2><rsa_' +
              'private_2>02010002818096438f41bc78b6b1a28af6c92051a2ba7f976985e19c741f08b70b74a710406e4307018a6e7118' +
              '1b2c300796f925b8aea3f00f03691dcf3be061a0ea491f72c55844a1c57a0717cb65f0efa2c5bb1ff83cecb18876f9db5cef' +
              '7356d85bab628c8cd3aaf92e6e6ab7fdaf81055687d21f5f943d6dc168a083dd29793f215eb22d020301000102818003ca3e' +
              '71c504326f768f14bc6bf1432e7b0b61a0fbd8cb61010244eab350089fff56cce625f4fa4bad28a4f85cde63b31bf5711b7f' +
              'ea3d90e00ef6551275159af4978fdcca6e0045efa5396a6d4506645f309a2e139d4090ae53f4a7249f05a03d9598c1bcbb3e' +
              'eda3830cc867fa7b6c659e5941f8e085c9a10b21b141f0bde10240ec0b67949a89d29c917465c8181e42417c99f585366438' +
              'c575aec7621c8e3e610a055237aa2f80e593e50e38e136a4061475fb02f0b9cda44cc34d32ca618c150240a2f7a59db389dc' +
              '5880ad50244a456abd2a69c0b5627339e8f42bdc9263f28387789c1db2e2197ccfb658437e218cbe25858b26f4a0e805cd50' +
              'c8dd7808015bb9024100427bd7d9e0492f7c0ff003f6feec0ee7590c183e441aa86edd7db404eb5c192fc5ddc7d18beac358' +
              '66769eb1e0a53e3fc42592e4ba1eeb4ffa9b0bb46192661d024097f2f436d05a305b4ccf4c342a14036c1189bd26e27eb176' +
              '2584680b0ea9e63d801580137481734070b2676bc978632ab51383e4ffca96ee52ee96a0fe45d5f10240b0d4ebc59f430f7f' +
              'fb38beb2b36b01f1606d79cd2b3dd40d18fb3be3c2eead2b46436f1c5252198731d9f57934194e756b26fc093d97193ce968' +
              '80e8557d5e4a</rsa_private_2>'              



type
  TForm1 = class(TForm)
    GroupBox1: TGroupBox;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    NameEdit: TEdit;
    OrgEdit: TEdit;
    HardIdEdit: TEdit;
    CustomEdit: TEdit;
    GroupBox2: TGroupBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    NumDaysEdit: TEdit;
    NumExecEdit: TEdit;
    ExpDate: TDateTimePicker;
    EnableDateCheck: TCheckBox;
    GroupBox3: TGroupBox;
    GroupBox4: TGroupBox;
    Label4: TLabel;
    FileNameEdit: TEdit;
    GenFileKey: TButton;
    GroupBox5: TGroupBox;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    RegistryEdit: TEdit;
    Button2: TButton;
    Edit3: TEdit;
    RegValueEdit: TEdit;
    LicRegEdit: TEdit;
    RadioGroup1: TRadioGroup;
    Button3: TButton;
    GroupBox6: TGroupBox;
    GenStaticSmartKey: TButton;
    SmartEdit: TEdit;
    TextKeyMemo: TMemo;
    GenDynamicSmartKey: TButton;
    procedure GenFileKeyClick(Sender: TObject);
    procedure EnableDateCheckClick(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure GenStaticSmartKeyClick(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure GenDynamicSmartKeyClick(Sender: TObject);
  private
     { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.GenFileKeyClick(Sender: TObject);
var
  LicenseKeyBuff: ARRAY[0..4000] of AnsiChar;
  SizeKey, i: integer;
  pName: PAnsiChar;
  pOrg: PAnsiChar;
  pCustom: PAnsiChar;
  pHardId: PAnsiChar;
  ExpDateSysTime: SYSTEMTIME;
  NumDays, NumExec: integer;
  KeyFile: FILE OF AnsiChar;

begin

  if length(NameEdit.Text) = 0 then
    pName := nil
  else
    pName := PAnsiChar(AnsiString(NameEdit.Text));

  if length(OrgEdit.Text) = 0 then
    pOrg := nil
  else
    pOrg := PAnsiChar(AnsiString(OrgEdit.Text));

  if length(CustomEdit.Text) = 0 then
    pCustom := nil
  else
    pCustom := PAnsiChar(AnsiString(CustomEdit.Text));

  if length(HardIdEdit.Text) = 0 then
    pHardId := nil
  else
    pHardId := PAnsiChar(AnsiString(HardIdEdit.Text));

  if length(NumDaysEdit.Text) = 0 then
    NumDays := 0
  else
    NumDays := StrToInt(NumDaysEdit.Text);

  if length(NumExecEdit.Text) = 0 then
    NumExec := 0
  else
    NumExec := StrToInt(NumExecEdit.Text);

  if EnableDateCheck.Checked = False  then
    ZeroMemory(Addr(ExpDateSysTime), sizeof(ExpDateSysTime))
  else
    DateTimeToSystemTime(ExpDate.Date, ExpDateSysTime);

  SizeKey := WLGenLicenseFileKey(LicenseHash, pName, pOrg, pCustom, pHardId, NumDays, NumExec, ExpDateSysTime, 0, 0, 0, LicenseKeyBuff) ;

 // save generated key in user file

 AssignFile(KeyFile, FileNameEdit.Text);

 Rewrite(KeyFile);

 for i:= 0 TO SizeKey-1 DO
   Write(KeyFile, LicenseKeyBuff[i]);

 CloseFile(KeyFile);

  MessageBox(0, 'A License key file has been generated', 'License Key Generator SDK', MB_OK);

end;

procedure TForm1.EnableDateCheckClick(Sender: TObject);
begin

 if EnableDateCheck.State =  cbChecked  then
    ExpDate.Enabled := True
 else
    ExpDate.Enabled := False;


end;

procedure TForm1.Button3Click(Sender: TObject);
var
  ExpDateSysTime: SYSTEMTIME;
  LicenseKeyBuff: ARRAY[0..4000] of AnsiChar;
  SizeKey: integer;
  pName: PAnsiChar;
  pOrg: PAnsiChar;
  pCustom: PAnsiChar;
  pHardId: PAnsiChar;
  NumDays, NumExec: integer;

begin

  if length(NameEdit.Text) = 0 then
    pName := nil
  else
    pName := PAnsiChar(AnsiString(NameEdit.Text));

  if length(OrgEdit.Text) = 0 then
    pOrg := nil
  else
    pOrg := PAnsiChar(AnsiString(OrgEdit.Text));

  if length(CustomEdit.Text) = 0 then
    pCustom := nil
  else
    pCustom := PAnsiChar(AnsiString(CustomEdit.Text));

  if length(HardIdEdit.Text) = 0 then
    pHardId := nil
  else
    pHardId := PAnsiChar(AnsiString(HardIdEdit.Text));

  if length(NumDaysEdit.Text) = 0 then
    NumDays := 0
  else
    NumDays := StrToInt(NumDaysEdit.Text);

  if length(NumExecEdit.Text) = 0 then
    NumExec := 0
  else
    NumExec := StrToInt(NumExecEdit.Text);

  if EnableDateCheck.Checked = False  then
    ZeroMemory(Addr(ExpDateSysTime), sizeof(ExpDateSysTime))
  else
   DateTimeToSystemTime(ExpDate.Date, ExpDateSysTime);

  SizeKey := WLGenLicenseTextKey(LicenseHash, pName, pOrg, pCustom, pHardId, NumDays, NumExec, ExpDateSysTime, 0, 0, 0, LicenseKeyBuff) ;
  TextKeyMemo.Text := LicenseKeyBuff;

end;

procedure TForm1.GenStaticSmartKeyClick(Sender: TObject);
var
  ExpDateSysTime: SYSTEMTIME;
  LicenseKeyBuff: ARRAY[0..1000] of AnsiChar;
  SizeKey: integer;
  pName: PAnsiChar;
  pOrg: PAnsiChar;
  pCustom: PAnsiChar;
  pHardId: PAnsiChar;
  NumDays, NumExec: integer;

begin

  if length(NameEdit.Text) = 0 then
    pName := nil
  else
    pName := PAnsiChar(AnsiString(NameEdit.Text));

  if length(OrgEdit.Text) = 0 then
    pOrg := nil
  else
    pOrg := PAnsiChar(AnsiString(OrgEdit.Text));

  if length(CustomEdit.Text) = 0 then
    pCustom := nil
  else
    pCustom := PAnsiChar(AnsiString(CustomEdit.Text));

  if length(HardIdEdit.Text) = 0 then
    pHardId := nil
  else
    pHardId := PAnsiChar(AnsiString(HardIdEdit.Text));

  if length(NumDaysEdit.Text) = 0 then
    NumDays := 0
  else
    NumDays := StrToInt(NumDaysEdit.Text);

  if length(NumExecEdit.Text) = 0 then
    NumExec := 0
  else
    NumExec := StrToInt(NumExecEdit.Text);

  if EnableDateCheck.Checked = False  then
    ZeroMemory(Addr(ExpDateSysTime), sizeof(ExpDateSysTime))
  else
   DateTimeToSystemTime(ExpDate.Date, ExpDateSysTime);
                     
  SizeKey := WLGenLicenseSmartKey(LicenseHash, pName, pOrg, pCustom, pHardId, NumDays, NumExec, ExpDateSysTime, LicenseKeyBuff) ;
  SmartEdit.Text := LicenseKeyBuff;

end;

procedure TForm1.Button2Click(Sender: TObject);
var
  ExpDateSysTime: SYSTEMTIME;
  LicenseKeyBuff: ARRAY[0..4000] of AnsiChar;
  SizeKey, i: integer;
  pName: PAnsiChar;
  pOrg: PAnsiChar;
  pCustom: PAnsiChar;
  pHardId: PAnsiChar;
  NumDays, NumExec: integer;
  KeyFile: FILE OF AnsiChar;

begin

  if length(NameEdit.Text) = 0 then
    pName := nil
  else
    pName := PAnsiChar(AnsiString(NameEdit.Text));

  if length(OrgEdit.Text) = 0 then
    pOrg := nil
  else
    pOrg := PAnsiChar(AnsiString(OrgEdit.Text));

  if length(CustomEdit.Text) = 0 then
    pCustom := nil
  else
    pCustom := PAnsiChar(AnsiString(CustomEdit.Text));

  if length(HardIdEdit.Text) = 0 then
    pHardId := nil
  else
    pHardId := PAnsiChar(AnsiString(HardIdEdit.Text));

  if length(NumDaysEdit.Text) = 0 then
    NumDays := 0
  else
    NumDays := StrToInt(NumDaysEdit.Text);

  if length(NumExecEdit.Text) = 0 then
    NumExec := 0
  else
    NumExec := StrToInt(NumExecEdit.Text);

  if EnableDateCheck.Checked = False  then
    ZeroMemory(Addr(ExpDateSysTime), sizeof(ExpDateSysTime))
  else
   DateTimeToSystemTime(ExpDate.Date, ExpDateSysTime);

  SizeKey := WLGenLicenseRegistryKey(LicenseHash, pName, pOrg, pCustom, pHardId, NumDays, NumExec, ExpDateSysTime, 0, 0, 0, PAnsiChar('HKEY_LOCAL_MACHINE\' + RegistryEdit.Text), PAnsiChar(RegValueEdit.Text), LicenseKeyBuff) ;

 // save generated key in user file

 AssignFile(KeyFile, LicRegEdit.Text);

 Rewrite(KeyFile);

 for i:= 0 TO SizeKey-1 DO
   Write(KeyFile, LicenseKeyBuff[i]);

 CloseFile(KeyFile);

  MessageBox(0, 'A License key file has been generated', 'License Key Generator SDK', MB_OK);

end;

procedure TForm1.GenDynamicSmartKeyClick(Sender: TObject);
var                  
  pName: PAnsiChar;
  pOrg: PAnsiChar;
  pCustom: PAnsiChar;
  pHardId: PAnsiChar;
  LicenseKeyBuff: ARRAY[0..1000] of AnsiChar;
  LicenseFeatures: sLicenseFeatures;
  SizeKey: Integer;
  NumDays, NumExec: integer;

begin

  if length(NameEdit.Text) = 0 then
    pName := nil
  else
    pName := PAnsiChar(AnsiString(NameEdit.Text));

  if length(OrgEdit.Text) = 0 then
    pOrg := nil
  else
    pOrg := PAnsiChar(AnsiString(OrgEdit.Text));

  if length(CustomEdit.Text) = 0 then
    pCustom := nil
  else
    pCustom := PAnsiChar(AnsiString(CustomEdit.Text));

  if length(HardIdEdit.Text) = 0 then
    pHardId := nil
  else
    pHardId := PAnsiChar(AnsiString(HardIdEdit.Text));

  if length(NumDaysEdit.Text) = 0 then
    NumDays := 0
  else
    NumDays := StrToInt(NumDaysEdit.Text);

  if length(NumExecEdit.Text) = 0 then
    NumExec := 0
  else
    NumExec := StrToInt(NumExecEdit.Text);

  // initialize LicenseFeatures

  FillChar(LicenseFeatures, SizeOf(sLicenseFeatures), 0);
  LicenseFeatures.cb := SizeOf(sLicenseFeatures);

  // set LicenseFeatures according to GUI settings

  LicenseFeatures.NumDays := NumDays;
  LicenseFeatures.NumExec := NumExec;

  if EnableDateCheck.Checked then
    DateTimeToSystemTime(ExpDate.Date, LicenseFeatures.InstallDate);

  // Generate Dynamic SmartKey and display it on GUI

  SizeKey := WLGenLicenseDynSmartKey(LicenseHash, pName, pOrg, pCustom, pHardId, LicenseFeatures, LicenseKeyBuff);
  SmartEdit.Text := LicenseKeyBuff;

end;

end.
