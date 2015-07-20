USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorCodigo]    Script Date: 14/06/2014 12:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,Telefone2
  FROM Pessoa WHERE Codigo = @Codigo
 END



