@echo off

ml /c /coff example.asm

Link /SUBSYSTEM:WINDOWS example.obj PassResource.res

del *.obj

pause
