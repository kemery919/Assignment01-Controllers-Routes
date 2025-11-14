rm -rf ./Migrations; rm -rf ./app.db;

dotnet ef migrations add InitialMigration && dotnet ef database update