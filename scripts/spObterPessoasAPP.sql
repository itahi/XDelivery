USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spObterPessoasApp]    Script Date: 27/06/2015 15:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spObterPessoasApp]
as
 BEGIN
  SELECT Codigo,Nome,Telefone FROM Pessoa P
 Where P.Observacao ='Cadastrado via App' 
 END


