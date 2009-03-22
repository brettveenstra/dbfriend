
/****** Object:  UserDefinedFunction [dbo].[fnt_GetPosts]    Script Date: 03/15/2009 19:36:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnt_GetPosts]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnt_GetPosts]
GO


/****** Object:  UserDefinedFunction [dbo].[fnt_GetPosts]    Script Date: 03/15/2009 19:36:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnt_GetPosts]()
RETURNS TABLE
AS
	RETURN(

		SELECT p.Id,
			   p.PostTitle,
			   p.BlogId
		FROM t_Post p
	)


GO






/****** Object:  UserDefinedFunction [dbo].[fnc_SomeScalar]    Script Date: 03/15/2009 19:36:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnc_SomeScalar]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnc_SomeScalar]
GO


/****** Object:  UserDefinedFunction [dbo].[fnc_SomeScalar]    Script Date: 03/15/2009 19:36:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnc_SomeScalar]()
RETURNS INT
AS
BEGIN

	DECLARE @RETURN	INT;
	
	SET @RETURN = 1024;
	
	
	RETURN(@RETURN);

END

GO


