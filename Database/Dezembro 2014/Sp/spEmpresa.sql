USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spAlterarEmpresa]    Script Date: 12/10/2014 21:44:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spAlterarEmpresa]
--@Codigo int,
@Nome nvarchar(100),
@CNPJ varchar(14),
@Telefone varchar(20),
@Telefone2 varchar(20),
@Contato nvarchar(50),
@Cep varchar(8),
@Endereco nvarchar(100),
@Cidade nvarchar(50),
@Bairro varchar(50),
@Numero varchar(10),
@UF char(2),
@PontoReferencia nvarchar(max),
@Servidor nvarchar(max),
@Banco nvarchar(max),
@DataInicio datetime,
@VersaoBanco char(2)

as
begin
Update
Empresa set
nome = @Nome,
CNPJ =@CNPJ ,
Telefone=@Telefone ,
Telefone2=@Telefone2,
Contato=@Contato,
Cep=@Cep,
Endereco=@Endereco,
Cidade=@Cidade,
Bairro=@Bairro,
Numero=@Numero,
UF=@UF,
PontoReferencia=@PontoReferencia,
Servidor=@Servidor,
Banco=@Banco,
DataInicio = @DataInicio,
VersaoBanco = @VersaoBanco
end
go
ALTER procedure [dbo].[spAdicionarEmpresa]
@Nome nvarchar(100),
@CNPJ varchar(14),
@Telefone varchar(20),
@Telefone2 varchar(20),
@Contato nvarchar(50),
@Cep varchar(8),
@Endereco nvarchar(100),
@Cidade nvarchar(50),
@Bairro varchar(50),
@Numero varchar(10),
@UF char(2),
@PontoReferencia nvarchar(max),
@Servidor nvarchar(max),
@Banco nvarchar(max),
@DataInicio datetime,
@VersaoBanco char(2)

as 
Insert into Empresa (nome,CNPJ,Telefone ,Telefone2,Contato,Cep,Endereco,Cidade,Bairro,Numero,UF,PontoReferencia,Servidor,Banco,DataInicio,VersaoBanco)
            Values (@Nome,@CNPJ,@Telefone,@Telefone2,@Contato,@Cep,@Endereco,@Cidade,@Bairro,@Numero,@UF,@PontoReferencia,@Servidor,@Banco,@DataInicio,@VersaoBanco)
