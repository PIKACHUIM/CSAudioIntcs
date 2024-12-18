unit KeyUser;

{
  Delphi key generator example

  (c) 2004-2006 element 5
  (c) 2006 Digital River GmbH

  written by Stefan Weber

  SDK 3 File Revision 3

  IMPLEMENT YOUR CODE HERE!
}

interface

uses
  KeyIntf, SysUtils, Windows;


{code for using WinLicense Smart Key}

const
  LicenseHash = <Your product license hash>

type
  TMyKeyGen = class(TAbstractKeyGen)
  protected
    function GetTitle: string; override;
    function GenerateKey: integer; override;
  end;


//  The dll File this function refers to is just the WinlicenseSDK.dll 
//  renamed which you supply with your code generator exe
 function WLGenLicenseSmartKey(
 pLicenseHash:PChar;
 pUserName:PChar;
 pOrganization:PChar;
 pCustomData:PChar;
 pMachineID:PChar;
 NumDays:Integer;
 NumExec:Integer;
 pNewDate:Pointer;
 pBufferOut:PChar):Integer; stdcall; external 'codegenyourproductname.dll';


implementation

{ TMyKeyGen }
function TMyKeyGen.GetTitle: string;
begin
  Result := 'Title of Generator';
end;

// implement your key generator here
function TMyKeyGen.GenerateKey: integer;
var
  REG_NAME, EMAIL, QUANTITY, PRODUCT_ID: WideString; // input data
  key: WideString;
  RegNameStr: String;
  QuantityStr: String;
  GenerateKeyNo: Integer;
  UserName: PChar;
  Organization: PChar;
  CustomData:  PChar;
  MachineID:  PChar;
  NumDays: Integer;
  NumExec: Integer;
  LicenseKeyBuff: ARRAY[0..1000] of char;
  pNewdate: ^SystemTime;
  LicenseKey: string;
  LicenseHash: string;
  EmailStr: string;
begin
  // get input values
  REG_NAME := WideValue('REG_NAME');
  EMAIL    := WideValue('EMAIL');
  QUANTITY := WideValue('QUANTITY');
  PRODUCT_ID := WideValue('PRODUCT_ID');
  // get more values as you need them ...

  // You need to convert any widestring input values to strings, otherwise the
  // generated license key will not be correct.
  EmailStr:= String(EMAIL);
  QuantityStr:= String(QUANTITY);
  RegNameStr:= String(REG_NAME);

  if (Length(REG_NAME) < 8) then
    raise EKeyException.Create('REG_NAME must have at least 8 characters',
      ERC_BAD_INPUT);


    UserName := PChar(RegNameStr);
    Organization := nil;
    MachineID:= nil;
    CustomData := PChar(QuantityStr + '-User License;' +
      EmailStr);
    MachineId := nil;
    NumDays := 0;
    NumExec := 0;
    pNewDate:= nil;
    GenerateKeyNo := WLGenLicenseSmartKey(
      PChar(LicenseHash),
      UserName,
      Organization,
      CustomData,
      MachineID,
      NumDays,
      NumExec,
      pNewDate,
      LicenseKeyBuff);
    if GenerateKeyNo > 0 then
    begin
      LicenseKey:= LicenseKeyBuff;
    end
    else
    begin
     raise EKeyException.Create('Key not generated.',
      ERC_ERROR);
    end;

// The value returned in UserKey is what will replace the <%KEY%> value in the email that
// ShareIt! send out on your behalf.

{$IFDEF BINARY_GEN}
  KeyMIMEType := 'text/plain';
  KeyDisplayFileName := 'key.txt';
  KeyData := Key;
  Result := ERC_SUCCESS_BIN;
{$ELSE}
  UserKey := 'Registration Name: '+ Reg_Name +#13#10+'Registration Code: '+Licensekey+ #13#10+
              'Custom Data: '+Quantity +'-User License;'+Email;
  CCKey   := REG_NAME + '   ' + EMAIL + '   ' +PRODUCT_ID + '   '+ Licensekey;
  Result  := ERC_SUCCESS;
{$ENDIF}

end;

end.