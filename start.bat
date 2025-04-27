@echo off

REM Háttérben indítja, nem nyit új ablakot
cd BMZSWeb
start /B npm  i && npm start

cd ..


REM Parancsikon futtatása ugyanebben a konzolban (de ez valójában külön folyamatként indul)
start /B "" "ApiStarter.lnk"
pause
