USE [master]
GO
/****** Object:  Database [DBExpert]    Script Date: 10/07/2014 21:01:06 ******/
CREATE DATABASE [DBExpert]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBExpert', FILENAME = N'E:\Data\DBExpert.mdf' , SIZE = 113920KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBExpert_log', FILENAME = N'E:\Data\DBExpert_log.LDF' , SIZE = 270016KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBExpert] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBExpert].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBExpert] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBExpert] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBExpert] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBExpert] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBExpert] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBExpert] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBExpert] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBExpert] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBExpert] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBExpert] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBExpert] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBExpert] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBExpert] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBExpert] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBExpert] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBExpert] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBExpert] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBExpert] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBExpert] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBExpert] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBExpert] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBExpert] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBExpert] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBExpert] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBExpert] SET  MULTI_USER 
GO
ALTER DATABASE [DBExpert] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBExpert] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBExpert] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBExpert] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DBExpert]
GO
/****** Object:  StoredProcedure [dbo].[spAdicionaEmpresa]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionaEmpresa]
@Nome nvarchar(100),
@CNPJ varchar(14),
@Telefone varchar(20),
@Telefone2 varchar(20),
@Endereco nvarchar(100),
@Cep varchar(8),
@Cidade nvarchar(50),
@Numero varchar(10),
@Bairro varchar(50),
@UF char(2),
@PontoReferencia nvarchar(max),
@Banco nvarchar(max),
@Servidor nvarchar(max)

as 
Insert into Empresa (nome,Telefone,CNPJ, Telefone2,Endereco,Cep,Cidade,Numero,Bairro,UF,PontoReferencia,Banco,Servidor)
            Values (@nome,@Telefone,@CNPJ,@Telefone2,@Endereco,@Cep,@Cidade,@Numero,@Bairro,@UF,@PontoReferencia,@Banco,@Servidor)






GO
/****** Object:  StoredProcedure [dbo].[spAdicionarCep]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarClienteDelivery]    Script Date: 10/07/2014 21:01:07 ******/
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
@Telefone2 varchar(20)
as 
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2)



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarConfiguracao]    Script Date: 10/07/2014 21:01:07 ******/
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
@Usa2Telefones bit

as begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,ControlaEntregador,ProdutoPorCodigo,Usa2Telefones) values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones)
end

					
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEntregador]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spAdicionarEntregador]

	@Nome nvarchar(50)
	
as
	BEGIN
		INSERT INTO Entregador(Nome)
		Values(@Nome)
	END



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarFormaPagamento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarGrupo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarItemExtra]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarItemVenda]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
	@RealizadoEm   datetime
as
		BEGIN
			INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm)
			Values(
				@CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm
			);
			SET @Codigo = SCOPE_IDENTITY()
			RETURN @Codigo
		END


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoa]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarProduto]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarProduto]

	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50)
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto)
		Values(
			@Nome,
			@Descricao,
			@Preco,
			@GrupoProduto
		)
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuario]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionarUsuario]
@Nome nvarchar(max),
@Senha nvarchar(128)
as begin

insert into Usuario (Nome,Senha) values
					(@Nome,@Senha)
  end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarVenda]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarConfiguracao]    Script Date: 10/07/2014 21:01:07 ******/
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
@Usa2Telefones bit

AS 
	Begin
	Update Configuracao set
	ImpViaCozinha = @ImpViaCozinha,
	UsaDataNascimento = @UsaDataNascimento,
	UsaLoginSenha = @UsaLoginSenha,
	QtdCaracteresImp = @QtdCaracteresImp,
	ControlaEntregador = @ControlaEntregador,
	ProdutoPorCodigo = @ProdutoPorCodigo,
	Usa2Telefones   = @Usa2Telefones
	
	where cod=@cod
	end
GO
/****** Object:  StoredProcedure [dbo].[spAlterarFormaPagamento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarGrupo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarItemExtraPorCodigo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarItemPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarPessoa]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarPessoa]

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






GO
/****** Object:  StoredProcedure [dbo].[spAlterarProduto]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarProduto]

	@Codigo int,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50)

AS
	BEGIN
		UPDATE Produto

		SET 
		
		NomeProduto = @Nome,
		DescricaoProduto = @Descricao,
		PrecoProduto = @Preco,
		GrupoProduto = @GrupoProduto

		WHERE Codigo = @Codigo;
	END




GO
/****** Object:  StoredProcedure [dbo].[spAlterarTotalPedido]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2)

AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido

		WHERE 
			Codigo = @Codigo --Codigo Produto
	END




GO
/****** Object:  StoredProcedure [dbo].[spAlterarTrocoParaFormaPagamento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarUsuario]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAlterarUsuario]
@codigo int,
@Nome nvarchar(max),
@Senha nvarchar(128)
as begin
update Usuario set 
Nome=@Nome,
Senha = @Senha
where cod=@Codigo
   END

GO
/****** Object:  StoredProcedure [dbo].[spCriarPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirCliente]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirGrupo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirItemExtraPorCodigo]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirItemExtraPorCodigo]
		@Codigo int
	AS
		DELETE FROM ItemsExtras WHERE Codigo = @Codigo




GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirProduto]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterConfiguracao]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterConfiguracao]


as 

Select * from Configuracao
GO
/****** Object:  StoredProcedure [dbo].[spObterEmpresa]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterEmpresa]


as 

Select * from Empresa 




GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPorCep]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterEntregador]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterEntregador]
as
 BEGIN
  SELECT * 
  FROM Pessoa 
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterEntregadores]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterFormaPagamento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterGrupo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterItemsExtrasPorPedido]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsExtrasPorPedido]
		@CodPedido int
	AS
		SELECT CodPedido,Descricao,Valor FROM ItemsExtras WHERE CodPedido = @CodPedido




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterNomeProduto]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedido]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT *
		FROM Pedido WHERE Finalizado = 0 ORDER BY Codigo DESC
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorCodigo]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,Telefone2
  FROM Pessoa WHERE Codigo = @Codigo
 END




GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorTelefone]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone int

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoas]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoas]


as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone
  FROM Pessoa 
 END




GO
/****** Object:  StoredProcedure [dbo].[spObterProduto]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterProdutoCompleto]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto
	FROM Produto WHERE Codigo = @Codigo;


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodigo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorGrupo]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterUsuario]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[spObterUsuario]
as begin
select * from Usuario
end			



GO
/****** Object:  StoredProcedure [dbo].[spObterViasDoPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  StoredProcedure [dbo].[spSinalizarPedidoConcluido]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int	
AS
	Update Pedido SET Finalizado = 1 WHERE Codigo = @Codigo



GO
/****** Object:  Table [dbo].[APagar]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[APagarParcelas]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[AReceber]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[AReceberParcelas]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[base_cep]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[CaixaBanco]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[CaixaMovimento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[CentroCusto]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[ClasseFinanceira]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Configuracao]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
 CONSTRAINT [Pk_cod_Config] PRIMARY KEY CLUSTERED 
(
	[cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 10/07/2014 21:01:07 ******/
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
	[TipoEmpresa] [int] NOT NULL,
	[VersaoBanco] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Entregador]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entregador](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_COD_ENTREGADOR] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estoque]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[FormaPagamento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Funcionario]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Grupo]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NomeGrupo] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GrupoPessoas]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[GrupoProduto]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[ItemsPedido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[ItemsVenda]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[LicenseKeyGenareted]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Motorista]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Movimento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[MovimentoItems]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Orcamento]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[OrcamentoItems]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[OrdemServico]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[OrdemServicoAndamentos]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[OrdemServicoItems]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[OrdemServicoSituacao]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Pago]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Pedido]    Script Date: 10/07/2014 21:01:07 ******/
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
	[RealizadoEm] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 10/07/2014 21:01:07 ******/
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
	[Cep] [int] NULL,
	[Logradouro] [nvarchar](25) NULL,
	[Endereco] [nvarchar](100) NULL,
	[Complemento] [nvarchar](100) NULL,
	[Numero] [int] NULL,
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
	[Telefone] [int] NULL,
	[Telefone2] [int] NULL,
	[DataCadastro] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductLicenseKeyLayout]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Produto]    Script Date: 10/07/2014 21:01:07 ******/
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
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Receber]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Recebido]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Servico]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[SubOrdemsServico]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[TabelaPreco]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[TabelaPrecoItems]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/07/2014 21:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[cod] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[Senha] [nvarchar](128) NULL,
 CONSTRAINT [Pk_Cod_Usuario] PRIMARY KEY CLUSTERED 
(
	[cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Veiculos]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Venda]    Script Date: 10/07/2014 21:01:07 ******/
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
/****** Object:  Table [dbo].[Vendedores]    Script Date: 10/07/2014 21:01:07 ******/
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
ALTER TABLE [dbo].[AReceber] ADD  DEFAULT ((0)) FOR [Quitado]
GO
ALTER TABLE [dbo].[Configuracao] ADD  DEFAULT ((0)) FOR [ControlaEntregador]
GO
ALTER TABLE [dbo].[Empresa] ADD  DEFAULT ((0)) FOR [strConnectionResult]
GO
ALTER TABLE [dbo].[Estoque] ADD  DEFAULT ('0.00') FOR [QuantidadeEstoque]
GO
ALTER TABLE [dbo].[OrdemServicoSituacao] ADD  DEFAULT ((0)) FOR [Situacao]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_Finalizado]  DEFAULT ((0)) FOR [Finalizado]
GO
ALTER TABLE [dbo].[Pessoa] ADD  DEFAULT (getdate()) FOR [DataCadastro]
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
USE [master]
GO
ALTER DATABASE [DBExpert] SET  READ_WRITE 
GO
