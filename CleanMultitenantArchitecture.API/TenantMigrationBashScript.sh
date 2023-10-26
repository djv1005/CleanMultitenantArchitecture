#!/bin/bash

# Set the name of the new tenant
NEW_TENANT_NAME="{{Database_Tenant_Name}}"
USER_NEW_TENANT="{{User_Db_Tenant}}"
PWD_NEW_TENANT="{{Pwd_Db_Tenant}}"
SERVER="{{Server}}"


# Set the connection string for the new tenant
NEW_TENANT_CONNECTION_STRING="Server=$SERVER;Database=$NEW_TENANT_NAME;User Id=$USER_NEW_TENANT;Password=$PWD_NEW_TENANT;"

# Create a migration for the new tenant
dotnet ef migrations add InitialMigration --context ProductDbContext -p CleanMultitenantArchitecture.Domain/CleanMultitenantArchitecture.Domain.csproj -s CleanMultitenantArchitecture.API/CleanMultitenantArchitecture.API.csproj -o Migrations/Tenants/$NEW_TENANT_NAME

# Apply the migration to the new tenant's database
dotnet ef database update --context TenantDbContext -p CleanMultitenantArchitecture.Domain/CleanMultitenantArchitecture.Domain.csproj -s CleanMultitenantArchitecture.API/CleanMultitenantArchitecture.API.csproj --connection "$NEW_TENANT_CONNECTION_STRING"
