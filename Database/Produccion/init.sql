IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Cupo] (
    [Id] int NOT NULL IDENTITY,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Cupo] PRIMARY KEY ([Id])
);

CREATE TABLE [Rol] (
    [Id] int NOT NULL IDENTITY,
    [NombreRol] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Rol] PRIMARY KEY ([Id])
);

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [NombreCompleto] nvarchar(max) NOT NULL,
    [IdentificadorUsuario] nvarchar(max) NOT NULL,
    [NumeroTelefono] nvarchar(max) NULL,
    [Contrasena] nvarchar(max) NOT NULL,
    [RolId] int NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Usuario_Rol_RolId] FOREIGN KEY ([RolId]) REFERENCES [Rol] ([Id])
);

CREATE TABLE [Vehiculo] (
    [Id] int NOT NULL IDENTITY,
    [Placa] nvarchar(max) NOT NULL,
    [UsuarioId] int NOT NULL,
    [Cilindraje] int NOT NULL,
    CONSTRAINT [PK_Vehiculo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vehiculo_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Reserva] (
    [Id] int NOT NULL IDENTITY,
    [VehiculoId] int NOT NULL,
    [CupoId] int NOT NULL,
    [FechaReserva] datetime2 NOT NULL,
    [FechaIngreso] datetime2 NOT NULL,
    [FechaSalida] datetime2 NULL,
    [Costo] float NULL,
    [Duracion] float NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Reserva] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reserva_Cupo_CupoId] FOREIGN KEY ([CupoId]) REFERENCES [Cupo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reserva_Vehiculo_VehiculoId] FOREIGN KEY ([VehiculoId]) REFERENCES [Vehiculo] ([Id]) ON DELETE NO ACTION
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Estado') AND [object_id] = OBJECT_ID(N'[Cupo]'))
    SET IDENTITY_INSERT [Cupo] ON;
INSERT INTO [Cupo] ([Id], [Estado])
VALUES (1, CAST(1 AS bit)),
(2, CAST(1 AS bit)),
(3, CAST(1 AS bit)),
(4, CAST(0 AS bit)),
(5, CAST(0 AS bit)),
(6, CAST(0 AS bit)),
(7, CAST(0 AS bit)),
(8, CAST(0 AS bit)),
(9, CAST(0 AS bit)),
(10, CAST(0 AS bit)),
(11, CAST(0 AS bit)),
(12, CAST(0 AS bit)),
(13, CAST(0 AS bit)),
(14, CAST(0 AS bit)),
(15, CAST(0 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Estado') AND [object_id] = OBJECT_ID(N'[Cupo]'))
    SET IDENTITY_INSERT [Cupo] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NombreRol') AND [object_id] = OBJECT_ID(N'[Rol]'))
    SET IDENTITY_INSERT [Rol] ON;
INSERT INTO [Rol] ([Id], [NombreRol])
VALUES (1, N'Admin'),
(2, N'Cliente'),
(3, N'Operador');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NombreRol') AND [object_id] = OBJECT_ID(N'[Rol]'))
    SET IDENTITY_INSERT [Rol] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Contrasena', N'IdentificadorUsuario', N'NombreCompleto', N'NumeroTelefono', N'RolId') AND [object_id] = OBJECT_ID(N'[Usuario]'))
    SET IDENTITY_INSERT [Usuario] ON;
INSERT INTO [Usuario] ([Id], [Contrasena], [IdentificadorUsuario], [NombreCompleto], [NumeroTelefono], [RolId])
VALUES (1, N'Ow4CnX0NoSHlhq96QYGPFQ==', N'CamiloAPerezP', N'Camilo Andres Perez Parra', NULL, 2),
(2, N'eSuwoPwEaN2wXV/NfQt/lA==', N'JhoanaECifuentes', N'Jhoana Estefania Cifuentes', NULL, 2),
(3, N'UsPN3lexcl9LyHlIHuw6ew==', N'NelsonAMunozS', N'Nelson Andres Muñoz Salinas', NULL, 1),
(4, N'ouj96EN+PovtRy940zJAIA==', N'SilenidSSarmiento', N'Silenid Salinas Sarmiento', NULL, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Contrasena', N'IdentificadorUsuario', N'NombreCompleto', N'NumeroTelefono', N'RolId') AND [object_id] = OBJECT_ID(N'[Usuario]'))
    SET IDENTITY_INSERT [Usuario] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cilindraje', N'Placa', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Vehiculo]'))
    SET IDENTITY_INSERT [Vehiculo] ON;
INSERT INTO [Vehiculo] ([Id], [Cilindraje], [Placa], [UsuarioId])
VALUES (1, 125, N'SBX-78F', 1),
(2, 200, N'ERF-23G', 2),
(3, 400, N'FGH-66H', 4),
(4, 125, N'AAE-34F', 1),
(5, 200, N'GJK-45B', 2),
(6, 250, N'FCX-66B', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cilindraje', N'Placa', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Vehiculo]'))
    SET IDENTITY_INSERT [Vehiculo] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Costo', N'CupoId', N'Duracion', N'Estado', N'FechaIngreso', N'FechaReserva', N'FechaSalida', N'VehiculoId') AND [object_id] = OBJECT_ID(N'[Reserva]'))
    SET IDENTITY_INSERT [Reserva] ON;
INSERT INTO [Reserva] ([Id], [Costo], [CupoId], [Duracion], [Estado], [FechaIngreso], [FechaReserva], [FechaSalida], [VehiculoId])
VALUES (1, NULL, 1, NULL, CAST(1 AS bit), '2025-10-10T00:00:00.0000000', '2025-10-10T00:00:00.0000000', NULL, 1),
(2, NULL, 2, NULL, CAST(1 AS bit), '2025-10-09T00:00:00.0000000', '2025-10-09T00:00:00.0000000', NULL, 2),
(3, NULL, 3, NULL, CAST(1 AS bit), '2025-10-05T00:00:00.0000000', '2025-10-05T00:00:00.0000000', NULL, 3),
(4, 96000.0E0, 4, 24.0E0, CAST(1 AS bit), '2025-10-19T00:00:00.0000000', '2025-10-18T00:00:00.0000000', '2025-10-20T00:00:00.0000000', 6);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Costo', N'CupoId', N'Duracion', N'Estado', N'FechaIngreso', N'FechaReserva', N'FechaSalida', N'VehiculoId') AND [object_id] = OBJECT_ID(N'[Reserva]'))
    SET IDENTITY_INSERT [Reserva] OFF;

CREATE INDEX [IX_Reserva_CupoId] ON [Reserva] ([CupoId]);

CREATE INDEX [IX_Reserva_VehiculoId] ON [Reserva] ([VehiculoId]);

CREATE INDEX [IX_Usuario_RolId] ON [Usuario] ([RolId]);

CREATE INDEX [IX_Vehiculo_UsuarioId] ON [Vehiculo] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251019185704_CreacionInicialDatabase', N'9.0.9');

CREATE TABLE [Soporte] (
    [Id] int NOT NULL IDENTITY,
    [Asunto] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Soporte] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Soporte_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_Soporte_UsuarioId] ON [Soporte] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251023004336_CreacionEntidadSoporte', N'9.0.9');

COMMIT;
GO

