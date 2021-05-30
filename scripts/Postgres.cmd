@echo off

for /f %%a in ('docker container ls --filter name^=Postgres --quiet') do (
    echo Killing existing Postgres container...
    docker container kill "%%a"
)

echo Running Postgres container...
docker run --rm --detach ^
    --name Postgres ^
    --publish 5432:5432 ^
    --env "POSTGRES_DB=postgres" ^
    --env "POSTGRES_USER=postgres" ^
    --env "POSTGRES_PASSWORD=postgres" ^
    postgres
