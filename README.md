Required .NET 6 SDK.
Change "connectionstring" inside in appsettings.json (ConnectionStrings:SqlConnection) properly as to your local MsSql Database.
Open in Terminal on Solution Project
Add command line: PM>  add-migration firstmigration
Add command line: update-database
Done!


