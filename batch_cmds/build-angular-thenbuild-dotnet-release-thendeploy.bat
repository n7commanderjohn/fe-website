call .\build-angular-prod.bat
cd ..\batch_cmds
call .\build-dotnet-release.bat
cd ..\batch_cmds
call .\deploy.bat
