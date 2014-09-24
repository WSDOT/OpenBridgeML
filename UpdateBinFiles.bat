REM - Script to prepare for Release

SET BINTARGET=bin
SET REGFREECOM=\ARP\BridgeLink\RegFreeCOM


copy /Y %REGFREECOM%\Win32\Release\OpenBridgeML.dll	%BINTARGET%\Win32\
copy /Y %REGFREECOM%\x64\Release\OpenBridgeML.dll	%BINTARGET%\x64\
