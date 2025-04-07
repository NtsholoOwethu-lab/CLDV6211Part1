--DATABASE CREATION
USE master
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'RegistrationDatabase')
DROP DATABASE RegistrationDatabase
CREATE DATABASE RegistrationDatabase


USE RegistrationDatabase

--TABLE CREATION
CREATE TABLE StudentInput(
   StudentID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
   Name VARCHAR (250) NOT NULL,
   Surname VARCHAR (250) NOT NULL,
   email VARCHAR (250) NOT NULL

);

--TABLE INSERTION
INSERT INTO StudentInput([Name], Surname, email)
VALUES ('Owethu', 'Ntsholo', 'wow@gmail.com')

SELECT * FROM StudentInput