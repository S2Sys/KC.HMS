# KC.HMS

HOW TO:
******************************

Restore the database from \KOVAI_CO_HMS.bak 

Open the "\KC.HMS.sln"
Right Click Solution and Choose "Set Startup Projects"
set following projects as startup 

KC.HMS.Web
KC.HMS.WebAPI

Open KC.HMS.Web appsettings.json
---------------------------------------- 
Update "ExternalAPI" value to KC.HMS.WebAPI Url 
Update "DefaultConnection" value with proper databse connection

Open KC.HMS.WebAPI appsettings.json
----------------------------------------
  
Update "DefaultConnection" value with proper databse connection

Build the application and run the applications 


Sample Users name is listed below common password used for all these users 

*****************************************************************************
admin@test.com
SuperAdministrator@test.com
guest@test.com

Password is !QAZ1qaz 
