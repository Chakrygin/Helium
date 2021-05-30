@echo off

for /f %%a in ('docker container ls --filter name^=MySql --quiet') do (
    echo Killing existing MySql container...
    docker container kill "%%a"
)

echo Running MySql container...
docker run --rm --detach ^
    --name MySql ^
    --publish 3306:3306 ^
    --env "MYSQL_ROOT_PASSWORD=Password12!" ^
    mysql
