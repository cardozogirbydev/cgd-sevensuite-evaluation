CREATE DATABASE [SevenSuiteDB];

GO

USE [SevenSuiteDB];

CREATE TABLE [ESTADO_CIVIL]
(
  [id_estado]   INT PRIMARY KEY IDENTITY(1, 1),
  [descripcion] VARCHAR(50) NOT NULL
);

CREATE TABLE [SEVECLIE] 
(
  [id_clie]         INT PRIMARY KEY IDENTITY(1, 1),
  [cedula]          VARCHAR(20) NOT NULL UNIQUE,
  [nombre]          VARCHAR(100) NOT NULL,
  [genero]          CHAR(1) CONSTRAINT [CHK_genero] CHECK([genero] IN('M', 'F')),
  [fecha_nac]       DATE NOT NULL,
  [id_estado_civil] INT FOREIGN KEY REFERENCES [ESTADO_CIVIL]([id_estado])
);
