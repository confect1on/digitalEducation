# digitalEducation
Для работы приложения необходимо иметь mySQL v. 8.0. 
Далее step-by-step:
В папке C:\Users\[твое_имя_пользователя] нужно создать файл с названием .wslconfig

```
[wsl2]
memory=1GB
processors=2
```
Установить docker desktop отсюда: https://www.docker.com
Перезапустить ноутбукСпулить mysql отсюда: https://hub.docker.com/_/mysql

При монтировании контейнера указать ENVIRONMENT variable
MYSQL_ROOT_PASSWORD: mypassword

Port: 3306/tcp
