USE [PRV_BD]
GO
/****** Object:  UserDefinedTableType [dbo].[TipoDetalleCompra]    Script Date: 20/09/2026 19:45:41 ******/
CREATE TYPE [dbo].[TipoDetalleCompra] AS TABLE(
	[IdProducto] [bigint] NOT NULL,
	[Cantidad] [int] NOT NULL
)
GO
/****** Object:  Table [dbo].[Detalle_Venta]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalle_Venta](
	[IdDetalleVenta] [bigint] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [numeric](18, 0) NOT NULL,
	[Subtotal] [numeric](18, 0) NOT NULL,
	[IdVenta] [bigint] NOT NULL,
	[IdProducto] [bigint] NOT NULL,
 CONSTRAINT [PK_Detalle_Venta] PRIMARY KEY CLUSTERED 
(
	[IdDetalleVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[IdEmpresa] [bigint] IDENTITY(1,1) NOT NULL,
	[Rut] [varchar](12) NOT NULL,
	[RazonSocial] [varchar](150) NOT NULL,
	[EmailCorporativo] [varchar](254) NOT NULL,
	[Telefono] [varchar](20) NULL,
	[Direccion] [varchar](255) NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Empresa_Rut] UNIQUE NONCLUSTERED 
(
	[Rut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[PrecioVenta] [numeric](18, 0) NOT NULL,
	[PrecioMayorista] [numeric](18, 0) NOT NULL,
	[Stock] [int] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[IdEmpresa] [bigint] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Rol_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[Rut] [varchar](12) NULL,
	[Telefono] [varchar](20) NULL,
	[Direccion] [varchar](255) NULL,
	[PasswordHash] [varchar](255) NULL,
	[TipoCliente] [varchar](15) NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
	[IdEmpresa] [bigint] NULL,
	[IdRol] [bigint] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Usuario_Empresa_Email] UNIQUE NONCLUSTERED 
(
	[IdEmpresa] ASC,
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[IdVenta] [bigint] IDENTITY(1,1) NOT NULL,
	[FechaVenta] [datetime] NOT NULL,
	[Total] [numeric](18, 0) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[Observacion] [varchar](500) NULL,
	[IdUsuario] [bigint] NOT NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[IdVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_Empresa_Rut]    Script Date: 20/09/2026 19:45:41 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Usuario_Empresa_Rut] ON [dbo].[Usuario]
(
	[IdEmpresa] ASC,
	[Rut] ASC
)
WHERE ([Rut] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF__Usuario__EsClien__3F466844]  DEFAULT ((0)) FOR [TipoCliente]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF__Usuario__Estado__403A8C7D]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Detalle_Venta]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Venta_Producto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[Detalle_Venta] CHECK CONSTRAINT [FK_Detalle_Venta_Producto]
GO
ALTER TABLE [dbo].[Detalle_Venta]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Venta_Venta] FOREIGN KEY([IdVenta])
REFERENCES [dbo].[Venta] ([IdVenta])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Detalle_Venta] CHECK CONSTRAINT [FK_Detalle_Venta_Venta]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Empresa]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Empresa]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Usuario]
GO
ALTER TABLE [dbo].[Detalle_Venta]  WITH CHECK ADD  CONSTRAINT [CK_Detalle_Venta_Cantidad] CHECK  (([Cantidad]>(0)))
GO
ALTER TABLE [dbo].[Detalle_Venta] CHECK CONSTRAINT [CK_Detalle_Venta_Cantidad]
GO
ALTER TABLE [dbo].[Detalle_Venta]  WITH CHECK ADD  CONSTRAINT [CK_Detalle_Venta_PrecioUnitario] CHECK  (([PrecioUnitario]>=(0)))
GO
ALTER TABLE [dbo].[Detalle_Venta] CHECK CONSTRAINT [CK_Detalle_Venta_PrecioUnitario]
GO
ALTER TABLE [dbo].[Detalle_Venta]  WITH CHECK ADD  CONSTRAINT [CK_Detalle_Venta_Subtotal] CHECK  (([Subtotal]>=(0)))
GO
ALTER TABLE [dbo].[Detalle_Venta] CHECK CONSTRAINT [CK_Detalle_Venta_Subtotal]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_PrecioMayorista] CHECK  (([PrecioMayorista]>=(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_PrecioMayorista]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_PrecioVenta] CHECK  (([PrecioVenta]>=(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_PrecioVenta]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Stock] CHECK  (([Stock]>=(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Stock]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [CK_Venta_Estado] CHECK  (([Estado]='Anulada' OR [Estado]='Confirmada' OR [Estado]='Pendiente'))
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [CK_Venta_Estado]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [CK_Venta_Total] CHECK  (([Total]>=(0)))
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [CK_Venta_Total]
GO
/****** Object:  StoredProcedure [dbo].[sp_Empresa_Crear]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Empresa_Crear]
    @Rut VARCHAR(12),
    @RazonSocial VARCHAR(150),
    @EmailCorporativo VARCHAR(254),
    @Telefono VARCHAR(20),
    @Direccion VARCHAR(255),
    @Estado BIT,

    @NombreAdministrador VARCHAR(150),
    @EmailAdministrador VARCHAR(254)
AS
BEGIN

    SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRANSACTION;


        /* =========================================
           CREAR EMPRESA
           ========================================= */

        INSERT INTO dbo.Empresa
        (
            Rut,
            RazonSocial,
            EmailCorporativo,
            Telefono,
            Direccion,
            Estado,
            FechaRegistro
        )
        VALUES
        (
            @Rut,
            @RazonSocial,
            @EmailCorporativo,
            @Telefono,
            @Direccion,
            @Estado,
            GETDATE()
        );


        /* Obtener IdEmpresa recién creada */

        DECLARE @IdEmpresa BIGINT;

        SET @IdEmpresa = SCOPE_IDENTITY();


        /* =========================================
           CREAR USUARIO ADMINISTRADOR
           ========================================= */

        INSERT INTO dbo.Usuario
        (
            Nombre,
            Email,
            PasswordHash,
            TipoCliente,
            Estado,
            FechaRegistro,
            IdEmpresa,
            IdRol
        )
        VALUES
        (
            @NombreAdministrador,
            @EmailAdministrador,
            'demo',
            NULL,
            1,
            GETDATE(),
            @IdEmpresa,
            2
        );


        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH

END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Empresa_Editar]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Empresa_Editar]
    @IdEmpresa BIGINT,
    @Rut VARCHAR(12),
    @RazonSocial VARCHAR(150),
    @EmailCorporativo VARCHAR(254),
    @Telefono VARCHAR(20),
    @Direccion VARCHAR(255),
    @Estado BIT,

    @NombreAdministrador VARCHAR(150),
    @EmailAdministrador VARCHAR(254)
AS
BEGIN

    SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRANSACTION;


        /* =========================================
           EDITAR EMPRESA
           ========================================= */

        UPDATE dbo.Empresa
        SET
            Rut = @Rut,
            RazonSocial = @RazonSocial,
            EmailCorporativo = @EmailCorporativo,
            Telefono = @Telefono,
            Direccion = @Direccion,
            Estado = @Estado
        WHERE IdEmpresa = @IdEmpresa;


        /* =========================================
           EDITAR ADMINISTRADOR
           ========================================= */

        UPDATE dbo.Usuario
        SET
            Nombre = @NombreAdministrador,
            Email = @EmailAdministrador
        WHERE IdEmpresa = @IdEmpresa
          AND IdRol = 2;


        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH

END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Empresa_Eliminar]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_Empresa_Eliminar]
    @IdEmpresa BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Empresa
    WHERE IdEmpresa = @IdEmpresa;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Empresa_Listar]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Empresa_Listar]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        E.IdEmpresa,
        E.Rut,
        E.RazonSocial,
        E.EmailCorporativo,
        E.Telefono,
        E.Direccion,
        E.Estado,
        E.FechaRegistro,

        U.Nombre AS NombreAdministrador,
        U.Email AS EmailAdministrador

    FROM dbo.Empresa E

    LEFT JOIN dbo.Usuario U
        ON U.IdEmpresa = E.IdEmpresa
       AND U.IdRol = 2

    ORDER BY E.IdEmpresa;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Producto_ListarPorEmpresa]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Producto_ListarPorEmpresa]
    @IdEmpresa BIGINT
AS
BEGIN

    SET NOCOUNT ON;

    SELECT
        IdProducto,
        Nombre,
        Descripcion,
        PrecioVenta,
        PrecioMayorista,
        Stock,
        Estado,
        FechaRegistro,
        IdEmpresa
    FROM dbo.Producto
    WHERE IdEmpresa = @IdEmpresa
      AND Estado = 'Activo'
    ORDER BY Nombre;

END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Usuario_BuscarPorEmailEmpresa]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_Usuario_BuscarPorEmailEmpresa]
    @Email VARCHAR(254),
    @IdEmpresa BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdUsuario,
        Nombre,
        Email,
        Rut,
        Telefono,
        Direccion,
        PasswordHash,
        TipoCliente,
        Estado,
        FechaRegistro,
        IdEmpresa,
        IdRol
    FROM dbo.Usuario
    WHERE Email = @Email
      AND IdEmpresa = @IdEmpresa;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Usuario_Crear]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Usuario_Crear]
    @Nombre VARCHAR(150),
    @Email VARCHAR(254),
    @Rut VARCHAR(12) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Direccion VARCHAR(255) = NULL,
    @PasswordHash VARCHAR(255) = NULL,
    @TipoCliente VARCHAR(20) = NULL,
    @Estado BIT = 1,
    @IdEmpresa BIGINT = NULL,
    @IdRol BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.Usuario
        WHERE Email = @Email
          AND IdEmpresa = @IdEmpresa
    )
    BEGIN
        UPDATE dbo.Usuario
        SET
            Nombre = @Nombre,
            Rut = @Rut,
            Telefono = @Telefono,
            Direccion = @Direccion,
            TipoCliente = @TipoCliente,
            Estado = @Estado
        WHERE Email = @Email
          AND IdEmpresa = @IdEmpresa;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.Usuario
        (
            Nombre,
            Email,
            Rut,
            Telefono,
            Direccion,
            PasswordHash,
            TipoCliente,
            Estado,
            FechaRegistro,
            IdEmpresa,
            IdRol
        )
        VALUES
        (
            @Nombre,
            @Email,
            @Rut,
            @Telefono,
            @Direccion,
            @PasswordHash,
            @TipoCliente,
            @Estado,
            GETDATE(),
            @IdEmpresa,
            @IdRol
        );
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Usuario_ValidarClave]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_Usuario_ValidarClave]
    @Email VARCHAR(254),
    @IdEmpresa BIGINT,
    @PasswordHash VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdUsuario,
        Nombre,
        Email,
        TipoCliente,
        IdEmpresa
    FROM dbo.Usuario
    WHERE Email = @Email
      AND IdEmpresa = @IdEmpresa
      AND PasswordHash = @PasswordHash
      AND TipoCliente = 'Mayorista'
      AND Estado = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_Venta_Pagar]    Script Date: 20/09/2026 19:45:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Venta_Pagar]
    @IdUsuario BIGINT,
    @Detalle dbo.TipoDetalleCompra READONLY
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;


        -- =============================================
        -- VALIDAR QUE EXISTAN PRODUCTOS
        -- =============================================
        IF NOT EXISTS (SELECT 1 FROM @Detalle)
        BEGIN
            THROW 50001, 'La compra no contiene productos.', 1;
        END;


        -- =============================================
        -- VALIDAR CANTIDADES
        -- =============================================
        IF EXISTS
        (
            SELECT 1
            FROM @Detalle
            WHERE Cantidad <= 0
        )
        BEGIN
            THROW 50002, 'La cantidad debe ser mayor a cero.', 1;
        END;


        -- =============================================
        -- VALIDAR PRODUCTOS Y STOCK
        -- =============================================
        IF EXISTS
        (
            SELECT 1
            FROM @Detalle d
            LEFT JOIN dbo.Producto p
                ON p.IdProducto = d.IdProducto
            WHERE p.IdProducto IS NULL
               OR p.Estado <> 'Activo'
               OR p.Stock < d.Cantidad
        )
        BEGIN
            THROW 50003, 'Uno de los productos no existe, no está activo o no tiene stock suficiente.', 1;
        END;


        -- =============================================
        -- TOTAL DE UNIDADES
        -- =============================================
        DECLARE @CantidadTotal INT;

        SELECT @CantidadTotal = SUM(Cantidad)
        FROM @Detalle;


        -- =============================================
        -- CALCULAR TOTAL
        -- 3 O MÁS UNIDADES = PRECIO MAYORISTA
        -- =============================================
        DECLARE @Total NUMERIC(18,0);

        SELECT @Total =
            SUM
            (
                d.Cantidad *
                CASE
                    WHEN @CantidadTotal >= 3
                        THEN p.PrecioMayorista
                    ELSE p.PrecioVenta
                END
            )
        FROM @Detalle d
        INNER JOIN dbo.Producto p
            ON p.IdProducto = d.IdProducto;


        -- =============================================
        -- CREAR VENTA
        -- =============================================
        INSERT INTO dbo.Venta
        (
            FechaVenta,
            Total,
            Estado,
            Observacion,
            IdUsuario
        )
        VALUES
        (
            GETDATE(),
            @Total,
            'Confirmada',
            NULL,
            @IdUsuario
        );


        DECLARE @IdVenta BIGINT;

        SET @IdVenta = SCOPE_IDENTITY();


        -- =============================================
        -- CREAR DETALLE DE VENTA
        -- =============================================
        INSERT INTO dbo.Detalle_Venta
        (
            Cantidad,
            PrecioUnitario,
            Subtotal,
            IdVenta,
            IdProducto
        )
        SELECT
            d.Cantidad,

            CASE
                WHEN @CantidadTotal >= 3
                    THEN p.PrecioMayorista
                ELSE p.PrecioVenta
            END,

            d.Cantidad *
            CASE
                WHEN @CantidadTotal >= 3
                    THEN p.PrecioMayorista
                ELSE p.PrecioVenta
            END,

            @IdVenta,
            d.IdProducto

        FROM @Detalle d

        INNER JOIN dbo.Producto p
            ON p.IdProducto = d.IdProducto;


        -- =============================================
        -- DESCONTAR STOCK
        -- =============================================
        UPDATE p
        SET p.Stock = p.Stock - d.Cantidad

        FROM dbo.Producto p

        INNER JOIN @Detalle d
            ON d.IdProducto = p.IdProducto;


        COMMIT TRANSACTION;


        -- =============================================
        -- RESULTADO
        -- =============================================
        SELECT
            @IdVenta AS IdVenta,
            @Total AS Total;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH;

END;
GO
