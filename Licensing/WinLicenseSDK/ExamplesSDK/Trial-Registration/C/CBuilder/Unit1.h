//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ComCtrls.hpp>
#include <ExtCtrls.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
        TGroupBox *GroupBox1;
        TGroupBox *GroupBox2;
    TButton *Button5;
    TLabel *StatusLabel;
    TLabel *Label6;
    TLabel *DaysLeftLabel;
    TLabel *Label3;
    TLabel *Label7;
    TLabel *Label1;
    TLabel *Label8;
    TLabel *ExecLeftLabel;
    TLabel *Label13;
    TLabel *Label15;
    TLabel *MinutesLeftLabel;
    TLabel *Label17;
    TLabel *RuntimeLeftLabel;
    TGroupBox *GroupBox3;
    TLabel *RegDaysLeftLabel;
    TLabel *Label9;
    TLabel *Label10;
    TLabel *RegExecLeftLabel;
    TLabel *Label19;
    TGroupBox *GroupBox4;
    TButton *Button1;
    TEdit *HardwareIdEdit;
    TLabel *Label2;
    TLabel *Label4;
    TEdit *RegNameEdit;
    TEdit *CompanyNameEdit;
    TLabel *Label5;
    TMemo *CustomEdit;
    TDateTimePicker *ExpDatePicker;
    TDateTimePicker *RegDateTimePicker1;
    void __fastcall Button5Click(TObject *Sender);
    void __fastcall Button1Click(TObject *Sender);
private:	// User declarations

        char*    LicenseHash;

public:		// User declarations
        __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
