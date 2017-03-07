CREATE TABLE [User] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[First_Name]	NVARCHAR (20)					NOT NULL,
	[Last_Name]		NVARCHAR (30)					NOT NULL,
	[Phone_number]	NVARCHAR (20)					NULL,
	[Email]			NVARCHAR (50)					NOT NULL,
	[Address]		NVARCHAR (50)					NOT NULL,
	[Postal_Code]	NVARCHAR (20)					NOT NULL,
	[City]			NVARCHAR (20)					NOT NULL,
	[Country]		NVARCHAR (50)					NOT NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [Category] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[Category]		NVARCHAR (20)					NOT NULL,
	[Product_Id]	INT								NOT NULL,
	CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC),
)

CREATE TABLE [Target_Group] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[Target_Group]	NVARCHAR (20)					NOT NULL,
	[Product_Id]	INT								NOT NULL,
	CONSTRAINT [PK_Target_Group] PRIMARY KEY CLUSTERED ([Id] ASC),
)

CREATE TABLE [Order] (
    [Id]			INT          IDENTITY (1, 1)	NOT NULL,
    [User_Id]		INT								NOT NULL,
	[Order_Status]	NVARCHAR (20)					NOT NULL,
	[Order_Number]	NVARCHAR (10)					NOT NULL,	
    [Total_Price]	FLOAT							NOT NULL,
	[Address]		NVARCHAR (50)					NOT NULL,
	[Postal_Code]	NVARCHAR (20)					NOT NULL,
	[City]			NVARCHAR (20)					NOT NULL,
	[Country]		NVARCHAR (50)					NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
)

CREATE TABLE [Product] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[Category_Id]	INT								NOT NULL,
	[Target_Group_Id]INT							NOT NULL,
	[Price]			FLOAT							NOT NULL,
	[Name]			NVARCHAR (50)					NOT NULL,
	[Product_Status]NVARCHAR (20)					NOT NULL,
	[Quantity]		INT								NOT NULL,
	CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Category_Id] FOREIGN KEY ([Category_Id]) REFERENCES [dbo].[Category] ([Id]),
	CONSTRAINT [FK_Target_Group_Id] FOREIGN KEY ([Target_Group_Id]) REFERENCES [dbo].[Target_Group] ([Id])
)

CREATE TABLE [Color] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[Product_Id]	INT								NOT NULL,
	[Color]			NVARCHAR (20)					NOT NULL,
	[Img_Name]		NVARCHAR (20)					NOT NULL,
	CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Co_Product_Id] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Product] ([Id])
)

CREATE TABLE [Size] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[Size]			NVARCHAR (20)					NOT NULL,
	[Product_Id]	INT								NOT NULL,
	CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Si_Product_Id] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Product] ([Id])
)




CREATE TABLE [Order_Details] (
	[Id]			INT			IDENTITY (1, 1)		NOT NULL,
	[Order_Id]		INT								NOT NULL,
	[Product_Id]	INT								NOT NULL,
	[Color_Id]		INT								NOT NULL,
	[Size_Id]		INT								NOT NULL,
	[Quantity]		INT								NOT NULL,
	CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Order_Id] FOREIGN KEY ([Order_Id]) REFERENCES [dbo].[Order] ([Id]),
	CONSTRAINT [FK_Od_Product_Id] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT [FK_Color_Id] FOREIGN KEY ([Color_Id]) REFERENCES [dbo].[Color] ([Id]),
	CONSTRAINT [FK_Size_Id] FOREIGN KEY ([Size_Id]) REFERENCES [dbo].[Size] ([Id])
)