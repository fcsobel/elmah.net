SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE spCreateDatabase
	@name varchar(1000),
	@path varchar(1000) = 'c:\web\c3o\elmah.net\sql\'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @sql varchar(2000)
	
	select @sql = 'CREATE DATABASE [' + @name + '] ON  PRIMARY ' 
					+	'(	NAME = N''' + @name + '''' 
					+ 	 ', FILENAME = N''' + @path + @name + '.mdf'''
					+	 ', SIZE = 2048KB, MAXSIZE = 10MB, FILEGROWTH = 1024KB '
					+	')'
					+	'LOG ON '
					+	'( NAME = N''' + @name + '_log'''
					+ 	', FILENAME = N''' + @path + @name + '_log.ldf'''
					+	', SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%'
					+	');';
		
	EXEC(@sql);
	
	-- settings	
	EXEC('ALTER DATABASE [' + @name + '] SET COMPATIBILITY_LEVEL = 100;');
	EXEC('ALTER DATABASE [' + @name + '] SET ANSI_NULL_DEFAULT OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET ANSI_NULLS OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET ANSI_PADDING OFF ');
	EXEC('ALTER DATABASE [' + @name + '] SET ANSI_WARNINGS OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET ARITHABORT OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET AUTO_CLOSE OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET AUTO_CREATE_STATISTICS ON ;');
	EXEC('ALTER DATABASE [' + @name + '] SET AUTO_SHRINK OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET AUTO_UPDATE_STATISTICS ON ;');
	EXEC('ALTER DATABASE [' + @name + '] SET CURSOR_CLOSE_ON_COMMIT OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET CURSOR_DEFAULT  GLOBAL ;');
	EXEC('ALTER DATABASE [' + @name + '] SET CONCAT_NULL_YIELDS_NULL OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET NUMERIC_ROUNDABORT OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET QUOTED_IDENTIFIER OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET RECURSIVE_TRIGGERS OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET DISABLE_BROKER ;');
	EXEC('ALTER DATABASE [' + @name + '] SET AUTO_UPDATE_STATISTICS_ASYNC OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET DATE_CORRELATION_OPTIMIZATION OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET TRUSTWORTHY OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET ALLOW_SNAPSHOT_ISOLATION OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET PARAMETERIZATION SIMPLE ;');
	EXEC('ALTER DATABASE [' + @name + '] SET READ_COMMITTED_SNAPSHOT OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET HONOR_BROKER_PRIORITY OFF ;');
	EXEC('ALTER DATABASE [' + @name + '] SET READ_WRITE ;');
	EXEC('ALTER DATABASE [' + @name + '] SET RECOVERY SIMPLE ;');
	EXEC('ALTER DATABASE [' + @name + '] SET MULTI_USER ;');
	EXEC('ALTER DATABASE [' + @name + '] SET PAGE_VERIFY CHECKSUM  ;');
	EXEC('ALTER DATABASE [' + @name + '] SET DB_CHAINING OFF ;');
    
	---- run this to grant access
	--EXEC('USE ['+ @name + '];'
	--	+ 'create USER [IIS APPPOOL\c3o_web_elmah] FOR LOGIN [IIS APPPOOL\c3o_web_elmah]'
	--	+ 'exec sp_addrolemember ''db_owner'', ''IIS APPPOOL\c3o_web_elmah'';');
	
END
GO
