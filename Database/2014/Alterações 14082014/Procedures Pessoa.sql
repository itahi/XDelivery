USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spObterPessoas]    Script Date: 14/08/2014 22:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spObterPessoas]
as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,DataNascimento
  FROM Pessoa 
 END

 GO
ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone int

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero,DataNascimento
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END
 GO
ALTER PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,Telefone2,DataNascimento
  FROM Pessoa WHERE Codigo = @Codigo
 END
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
 @Telefone2 int,
 @DataNascimento datetime
 
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
  Telefone2 = @Telefone2,
  DataNascimento = @DataNascimento

  where Codigo = @Codigo
  END
GO
ALTER procedure [dbo].[spAdicionarClienteDelivery]
@Nome nvarchar(100),
@Cep varchar(8),
@Endereco nvarchar(100),
@Numero varchar(10),
@Bairro varchar(50),
@Cidade nvarchar(100),
@UF char(2),
@PontoReferencia nvarchar(max),
@Telefone varchar(20),
@Observacao nvarchar(max),
@Telefone2 varchar(20),
@DataNascimento datetime
as 
begin
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2,DataNascimento)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2,@DataNascimento)


end




