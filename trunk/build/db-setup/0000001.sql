IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[log_Activity]') AND type in (N'U'))
DROP TABLE [dbo].[log_Activity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[log_Activity](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Action] [varchar](500) NOT NULL,
	[LogDate] [datetime] NOT NULL
 CONSTRAINT [PK_log_Activity] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_t_Post_t_Blog]') AND parent_object_id = OBJECT_ID(N'[dbo].[t_Post]'))
ALTER TABLE [dbo].[t_Post] DROP CONSTRAINT [FK_t_Post_t_Blog]
GO


/****** Object:  Table [dbo].[t_Blog]    Script Date: 03/14/2009 09:47:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_Blog]') AND type in (N'U'))
DROP TABLE [dbo].[t_Blog]
GO


/****** Object:  Table [dbo].[t_Blog]    Script Date: 03/14/2009 09:47:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlogName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_t_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TRIGGER [dbo].[t_Blog_trg_ins] ON [dbo].[t_Blog]
AFTER INSERT
AS

BEGIN

	SET NOCOUNT ON
	
	INSERT INTO [dbo].[log_Activity] ([Action], [LogDate])
	SELECT 'Blog Added:' + BlogName, GETDATE()
	FROM inserted
	
END

GO




SET ANSI_PADDING OFF
GO






IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_t_Post_t_Blog]') AND parent_object_id = OBJECT_ID(N'[dbo].[t_Post]'))
ALTER TABLE [dbo].[t_Post] DROP CONSTRAINT [FK_t_Post_t_Blog]
GO


/****** Object:  Table [dbo].[t_Post]    Script Date: 03/14/2009 09:47:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_Post]') AND type in (N'U'))
DROP TABLE [dbo].[t_Post]
GO


/****** Object:  Table [dbo].[t_Post]    Script Date: 03/14/2009 09:47:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostTitle] [varchar](255) NOT NULL,
	[BlogId] [int] NOT NULL,
 CONSTRAINT [PK_t_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_Post]  WITH CHECK ADD  CONSTRAINT [FK_t_Post_t_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[t_Blog] ([Id])
GO

ALTER TABLE [dbo].[t_Post] CHECK CONSTRAINT [FK_t_Post_t_Blog]
GO


