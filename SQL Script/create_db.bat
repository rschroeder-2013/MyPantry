ECHO off

rem batch file to run a script to create a database
rem 10/20/2020

sqlcmd -S localhost -E -i my_pantry_db2.sql
rem sqlcmd -S localhost\sqlexpress -E -i my_pantry_db2.sqlcmd
rem sqlcmd -S localhost\mssqlserver -E -i my_pantry_db2.sqlcmd

Echo .
Echo If no error messages appear, DB was created
PAUSE