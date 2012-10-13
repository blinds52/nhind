/*
   Saturday, Oct 13, 2012
   User: JoeShook@kryptiq.com
   
   Alter domains table to inlcude a agentName identity for dynamic domain support.
*/


USE [$(DBName)]

if not exists (select * from Information_SCHEMA.columns where Table_name='Domains' and column_name='AgentName')
Begin
	ALTER TABLE Domains ADD AgentName varchar(25) NULL
End
