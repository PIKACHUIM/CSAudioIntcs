;  
; Module: Example
; Description: Shows how to generate Trial Extension keys in asm language
;
; Authors: Rafael Ahucha  
; (c) 2004 Winlicense Professional
;  

.586
.model flat,stdcall
option casemap:none


;  
;                   Libraries used by this module
;  

include \masm32\include\windows.inc
include \masm32\include\user32.inc
include \masm32\include\kernel32.inc

includelib \masm32\lib\user32.lib
includelib \masm32\lib\kernel32.lib
includelib ..\..\..\..\Lib\COFF\WinlicenseSDK.lib


;  
;                          Functions Prototype
;  


WLGenPassword            PROTO :DWORD, :DWORD, :DWORD
PasswordDlgFunc             PROTO :DWORD, :DWORD, :DWORD, :DWORD


;  
;                            Exported functions
;  


;  
;                                  Macros 
;  

.const

literal MACRO quoted_text:VARARG

  LOCAL local_text

  .data
   local_text db quoted_text,0

  .code
   EXITM <local_text>

ENDM

SADD MACRO quoted_text:VARARG

  EXITM <ADDR literal(quoted_text)>

ENDM    


;  
;                                Constants
;  

IDD_PASS_DLG            EQU     101
IDD_EDIT_NAME           EQU     3000
IDD_GENERATE_PASSWORD   EQU     3001
IDD_EDIT_PASSWORD       EQU     3002


;  
;                                Global data
;  
  
.data

PasswordHash        db  "432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289d", 0
hInstance           dd  0


;  
;                                Code section
;  

.code

StartExample:

    invoke  GetModuleHandle,NULL
    mov     hInstance, eax
    
    invoke  DialogBoxParam, hInstance, IDD_PASS_DLG, NULL, ADDR PasswordDlgFunc, NULL

    invoke  ExitProcess, NULL



; *****************************************************************************
; * PasswordDlgFunc
; *
; *  Password dialog message handler
; *
; * Inputs
; *  Standard dialog messages handler
; * 
; * Outputs
; *  None
; *
; * Returns
; *  None
; ******************************************************************************

PasswordDlgFunc proc hDlg:DWORD, uMsg:DWORD, wParam:DWORD, lParam:DWORD

    LOCAL Return:DWORD, NameBuffer[80]:byte, PasswordBuffer[40]:DWORD

    pushad

    .if uMsg == WM_INITDIALOG
        

    .elseif uMsg == WM_CLOSE

        invoke  EndDialog, hDlg, NULL

    .elseif uMsg == WM_COMMAND

        mov     eax, wParam
        mov     edx, eax
        shr     edx, 16

        .if dx == BN_CLICKED

            .if ax == IDD_GENERATE_PASSWORD

                invoke  GetDlgItemText, hDlg, IDD_EDIT_NAME, ADDR NameBuffer, 80

                ; generate password for the current Name
                
                invoke  WLGenPassword, ADDR PasswordHash, ADDR NameBuffer, ADDR PasswordBuffer

                ; show password in GUI

                invoke  SetDlgItemText, hDlg, IDD_EDIT_PASSWORD, ADDR PasswordBuffer
                
            .endif    


       .endif

    .else
    
        xor     eax, eax

    .endif

    mov     Return, eax
    popad
    mov     eax, Return
    ret

PasswordDlgFunc endp

    
end StartExample
