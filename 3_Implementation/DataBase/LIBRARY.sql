SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentid] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NULL,
	[studentname] [nvarchar](50) NULL,
	[studentbranch] [nvarchar](50) NULL,
	[studentyear] [nvarchar](50) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[studentid] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BookRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bookid] [nvarchar](50) NOT NULL,
	[bookname] [nvarchar](250) NULL,
	[bookpubname] [nvarchar](50) NULL,
	[bookpubyear] [nvarchar](50) NULL,
	[bookprice] [nvarchar](50) NULL,
	[bookquantity] [nvarchar](50) NULL,
	[recorddate] [nvarchar](50) NULL,
 CONSTRAINT [PK_BookRecord] PRIMARY KEY CLUSTERED 
(
	[bookid] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[statusdetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[statusdetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[statusid] [nchar](10) NOT NULL,
	[statusname] [nvarchar](50) NULL,
 CONSTRAINT [PK_statusdetails] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assign]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Assign](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[assignid]  AS ('AS'+right('000'+CONVERT([varchar](3),[id],(0)),(6))),
	[studentid] [nvarchar](50) NULL,
	[bookid] [nvarchar](50) NULL,
	[assigneddate] [nvarchar](50) NULL,
	[returndate] [nvarchar](50) NULL,
	[penality] [nvarchar](50) NULL,
	[statusid] [nchar](10) NULL,
	[updatestatusdate] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assign_BookRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assign]'))
ALTER TABLE [dbo].[Assign]  WITH CHECK ADD  CONSTRAINT [FK_Assign_BookRecord] FOREIGN KEY([bookid])
REFERENCES [dbo].[BookRecord] ([bookid])
GO
ALTER TABLE [dbo].[Assign] CHECK CONSTRAINT [FK_Assign_BookRecord]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Assign_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Assign]'))
ALTER TABLE [dbo].[Assign]  WITH CHECK ADD  CONSTRAINT [FK_Assign_Student] FOREIGN KEY([studentid])
REFERENCES [dbo].[Student] ([studentid])
GO
ALTER TABLE [dbo].[Assign] CHECK CONSTRAINT [FK_Assign_Student]
