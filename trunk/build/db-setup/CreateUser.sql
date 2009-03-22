USE [master]
GO


/****** Object:  Login [app_dbfriend]    Script Date: 03/21/2009 12:24:02 ******/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'app_dbfriend')
DROP LOGIN [app_dbfriend]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [app_dbfriend]    Script Date: 03/21/2009 12:24:02 ******/
CREATE LOGIN [app_dbfriend] WITH PASSWORD=N'hellodbfriend123', DEFAULT_DATABASE=[dbFriend_IntegrationTest], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO




USE [dbFriend_IntegrationTest]
GO

/****** Object:  User [app_dbfriend]    Script Date: 03/21/2009 12:47:56 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'app_dbfriend')
DROP USER [app_dbfriend]
GO

USE [dbFriend_IntegrationTest]
GO

/****** Object:  User [app_dbfriend]    Script Date: 03/21/2009 12:47:56 ******/
GO

CREATE USER [app_dbfriend] FOR LOGIN [app_dbfriend] WITH DEFAULT_SCHEMA=[dbo]
GO


--Need to be able to see all objects
EXEC sp_addrolemember @rolename='db_owner', @membername=N'app_dbfriend'
GO
