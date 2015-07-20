USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorTelefone]    Script Date: 14/06/2014 10:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone int

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END
