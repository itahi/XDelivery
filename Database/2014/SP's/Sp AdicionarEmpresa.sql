USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEmpresa]    Script Date: 13/07/2014 16:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
@Banco nvarchar(max)

as 
Insert into Empresa (nome,CNPJ,Telefone ,Telefone2,Contato,Cep,Endereco,Cidade,Bairro,Numero,UF,PontoReferencia,Servidor,Banco)
            Values (@Nome,@CNPJ,@Telefone,@Telefone2,@Contato,@Cep,@Endereco,@Cidade,@Bairro,@Numero,@UF,@PontoReferencia,@Servidor,@Banco)





