USE [RM]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 03/13/2015 22:41:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierCode] [varchar](7) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](255) NULL,
	[ContactPerson1] [nvarchar](50) NULL,
	[ContactPerson2] [nvarchar](50) NULL,
	[ContactPerson3] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](15) NULL,
 CONSTRAINT [pk_SupplierID] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SupplierContactDetails]    Script Date: 03/13/2015 22:41:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SupplierContactDetails](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ContactPerson] [varchar](100) NULL,
	[ContactNo] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
 CONSTRAINT [PK_SupplierContactDetails] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_SupplierContact_Supplier]    Script Date: 03/13/2015 22:41:52 ******/
ALTER TABLE [dbo].[SupplierContactDetails]  WITH CHECK ADD  CONSTRAINT [FK_SupplierContact_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO
ALTER TABLE [dbo].[SupplierContactDetails] CHECK CONSTRAINT [FK_SupplierContact_Supplier]
GO
