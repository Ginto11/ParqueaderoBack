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
    [EstadoDescripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Cupo] PRIMARY KEY ([Id]),
    CONSTRAINT [CH_Cupo_EstadoDescripcion] CHECK ([EstadoDescripcion] IN ('Reservado', 'Ocupado', 'Disponible'))
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

CREATE TABLE [Soporte] (
    [Id] int NOT NULL IDENTITY,
    [Asunto] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Soporte] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Soporte_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
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
    [FechaIngresoEstipulada] datetime2 NOT NULL,
    [FechaIngresoReal] datetime2 NULL,
    [FechaSalida] datetime2 NULL,
    [Costo] float NULL,
    [Duracion] float NULL,
    [Estado] bit NOT NULL,
    [EstadoDescripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Reserva] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Reserva_EstadoDescripcion] CHECK ([EstadoDescripcion] IN ('Activa', 'Finalizada', 'Cancelada')),
    CONSTRAINT [FK_Reserva_Cupo_CupoId] FOREIGN KEY ([CupoId]) REFERENCES [Cupo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reserva_Vehiculo_VehiculoId] FOREIGN KEY ([VehiculoId]) REFERENCES [Vehiculo] ([Id]) ON DELETE NO ACTION
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Estado', N'EstadoDescripcion') AND [object_id] = OBJECT_ID(N'[Cupo]'))
    SET IDENTITY_INSERT [Cupo] ON;
INSERT INTO [Cupo] ([Id], [Estado], [EstadoDescripcion])
VALUES (1, CAST(1 AS bit), N'Ocupado'),
(2, CAST(1 AS bit), N'Ocupado'),
(3, CAST(1 AS bit), N'Ocupado'),
(4, CAST(0 AS bit), N'Disponible'),
(5, CAST(0 AS bit), N'Disponible'),
(6, CAST(0 AS bit), N'Disponible'),
(7, CAST(1 AS bit), N'Ocupado'),
(8, CAST(0 AS bit), N'Disponible'),
(9, CAST(0 AS bit), N'Disponible'),
(10, CAST(0 AS bit), N'Disponible'),
(11, CAST(1 AS bit), N'Ocupado'),
(12, CAST(1 AS bit), N'Ocupado'),
(13, CAST(0 AS bit), N'Disponible'),
(14, CAST(0 AS bit), N'Disponible'),
(15, CAST(0 AS bit), N'Disponible'),
(16, CAST(0 AS bit), N'Disponible'),
(17, CAST(1 AS bit), N'Reservado'),
(18, CAST(0 AS bit), N'Disponible'),
(19, CAST(0 AS bit), N'Disponible'),
(20, CAST(1 AS bit), N'Ocupado');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Estado', N'EstadoDescripcion') AND [object_id] = OBJECT_ID(N'[Cupo]'))
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
(4, N'ouj96EN+PovtRy940zJAIA==', N'SilenidSSarmiento', N'Silenid Salinas Sarmiento', NULL, 2),
(5, N'FhqsQlGPwXDJgjpPBJ9mHA==', N'DanielaFRamirezO', N'Daniela Fernanda Ramirez Ortiz', NULL, 2),
(6, N'S7EAxI4LweQlUKawN7+yMw==', N'CarlosETorresM', N'Carlos Eduardo Torres Medina', NULL, 2),
(7, N'BqcM1SAYio4g4wx94cGD8A==', N'LauraVPinedaG', N'Laura Vanessa Pineda Gomez', NULL, 2),
(8, N'Ura9j/soWm9XtY6aZeh+mg==', N'JuanCRodriguezS', N'Juan Camilo Rodriguez Silva', NULL, 2),
(9, N'/wIwgBJC+x66mtAn9Rtqcg==', N'ValentinaNRojasD', N'Valentina Nicole Rojas Diaz', NULL, 2),
(10, N'qRPJ1XEsRadus8k762LyPw==', N'DavidAAcostaP', N'David Alejandro Acosta Peña', NULL, 2),
(11, N'BFMmtRWsaydyTpPSN+iRHg==', N'MarianaSCastilloL', N'Mariana Sofía Castillo Lara', NULL, 2),
(12, N'iadC0NZMHKxjZX5ekTEOXw==', N'SebastianAVegaM', N'Sebastian Andres Vega Muñoz', NULL, 2),
(13, N'zp933pVbM1vUX/UIE+jCEg==', N'NicolasAHerreraC', N'Nicolas Adrian Herrera Cruz', NULL, 2),
(14, N'ojD4mP/JBovXB46YzCEIlg==', N'PaulaAMendozaR', N'Paula Andrea Mendoza Ruiz', NULL, 2);
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
(6, 250, N'FCX-66B', 1),
(7, 125, N'HPL-90C', 5),
(8, 250, N'KSD-81A', 6),
(9, 200, N'ZXC-22D', 6),
(10, 400, N'RTV-14F', 8),
(11, 125, N'POE-76B', 8),
(12, 250, N'BMN-99E', 8),
(13, 200, N'LMQ-45C', 10),
(14, 400, N'QWE-33H', 11),
(15, 125, N'PLM-50G', 11),
(16, 250, N'VBN-07K', 13);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cilindraje', N'Placa', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Vehiculo]'))
    SET IDENTITY_INSERT [Vehiculo] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Costo', N'CupoId', N'Duracion', N'Estado', N'EstadoDescripcion', N'FechaIngresoEstipulada', N'FechaIngresoReal', N'FechaReserva', N'FechaSalida', N'VehiculoId') AND [object_id] = OBJECT_ID(N'[Reserva]'))
    SET IDENTITY_INSERT [Reserva] ON;
INSERT INTO [Reserva] ([Id], [Costo], [CupoId], [Duracion], [Estado], [EstadoDescripcion], [FechaIngresoEstipulada], [FechaIngresoReal], [FechaReserva], [FechaSalida], [VehiculoId])
VALUES (1, NULL, 1, NULL, CAST(1 AS bit), N'Activa', '2025-10-10T00:00:00.0000000', '2025-10-10T00:00:00.0000000', '2025-10-10T00:00:00.0000000', NULL, 1),
(2, NULL, 2, NULL, CAST(1 AS bit), N'Activa', '2025-10-09T00:00:00.0000000', '2025-10-09T00:00:00.0000000', '2025-10-09T00:00:00.0000000', NULL, 2),
(3, NULL, 3, NULL, CAST(1 AS bit), N'Activa', '2025-10-05T00:00:00.0000000', '2025-10-05T00:00:00.0000000', '2025-10-05T00:00:00.0000000', NULL, 3),
(4, 96000.0E0, 4, 24.0E0, CAST(1 AS bit), N'Finalizada', '2025-10-19T00:00:00.0000000', '2025-10-19T00:00:00.0000000', '2025-10-18T00:00:00.0000000', '2025-10-20T00:00:00.0000000', 6),
(5, NULL, 11, NULL, CAST(1 AS bit), N'Activa', '2025-11-14T00:00:00.0000000', '2025-11-15T00:00:00.0000000', '2025-10-13T00:00:00.0000000', NULL, 14),
(6, NULL, 18, NULL, CAST(1 AS bit), N'Cancelada', '2025-10-13T00:00:00.0000000', NULL, '2025-10-12T00:00:00.0000000', NULL, 8),
(7, 168000.0E0, 9, 48.0E0, CAST(1 AS bit), N'Finalizada', '2025-10-09T00:00:00.0000000', '2025-10-09T00:00:00.0000000', '2025-10-08T00:00:00.0000000', '2025-10-11T00:00:00.0000000', 5),
(8, NULL, 20, NULL, CAST(1 AS bit), N'Activa', '2025-10-06T00:00:00.0000000', '2025-10-06T00:00:00.0000000', '2025-10-05T00:00:00.0000000', NULL, 16),
(9, 336000.0E0, 13, 96.0E0, CAST(1 AS bit), N'Finalizada', '2025-10-11T00:00:00.0000000', '2025-10-12T00:00:00.0000000', '2025-10-10T00:00:00.0000000', '2025-10-16T00:00:00.0000000', 11),
(10, NULL, 7, NULL, CAST(1 AS bit), N'Activa', '2025-10-04T00:00:00.0000000', '2025-10-04T00:00:00.0000000', '2025-10-03T00:00:00.0000000', NULL, 3),
(11, NULL, 17, NULL, CAST(1 AS bit), N'Activa', '2025-10-16T00:00:00.0000000', '2025-10-16T00:00:00.0000000', '2025-10-15T00:00:00.0000000', NULL, 9),
(12, 96000.0E0, 15, 24.0E0, CAST(1 AS bit), N'Finalizada', '2025-10-03T00:00:00.0000000', '2025-10-03T00:00:00.0000000', '2025-10-02T00:00:00.0000000', '2025-10-04T00:00:00.0000000', 10),
(13, NULL, 12, NULL, CAST(1 AS bit), N'Activa', '2025-10-18T00:00:00.0000000', '2025-10-18T00:00:00.0000000', '2025-10-17T00:00:00.0000000', NULL, 4),
(14, 180000.0E0, 19, 24.0E0, CAST(1 AS bit), N'Finalizada', '2025-10-01T00:00:00.0000000', '2025-10-01T00:00:00.0000000', '2025-09-30T00:00:00.0000000', '2025-10-02T00:00:00.0000000', 15);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Costo', N'CupoId', N'Duracion', N'Estado', N'EstadoDescripcion', N'FechaIngresoEstipulada', N'FechaIngresoReal', N'FechaReserva', N'FechaSalida', N'VehiculoId') AND [object_id] = OBJECT_ID(N'[Reserva]'))
    SET IDENTITY_INSERT [Reserva] OFF;

CREATE INDEX [IX_Reserva_CupoId] ON [Reserva] ([CupoId]);

CREATE INDEX [IX_Reserva_VehiculoId] ON [Reserva] ([VehiculoId]);

CREATE INDEX [IX_Soporte_UsuarioId] ON [Soporte] ([UsuarioId]);

CREATE INDEX [IX_Usuario_RolId] ON [Usuario] ([RolId]);

CREATE INDEX [IX_Vehiculo_UsuarioId] ON [Vehiculo] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251026175200_CreacionInicialDatabase', N'9.0.9');

CREATE TABLE [MetodoPago] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MetodoPago] PRIMARY KEY ([Id])
);

CREATE TABLE [TipoTransaccion] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TipoTransaccion] PRIMARY KEY ([Id])
);

CREATE TABLE [Transaccion] (
    [Id] int NOT NULL IDENTITY,
    [FechaHora] datetime2 NOT NULL,
    [TipoTransaccionId] int NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [Monto] decimal(18,2) NOT NULL,
    [MetodoPagoId] int NOT NULL,
    [ReservaId] int NOT NULL,
    CONSTRAINT [PK_Transaccion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transaccion_MetodoPago_MetodoPagoId] FOREIGN KEY ([MetodoPagoId]) REFERENCES [MetodoPago] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transaccion_Reserva_ReservaId] FOREIGN KEY ([ReservaId]) REFERENCES [Reserva] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transaccion_TipoTransaccion_TipoTransaccionId] FOREIGN KEY ([TipoTransaccionId]) REFERENCES [TipoTransaccion] ([Id]) ON DELETE NO ACTION
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[MetodoPago]'))
    SET IDENTITY_INSERT [MetodoPago] ON;
INSERT INTO [MetodoPago] ([Id], [Nombre])
VALUES (1, N'Efectivo'),
(2, N'Tarjeta'),
(3, N'Transferencia');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[MetodoPago]'))
    SET IDENTITY_INSERT [MetodoPago] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[TipoTransaccion]'))
    SET IDENTITY_INSERT [TipoTransaccion] ON;
INSERT INTO [TipoTransaccion] ([Id], [Nombre])
VALUES (1, N'Ingreso'),
(2, N'Egreso');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[TipoTransaccion]'))
    SET IDENTITY_INSERT [TipoTransaccion] OFF;

CREATE INDEX [IX_Transaccion_MetodoPagoId] ON [Transaccion] ([MetodoPagoId]);

CREATE INDEX [IX_Transaccion_ReservaId] ON [Transaccion] ([ReservaId]);

CREATE INDEX [IX_Transaccion_TipoTransaccionId] ON [Transaccion] ([TipoTransaccionId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251106002946_CreacionDeTablaTransaccion', N'9.0.9');

COMMIT;
GO

