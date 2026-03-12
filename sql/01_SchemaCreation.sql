CREATE DATABASE [SevenSuiteDB];

GO

USE [SevenSuiteDB];

CREATE TABLE [ESTADO_CIVIL]
(
  [id_estado_civil]   INT PRIMARY KEY IDENTITY(1, 1),
  [descripcion] VARCHAR(50) NOT NULL
);

CREATE TABLE [SEVECLIE] 
(
  [id_clie]         INT PRIMARY KEY IDENTITY(1, 1),
  [cedula]          VARCHAR(20) NOT NULL UNIQUE,
  [nombre]          VARCHAR(100) NOT NULL,
  [genero]          CHAR(1) CONSTRAINT [CHK_genero] CHECK([genero] IN('M', 'F')),
  [fecha_nac]       DATE NOT NULL,
  [id_estado_civil] INT FOREIGN KEY REFERENCES [ESTADO_CIVIL]([id_estado_civil])
);

CREATE TABLE [USUARIO]
(
  [id_usuario] INT PRIMARY KEY IDENTITY(1, 1),
  [username]   VARCHAR(50) NOT NULL UNIQUE,
  [password]   VARCHAR(255) NOT NULL
);

CREATE PROCEDURE [usp_ClientsGet]
  @Filtro VARCHAR(100) = NULL
AS
BEGIN
  SELECT
    CL.[id_clie],
    CL.[cedula],
    CL.[nombre],
    CL.[genero],
    CL.[fecha_nac],
    EC.[id_estado_civil],
    EC.[descripcion] AS [desc_estado_civil]
  FROM [dbo].[SEVECLIE] CL
    INNER JOIN [dbo].[ESTADO_CIVIL] EC ON CL.[id_estado_civil] = EC.[id_estado_civil]
  WHERE (@Filtro IS NULL OR CL.[nombre] LIKE '%' + @Filtro + '%' OR CL.[cedula] LIKE '%' + @Filtro + '%')
  ORDER BY CL.[nombre] ASC
END;

CREATE PROCEDURE [usp_ClientGetById]
  @id INT
AS
BEGIN
  SELECT *
  FROM [SEVECLIE]
  WHERE [id_clie] = @id
END;

CREATE PROCEDURE [usp_ClientUpsert]
  @id_clie          INT,
  @cedula           VARCHAR(20),
  @nombre           VARCHAR(100),
  @genero           CHAR(1),
  @fecha_nac        DATE,
  @id_estado_civil  INT
AS
BEGIN
  MERGE INTO [dbo].[SEVECLIE] AS [Target]
  USING(SELECT @id_clie AS [id_clie]) AS [Source]
  ON [Target].[id_clie] = [Source].[id_clie] AND [Source].[id_clie] > 0
  WHEN MATCHED THEN
    UPDATE SET
      [cedula] = @cedula,
      [nombre] = @nombre,
      [genero] = @genero,
      [fecha_nac] = @fecha_nac,
      [id_estado_civil] = @id_estado_civil
  WHEN NOT MATCHED THEN
    INSERT
    (
      [cedula],
      [nombre],
      [genero],
      [fecha_nac],
      [id_estado_civil]
    )
    VALUES(
      @cedula,
      @nombre,
      @genero,
      @fecha_nac,
      @id_estado_civil
    );
END;

CREATE PROCEDURE [usp_ClientDelete]
  @id_clie INT
AS
BEGIN
  DELETE FROM [dbo].[SEVECLIE]
  WHERE [id_clie] = @id_clie;
END;

CREATE PROCEDURE [usp_CivilStatusesGet]
AS
BEGIN
  SELECT
    [id_estado_civil],
    [descripcion]
  FROM [ESTADO_CIVIL]
  ORDER BY [descripcion]
END;
