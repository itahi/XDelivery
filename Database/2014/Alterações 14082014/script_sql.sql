
if not EXISTS ( select DataNascimento from Pessoa)
begin
alter table Pessoa add DataNascimento datetime
end
if not Exists(select Tipo from Pedido)
begin
alter table Pedido add Tipo nvarchar(20)
alter table Pedido add NumeroMesa nvarchar(20);
end
if not Exists( select UsaControleMesa,ImprimeViaEntrega from Configuracao)
begin
alter table Configuracao add UsaControleMesa bit;
alter table Configuracao add ImprimeViaEntrega bit default 0;
end
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
Go
ALTER procedure [dbo].[spAlterarConfiguracao]
@cod int,
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega bit

AS 
	Begin
	Update Configuracao set
	ImpViaCozinha = @ImpViaCozinha,
	UsaDataNascimento = @UsaDataNascimento,
	UsaLoginSenha = @UsaLoginSenha,
	QtdCaracteresImp = @QtdCaracteresImp,
	ControlaEntregador = @ControlaEntregador,
	ProdutoPorCodigo = @ProdutoPorCodigo,
	Usa2Telefones   = @Usa2Telefones,
	UsaControleMesa = @UsaControleMesa,
	ImprimeViaEntrega = @ImprimeViaEntrega
	
	where cod=@cod
	end
	GO
ALTER procedure [dbo].[spAdicionarConfiguracao]
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega bit

as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,ImprimeViaEntrega) values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,@ImprimeViaEntrega)
end




