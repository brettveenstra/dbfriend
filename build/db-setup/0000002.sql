/****** Object:  StoredProcedure [dbo].[p_Blog_GetAll]    Script Date: 03/15/2009 04:04:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[p_Blog_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[p_Blog_GetAll]
GO


/****** Object:  StoredProcedure [dbo].[p_Blog_GetAll]    Script Date: 03/15/2009 04:04:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[p_Blog_GetAll]
AS
BEGIN

	SELECT * FROM dbo.t_Blog
	
END
GO


GRANT EXECUTE ON [dbo].[p_Blog_GetAll] TO [app_dbfriend]