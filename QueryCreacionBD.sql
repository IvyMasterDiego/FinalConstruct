GO
CREATE DATABASE FinalProject
GO
USE FinalProject
GO
CREATE TABLE Person
(
	id int not null,
	Cedula varchar(50) not null,
	Nombre varchar(50) not null,
	Apellido varchar(50) not null,
	Celular varchar(50) not null,
	Direccion varchar(200)not null,
)
GO
SELECT * FROM Person