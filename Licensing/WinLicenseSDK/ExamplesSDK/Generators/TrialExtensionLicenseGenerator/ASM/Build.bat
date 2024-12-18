@echo off

ml /c /coff example.asm

Link /SUBSYSTEM:WINDOWS example.obj

del *.obj

pause
