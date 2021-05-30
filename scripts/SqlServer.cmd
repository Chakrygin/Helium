@echo off

for /f %%a in ('docker container ls --filter name^=SqlServer --quiet') do (
    echo Killing existing SqlServer container...
    docker container kill "%%a"
)

echo Running SqlServer container...
docker run --rm --detach ^
    --name SqlServer ^
    --publish 1433:1433 ^
    --env "ACCEPT_EULA=Y" ^
    --env "SA_PASSWORD=Password12!" ^
    mcr.microsoft.com/mssql/server
