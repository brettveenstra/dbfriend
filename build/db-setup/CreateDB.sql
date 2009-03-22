USE [master]
GO


IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'dbFriend_IntegrationTest') BEGIN

	ALTER DATABASE [dbFriend_IntegrationTest]
	SET SINGLE_USER
	WITH ROLLBACK IMMEDIATE;

	
	DROP DATABASE [dbFriend_IntegrationTest]

END

GO


CREATE DATABASE [dbFriend_IntegrationTest]
GO


EXEC sp_dbcmptlevel [dbFriend_IntegrationTest], 90
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbFriend_IntegrationTest].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET ARITHABORT OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET  DISABLE_BROKER 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET  READ_WRITE 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET  MULTI_USER 
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [dbFriend_IntegrationTest] SET DB_CHAINING OFF 
GO

