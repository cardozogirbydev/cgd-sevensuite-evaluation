USE [SevenSuiteDB];

-- ESTADO_CIVIL
DECLARE 
  @desc_soltero VARCHAR(50) = 'soltero(a)',
  @desc_casado VARCHAR(50) = 'casado(a)',
  @desc_divorciado VARCHAR(50) = 'divorciado(a)',
  @desc_viudo VARCHAR(50) = 'viudo(a)';

IF NOT EXISTS(SELECT 1 FROM [dbo].[ESTADO_CIVIL] WHERE [descripcion] = @desc_soltero)
BEGIN
  INSERT INTO [dbo].[ESTADO_CIVIL]([descripcion])
  VALUES(@desc_soltero);
END;

IF NOT EXISTS(SELECT 1 FROM [dbo].[ESTADO_CIVIL] WHERE [descripcion] = @desc_casado)
BEGIN
  INSERT INTO [dbo].[ESTADO_CIVIL]([descripcion])
  VALUES(@desc_casado);
END;

IF NOT EXISTS(SELECT 1 FROM [dbo].[ESTADO_CIVIL] WHERE [descripcion] = @desc_divorciado)
BEGIN
  INSERT INTO [dbo].[ESTADO_CIVIL]([descripcion])
  VALUES(@desc_divorciado);
END;

IF NOT EXISTS(SELECT 1 FROM [dbo].[ESTADO_CIVIL] WHERE [descripcion] = @desc_viudo)
BEGIN
  INSERT INTO [dbo].[ESTADO_CIVIL]([descripcion])
  VALUES(@desc_viudo);
END;

-- USUARIOS
DECLARE @admin_password VARCHAR(255) = 'admin123';

IF NOT EXISTS(SELECT 1 FROM [dbo].[USUARIO] WHERE [username] = 'admin')
BEGIN
  INSERT INTO [dbo].[USUARIO]([username], [password])
  VALUES('admin', @admin_password);
END
ELSE BEGIN
  UPDATE [dbo].[USUARIO]
    SET [password] = @admin_password
  WHERE [username] = 'admin';
END;
