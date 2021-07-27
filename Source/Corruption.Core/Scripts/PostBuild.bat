xcopy  "%~dp0..\Corruption.Core-1.3\Output" "%~dp0..\Output" /k/r/e/i/s/c/h/f/o/x/y
pause
robocopy "%~dp0..\Corruption.Core-1.2\Output" "%~dp0..\Output"
pause
robocopy "%~dp0..\Output" "%~dp0..\..\..\"
pause