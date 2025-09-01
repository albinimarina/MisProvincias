# 📦 Relevamiento de Provincias - Base de Datos

Este proyecto contiene el script para crear la base de datos **Relevamiento-Provincias** en SQL Server.  
Incluye tres tablas principales: **Animales**, **Plantas** y **Provincias**, con sus relaciones.

---

## 🛠️ Requisitos

- **SQL Server 2019** o superior (funciona también en versiones anteriores compatibles).
- **SQL Server Management Studio (SSMS)** o cualquier cliente SQL para ejecutar scripts.
- **Visual Studio** (opcional, si se integra con un proyecto .NET).

---

## 📂 Estructura de la Base de Datos

### 🔹 Crear base de datos
```sql
CREATE DATABASE [Relevamiento-Provincias];
GO
USE [Relevamiento-Provincias];
GO


🔹 Tablas principales
1. Animales
CREATE TABLE [dbo].[Animales](
    [IdAnimal] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL
);
2. Plantas
CREATE TABLE [dbo].[Plantas](
    [IdPlanta] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL
);

3. Provincias
CREATE TABLE [dbo].[Provincias](
    [IdProvincia] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] VARCHAR(100) NOT NULL,
    [Capital] VARCHAR(100) NOT NULL,
    [IdAnimal] INT NOT NULL,
    [IdPlanta] INT NOT NULL,
    [FechaBaja] DATE NULL
);

🔹 Relaciones
ALTER TABLE [dbo].[Provincias]  
    ADD CONSTRAINT [FK_Provincias_Animales] FOREIGN KEY([IdAnimal])
    REFERENCES [dbo].[Animales] ([IdAnimal]);

ALTER TABLE [dbo].[Provincias]  
    ADD CONSTRAINT [FK_Provincias_Plantas] FOREIGN KEY([IdPlanta])
    REFERENCES [dbo].[Plantas] ([IdPlanta]);

🌱 Datos iniciales
-- Animales
INSERT INTO Animales (Nombre) VALUES 
('Carpincho'),
('Guanaco'),
('Jaguareté'),
('Cóndor'),
('Hornero'),
('Puma'),
('Oso'),
('Ballena'),
('Leopardo'),
('Pingüino')


-- Plantas
INSERT INTO Plantas (Nombre) VALUES 
('Ceibo'),
('Pampas Grass'),
('Algarrobo blanco'),
('Jarilla'),
('Ombú'),
('El cardón'),
('Lapacho rosado'),
('Yerba mate'),
('Passiflora edulis'),
('Opuntia sulphurea'),
('Tala')


🔎 Ejemplo de consulta
SELECT 
    p.Nombre AS Provincia, 
    p.Capital, 
    a.Nombre AS Animal, 
    pl.Nombre AS Planta
FROM Provincias p
INNER JOIN Animales a ON p.IdAnimal = a.IdAnimal
INNER JOIN Plantas pl ON p.IdPlanta = pl.IdPlanta;

📌 Notas

La tabla Provincias se relaciona con Animales y Plantas mediante claves foráneas.

Los campos IdAnimal y IdPlanta en Provincias deben existir previamente en sus tablas.

El campo FechaBaja permite manejar bajas lógicas de provincias (en vez de borrarlas físicamente).

📅 Fecha: 31/08/2025
✍️ Autor: Marina Albini