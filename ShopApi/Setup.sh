#!/bin/bash
set -e
export  ASPNETCORE_ENVIRONMENT=Docker

dotnet dev-certs https --trust

until /root/.dotnet/tools/dotnet-ef database update ; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"


exec dotnet run --launch-profile "Docker-Profile" 