REM - Script to prepare for Release

SET BINTARGET=bin
SET REGFREECOM=\ARP\BridgeLink\RegFreeCOM


xcopy /Y /d %REGFREECOM%\Win32\Release\OpenBridgeML.dll	%BINTARGET%\Win32\
xcopy /Y /d %REGFREECOM%\x64\Release\OpenBridgeML.dll	%BINTARGET%\x64\
