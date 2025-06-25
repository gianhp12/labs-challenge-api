#!/bin/bash
password=Magazine123

echo "Waiting running SQL..."

until /opt/mssql-tools18/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -Q "SELECT 1" -C
do
  echo "SQL Server is not available. Retrying in 15 seconds..."
  sleep 15
done

echo "SQL Server is available! Importing script..."

/opt/mssql-tools18/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i ./setup.sql -C