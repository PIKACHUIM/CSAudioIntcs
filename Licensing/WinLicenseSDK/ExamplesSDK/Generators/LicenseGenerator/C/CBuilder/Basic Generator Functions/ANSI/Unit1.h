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
        TLabel *Label5;
        TLabel *Label6;
        TLabel *Label7;
        TLabel *Label8;
        TGroupBox *GroupBox2;
        TLabel *Label1;
        TLabel *Label2;
        TLabel *Label3;
        TDateTimePicker *ExpDate;
        TGroupBox *GroupBox3;
        TGroupBox *GroupBox4;
        TLabel *Label4;
        TEdit *FileNameEdit;
        TButton *Button1;
        TGroupBox *GroupBox5;
        TLabel *Label9;
        TEdit *RegistryEdit;
        TButton *Button2;
        TEdit *Edit3;
        TLabel *Label10;
        TEdit *RegValueEdit;
        TLabel *Label11;
        TEdit *LicRegEdit;
        TRadioGroup *RadioGroup1;
        TButton *Button3;
        TGroupBox *GroupBox6;
        TButton *Button4;
        TEdit *SmartEdit;
        TEdit *NameEdit;
        TEdit *OrgEdit;
        TEdit *HardIdEdit;
        TEdit *CustomEdit;
        TEdit *NumDaysEdit;
        TEdit *NumExecEdit;
        TCheckBox *EnableDateCheck;
        TMemo *TextKeyMemo;
        void __fastcall Button1Click(TObject *Sender);
        void __fastcall Button3Click(TObject *Sender);
        void __fastcall Button4Click(TObject *Sender);
        void __fastcall EnableDateCheckClick(TObject *Sender);
    void __fastcall Button2Click(TObject *Sender);
private:	// User declarations

        char*    LicenseHash;

public:		// User declarations
        __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
