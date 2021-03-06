USE [ADSAssignment]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2019/09/28 10:30:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[Discontinued] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[MobileNo] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[EmployeePositionID] [int] NULL,
	[Discontinued] [bit] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePositions]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePositions](
	[EmployeePositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeePositions] PRIMARY KEY CLUSTERED 
(
	[EmployeePositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[IDNO] [nvarchar](13) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[ReportTo] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[PositionID] [int] NOT NULL,
	[Discontinued] [bit] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceNumber] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[EmployeeID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceItems]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceItems](
	[LineNumber] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [int] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Qty] [int] NOT NULL,
	[TotalPrice] [money] NOT NULL,
 CONSTRAINT [PK_InvoiceItems] PRIMARY KEY CLUSTERED 
(
	[LineNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [bigint] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[QuantintyPerUnit] [nvarchar](20) NOT NULL,
	[Unitprice] [money] NOT NULL,
	[UnitsInStock] [int] NOT NULL,
	[ReorderLevel] [int] NOT NULL,
	[Discontinued] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 2019/09/28 10:30:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[City] [nvarchar](15) NULL,
	[Region] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](24) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Discontinued] [bit] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Discontinued]) VALUES (1, N'Breakfast', N'bread', 0)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Discontinued]) VALUES (2, N'Breakfast', N'bread,rama,sugar,teabags', 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Discontinued]) VALUES (3, N'Chip', N'Chips', 0)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Discontinued]) VALUES (11, N'Soup,washing', N'sunlight,OMO,greenbar', 0)
SET IDENTITY_INSERT [dbo].[EmployeePositions] ON 

INSERT [dbo].[EmployeePositions] ([EmployeePositionID], [PositionName]) VALUES (1, N'Cashier')
INSERT [dbo].[EmployeePositions] ([EmployeePositionID], [PositionName]) VALUES (2, N'Supervisor')
SET IDENTITY_INSERT [dbo].[EmployeePositions] OFF
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (101, N'Khawula', N'Mxolisi', N'Mr', N'9811205754082', N'Durban 4001', 2, N'mxolisizondi@gmail', N'Mxolisi', N'Mxo', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (102, N'Xulu', N'Cedric', N'Mr', N'9910195655082', N'Durban 400', 2, N'ced.xulu@gmail.com', N'Cedric', N'12345', 2, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (103, N'Khawula', N'Sizwe', N'Mr', N'910245675984', N'Mlazi', 1, N'sizwe@gmail.com', N'sizwe', N'123', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (104, N'Khawula', N'Sihle', N'Mr', N'9606053456082', N'PMB', 1, N'sihle@gmail.com', N'sihle', N'123', 2, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (105, N'Zondi', N'Khulani', N'Mr', N'005124648566', N'PS', 2, N'khule@gmail.com', N'khule', N'123', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (106, N'Zondi', N'The', N'Miss', N'9810195655082', N'PS', 2, N'the@gmail.com', N'the', N'123', 2, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (109, N'sdfg', N'sdfgh', N'sdfg', N'8520025520', N'sdfg', 106, N'cvbnm', N'fghj,', N'cvbnm,', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (2010, N'Ngidi', N'Sya', N'Mr', N'9810195655082', N'Durban 100', 106, N'mxo@gmail.com', N'syah', N'syah', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (2020, N'Zondi', N'Mxo', N'Mr', N'9810195655082', N'POBox3211', 102, N'mxolisi@gmail.com', N'Mxo', N'12345', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (2140, N'XCVB', N'ASDFG', N'dfvbn', N'9810', N'sdfg', 106, N'mx', N'mx', N'201', 1, 0)
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Title], [IDNO], [Address], [ReportTo], [Email], [Username], [Password], [PositionID], [Discontinued]) VALUES (200001, N'Zondi', N'Mxolisi', N'Mr', N'9810195655082', N'PMB', 102, N'mxolisi@gmail.com', N'mxo', N'123', 1, 0)
SET IDENTITY_INSERT [dbo].[Invoice] ON 

INSERT [dbo].[Invoice] ([InvoiceNumber], [InvoiceDate], [EmployeeID]) VALUES (1025, CAST(N'2019-04-17' AS Date), 101)
INSERT [dbo].[Invoice] ([InvoiceNumber], [InvoiceDate], [EmployeeID]) VALUES (1026, CAST(N'2019-04-17' AS Date), 103)
INSERT [dbo].[Invoice] ([InvoiceNumber], [InvoiceDate], [EmployeeID]) VALUES (1027, CAST(N'2019-04-20' AS Date), 101)
INSERT [dbo].[Invoice] ([InvoiceNumber], [InvoiceDate], [EmployeeID]) VALUES (2027, CAST(N'2019-09-28' AS Date), 101)
SET IDENTITY_INSERT [dbo].[Invoice] OFF
SET IDENTITY_INSERT [dbo].[InvoiceItems] ON 

INSERT [dbo].[InvoiceItems] ([LineNumber], [InvoiceNumber], [ProductID], [Qty], [TotalPrice]) VALUES (1029, 1025, 203, 1, 15.0000)
INSERT [dbo].[InvoiceItems] ([LineNumber], [InvoiceNumber], [ProductID], [Qty], [TotalPrice]) VALUES (1030, 1026, 606, 3, 151.0000)
INSERT [dbo].[InvoiceItems] ([LineNumber], [InvoiceNumber], [ProductID], [Qty], [TotalPrice]) VALUES (1031, 1026, 607, 1, 20.0000)
INSERT [dbo].[InvoiceItems] ([LineNumber], [InvoiceNumber], [ProductID], [Qty], [TotalPrice]) VALUES (1032, 1027, 608, 4, 100.0000)
INSERT [dbo].[InvoiceItems] ([LineNumber], [InvoiceNumber], [ProductID], [Qty], [TotalPrice]) VALUES (1033, 1027, 609, 1, 40.0000)
INSERT [dbo].[InvoiceItems] ([LineNumber], [InvoiceNumber], [ProductID], [Qty], [TotalPrice]) VALUES (2032, 2027, 604, 2, 138.0000)
SET IDENTITY_INSERT [dbo].[InvoiceItems] OFF
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (203, N'Bread', 210, 1, N'13', 15.0000, 19, 10, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (604, N'Oil 5kg', 605, 1, N'50', 69.0000, 53, 30, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (605, N'Viyena', 202, 2, N'500ml', 26.0000, 0, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (606, N'rama', 1, 1, N'5gram', 50.0000, 28, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (607, N'Amasi', 2, 2, N'2litre', 20.0000, 42, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (608, N'Cocacola', 1, 1, N'2litre', 25.0000, 83, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (609, N'Sugar', 1, 1, N'500gm', 40.0000, 97, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (610, N'Snowball', 1, 1, N'200g', 5.0000, 50, 20, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (611, N'Chips', 1, 1, N'2g', 20.0000, 100, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (6001, N'asdfgh', 201, 1, N'20g', 20.0000, 200, 20, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (60001, N'Guava Juice', 210, 1, N'1', 16.5000, 600, 200, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (6001306002440, N'Noodles', 201, 1, N'1', 10.0000, 3, 5, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [SupplierID], [CategoryID], [QuantintyPerUnit], [Unitprice], [UnitsInStock], [ReorderLevel], [Discontinued]) VALUES (6008454002605, N'CounterBook', 210, 1, N'1', 10.0000, 6, 10, 0)
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Email], [Discontinued]) VALUES (210, N'Immedia', N'Umhlanga 501', N'Umlanga', N'Durban', N'4001', N'SA', N'0603872453', N'mxolisizondi20@gmail.com', 0)
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Email], [Discontinued]) VALUES (201, N'MUT', N'mut.ac.za', N'Umlazi', N'SA', N'4001', N'SA', N'03178755478', N'Umlazi 201', 0)
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Email], [Discontinued]) VALUES (202, N'Immedia', N'Regside 201', N'Durban', N'SA', N'6001', N'SA', N'0315475621', N'immedia@gmail.com', 0)
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Email], [Discontinued]) VALUES (2001, N'Mangosuthu', N'Umlazi 100', N'Umalazi', N'Durban', N'4525', N'SA', N'0603872455', N'mxolisizondi20@gmail.com', 0)
INSERT [dbo].[Suppliers] ([SupplierID], [CompanyName], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Email], [Discontinued]) VALUES (12345, N'MUTAC', N'PO', N'NUZ', N'SA', N'4001', N'SA', N'0603872456', N'mxolisi@gmail.com', 0)
