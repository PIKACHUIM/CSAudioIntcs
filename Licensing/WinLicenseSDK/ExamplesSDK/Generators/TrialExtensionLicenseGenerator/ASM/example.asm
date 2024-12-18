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


WLGenTrialExtensionFileKey   PROTO :DWORD, :DWORD, :DWORD, :DWORD, :DWORD, :DWORD, \
                                  :DWORD, :DWORD

CreateLicenseFile           PROTO :DWORD, :DWORD, :DWORD


;  
;                            Exported functions
;  


;  
;                            Constants and macros 
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
;                                Global data
;  
  
.data

TrialHash           db  "432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289d", 0
TrialKeyBuffer      db  100 dup (0)    
SizeKey             dd  0

CurrentDirectory    db MAX_PATH + 1 dup (0)

;  
;                                Code section
;  

.code

StartExample:

    ; create a trial extension key, extending 2 days and 5 executions

    invoke  WLGenTrialExtensionFileKey, ADDR TrialHash, 1, 2, 5, 0, 0, 0, ADDR TrialKeyBuffer
    mov     SizeKey, eax

    invoke  CreateLicenseFile, SADD("extendkey.dat"), ADDR TrialKeyBuffer, SizeKey

    invoke  MessageBox, NULL, SADD("A trial extension key has been generated"), SADD("Trial Extension SDK"), MB_OK

    invoke  ExitProcess, NULL


; *****************************************************************************
; * CreateLicenseFile
; *
; *  Create a license key file for a particular user
; *
; * Inputs
; *  NameDir: Directory where to store the generated key
; * 
; * Outputs
; *  None
; *
; * Returns
; *  None
; ******************************************************************************

CreateLicenseFile proc NameFile:DWORD, KeyBuffer:DWORD, Sizekey:DWORD

    LOCAL   hFileKey:DWORD, NumBytesWritten:DWORD
    
    pushad

    ; create specific key

    invoke  CreateFile, NameFile, GENERIC_READ or GENERIC_WRITE,NULL, \
                        NULL,CREATE_ALWAYS,FILE_ATTRIBUTE_ARCHIVE, NULL

    mov     hFileKey, eax
    
    invoke  WriteFile, hFileKey, KeyBuffer, Sizekey, ADDR NumBytesWritten, NULL
    invoke  CloseHandle, hFileKey

    popad
    ret

CreateLicenseFile endp
    
end StartExample


