USE [PRV_BD2]
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 
GO
INSERT [dbo].[Rol] ([IdRol], [Nombre], [Descripcion], [Estado]) VALUES (1, N'SuperAdministrador', N'Administrador general del sistema PRV', 1)
GO
INSERT [dbo].[Rol] ([IdRol], [Nombre], [Descripcion], [Estado]) VALUES (2, N'Administrador', N'Administrador de una empresa', 1)
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Empresa] ON 
GO
INSERT [dbo].[Empresa] ([IdEmpresa], [Rut], [RazonSocial], [EmailCorporativo], [Telefono], [Direccion], [Estado], [FechaRegistro]) VALUES (14, N'1-9', N'Abarrotes S.A.', N'abarrotes@abarotes.cl', N'912345678', N'Mares  888', 1, CAST(N'2026-09-13T19:49:23.743' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Empresa] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Email], [Rut], [Telefono], [Direccion], [PasswordHash], [TipoCliente], [Estado], [FechaRegistro], [IdEmpresa], [IdRol]) VALUES (3, N'Matias', N'superadmin@prv.cl', NULL, NULL, NULL, N'demo', NULL, 1, CAST(N'2026-09-11T17:41:40.800' AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (1, N'Arroz 1 Kg', N'Arroz grado 1.', CAST(1500 AS Numeric(18, 0)), CAST(1200 AS Numeric(18, 0)), 47, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (2, N'Aceite 1 Litro', N'Aceite vegetal.', CAST(2200 AS Numeric(18, 0)), CAST(1800 AS Numeric(18, 0)), 30, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (3, N'Azúcar 1 Kg', N'Azúcar granulada.', CAST(1300 AS Numeric(18, 0)), CAST(950 AS Numeric(18, 0)), 40, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (4, N'Harina 1 Kg', N'Harina de trigo.', CAST(1200 AS Numeric(18, 0)), CAST(900 AS Numeric(18, 0)), 45, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (5, N'Fideos 400 g', N'Fideos tradicionales.', CAST(900 AS Numeric(18, 0)), CAST(700 AS Numeric(18, 0)), 60, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (6, N'Sal 1 Kg', N'Sal fina de mesa.', CAST(800 AS Numeric(18, 0)), CAST(600 AS Numeric(18, 0)), 30, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (7, N'Lentejas 1 Kg', N'Lentejas seleccionadas.', CAST(2400 AS Numeric(18, 0)), CAST(1900 AS Numeric(18, 0)), 0, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (8, N'Porotos 1 Kg', N'Porotos seleccionados.', CAST(2800 AS Numeric(18, 0)), CAST(2200 AS Numeric(18, 0)), 25, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (9, N'Atún en lata', N'Atún en conserva.', CAST(1800 AS Numeric(18, 0)), CAST(1400 AS Numeric(18, 0)), 27, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
INSERT [dbo].[Producto] ([IdProducto], [Nombre], [Descripcion], [PrecioVenta], [PrecioMayorista], [Stock], [Estado], [FechaRegistro], [IdEmpresa]) VALUES (10, N'Leche 1 Litro', N'Leche entera.', CAST(1400 AS Numeric(18, 0)), CAST(1100 AS Numeric(18, 0)), 49, N'Activo', CAST(N'2026-09-20T13:25:23.103' AS DateTime), 14)
GO
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
