-- Create database owner: c3o_web_elmah
--USE [master]
--CREATE LOGIN [IIS APPPOOL\c3o_web_elmah] FROM WINDOWS WITH DEFAULT_DATABASE=[c3o_logger_acme], DEFAULT_LANGUAGE=[us_english]
USE [c3o_logger_acme]
create USER [IIS APPPOOL\c3o_web_elmah] FOR LOGIN [IIS APPPOOL\c3o_web_elmah]
exec sp_addrolemember 'db_owner', 'IIS APPPOOL\c3o_web_elmah'
