   # Build stage
   FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
   WORKDIR /src
   COPY . .
   RUN dotnet publish SnapSaves.sln -c Release -o /app

   # Runtime stage
   FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

   # Install MySQL and MongoDB
   RUN apt-get update && \
       apt-get install -y mysql-server mongodb && \
       rm -rf /var/lib/apt/lists/*

   # Set up MySQL and MongoDB directories
   RUN mkdir -p /var/run/mysqld && chown -R mysql:mysql /var/run/mysqld
   RUN mkdir -p /var/lib/mysql && chown -R mysql:mysql /var/lib/mysql
   RUN mkdir -p /data/db

   WORKDIR /app
   COPY --from=build /app ./
   COPY entrypoint.sh /entrypoint.sh
   RUN chmod +x /entrypoint.sh

   ENV ASPNETCORE_URLS=http://+:8080
   EXPOSE 8080 3306 27017

   ENTRYPOINT ["/entrypoint.sh"]
   