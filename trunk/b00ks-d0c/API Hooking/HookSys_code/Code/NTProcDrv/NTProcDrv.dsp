# Microsoft Developer Studio Project File - Name="NTProcDrv" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Dynamic-Link Library" 0x0102

CFG=NTProcDrv - Win32 Release
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "NTProcDrv.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "NTProcDrv.mak" CFG="NTProcDrv - Win32 Release"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "NTProcDrv - Win32 Debug" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE "NTProcDrv - Win32 Release" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "NTProcDrv - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "Debug"
# PROP Intermediate_Dir "Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "NTProcDrv_EXPORTS" /Yu"stdafx.h" /FD /GZ /c
# ADD CPP /nologo /Gz /ML /W3 /GX /Z7 /Oi /Gf /Gy /I "F:\WINDDK\inc" /D "_DEBUG" /D "_X86_" /D "i386" /D "STD_CALL" /D "CONDITION_HANDLING" /D "WIN32_LEAN_AND_MEAN" /D "NT_UP" /D "RDRDBG" /D "SRVDBG" /D "DBG" /D "_IDWBUILD" /U "NT_INST" /FR /FD /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x409 /d "_DEBUG"
# ADD RSC /l 0x409 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /debug /machine:I386 /pdbtype:sept
# ADD LINK32 ntoskrnl.lib hal.lib /nologo /base:"0x10000" /entry:"DriverEntry@8" /dll /incremental:no /debug /debugtype:both /machine:I386 /nodefaultlib /out:"..\..\Output\NTProcDrv.sys" /SUBSYSTEM:native
# SUBTRACT LINK32 /pdb:none

!ELSEIF  "$(CFG)" == "NTProcDrv - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "NTProcDrv___Win32_Release"
# PROP BASE Intermediate_Dir "NTProcDrv___Win32_Release"
# PROP BASE Ignore_Export_Lib 0
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "Release"
# PROP Intermediate_Dir "Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /Gz /ML /W3 /GX /Z7 /Oi /Gf /Gy /I "F:\WINDDK\inc" /D "_DEBUG" /D "_X86_" /D "i386" /D "STD_CALL" /D "CONDITION_HANDLING" /D "WIN32_LEAN_AND_MEAN" /D "NT_UP" /D "RDRDBG" /D "SRVDBG" /D "DBG" /D "_IDWBUILD" /U "NT_INST" /FR /FD /GZ /c
# ADD CPP /nologo /Gz /ML /W3 /GX /Z7 /Oi /Gf /Gy /I "F:\WINDDK\inc" /D "_X86_" /D "i386" /D "STD_CALL" /D "CONDITION_HANDLING" /D "WIN32_LEAN_AND_MEAN" /D "NT_UP" /D "RDRDBG" /D "SRVDBG" /D "DBG" /D "_IDWBUILD" /D "UNICODE" /U "NT_INST" /FR /FD /GZ /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x409 /d "_DEBUG"
# ADD RSC /l 0x409
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 ntoskrnl.lib hal.lib /nologo /base:"0x10000" /entry:"DriverEntry@8" /dll /incremental:no /debug /debugtype:both /machine:I386 /out:"Debug/NTProcDrv.sys" /SUBSYSTEM:native
# SUBTRACT BASE LINK32 /pdb:none
# ADD LINK32 ntoskrnl.lib hal.lib /nologo /base:"0x10000" /entry:"DriverEntry@8" /dll /incremental:no /machine:I386 /out:"..\..\Output\NTProcDrv.sys" /SUBSYSTEM:native
# SUBTRACT LINK32 /pdb:none /debug

!ENDIF 

# Begin Target

# Name "NTProcDrv - Win32 Debug"
# Name "NTProcDrv - Win32 Release"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\NTProcDrv.c
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# End Group
# End Target
# End Project
