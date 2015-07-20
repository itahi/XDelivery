USE [DBExpert_Teste]
GO
/****** Object:  User [digital]    Script Date: 07/04/2015 20:16:22 ******/
CREATE USER [digital] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  StoredProcedure [dbo].[AlteraFidelidade]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AlteraFidelidade]
@CodPessoa int ,
@Tickect int
as 
Begin Update Pessoa
set 
TicketFidelidade =TicketFidelidade +@Tickect
where
Codigo = @CodPessoa
end

GO
/****** Object:  StoredProcedure [dbo].[ObterFidelidade]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ObterFidelidade]
@CodPessoa int ,
@Tickect int
as 
begin
select TicketFidelidade from Pessoa
where
Codigo = @CodPessoa
end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarCep]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarCep]
		@Cep int,
		@Logradouro nvarchar(100),
		@Bairro nvarchar(100),
		@Cidade nvarchar(100),
		@Estado nvarchar(100)
	as
		INSERT INTO base_cep(cep,logradouro,bairro,cidade,estado)
					VALUES(@Cep,@Logradouro,@Bairro,@Cidade,@Estado)

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarClienteDelivery]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarClienteDelivery]
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
@DataNascimento datetime,
@TicketFidelidade int 
as 
begin
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2,DataNascimento,TicketFidelidade)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2,@DataNascimento,@TicketFidelidade)


end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarConfiguracao]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionarConfiguracao]
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega bit,
@ControlaFidelidade bit,
@PedidosParaFidelidade int ,
@DescontoDiaSemana bit,
@PrevisaoEntrega nvarchar(10),
@PrevisaoEntregaSN bit,
@CobraTaxaGarcon bit,
@TamanhoFont nvarchar(10),
@ImpLPT bit,
@PortaLPT  nvarchar(10),
@EnviaSMS bit,
@ViasEntrega char(2),
@ViasCozinha char(2),
@ViasBalcao char(2)

as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,
                          ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,
						  ImprimeViaEntrega,ControlaFidelidade,PedidosParaFidelidade,DescontoDiaSemana,
						  PrevisaoEntrega,PrevisaoEntregaSN,CobraTaxaGarcon ,TamanhoFont,ImpLPT,PortaLPT,EnviaSMS,
						  ViasEntrega,ViasCozinha,ViasBalcao)
						  values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,
							@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,
							@ImprimeViaEntrega,@ControlaFidelidade,@PedidosParaFidelidade,@DescontoDiaSemana,
							@PrevisaoEntrega,@PrevisaoEntregaSN,@CobraTaxaGarcon,@TamanhoFont,@ImpLPT,@PortaLPT,@EnviaSMS,
							@ViasEntrega,@ViasCozinha,@ViasBalcao)
end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarDescontoSemana]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[spAdicionarDescontoSemana]
@CodProduto int,
@DiaSemana  nvarchar(100),
@PrecoComDesconto decimal(5,2)
as 
BEGIN 
update Produto 
set
 DiaSemana = @DiaSemana,
 PrecoDesconto = @PrecoComDesconto
       
where Codigo = @CodProduto
end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEmpresa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarEmpresa]
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

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEntregador]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarEntregador]

	@Nome nvarchar(50),
	@Comissao numeric(5,2)
	
as
	BEGIN
		INSERT INTO Entregador(Nome,Comissao)
		Values(@Nome,@Comissao)
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarFormaPagamento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarFormaPagamento]
@Descricao nvarchar(100)


as
begin 
Insert into FormaPagamento(Descricao)
            Values (@Descricao)

end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarGrupo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarGrupo]

	@NomeGrupo nvarchar(50)
	
as
	BEGIN
		INSERT INTO Grupo(NomeGrupo)
		Values(@NomeGrupo)
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarItemAoPedido]
	@CodPedido int,
	@CodProduto int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)	
as
	BEGIN
		INSERT INTO ItemsPedido(CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item)
		VALUES(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item)
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemExtra]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarItemExtra]
		@CodPedido int,
		@Descricao nvarchar(max),
		@Valor decimal(10,2)
	AS
		INSERT INTO ItemsExtras(CodPedido,Descricao,Valor) Values(@CodPedido,@Descricao,@Valor)

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemVenda]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarItemVenda]

@CodVenda int,
@NomeProduto varchar(100),
@CodBarProduto varchar(13),
@Quantidade decimal(10,2),
@PrecoUnitario decimal(10,2),
@PrecoTotalItem decimal(10,2)
AS
INSERT INTO ItemsVenda
(
CodVenda,
NomeProduto,
CodBarProduto,
Quantidade,
PrecoUnitario,
PrecoTotalItem
)
VALUES
(
@CodVenda,
@NomeProduto,
@CodBarProduto,
@Quantidade,
@PrecoUnitario,
@PrecoTotalItem
)

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarMensagen]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------
create procedure [dbo].[spAdicionarMensagen]
@Tipo char(2),
@Conteudo nvarchar(150)
as
begin
insert into Mensagens (Conteudo,Tipo) values (@Conteudo,@Tipo)
end
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarPedido]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@TotalPedido decimal(10,2),
	@TrocoPara nvarchar(max),
	@FormaPagamento nvarchar(100),
	@RealizadoEm   datetime,
	@Tipo nvarchar(100),
	@NumeroMesa nvarchar(20),
	@Status     nvarchar(20)
as
		BEGIN
			INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,[Status])
			Values(
				@CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,@Status
			);
			SET @Codigo = SCOPE_IDENTITY()
			RETURN @Codigo
		END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarPessoa]
		@Nome nvarchar(255),
		@Telefone int,
		@Cep int,
		@Endereco nvarchar(100),
		@Numero int,
		@Bairro nvarchar(100),
		@Cidade nvarchar(100),
		@Uf char(2),
		@PontoReferencia nvarchar(200),
		@Observacao nvarchar(max)
	as
		INSERT INTO Pessoa(Nome,Cep,Telefone,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Observacao)
					VALUES(@Nome,@Cep,@Telefone,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Observacao)

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarProduto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarProduto]
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@DiaSemana nvarchar(100),
	@PrecoDesconto decimal(5,2)
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,DiaSemana,PrecoDesconto)
		Values(
			@Nome,
			@Descricao,
			@Preco,
			@GrupoProduto,
			@DiaSemana,
			@PrecoDesconto
		)
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuario]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarUsuario]
@Nome nvarchar(max),
@Senha nvarchar(128),
@CancelaPedidosSN bit,
@AlteraProdutosSN bit,
@AdministradorSN bit,
@AcessaRelatoriosSN bit
as begin

insert into Usuario (Nome,Senha,CancelaPedidosSN,AlteraProdutosSN,AdministradorSN,AcessaRelatoriosSN) values
					(@Nome,@Senha,@CancelaPedidosSN,@AlteraProdutosSN,@AdministradorSN,@AcessaRelatoriosSN)
  end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuarioDefault]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarUsuarioDefault]
@Nome nvarchar(max),
@Senha nvarchar(128)

as begin

insert into Usuario (Nome,Senha) values
					(@Nome,@Senha)
  end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarVenda]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarVenda]
@Codigo int output,
@codigoPessoa int,
@ValorDaVenda decimal(12,2),
@DataVenda datetime
AS
INSERT INTO Venda
(
CodPessoa,
ValorVenda,
DataVenda
)
VALUES
(
@codigoPessoa,
@ValorDaVenda,
@DataVenda
)
SET @Codigo = SCOPE_IDENTITY()
return @Codigo

GO
/****** Object:  StoredProcedure [dbo].[spAltera]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[spAltera]

 @VersaoBanco varchar(1),
 @DataInicio Datetime
AS
 BEGIN
  UPDATE Empresa

  SET   
  VersaoBanco=@VersaoBanco,
  DataInicio = @DataInicio
  
END

GO
/****** Object:  StoredProcedure [dbo].[spAlteraFidelidade]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraFidelidade]
@CodPessoa int ,
@Ticket int
as 
Begin Update Pessoa
set 
TicketFidelidade =TicketFidelidade +@Ticket
where
Codigo = @CodPessoa
end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarConfiguracao]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarConfiguracao]
@cod int,
@ImpViaCozinha bit,
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo bit,
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega bit,
@ControlaFidelidade bit,
@PedidosParaFidelidade int ,
@DescontoDiaSemana bit,
@PrevisaoEntrega nvarchar(10),
@PrevisaoEntregaSN bit,
@CobraTaxaGarcon bit,
@TamanhoFont nvarchar(10),
@ImpLPT bit,
@PortaLPT nvarchar(10),
@EnviaSMS bit,
@ViasEntrega char(2),
@ViasCozinha char(2),
@ViasBalcao char(2)
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
	ImprimeViaEntrega = @ImprimeViaEntrega,
	ControlaFidelidade=@ControlaFidelidade,
	PedidosParaFidelidade  =@PedidosParaFidelidade ,
	DescontoDiaSemana = @DescontoDiaSemana,
	PrevisaoEntrega  = @PrevisaoEntrega,
    PrevisaoEntregaSN= @PrevisaoEntregaSN,
    CobraTaxaGarcon = @CobraTaxaGarcon,
    TamanhoFont =@TamanhoFont,
    ImpLPT= @ImpLPT ,
    PortaLPT = @PortaLPT,
    EnviaSMS =@EnviaSMS,
    ViasEntrega= @ViasEntrega,
    ViasCozinha= @ViasCozinha ,
    ViasBalcao=  @ViasBalcao 
	where cod=@cod
	end
GO
/****** Object:  StoredProcedure [dbo].[spAlterarEmpresa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarEmpresa]
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

GO
/****** Object:  StoredProcedure [dbo].[spAlterarEntregador]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spAlterarEntregador]
@Codigo int,
@Nome nvarchar(100),
@Comissao decimal(5,2)


as
begin
Update
Entregador set
nome = @Nome,
Comissao =@Comissao 
where Codigo=@Codigo
end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarFormaPagamento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarFormaPagamento]
@Codigo int,
@Descricao nvarchar(100) 

as 
begin
update FormaPagamento set Descricao=@Descricao 
        where Codigo=@Codigo
end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarGrupo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50)

AS
	BEGIN
		UPDATE Grupo

		SET NomeGrupo = @NomeGrupo

		WHERE Codigo = @Codigo;
	END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarItemExtraPorCodigo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarItemExtraPorCodigo]
		@Codigo int,
		@Descricao nvarchar(max),
		@Valor decimal(10,2)
	AS
		UPDATE ItemsExtras SET Descricao = @Descricao, Valor = @Valor WHERE Codigo = @Codigo

GO
/****** Object:  StoredProcedure [dbo].[spAlterarItemPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarItemPedido]

	@CodProduto int,
	@CodPedido int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
AS
	BEGIN
		UPDATE ItemsPedido

		SET 
		NomeProduto = @NomeProduto,
		Quantidade = @Quantidade,
		PrecoItem = @PrecoUnitario,
		PrecoTotalItem = @PrecoTotal,
		Item = @Item
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido ; -- Código Pedido
	END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarMensagens]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarMensagens]
@Codigo int,
@Conteudo  nvarchar(150)
as 
begin
update Mensagens set Conteudo=@Conteudo
where Codigo=@Codigo
end
GO
/****** Object:  StoredProcedure [dbo].[spAlterarPessoa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarPessoa]

 @Codigo int,
 @Nome nvarchar(100),
 @Cep nvarchar(max),
 @Endereco nvarchar(100),
 @Numero nvarchar(20),
 @Bairro nvarchar(100),
 @Cidade nvarchar(100),
 @Uf char(2),
 @PontoReferencia nvarchar(200),
 @Observacao nvarchar(max),
 @Telefone nvarchar(20),
 @Telefone2 nvarchar(20),
 @DataNascimento datetime,
 @TicketFidelidade int 
 
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
  DataNascimento = @DataNascimento,
  TicketFidelidade= @TicketFidelidade 
  
  where Codigo=@Codigo
END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarProduto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarProduto]

	@Codigo int,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@DiaSemana nvarchar(100),
	@PrecoDesconto decimal(5,2)

AS
	BEGIN
		UPDATE Produto

		SET 
		
		NomeProduto = @Nome,
		DescricaoProduto = @Descricao,
		PrecoProduto = @Preco,
		GrupoProduto = @GrupoProduto,
		DiaSemana = @DiaSemana,
		PrecoDesconto = @PrecoDesconto

		WHERE Codigo = @Codigo;
	END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarTotalPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2),
	@Tipo nvarchar(20),
	@NumeroMesa nvarchar(20)

AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido,
		Tipo = @Tipo,
		NumeroMesa = @NumeroMesa

		WHERE 
			Codigo = @Codigo --Codigo Produto
	END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarTrocoParaFormaPagamento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarTrocoParaFormaPagamento]
		@Codigo int,
		@TrocoPara nvarchar(max),
		@FormaPagamento nvarchar(max)	
	AS
		Update Pedido SET TrocoPara = @TrocoPara, FormaPagamento = @FormaPagamento WHERE Codigo = @Codigo

GO
/****** Object:  StoredProcedure [dbo].[spAlterarUsuario]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarUsuario]
@codigo int,
@Nome nvarchar(max),
@Senha nvarchar(128),
@CancelaPedidosSN bit,
@AlteraProdutosSN bit,
@AdministradorSN bit,
@AcessaRelatoriosSN bit

as begin
update Usuario set 
Nome=@Nome,
Senha = @Senha,
CancelaPedidosSN=@CancelaPedidosSN ,
AlteraProdutosSN= @AlteraProdutosSN ,
AdministradorSN= @AdministradorSN ,
AcessaRelatoriosSN = @AcessaRelatoriosSN
where cod=@Codigo
   END

GO
/****** Object:  StoredProcedure [dbo].[spCancelarPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCancelarPedido]

 @Codigo int,
 @Status nvarchar(100),
 @RealizadoEm Datetime
AS
 BEGIN
  UPDATE Pedido

  SET   
  [status] = @Status,
  RealizadoEm = @RealizadoEm
  


  where Codigo = @Codigo
END

GO
/****** Object:  StoredProcedure [dbo].[spCriarPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCriarPedido]
@CodPedido int,
@CodProduto int,
@NomeProduto nvarchar(max),
@Quantidade int,
@PrecoUnitario decimal(10,2),
@PrecoTotal decimal(10,2),
@Item nvarchar(max)
AS
INSERT INTO dbo.ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item)
					  Values(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item)

GO
/****** Object:  StoredProcedure [dbo].[spExcluirCliente]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spExcluirCliente]

	@CodCliente int

AS
	BEGIN
		DELETE FROM 
			Pessoa
		WHERE 
			Codigo = @CodCliente --Codigo Grupo
	END

GO
/****** Object:  StoredProcedure [dbo].[spExcluirGrupo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirGrupo]

	@Codigo int

AS
	BEGIN
		DELETE FROM 
			Grupo
		WHERE 
			Codigo = @Codigo --Codigo Grupo
	END

GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemExtraPorCodigo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirItemExtraPorCodigo]
		@Codigo int
	AS
		DELETE FROM ItemsExtras WHERE Codigo = @Codigo

GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirItemPedido]

	@CodProduto int,
	@CodPedido int

AS
	BEGIN
		DELETE FROM 
			ItemsPedido
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido ; -- Código Pedido
	END

GO
/****** Object:  StoredProcedure [dbo].[spExcluirProduto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirProduto]

	@Codigo int

AS
	BEGIN
		DELETE FROM 
			Produto
		WHERE 
			Codigo = @Codigo --Codigo Produto
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterAnivesariantes]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterAnivesariantes]
@DataInicial datetime,
@DataFinal datetime
as
 BEGIN
  SELECT Telefone,Nome
  FROM Pessoa 
  where DataNascimento BETWEEN @DataInicial and @DataFinal
 END
GO
/****** Object:  StoredProcedure [dbo].[spObterClientesSemPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterClientesSemPedido]
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo , Telefone from Pessoa PE left join Pedido PD on PD.CodPessoa=PE.Codigo 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
end


GO
/****** Object:  StoredProcedure [dbo].[spObterConfiguracao]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterConfiguracao]


as 


select isnull(cod,0) as cod ,
isnull(ImpViaCozinha,0) as ImpViaCozinha ,
isnull(UsaDataNascimento,0) as UsaDataNascimento 
,isnull(UsaLoginSenha,0) as UsaLoginSenha ,
isnull(QtdCaracteresImp,0) as QtdCaracteresImp ,
isnull(ControlaEntregador,0) as ControlaEntregador
 ,isnull(ProdutoPorCodigo,0) as ProdutoPorCodigo ,
isnull(Usa2Telefones,0) as Usa2Telefones ,
isnull(ImprimiSN,0) as ImprimiSN 
 ,isnull(UsaControleMesa,0) as UsaControleMesa
 ,isnull(ImprimeViaEntrega,0) as ImprimeViaEntrega
 ,isnull(ControlaFidelidade,0) as ControlaFidelidade
 ,isnull(PedidosParaFidelidade,0) as PedidosParaFidelidade
 ,isnull(DescontoDiaSemana,0) as DescontoDiaSemana
 ,isnull(PrevisaoEntregaSN,0) as PrevisaoEntregaSN
 -------
 ,isnull(PrevisaoEntrega,0) as PrevisaoEntrega
 ,isnull(ImpressoraCozinha,0) as ImpressoraCozinha
 ,isnull(ImpressoraEntrega,0) as ImpressoraEntrega
 ,isnull(ImpressoraCopaBalcao,0) as ImpressoraCopaBalcao
 ,isnull(CobraTaxaGarcon,0) as CobraTaxaGarcon
 ,isnull(TamanhoFont,0) as TamanhoFont
 -----
 ,isnull(ImpLPT,0) as ImpLPT
 ,isnull(PortaLPT,0) as PortaLPT
 ,isnull(EnviaSMS,0) as EnviaSMS
 ,isnull(ViasEntrega,0) as ViasEntrega
 ,isnull(ViasCozinha,0) as ViasCozinha
 ,isnull(ViasBalcao,0) as ViasBalcao
 
 from Configuracao


GO
/****** Object:  StoredProcedure [dbo].[spObterEmpresa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterEmpresa]


as 

Select * from Empresa

GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPorCep]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterEnderecoPorCep]
		@Cep int
	as
		SELECT logradouro,bairro,Cidade,estado
		FROM base_cep WHERE cep = @Cep

GO
/****** Object:  StoredProcedure [dbo].[spObterEntregador]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterEntregador]
as
 BEGIN
  SELECT * 
  FROM Entregador 
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterEntregadores]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterEntregadores]
as
Begin

select * from Entregador

end

GO
/****** Object:  StoredProcedure [dbo].[spObterFormaPagamento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterFormaPagamento]
as 
begin
select Codigo,Descricao from FormaPagamento
end

GO
/****** Object:  StoredProcedure [dbo].[spObterGrupo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterGrupo]
	as
		SELECT Codigo,NomeGrupo 
			FROM Grupo 
		ORDER BY NomeGrupo ASC

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsExtrasPorPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsExtrasPorPedido]
		@CodPedido int
	AS
		SELECT CodPedido,Descricao,Valor FROM ItemsExtras WHERE CodPedido = @CodPedido

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsPedido]
	@Codigo int	
as
	BEGIN
		SELECT *
		FROM ItemsPedido WHERE CodPedido = @Codigo ORDER BY Codigo DESC
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsVendidos]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterItemsVendidos]
@DataInicio date,
@DataFim    date
as
	BEGIN
	SELECT CodProduto,NomeProduto,Sum(Quantidade)Quantidade,PrecoItem
  FROM ItemsPedido ITPE 
       join Pedido Pe on Pe.Codigo=ITPE.CodPedido
      
  where Pe.RealizadoEm  between @DataInicio and @DataFim and Pe.Finalizado=1
  group by CodProduto,NomeProduto,PrecoItem
     
 -- group by CodProduto,NomeProduto,RealizadoEm,Quantidade,PrecoItem
	END
	
	--select * from Pedido where RealizadoEm between '17-09-2014 20:32:55.347' and '17-09-2014 20:32:55.347'
	
	


GO
/****** Object:  StoredProcedure [dbo].[spObterMensages]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterMensages]
as 
begin
select * from Mensagens
end

GO
/****** Object:  StoredProcedure [dbo].[spObterNomeProduto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterNomeProduto]
	@Codigo int
as
	SELECT NomeProduto
	FROM Produto WHERE Codigo = @Codigo;

GO
/****** Object:  StoredProcedure [dbo].[spObterPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT P.Nome ,Pe.* 
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoCancelado]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterPedidoCancelado]
as
	BEGIN
		select * from Pedido
		end

GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoFinalizado]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterPedidoFinalizado]
as
	BEGIN
		SELECT *
		FROM Pedido
		
	  WHERE Finalizado = 1 ORDER BY Codigo DESC
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorData]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[spObterPedidoPorData]
@DataInicio Date,
@DataFim date
as
	BEGIN
		SELECT *
		FROM Pedido
		
	  WHERE Finalizado = 1  and RealizadoEm between @DataInicio and @DataFim  ORDER BY Codigo DESC
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoSemMesas]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterPedidoSemMesas]
as
	BEGIN
		SELECT pe.Codigo,P.Nome, CodPessoa,TotalPedido,TrocoPara,FormaPagamento,Finalizado,RealizadoEm
		FROM Pedido pe 
		join Pessoa P on P.Codigo= pe.CodPessoa 
		WHERE Finalizado = 0 ORDER BY pe.Codigo DESC
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorCodigo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,Telefone2,DataNascimento,TicketFidelidade
  FROM Pessoa WHERE Codigo = @Codigo
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorTelefone]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone nvarchar(100)

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero,DataNascimento
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoas]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoas]
as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,DataNascimento
  FROM Pessoa 
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterProduto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProduto]
as
BEGIN
SELECT *
FROM Produto ORDER BY Codigo ASC
END

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoCompleto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana
	FROM Produto WHERE Codigo = @Codigo;

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodigo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto
	FROM Produto WHERE Codigo = @Codigo;

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorGrupo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoPorGrupo]
	@GrupoProduto nvarchar(50)
as
	SELECT Codigo,NomeProduto,PrecoProduto
	FROM Produto WHERE GrupoProduto = @GrupoProduto;

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoSemDesconto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto
FROM Produto ORDER BY Codigo ASC
END

GO
/****** Object:  StoredProcedure [dbo].[spObterUsuario]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterUsuario]
as begin
select * from Usuario
end

GO
/****** Object:  StoredProcedure [dbo].[spObterViasDoPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterViasDoPedido]
	@CodPessoa int,
	@CodPedido int	
AS
	SELECT
		P.Nome,P.Endereco,P.Numero,P.Complemento,P.Telefone,P.Observacao,
		ITP.CodPedido,ITP.NomeProduto,ITP.Quantidade
	FROM 
		Pessoa P,
		ItemsPedido ITP
	INNER JOIN Pedido ON Pedido.Codigo = ITP.CodPedido
	INNER JOIN Pessoa ON Pessoa.Codigo = Pedido.CodPessoa
	WHERE Pedido.Codigo = @CodPedido and Pessoa.Codigo = @CodPessoa

GO
/****** Object:  StoredProcedure [dbo].[spSinalizarPedidoConcluido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int	
AS
	Update Pedido SET Finalizado = 1 WHERE Codigo = @Codigo

GO
/****** Object:  StoredProcedure [dbo].[spZerarFidelidade]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spZerarFidelidade]
@CodPessoa int,
@Ticket int
as 
begin
Update Pessoa 
set 
TicketFidelidade = @Ticket
where 
Codigo=@CodPessoa
end

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](255) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACADCLIE]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACADCLIE](
	[CODIGO] [nvarchar](255) NULL,
	[NOME] [nvarchar](255) NULL,
	[ENDERECO] [nvarchar](255) NULL,
	[BAIRRO] [nvarchar](255) NULL,
	[CIDADE] [nvarchar](255) NULL,
	[UF] [nvarchar](255) NULL,
	[CEP] [nvarchar](255) NULL,
	[CX_POSTAL] [nvarchar](255) NULL,
	[FONE] [nvarchar](255) NULL,
	[FAX] [nvarchar](255) NULL,
	[CONTATO] [nvarchar](255) NULL,
	[CGC] [nvarchar](255) NULL,
	[INSCRICAO] [nvarchar](255) NULL,
	[PRACA] [nvarchar](255) NULL,
	[DATA_FICHA] [datetime] NULL,
	[CI] [nvarchar](255) NULL,
	[CPF] [nvarchar](255) NULL,
	[TIT_ELEITOR] [nvarchar](255) NULL,
	[DATA_NASC] [datetime] NULL,
	[SEXO] [nvarchar](255) NULL,
	[EST_CIVIL] [nvarchar](255) NULL,
	[NATURAL] [nvarchar](255) NULL,
	[FILIACAO] [nvarchar](255) NULL,
	[NUM_DEPEND] [int] NULL,
	[CONJUGE] [nvarchar](255) NULL,
	[NASC_CONJ] [datetime] NULL,
	[ALUGUEL] [nvarchar](255) NULL,
	[VAL_ALUGUEL] [nvarchar](255) NULL,
	[TEMP_RESID] [nvarchar](255) NULL,
	[EMPRESA] [nvarchar](255) NULL,
	[FONE_EMPR] [nvarchar](255) NULL,
	[ADMISSAO] [datetime] NULL,
	[CEP_EMPR] [nvarchar](255) NULL,
	[CIDA_EMPR] [nvarchar](255) NULL,
	[UF_EMPR] [nvarchar](255) NULL,
	[FUNCAO] [nvarchar](255) NULL,
	[EMPR_ANT] [nvarchar](255) NULL,
	[TEMP_SERV] [nvarchar](255) NULL,
	[REF_COML] [nvarchar](255) NULL,
	[REF_BANC] [nvarchar](255) NULL,
	[CONS_SPC] [datetime] NULL,
	[OBS_SPC] [nvarchar](255) NULL,
	[TIPO_ATIV] [nvarchar](255) NULL,
	[OBS_GERAL] [nvarchar](255) NULL,
	[VENDEDOR] [nvarchar](255) NULL,
	[COBR_ENDER] [nvarchar](255) NULL,
	[COBR_BAIRR] [nvarchar](255) NULL,
	[COBR_CIDAD] [nvarchar](255) NULL,
	[COBR_UF] [nvarchar](255) NULL,
	[COBR_CEP] [nvarchar](255) NULL,
	[ICM_IPI] [nvarchar](255) NULL,
	[EMAIL] [nvarchar](255) NULL,
	[COBR_EMAIL] [nvarchar](255) NULL,
	[DATA_M] [datetime] NULL,
	[REF_PESSOAL1] [nvarchar](255) NULL,
	[REF_PESSOAL2] [nvarchar](255) NULL,
	[ULTIMA_COMPRA] [datetime] NULL,
	[CONVENIADO] [nvarchar](255) NULL,
	[ATIVO] [nvarchar](255) NULL,
	[Renda] [nvarchar](255) NULL,
	[Renda_Conjuge] [nvarchar](255) NULL,
	[Empresa_conjuge] [nvarchar](255) NULL,
	[Fpagto] [nvarchar](255) NULL,
	[Razao_social] [nvarchar](255) NULL,
	[Ponto_referencia] [nvarchar](255) NULL,
	[Adicionais] [nvarchar](255) NULL,
	[Meta] [nvarchar](255) NULL,
	[Valor] [nvarchar](255) NULL,
	[Desconto] [nvarchar](255) NULL,
	[TBLPRECO] [nvarchar](255) NULL,
	[NUMERO] [nvarchar](255) NULL,
	[COMPLEMENTO] [nvarchar](255) NULL,
	[COD_CIDADE] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[APagar]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APagar](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[DataCompra] [datetime] NOT NULL,
	[DataPagamento] [datetime] NOT NULL,
	[TotalPagamento] [decimal](12, 2) NOT NULL,
	[ValorRestante] [decimal](12, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[APagarParcelas]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APagarParcelas](
	[CodPagar] [int] NULL,
	[Parcela] [int] NULL,
	[ValorPago] [decimal](12, 2) NULL,
	[CodCaixaBanco] [int] NULL,
	[DataPagamento] [date] NULL,
	[TipoPagamento] [char](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AReceber]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AReceber](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[DataVenda] [datetime] NOT NULL,
	[TotalReceber] [decimal](12, 2) NOT NULL,
	[Quitado] [bit] NOT NULL,
	[CodigoMovimentacao] [int] NOT NULL,
	[Descricao] [nvarchar](max) NOT NULL,
	[CodPessoa] [int] NULL,
	[Documento] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AReceberParcelas]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AReceberParcelas](
	[CodReceber] [int] NOT NULL,
	[ValorRecebido] [decimal](12, 2) NOT NULL,
	[DataRecebimento] [datetime] NOT NULL,
	[Parcela] [nvarchar](max) NULL,
	[CodCaixaBanco] [int] NULL,
	[CodClasse] [int] NULL,
	[Tipo] [char](1) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[base_cep]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_cep](
	[id] [int] NOT NULL,
	[cep] [int] NULL,
	[logradouro] [nvarchar](100) NULL,
	[bairro] [nvarchar](100) NULL,
	[cidade] [nvarchar](100) NULL,
	[estado] [nvarchar](2) NULL,
 CONSTRAINT [PK_ID_CEP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[basecepES]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[basecepES](
	[Coluna 0] [varchar](50) NULL,
	[Coluna 1] [varchar](50) NULL,
	[Coluna 2] [nvarchar](100) NULL,
	[Coluna 3] [nvarchar](50) NULL,
	[Coluna 4] [nvarchar](50) NULL,
	[Coluna 5] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CaixaBanco]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CaixaBanco](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[TipoConta] [char](2) NULL,
	[NumeroConta] [varchar](20) NULL,
	[LimiteEspecial] [decimal](12, 2) NULL,
	[NomeCaixaBanco] [nvarchar](100) NULL,
	[CodAgencia] [int] NULL,
	[Caixa] [int] NULL,
	[Tipo] [nvarchar](20) NULL,
	[Operacao] [int] NULL,
	[EmiteBoleto] [bit] NULL,
	[Cedente] [nvarchar](50) NULL,
	[NossoNumero] [nvarchar](50) NULL,
	[CPFCNPJ] [nvarchar](50) NULL,
	[Nome] [nvarchar](50) NULL,
 CONSTRAINT [PK_CAIXA_CODIGO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CaixaMovimento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaixaMovimento](
	[CodCaixaBanco] [int] NOT NULL,
	[CodUsuario] [int] NOT NULL,
	[DataAbertura] [datetime] NULL,
	[DataFechamento] [datetime] NULL,
	[ValorAberturaCalculado] [decimal](12, 2) NULL,
	[ValorFechamentoCalculado] [decimal](12, 2) NULL,
	[ValorAberturaInformado] [decimal](12, 2) NULL,
	[ValorFechamentoInformado] [decimal](12, 2) NULL,
	[Diferenca] [decimal](12, 2) NULL,
	[Sangria] [decimal](12, 2) NULL,
	[Suprimento] [decimal](12, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CentroCusto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentroCusto](
	[NomeCentro] [nvarchar](50) NULL,
	[Situacao] [bit] NULL,
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_COD_Centro] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClasseFinanceira]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClasseFinanceira](
	[Codigo] [nvarchar](14) NOT NULL,
	[NomeClasse] [nvarchar](max) NULL,
	[TipoClasse] [char](1) NULL,
	[Resultado] [bit] NULL,
	[Financeiro] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[clie]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clie](
	[Coluna 2] [varchar](25) NULL,
	[Coluna 7] [varchar](4) NULL,
	[Coluna 8] [int] NULL,
	[Coluna 9] [varchar](4) NULL,
	[Coluna 10] [int] NULL,
	[Coluna 11] [datetime] NULL,
	[Coluna 12] [datetime] NULL,
	[Coluna 13] [varchar](42) NULL,
	[Coluna 14] [varchar](49) NULL,
	[Coluna 15] [varchar](29) NULL,
	[Coluna 16] [varchar](42) NULL,
	[Coluna 17] [varchar](38) NULL,
	[Coluna 18] [varchar](22) NULL,
	[Coluna 19] [varchar](15) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Configuracao]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Configuracao](
	[cod] [int] IDENTITY(1,1) NOT NULL,
	[ImpViaCozinha] [bit] NULL,
	[UsaDataNascimento] [bit] NULL,
	[UsaLoginSenha] [bit] NULL,
	[QtdCaracteresImp] [int] NULL,
	[ControlaEntregador] [bit] NULL,
	[ProdutoPorCodigo] [bit] NULL,
	[Usa2Telefones] [bit] NULL,
	[ImprimiSN] [bit] NULL,
	[UsaControleMesa] [bit] NULL,
	[ImprimeViaEntrega] [bit] NULL,
	[ControlaFidelidade] [bit] NULL,
	[PedidosParaFidelidade] [int] NULL,
	[DescontoDiaSemana] [bit] NULL,
	[PrevisaoEntregaSN] [bit] NULL,
	[PrevisaoEntrega] [nvarchar](10) NULL,
	[ImpressoraCozinha] [nvarchar](100) NULL,
	[ImpressoraEntrega] [nvarchar](100) NULL,
	[ImpressoraCopaBalcao] [nvarchar](100) NULL,
	[CobraTaxaGarcon] [bit] NULL,
	[TamanhoFont] [nvarchar](10) NULL,
	[ImpLPT] [bit] NULL,
	[PortaLPT] [nvarchar](10) NULL,
	[EnviaSMS] [bit] NULL,
	[ViasEntrega] [char](2) NULL,
	[ViasCozinha] [char](2) NULL,
	[ViasBalcao] [char](2) NULL,
	[DataAtualizacao] [datetime] NULL,
 CONSTRAINT [Pk_cod_Config] PRIMARY KEY CLUSTERED 
(
	[cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Empresa](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[CNPJ] [nvarchar](18) NULL,
	[Telefone] [varchar](20) NULL,
	[Telefone2] [varchar](20) NULL,
	[Endereco] [nvarchar](100) NULL,
	[Cep] [varchar](8) NULL,
	[Cidade] [nvarchar](50) NULL,
	[Numero] [varchar](10) NULL,
	[Bairro] [varchar](50) NULL,
	[UF] [char](2) NULL,
	[PontoReferencia] [nvarchar](max) NULL,
	[Banco] [nvarchar](max) NULL,
	[Contato] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Servidor] [nvarchar](max) NULL,
	[strConnectionResult] [bit] NULL,
	[VersaoBanco] [varchar](max) NULL,
	[DataInicio] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Entregador]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entregador](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Comissao] [decimal](5, 2) NULL,
 CONSTRAINT [PK_COD_ENTREGADOR] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estoque]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estoque](
	[CodProduto] [int] NOT NULL,
	[ValorCompra] [decimal](10, 2) NULL,
	[ValorVenda] [decimal](10, 2) NULL,
	[UltimaCompra] [datetime] NULL,
	[QuantidadeEstoque] [decimal](15, 4) NULL,
	[UltimaVenda] [datetime] NULL,
 CONSTRAINT [PK_CodEstoque] PRIMARY KEY CLUSTERED 
(
	[CodProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormaPagamento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormaPagamento](
	[Codigo] [int] IDENTITY(1,2) NOT NULL,
	[Descricao] [nvarchar](max) NULL,
	[ParcelaSN] [bit] NULL,
	[CodClasse] [int] NULL,
	[Parcelas] [int] NULL,
	[ValorMinimo] [decimal](12, 2) NULL,
 CONSTRAINT [PK_CODIGO_FPAGAMENTO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionario](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[CodUsuario] [int] NULL,
	[Tipo] [int] NULL,
 CONSTRAINT [PK_CODIGO_FUNCIONARIO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Grupo]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NomeGrupo] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GrupoPessoas]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrupoPessoas](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[LimiteCredito] [decimal](12, 2) NULL,
	[Comissao] [decimal](12, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GrupoProduto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrupoProduto](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NULL,
	[Comissao] [decimal](12, 2) NULL,
 CONSTRAINT [PK_CODIGO_Grupo] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemsPedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsPedido](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPedido] [int] NULL,
	[CodProduto] [int] NULL,
	[NomeProduto] [nvarchar](max) NULL,
	[Quantidade] [int] NULL,
	[PrecoItem] [decimal](10, 2) NULL,
	[PrecoTotalItem] [decimal](10, 2) NULL,
	[Item] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemsVenda]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsVenda](
	[CodVenda] [int] NOT NULL,
	[CodBarProduto] [int] NOT NULL,
	[Quantidade] [decimal](10, 2) NOT NULL,
	[PrecoUnitario] [decimal](10, 2) NOT NULL,
	[PrecoTotalItem] [decimal](10, 2) NOT NULL,
	[NomeProduto] [nvarchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LicenseKeyGenareted]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseKeyGenareted](
	[codPLK] [int] NULL,
	[chave] [nvarchar](34) NULL,
	[ativa] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mensagens]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mensagens](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [char](2) NULL,
	[Conteudo] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Motorista]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Motorista](
	[Codigo] [int] NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[CNH] [int] NULL,
	[CategoriaCNH] [char](2) NULL,
	[VencimentoCNH] [date] NULL,
	[Cep] [int] NULL,
	[Logradouro] [nvarchar](25) NULL,
	[Endereco] [nvarchar](100) NULL,
	[Complemento] [nvarchar](100) NULL,
	[Numero] [int] NULL,
	[Bairro] [nvarchar](100) NULL,
	[Cidade] [nvarchar](100) NULL,
	[Uf] [char](2) NULL,
	[PontoReferencia] [nvarchar](200) NULL,
	[Observacao] [nvarchar](max) NULL,
	[Telefone] [varchar](20) NULL,
	[Telefone2] [varchar](20) NULL,
 CONSTRAINT [PK_CODIGO_Motorista] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Movimento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimento](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[DataMovimentacao] [datetime] NOT NULL,
	[CodigoPessoa] [int] NULL,
	[CodCondicao] [int] NULL,
	[Total] [decimal](12, 2) NULL,
	[CodCaixaBanco] [int] NULL,
	[DescontoValor] [decimal](18, 0) NULL,
	[DescontoPercent] [decimal](18, 0) NULL,
	[TotalLiquido] [decimal](12, 2) NULL,
 CONSTRAINT [PK_Movimentacao_Codigo] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MovimentoItems]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimentoItems](
	[CodMovimento] [int] NOT NULL,
	[CodProduto] [int] NOT NULL,
	[ValorUnitario] [decimal](12, 2) NULL,
	[Quantidade] [decimal](10, 2) NULL,
	[ValorTotal] [decimal](12, 2) NULL,
	[DescontoItemPerct] [decimal](12, 2) NULL,
	[DescontoItem] [decimal](12, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orcamento]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orcamento](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NOT NULL,
	[CodFuncionario] [int] NOT NULL,
	[DataOrcamento] [date] NULL,
	[DataValidade] [date] NULL,
	[ValorProdutos] [decimal](12, 2) NULL,
	[ValorServicos] [decimal](12, 2) NULL,
	[ValorTotal] [decimal](12, 2) NULL,
	[Descricao] [nvarchar](max) NULL,
	[Status] [nvarchar](100) NULL,
	[Desconto] [decimal](12, 2) NULL,
	[CodVendedor] [int] NULL,
 CONSTRAINT [PK_CODIGO_ORCAMENTO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrcamentoItems]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrcamentoItems](
	[CodOrcamento] [int] NOT NULL,
	[CodProduto] [int] NULL,
	[CodServico] [int] NULL,
	[Quantidade] [decimal](12, 2) NULL,
	[ValorUnitario] [decimal](12, 2) NULL,
	[ValorTotal] [decimal](12, 2) NULL,
	[DescontoItem] [decimal](12, 2) NULL,
	[DescontoTotal] [decimal](12, 2) NULL,
 CONSTRAINT [PK_CodOrcamento_OR] PRIMARY KEY CLUSTERED 
(
	[CodOrcamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdemServico]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdemServico](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NULL,
	[Pedido] [int] NULL,
	[Lote] [int] NULL,
	[DataAbertura] [datetime] NULL,
	[DataFechamento] [datetime] NULL,
	[Situacao] [nvarchar](50) NULL,
	[ValorMaterial] [decimal](12, 2) NULL,
	[ValorServico] [decimal](12, 2) NULL,
	[ValorImovel] [decimal](12, 2) NULL,
	[ValorTotal] [decimal](12, 2) NULL,
	[PrevisaoEntrega] [datetime] NULL,
	[DataEntrega] [datetime] NULL,
	[NomeResponsavel] [nvarchar](100) NULL,
	[AutorizadoPor] [nvarchar](50) NULL,
	[ReclamacaoCliente] [ntext] NULL,
	[ObservacaoOs] [ntext] NULL,
 CONSTRAINT [PK_CODIGO_OS] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdemServicoAndamentos]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdemServicoAndamentos](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodOS] [int] NOT NULL,
	[CodSituacao] [int] NULL,
	[CodUsuario] [int] NULL,
	[Item] [int] NULL,
	[DataAndamento] [datetime] NULL,
 CONSTRAINT [PK_CODIGO_ANDAMENTO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdemServicoItems]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdemServicoItems](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodProduto] [int] NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[Quantidade] [decimal](5, 2) NULL,
	[DescontoItem] [decimal](2, 2) NULL,
	[ValorTotal] [decimal](10, 2) NULL,
	[CodOs] [int] NOT NULL,
	[ValorUnitario] [decimal](10, 2) NULL,
	[DescricaoProblema] [ntext] NULL,
 CONSTRAINT [PK_CodOSSItem] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdemServicoSituacao]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdemServicoSituacao](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodOs] [int] NULL,
	[Situacao] [int] NULL,
 CONSTRAINT [PK_CODIGO_OrdemServicoSituacao] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pago]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPagamento] [int] NOT NULL,
	[ValorPago] [decimal](12, 2) NOT NULL,
	[DataPagamento] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NULL,
	[TotalPedido] [decimal](10, 2) NULL,
	[TrocoPara] [nvarchar](max) NULL,
	[FormaPagamento] [nvarchar](100) NULL,
	[Finalizado] [bit] NULL,
	[RealizadoEm] [datetime] NULL,
	[Tipo] [nvarchar](20) NULL,
	[NumeroMesa] [nvarchar](20) NULL,
	[status] [nvarchar](10) NULL,
	[DataAtualizacao] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Pessoa](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CNPJCPF] [char](14) NULL,
	[Nome] [nvarchar](255) NULL,
	[Logradouro] [nvarchar](25) NULL,
	[Endereco] [nvarchar](100) NULL,
	[Complemento] [nvarchar](100) NULL,
	[Bairro] [nvarchar](100) NULL,
	[Cidade] [nvarchar](100) NULL,
	[Uf] [char](2) NULL,
	[Email] [nvarchar](100) NULL,
	[Contato] [nvarchar](100) NULL,
	[Situacao] [bit] NULL,
	[Tipo] [char](1) NULL,
	[PFPJ] [char](1) NULL,
	[PontoReferencia] [nvarchar](200) NULL,
	[Observacao] [nvarchar](max) NULL,
	[DataCadastro] [datetime] NULL,
	[DataNascimento] [datetime] NULL,
	[TicketFidelidade] [int] NULL,
	[Numero] [nvarchar](20) NULL,
	[Cep] [nvarchar](20) NULL,
	[Telefone] [nvarchar](100) NULL,
	[Telefone2] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductLicenseKeyLayout]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductLicenseKeyLayout](
	[codEmpresa] [int] NOT NULL,
	[dataAdesao] [date] NOT NULL,
	[dataExpiracao] [date] NOT NULL,
	[modulos] [nvarchar](7) NOT NULL,
	[versao] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_COD_PROD_LICENSE] PRIMARY KEY CLUSTERED 
(
	[codEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produto]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NomeProduto] [nvarchar](255) NULL,
	[DescricaoProduto] [nvarchar](max) NULL,
	[PrecoProduto] [decimal](10, 2) NULL,
	[GrupoProduto] [nvarchar](50) NULL,
	[PrecoDesconto] [decimal](5, 2) NULL,
	[DiaSemana] [nvarchar](100) NULL,
	[DataAtualizacao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Receber]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receber](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodVenda] [int] NOT NULL,
	[DataVenda] [datetime] NOT NULL,
	[DataRecebimento] [datetime] NOT NULL,
	[TotalReceber] [decimal](12, 2) NOT NULL,
	[ValorRestante] [decimal](12, 2) NOT NULL,
	[Quitado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Recebido]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recebido](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodReceber] [int] NOT NULL,
	[ValorRecebido] [decimal](12, 2) NOT NULL,
	[DataRecebimento] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Servico]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servico](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[Valor] [decimal](10, 2) NULL,
 CONSTRAINT [PK_COD_SERVICO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubOrdemsServico]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubOrdemsServico](
	[CodOrdem] [int] NOT NULL,
	[CodPessoa] [int] NOT NULL,
	[NumeroPonto] [int] NULL,
	[NumeroOrdem] [int] NULL,
	[Descricao_OS_Material] [nvarchar](max) NULL,
	[Quantidade] [decimal](12, 2) NULL,
	[ValorUnitario] [decimal](12, 2) NULL,
	[ValorTotal] [decimal](12, 2) NULL,
	[NotaFiscal] [int] NULL,
	[CodComponenten] [int] NULL,
 CONSTRAINT [PK_CODORDEM_SubOrdem] PRIMARY KEY CLUSTERED 
(
	[CodOrdem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TabelaPreco]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TabelaPreco](
	[Codigo] [int] NOT NULL,
	[CodGrupoProduto] [int] NULL,
	[CodGrupoPessoa] [int] NULL,
	[NomeTabela] [nvarchar](50) NULL,
	[DescontoPadrao] [decimal](2, 2) NULL,
 CONSTRAINT [PK_CODIGO_TABELA] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TabelaPrecoItems]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TabelaPrecoItems](
	[CodTabela] [int] NULL,
	[CodProduto] [int] NULL,
	[PrecoCadastro] [decimal](10, 2) NULL,
	[DescontoItem] [decimal](2, 2) NULL,
	[PrecoTabela] [decimal](10, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[cod] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[Senha] [nvarchar](128) NULL,
	[CancelaPedidosSN] [bit] NULL,
	[AlteraProdutosSN] [bit] NULL,
	[AdministradorSN] [bit] NULL,
	[AcessaRelatoriosSN] [bit] NULL,
 CONSTRAINT [Pk_Cod_Usuario] PRIMARY KEY CLUSTERED 
(
	[cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Veiculos]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Veiculos](
	[Placa] [varchar](8) NOT NULL,
	[NomePropietario] [nvarchar](100) NULL,
	[Marca] [nvarchar](100) NULL,
	[Modelo] [nvarchar](100) NULL,
 CONSTRAINT [PK_Placa_Veiculos] PRIMARY KEY CLUSTERED 
(
	[Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venda]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venda](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NOT NULL,
	[ValorVenda] [decimal](12, 2) NULL,
	[DataVenda] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vendedores]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendedores](
	[CodVendedor] [int] NOT NULL,
	[NomeVendedor] [nvarchar](50) NOT NULL,
	[Endereco] [nvarchar](50) NOT NULL,
	[Numero] [int] NULL,
	[Complemento] [nvarchar](30) NOT NULL,
	[Bairro] [nvarchar](30) NOT NULL,
	[Cep] [nvarchar](8) NOT NULL,
	[Cidade] [nvarchar](30) NOT NULL,
	[Estado] [char](2) NOT NULL,
	[CPF] [nvarchar](14) NOT NULL,
	[Inscricao_RG] [nvarchar](16) NOT NULL,
	[Observacao] [ntext] NOT NULL,
	[CodUsuario] [int] NOT NULL,
	[Telefone1] [nvarchar](16) NOT NULL,
	[Telefone2] [nvarchar](16) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FazPedidoClienteBloqueadoSN] [bit] NOT NULL,
	[PercMaxDesconto] [decimal](6, 3) NOT NULL,
	[CodTabela] [int] NOT NULL,
	[DataAdmissao] [datetime] NOT NULL,
	[Comissao] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Cod_Vendedor] PRIMARY KEY CLUSTERED 
(
	[CodVendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[xxx2]    Script Date: 07/04/2015 20:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[xxx2](
	[CODIGO] [nvarchar](255) NULL,
	[NOME] [nvarchar](255) NULL,
	[DESCRICAO] [nvarchar](255) NULL,
	[PROMOCAO_INICIO] [datetime] NULL,
	[PROMOCAO_FIM] [datetime] NULL,
	[PROMOCAO_VALOR] [nvarchar](255) NULL,
	[PRECO_VEND] [nvarchar](255) NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AReceber] ADD  DEFAULT ((0)) FOR [Quitado]
GO
ALTER TABLE [dbo].[Configuracao] ADD  DEFAULT ((0)) FOR [CobraTaxaGarcon]
GO
ALTER TABLE [dbo].[Configuracao] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Empresa] ADD  DEFAULT ((0)) FOR [strConnectionResult]
GO
ALTER TABLE [dbo].[Estoque] ADD  DEFAULT ('0.00') FOR [QuantidadeEstoque]
GO
ALTER TABLE [dbo].[OrdemServicoSituacao] ADD  DEFAULT ((0)) FOR [Situacao]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_Finalizado]  DEFAULT ((0)) FOR [Finalizado]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Pessoa] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Receber] ADD  DEFAULT ((0)) FOR [Quitado]
GO
ALTER TABLE [dbo].[TabelaPreco] ADD  DEFAULT ((0)) FOR [CodGrupoProduto]
GO
ALTER TABLE [dbo].[TabelaPreco] ADD  DEFAULT ((0)) FOR [CodGrupoPessoa]
GO
ALTER TABLE [dbo].[TabelaPreco] ADD  DEFAULT ((0)) FOR [DescontoPadrao]
GO
ALTER TABLE [dbo].[CaixaMovimento]  WITH CHECK ADD  CONSTRAINT [FK_CODCAIXABANCO] FOREIGN KEY([CodCaixaBanco])
REFERENCES [dbo].[CaixaBanco] ([Codigo])
GO
ALTER TABLE [dbo].[CaixaMovimento] CHECK CONSTRAINT [FK_CODCAIXABANCO]
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_PRODUTO] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_CODIGO_PRODUTO]
GO
ALTER TABLE [dbo].[ItemsPedido]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_PRODUTO_ITEM] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[ItemsPedido] CHECK CONSTRAINT [FK_CODIGO_PRODUTO_ITEM]
GO
ALTER TABLE [dbo].[ItemsVenda]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_ItemsVenda] FOREIGN KEY([CodVenda])
REFERENCES [dbo].[Venda] ([Codigo])
GO
ALTER TABLE [dbo].[ItemsVenda] CHECK CONSTRAINT [FK_CODIGO_ItemsVenda]
GO
ALTER TABLE [dbo].[LicenseKeyGenareted]  WITH CHECK ADD  CONSTRAINT [FK_COD_PRODK_LICENCENSE] FOREIGN KEY([codPLK])
REFERENCES [dbo].[ProductLicenseKeyLayout] ([codEmpresa])
GO
ALTER TABLE [dbo].[LicenseKeyGenareted] CHECK CONSTRAINT [FK_COD_PRODK_LICENCENSE]
GO
ALTER TABLE [dbo].[Movimento]  WITH CHECK ADD  CONSTRAINT [FK_Codigo_Caixa] FOREIGN KEY([CodCaixaBanco])
REFERENCES [dbo].[CaixaBanco] ([Codigo])
GO
ALTER TABLE [dbo].[Movimento] CHECK CONSTRAINT [FK_Codigo_Caixa]
GO
ALTER TABLE [dbo].[MovimentoItems]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_MOVIMENTO] FOREIGN KEY([CodMovimento])
REFERENCES [dbo].[Movimento] ([Codigo])
GO
ALTER TABLE [dbo].[MovimentoItems] CHECK CONSTRAINT [FK_CODIGO_MOVIMENTO]
GO
ALTER TABLE [dbo].[MovimentoItems]  WITH CHECK ADD  CONSTRAINT [FK_Codigo_Produto_MOvimento] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[MovimentoItems] CHECK CONSTRAINT [FK_Codigo_Produto_MOvimento]
GO
ALTER TABLE [dbo].[Orcamento]  WITH CHECK ADD  CONSTRAINT [FK_Codigo_Vendedor] FOREIGN KEY([CodVendedor])
REFERENCES [dbo].[Vendedores] ([CodVendedor])
GO
ALTER TABLE [dbo].[Orcamento] CHECK CONSTRAINT [FK_Codigo_Vendedor]
GO
ALTER TABLE [dbo].[OrcamentoItems]  WITH CHECK ADD  CONSTRAINT [FK_CodOrcamento_Orcamento] FOREIGN KEY([CodOrcamento])
REFERENCES [dbo].[Orcamento] ([Codigo])
GO
ALTER TABLE [dbo].[OrcamentoItems] CHECK CONSTRAINT [FK_CodOrcamento_Orcamento]
GO
ALTER TABLE [dbo].[OrdemServicoItems]  WITH CHECK ADD  CONSTRAINT [FK_CodOSSItem] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[OrdemServicoItems] CHECK CONSTRAINT [FK_CodOSSItem]
GO
ALTER TABLE [dbo].[OrdemServicoSituacao]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_OrdemServicoSituacao] FOREIGN KEY([CodOs])
REFERENCES [dbo].[OrdemServico] ([Codigo])
GO
ALTER TABLE [dbo].[OrdemServicoSituacao] CHECK CONSTRAINT [FK_CODIGO_OrdemServicoSituacao]
GO
ALTER TABLE [dbo].[SubOrdemsServico]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_OS] FOREIGN KEY([CodOrdem])
REFERENCES [dbo].[OrdemServico] ([Codigo])
GO
ALTER TABLE [dbo].[SubOrdemsServico] CHECK CONSTRAINT [FK_CODIGO_OS]
GO
ALTER TABLE [dbo].[TabelaPrecoItems]  WITH CHECK ADD  CONSTRAINT [FK_COD_Produto_Tabela] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[TabelaPrecoItems] CHECK CONSTRAINT [FK_COD_Produto_Tabela]
GO
ALTER TABLE [dbo].[TabelaPrecoItems]  WITH CHECK ADD  CONSTRAINT [FK_COD_Tabela] FOREIGN KEY([CodTabela])
REFERENCES [dbo].[TabelaPreco] ([Codigo])
GO
ALTER TABLE [dbo].[TabelaPrecoItems] CHECK CONSTRAINT [FK_COD_Tabela]
GO
