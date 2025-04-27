@echo off

cd BMZSWeb
start "BMZSWeb" cmd /k "npm i && npm start"
cd ..


start "BMZSApi" ApiStarter.exe.lnk

pause