USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spAlterarPessoa]    Script Date: 14/06/2014 11:22:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAlterarPessoa]

 @Codigo int,
 @Nome nvarchar(100),
 @Cep nvarchar(max),
 @Endereco nvarchar(100),
 @Numero int,
 @Bairro nvarchar(100),
 @Cidade nvarchar(100),
 @Uf char(2),
 @PontoReferencia nvarchar(200),
 @Observacao nvarchar(max),
 @Telefone int,
 @Telefone2 int
 
AS
 BEGIN
  UPDATE Pessoa

  SET   
  Nome = @Nome,
  Cep = @Cep,
  Endereco=@Endereco,
  Numero = @Numero,
  Bairro = @Bairro,
  Cidade = @Cidade,
  Uf = @UF,
  PontoReferencia = @PontoReferencia,
  Observacao = @Observacao,
  Telefone = @Telefone,
  Telefone2 = @Telefone2

  where Codigo = @Codigo
  END





