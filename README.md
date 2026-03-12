# SevenSuite Evaluation

Proyecto de evaluacion tecnica para programador web con SQL Server 2019, ASP.NET y C# (.NET Framework 4.6).

## Requisitos

- SQL Server 2019
- Visual Studio 2015 (o compatible con .NET Framework 4.6)
- .NET Framework 4.6

## Estructura

- `sql/...`: scripts para la base de datos.
- `src/...`: aplicación web.

## Instalacion

1. Ejecutar los scripts que se encuentran dentro de `sql/...` en SQL Server en el siguiente orden:
    1. `01_SchemaCreation.sql`.
    2. `02_SeedData.sql`.
2. Abrir con Visual Studio `src/SevenSuite.PruebaWebApp/SevenSuite.PruebaWebApp.sln`.
3. Validar connection string `DefaultConnection` en `~/Web.config`.
4. Ejecutar `clean` y `rebuild`.
4. Ejecutar el sitio.

## Credenciales iniciales

- Usuario: `admin`
- Contraseña: `admin123`
