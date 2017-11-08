REM - Script to prepare for Release

SET BINTARGET=bin
SET REGFREECOM=\ARP\BridgeLink\RegFreeCOM


xcopy /Y /d %REGFREECOM%\x64\Release\OpenBridgeML.dll	%BINTARGET%\x64\
