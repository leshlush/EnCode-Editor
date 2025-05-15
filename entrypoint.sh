     #!/bin/bash
     set -e

     # Start MySQL
     service mysql start

     # Set up MySQL root user and database if not already done
     mysql -u root -e "CREATE DATABASE IF NOT EXISTS defaultdb; ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'rootpassword'; FLUSH PRIVILEGES;"

     # Start MongoDB
     mongod --fork --logpath /var/log/mongod.log

     # Wait a bit for DBs to be ready
     sleep 5

     # Start the .NET app
     dotnet SnapSaves.dll
     