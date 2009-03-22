/****** Object:  View [dbo].[v_BlogPost]    Script Date: 03/15/2009 19:26:15 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_BlogPost]'))
DROP VIEW [dbo].[v_BlogPost]
GO


/****** Object:  View [dbo].[v_BlogPost]    Script Date: 03/15/2009 19:26:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_BlogPost]
AS

	SELECT 
		p.Id,
		p.PostTitle,
		p.BlogId,
		b.BlogName
	FROM t_Blog b
	INNER JOIN t_Post p ON b.Id = p.BlogId


GO


--TODO: Create TRIGGER ON VIEW