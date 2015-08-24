USE [master]
GO
/****** Object:  Database [DBExpert_Teste]    Script Date: 22/08/2015 12:02:33 ******/
CREATE DATABASE [DBExpert_Teste]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBExpert_Teste', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS2012\MSSQL\DATA\DBExpert_Teste.mdf' , SIZE = 17664KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBExpert_Teste_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS2012\MSSQL\DATA\DBExpert_Teste_log.LDF' , SIZE = 20416KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBExpert_Teste] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBExpert_Teste].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBExpert_Teste] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBExpert_Teste] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBExpert_Teste] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBExpert_Teste] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBExpert_Teste] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBExpert_Teste] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBExpert_Teste] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBExpert_Teste] SET  MULTI_USER 
GO
ALTER DATABASE [DBExpert_Teste] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBExpert_Teste] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBExpert_Teste] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBExpert_Teste] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DBExpert_Teste]
GO
/****** Object:  User [digital]    Script Date: 22/08/2015 12:02:33 ******/
CREATE USER [digital] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  StoredProcedure [dbo].[AlteraFidelidade]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[ObterFidelidade]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[sbObterUltimoPedido]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sbObterUltimoPedido]
  @CodPessoa int
  as
   begin
	select 
	top 1 Codigo,
		 CodPessoa,
		 TotalPedido,
		 (select FormaPagamento from Pedido PS where PS.Codigo = p.Codigo) as FP, 
		 max(RealizadoEm) as RealizadoEm
	from Pedido P
	where CodPessoa =@CodPessoa and Finalizado =1
	group by Codigo,CodPessoa,TotalPedido,RealizadoEm
	order by RealizadoEm desc
  end


GO
/****** Object:  StoredProcedure [dbo].[spAbrirCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAbrirCaixa]
 @CodUsuario int,
 @Data date,
 @Estado bit,
 @Historico nvarchar(100),
 @ValorAbertura decimal(10,2),
 @Numero varchar(10)
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura,Numero) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura,@Numero) 
   end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionaDiferenca]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionaDiferenca]
@NumeroCaixa nvarchar(10),
@Data datetime,
@ValorSomado    decimal(10,2),
@ValorInformado decimal(10,2),
@ValorDiferenca decimal(10,2),
@CodUsuario int ,
@Tipo char(1)
as 
begin
  insert into CaixaDiferenca (NumeroCaixa,Data,ValorSomado,ValorInformado,ValorDiferenca,CodUsuario,Tipo)
          values (@NumeroCaixa,@Data,@ValorSomado,@ValorInformado,@ValorDiferenca,@CodUsuario,@Tipo)
end
GO
/****** Object:  StoredProcedure [dbo].[spAdicionaHistoricoCancelamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionaHistoricoCancelamento]
@CodPessoa int,
@Motivo nvarchar(100),
@CodMotivo int,
@Data date
as
  begin
  insert into HistoricoCancelamentos (CodPessoa,Motivo,CodMotivo,Data)
         values (@CodPessoa,@Motivo,@CodMotivo,@Data)

  end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionaMotivoCancelamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionaMotivoCancelamento]
@Nome nvarchar(50),
@DataCadastro date
 as 
 begin
    insert into MotivoCancelamento(Nome,DataCadastro) values (@Nome,@DataCadastro)
 end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarCaixa]
@Numero nvarchar(10),
@Nome   nvarchar(50),
@DataCadastro datetime
as 
  begin
   insert into CaixaCadastro (Numero,Nome,DataCadastro) values (@Numero,@Nome,@DataCadastro)
  end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarCep]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarCep]
       
		@Cep nvarchar(8),
		@Logradouro nvarchar(100),
		@Bairro nvarchar(100),
		@Cidade nvarchar(100),
		@Estado nvarchar(100)
	as
		INSERT INTO base_cep(cep,logradouro,bairro,cidade,estado)
					VALUES(@Cep,@Logradouro,@Bairro,@Cidade,@Estado)





GO
/****** Object:  StoredProcedure [dbo].[spAdicionarClienteDelivery]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarClienteDelivery]
@Codigo int output,
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
@TicketFidelidade int ,
@CodRegiao int,
@DataCadastro Datetime
as 
begin
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2,DataNascimento,TicketFidelidade,CodRegiao,DataCadastro)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2,@DataNascimento,@TicketFidelidade,@CodRegiao,@DataCadastro)
SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo

end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarConfiguracao]    Script Date: 22/08/2015 12:02:33 ******/
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
@ViasBalcao char(2),
@RepeteUltimoPedido bit,
@RegistraCancelamentos bit

as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,
                          ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,
						  ImprimeViaEntrega,ControlaFidelidade,PedidosParaFidelidade,DescontoDiaSemana,
						  PrevisaoEntrega,PrevisaoEntregaSN,CobraTaxaGarcon ,TamanhoFont,ImpLPT,PortaLPT,EnviaSMS,
						  ViasEntrega,ViasCozinha,ViasBalcao,RepeteUltimoPedido,RegistraCancelamentos)
						  values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,
							@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,
							@ImprimeViaEntrega,@ControlaFidelidade,@PedidosParaFidelidade,@DescontoDiaSemana,
							@PrevisaoEntrega,@PrevisaoEntregaSN,@CobraTaxaGarcon,@TamanhoFont,@ImpLPT,@PortaLPT,@EnviaSMS,
							@ViasEntrega,@ViasCozinha,@ViasBalcao,@RepeteUltimoPedido,@RegistraCancelamentos)
	end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarDescontoSemana]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionaRegiao]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionaRegiao]

	@NomeRegiao nvarchar(8),
	@Bairro nvarchar(50),
	@TaxaServico decimal(10,2)
	
as
	BEGIN
		INSERT INTO RegiaoEntrega(NomeRegiao,Bairro,TaxaServico)
		Values(@NomeRegiao,@Bairro,@TaxaServico)
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEmpresa]    Script Date: 22/08/2015 12:02:33 ******/
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
@VersaoBanco char(2),
@CaminhoBackup nvarchar(max)

as 
Insert into Empresa (nome,CNPJ,Telefone ,Telefone2,Contato,Cep,
					Endereco,Cidade,Bairro,Numero,UF,PontoReferencia,
					Servidor,Banco,DataInicio,VersaoBanco,strConnectionResult)
            Values (@Nome,@CNPJ,@Telefone,@Telefone2,@Contato,@Cep,
			       @Endereco,@Cidade,@Bairro,@Numero,@UF,@PontoReferencia,
				   @Servidor,@Banco,@DataInicio,@VersaoBanco,@CaminhoBackup)

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEntregador]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarEvento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarEvento] 
@CodUsuario int,
@TipoEvento nvarchar(50),
@DataEvento datetime,
@LocalEvento nvarchar(100)
as 
  begin
  insert into EventosSistema (CodUsuario,TipoEvento,DataEvento,LocalEvento)
                      values (@CodUsuario,@TipoEvento,@DataEvento,@LocalEvento)
   
  end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarFormaPagamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarFormaPagamento]
@Descricao nvarchar(100),
@DescontoSN bit


as
begin 
Insert into FormaPagamento(Descricao,ParcelaSN)
            Values (@Descricao,@DescontoSN)

end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarGrupo]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
	@Item nvarchar(max),
	@ImpressoSN bit	
as
	BEGIN
		INSERT INTO ItemsPedido(CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
		VALUES(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN)
	END



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedidoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarItemAoPedidoApp]
	@CodigoMesa int,
	@CodProduto int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)	
	as
	BEGIN
		declare @CodPedido int
		--Busco pedidos em aberto
		set @CodPedido = (select Codigo from Pedido where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto')

		--se achou o pedido
		if (@CodPedido > 0)
		begin
			INSERT INTO ItemsPedido(CodPedido, CodProduto, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN)
			VALUES(@CodPedido, @CodProduto, @NomeProduto, @Quantidade, @PrecoUnitario, @PrecoTotal, @Item,0)

			exec spAlterarTotalPedidoApp @CodPedido
		end
	END



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemExtra]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarItemVenda]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarMensagen]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarMesas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarMesas]
@NumeroMesa nvarchar(10),
@StatusMesa int
  as
    begin
	 Insert into Mesas (NumeroMesa,StatusMesa)
	             Values (@NumeroMesa,@StatusMesa)
	end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
    @Status     nvarchar(20),
    @PedidoOrigem nvarchar(10),
	@CodigoMesa int	,
	@DescontoValor decimal(10,2)
as
        BEGIN
		set @CodigoMesa= (select NumeroMesa from Mesas where Codigo = @CodigoMesa)
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,[Status],PedidoOrigem,CodigoMesa,DescontoValor)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,@Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
        END
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedidoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@CodigoMesa int	
as
	BEGIN
		declare @NumMesa nvarchar(20);
		set @NumMesa = (select NumeroMesa from Mesas where Codigo = @CodigoMesa)
			
		INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa)
		Values(@CodPessoa, @TotalPedido, @RealizadoEm, @NumMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa);
		SET @Codigo = SCOPE_IDENTITY()
			
		--Atualizando status da mesa
		update Mesas set StatusMesa = 2 where Codigo = @CodigoMesa
			
		RETURN @Codigo
	END
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPermissao]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarPermissao]
(
@CodUsuario int,
@CancelaPedidoSN bit,
@RelatorioSN bit,
@AlteraPedidoSN bit,
@AlteraProdSN bit,
@AlteraClieSN bit,
@PermiteDescSN bit,
@AdministradorSN bit,
@DataAtualizacao datetime
)

as
  begin
    insert into UsuarioPermissao (CodUsuario ,CancelaPedidoSN,RelatorioSN,AlteraPedidoSN,AlteraProdSN ,
								AlteraClieSN,PermiteDescSN,AdministradorSN,DataAtualizacao)
		   values (@CodUsuario ,@CancelaPedidoSN,@RelatorioSN,@AlteraPedidoSN,@AlteraProdSN ,
								@AlteraClieSN,@PermiteDescSN,@AdministradorSN,@DataAtualizacao)

  end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoa]    Script Date: 22/08/2015 12:02:33 ******/
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
		@Observacao nvarchar(max),
		@DataNascimento datetime,
		@DataCadastro datetime,
		@CodRegiao int
	as
		INSERT INTO Pessoa(Nome,Cep,Telefone,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Observacao,DataCadastro,DataNascimento,CodRegiao)
					VALUES(@Nome,@Cep,@Telefone,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Observacao,@DataCadastro,@DataNascimento,@CodRegiao)



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoaApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarPessoaApp]
@Nome nvarchar(100),
@Telefone varchar(20)
as 
begin
Insert into Pessoa(Nome, Telefone, DataCadastro,CodRegiao,Observacao,DataNascimento,TicketFidelidade)
Values (@nome,@Telefone,GETDATE(),1,'Cadastrado via App',GETDATE(),1)
end



select * from Pessoa

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarProduto]    Script Date: 22/08/2015 12:02:33 ******/
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
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit 
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,DiaSemana,PrecoDesconto,AtivoSN)
		Values(
			@Nome,
			@Descricao,
			@Preco,
			@GrupoProduto,
			@DiaSemana,
			@PrecoDesconto,
			@AtivoSN
		)
	END


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuario]    Script Date: 22/08/2015 12:02:33 ******/
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
@AcessaRelatoriosSN bit,
@FinalizaPedidoSN bit,
@DescontoMax decimal,
@DescontoPedidoSN bit
as begin

insert into Usuario (Nome,Senha,CancelaPedidosSN,AlteraProdutosSN,AdministradorSN,AcessaRelatoriosSN,
						FinalizaPedidoSN,DescontoMax,DescontoPedidoSN)
					values
					(@Nome,@Senha,@CancelaPedidosSN,@AlteraProdutosSN,@AdministradorSN,@AcessaRelatoriosSN,
					@FinalizaPedidoSN,@DescontoMax,@DescontoPedidoSN )
  end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuarioDefault]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarUsuarioDefault]
@Nome nvarchar(max),
@Senha nvarchar(128),
@AdministradorSN bit

as begin

insert into Usuario (Nome,Senha,AdministradorSN) values
					(@Nome,@Senha,@AdministradorSN)
  end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarVenda]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionaX]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionaX]
@Data date
as
 insert into XSistemas (Data) values (@Data)



GO
/****** Object:  StoredProcedure [dbo].[spAltera]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlteraCAixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlteraCAixa]
@Numero nvarchar(10),
@Nome   nvarchar(50),
@Codigo int
 as
  begin
    update CaixaCadastro 
	   set
	   Numero = @Numero,
	   Nome   = @Nome

	   where Codigo = @Codigo
  end

GO
/****** Object:  StoredProcedure [dbo].[spAlteraFidelidade]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlteraMesas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlteraMesas]
@Codigo int,
@StatusMesa int,
@NumeroMesa nvarchar(10)
 as
   begin
    Update Mesas set 
	  StatusMesa = @StatusMesa,
	  NumeroMesa = @NumeroMesa,
	  DataAtualizacao = GETDATE()

	  where Codigo = @Codigo

   end

GO
/****** Object:  StoredProcedure [dbo].[spAlteraMotivoCancelamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlteraMotivoCancelamento]
@Nome nvarchar(50),
@DataCadastro date,
@Codigo int
as
  begin
    Update MotivoCancalemento 
	set 
	Nome=@Nome
	where 
	Codigo = @Codigo 
  end

GO
/****** Object:  StoredProcedure [dbo].[spAlteraPedidoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlteraPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100)	
as
	BEGIN			
		update Pedido set CodPessoa = @CodPessoa where Codigo = @Codigo
	END


GO
/****** Object:  StoredProcedure [dbo].[spAlteraPermissao]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlteraPermissao]
(
@CodUsuario int,
@CancelaPedidoSN bit,
@RelatorioSN bit,
@AlteraPedidoSN bit,
@AlteraProdSN bit,
@AlteraClieSN bit,
@PermiteDescSN bit,
@AdministradorSN bit,
@DataAtualizacao datetime
)
as
  begin
    update UsuarioPermissao set
        CancelaPedidoSN=@CancelaPedidoSN ,
		RelatorioSN=@RelatorioSN ,
		AlteraPedidoSN=@AlteraPedidoSN ,
		AlteraProdSN=@AlteraProdSN ,
		AlteraClieSN=@AlteraClieSN ,
		PermiteDescSN=@PermiteDescSN ,
		AdministradorSN=@AdministradorSN ,
		DataAtualizacao=@DataAtualizacao 
  where CodUsuario = @CodUsuario
  
  end



GO
/****** Object:  StoredProcedure [dbo].[spAlterarConfiguracao]    Script Date: 22/08/2015 12:02:33 ******/
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
@ViasBalcao char(2),
@RepeteUltimoPedido bit,
@RegistraCancelamentos bit
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
    ViasBalcao=  @ViasBalcao ,
	RepeteUltimoPedido = @RepeteUltimoPedido,
	RegistraCancelamentos = @RegistraCancelamentos
	where cod=@cod
	end

GO
/****** Object:  StoredProcedure [dbo].[spAlteraRegiao]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(8),
  @Bairro nvarchar(50),
  @TaxaServico decimal(10,2)
as
  begin
    update RegiaoEntrega 
	set
	  NomeRegiao = @NomeRegiao,
	  Bairro = @Bairro,
	  TaxaServico = @TaxaServico
    where 
	  Codigo = @Codigo
  end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarEmpresa]    Script Date: 22/08/2015 12:02:33 ******/
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
@VersaoBanco char(2),
@CaminhoBackup nvarchar(max)

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
VersaoBanco = @VersaoBanco,
strConnectionResult = @CaminhoBackup
end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarEntregador]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarFormaPagamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarFormaPagamento]
@Codigo int,
@Descricao nvarchar(100),
@DescontoSN bit

as 
begin
update FormaPagamento set 
     Descricao=@Descricao ,
	 ParcelaSN = @DescontoSN
	         where Codigo=@Codigo
end



GO
/****** Object:  StoredProcedure [dbo].[spAlterarGrupo]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarItemExtraPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarItemPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
	@Item nvarchar(max),
	@ImpressoSN bit
AS
	BEGIN
		UPDATE ItemsPedido

		SET 
		NomeProduto = @NomeProduto,
		Quantidade = @Quantidade,
		PrecoItem = @PrecoUnitario,
		PrecoTotalItem = @PrecoTotal,
		ImpressoSN=@ImpressoSN,
		Item = @Item
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido ; -- Código Pedido
	END



GO
/****** Object:  StoredProcedure [dbo].[spAlterarItemPedidoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarItemPedidoApp]
	@Codigo int,	
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
AS
	BEGIN
		declare @CodPedido int
		--Busco pedidos em aberto
		set @CodPedido = (select CodPedido from ItemsPedido where Codigo = @Codigo)
		
		if (@CodPedido > 0 )
		begin
			UPDATE 
				ItemsPedido			
			SET 
				Quantidade = @Quantidade,
				PrecoItem = @PrecoUnitario,
				PrecoTotalItem = @PrecoTotal,
				Item = @Item
			WHERE 
				Codigo = @Codigo


			exec spAlterarTotalPedidoApp @CodPedido
		end
		
	END


GO
/****** Object:  StoredProcedure [dbo].[spAlterarMensagens]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarPessoa]    Script Date: 22/08/2015 12:02:33 ******/
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
 @TicketFidelidade int,
 @CodRegiao int,
 @DataCadastro datetime

 
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
  DataCadastro = DataCadastro,
  TicketFidelidade= TicketFidelidade,
  CodRegiao =@CodRegiao
 
  
  where Codigo=@Codigo
END


GO
/****** Object:  StoredProcedure [dbo].[spAlterarPessoaApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarPessoaApp]
 @Codigo int,
 @Nome nvarchar(100),
 @Telefone nvarchar(20)
AS
 BEGIN
  UPDATE Pessoa SET Nome = @Nome, Telefone = @Telefone where Codigo= @Codigo
END


GO
/****** Object:  StoredProcedure [dbo].[spAlterarProduto]    Script Date: 22/08/2015 12:02:33 ******/
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
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit 

AS
	BEGIN
		UPDATE Produto

		SET 
		
		NomeProduto = @Nome,
		DescricaoProduto = @Descricao,
		PrecoProduto = @Preco,
		GrupoProduto = @GrupoProduto,
		DiaSemana = @DiaSemana,
		PrecoDesconto = @PrecoDesconto,
		AtivoSN = @AtivoSN 
		WHERE Codigo = @Codigo;
	END



GO
/****** Object:  StoredProcedure [dbo].[spAlterarSenha]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarSenha]
@codigo int,
@Senha nvarchar(128)
as begin
update Usuario set 
Senha = @Senha
where cod=@Codigo
   END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarTotalPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarTotalPedidoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarTotalPedidoApp]
	@Codigo int
AS
	BEGIN
		UPDATE 
			Pedido
		SET 
			TotalPedido = (Select SUM(PrecoTotalItem) from ItemsPedido where CodPedido = @Codigo)
		WHERE 
			Codigo = @Codigo
	END



GO
/****** Object:  StoredProcedure [dbo].[spAlterarTrocoParaFormaPagamento]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarUsuario]    Script Date: 22/08/2015 12:02:33 ******/
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
@AcessaRelatoriosSN bit,
@FinalizaPedidoSN bit,
@DescontoPedidoSN bit,
@DescontoMax decimal

as begin
update Usuario set 
Nome=@Nome,
Senha = @Senha,
CancelaPedidosSN=@CancelaPedidosSN ,
AlteraProdutosSN= @AlteraProdutosSN ,
AdministradorSN= @AdministradorSN ,
AcessaRelatoriosSN = @AcessaRelatoriosSN,
FinalizaPedidoSN=@FinalizaPedidoSN,
DescontoMax = @DescontoMax,
DescontoPedidoSN = @DescontoPedidoSN
where cod=@Codigo
   END



GO
/****** Object:  StoredProcedure [dbo].[spAlteraStatusMesa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraStatusMesa]
--@Codigo int,
@NumeroMesa nvarchar(10),
@StatusMesa int
  as
    begin
	update Mesas set StatusMesa = @StatusMesa
	 where NumeroMesa = @NumeroMesa --and Codigo = @Codigo
	end

GO
/****** Object:  StoredProcedure [dbo].[spCalculaSistema]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spCalculaSistema]
as
delete from XSistemas

GO
/****** Object:  StoredProcedure [dbo].[spCancelarPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spContaCancelamentos]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spContaCancelamentos]
@CodPessoa int
as
  begin
     select COUNT(CodPessoa)  as Quantidade from HistoricoCancelamentos
	 where CodPessoa = @CodPessoa
  end
GO
/****** Object:  StoredProcedure [dbo].[spContaPedidosPorCliente]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spContaPedidosPorCliente]
@Codigo int
  as
    begin
	select count(Codigo) as Quanti
	from 
	 Pedido
   where CodPessoa = @Codigo and Finalizado=1
	end


GO
/****** Object:  StoredProcedure [dbo].[spCriarPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
@Item nvarchar(max),
@ImpressoSN bit
AS
INSERT INTO dbo.ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
					  Values(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN)



GO
/****** Object:  StoredProcedure [dbo].[spExcluirCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirCaixa]
@Codigo int
 as 
   begin
    delete from CaixaCadastro where Codigo = @Codigo
   end

GO
/****** Object:  StoredProcedure [dbo].[spExcluirCliente]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirCliente]

	@Codigo int

AS
	BEGIN
		DELETE FROM 
			Pessoa
		WHERE 
			Codigo = @Codigo --Codigo Grupo
	END



GO
/****** Object:  StoredProcedure [dbo].[spExcluiRegiao]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[spExcluiRegiao]
   @Codigo int
   as
     begin
	   Delete from RegiaoEntrega
	     where Codigo = @Codigo
	 end



GO
/****** Object:  StoredProcedure [dbo].[spExcluirFormaPagamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirFormaPagamento]
@Codigo int
  as 
    begin
	  delete from FormaPagamento
	    where
		 Codigo = @Codigo
	end


GO
/****** Object:  StoredProcedure [dbo].[spExcluirGrupo]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirItemExtraPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirItemExtraPorCodigo]
		@Codigo int
	AS
		DELETE FROM ItemsExtras WHERE Codigo = @Codigo



GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirMotivoCancelamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirMotivoCancelamento]
@Codigo int
as 
  begin
    Delete from MotivoCancelamento where Codigo = @Codigo
  end

GO
/****** Object:  StoredProcedure [dbo].[spExcluirMovimentoCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirMovimentoCaixa]
@Data date,
@CodCaixa int
as 
  begin
    delete from CaixaMovimento 
	 where 
	  Data =@Data
	   and
	   CodCaixa = @CodCaixa
  end
GO
/****** Object:  StoredProcedure [dbo].[spExcluirProduto]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spFecharCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spFecharCaixa]
@CodUsuario int,
@Data date,
@Estado bit,
@Historico nvarchar(100),
@ValorFechamento decimal(10,2),
@Numero varchar(10)
as 
  begin
    update Caixa
	  set
	  CodUsuario = @CodUsuario,
	  Data       = @Data,
	  Estado     = @Estado,
	  Historico  = @Historico,
	  ValorFechamento = @ValorFechamento

	  where Data = @Data 
	  and Numero = @Numero
  end
GO
/****** Object:  StoredProcedure [dbo].[spInformaItemImpresso]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spInformaItemImpresso]
@CodPedido int,
@CodProduto int,
@ImpressoSN bit
as 
 update ItemsPedido 
   set ImpressoSN = @ImpressoSN
where CodPedido = @CodPedido and
      CodProduto= @CodProduto


GO
/****** Object:  StoredProcedure [dbo].[spInsereBoyPedido]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spInsereBoyPedido]
@CodPedido int ,
@CodMotoBoy int
as
 update Pedido 
 set CodMotoboy = @CodMotoBoy
 where Codigo = @CodPedido

GO
/****** Object:  StoredProcedure [dbo].[spInsereRegistro]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spInsereRegistro]
@Data date,
@RegistroMD5 nvarchar(max)
as
 insert into XSistemas (Data,RegistroMD5) values (@Data,@RegistroMD5)


GO
/****** Object:  StoredProcedure [dbo].[spInserirMovimentoCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spInserirMovimentoCaixa]
@CodCaixa int,
@Data datetime,
@Historico nvarchar(100),
@NumeroDocumento nvarchar(50),
@CodFormaPagamento int,
@Valor decimal(10,2),
@Tipo char(1),
@CodUser int 
as
  begin
     insert into CaixaMovimento (CodCaixa,Data,Historico,NumeroDocumento,CodFormaPagamento,Valor,Tipo,CodUsuario)
	        values (@CodCaixa,@Data,@Historico,@NumeroDocumento,@CodFormaPagamento,@Valor,@Tipo,@CodUser)
  end


GO
/****** Object:  StoredProcedure [dbo].[spMargemGarcon]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spMargemGarcon]
@Codigo int ,
@MargemGarcon decimal(10,2)
as
 begin
  update Pedido set TotalPedido = TotalPedido + @MargemGarcon ,  MargemGarcon = @MargemGarcon
  where Codigo = @Codigo
 end



GO
/****** Object:  StoredProcedure [dbo].[spObterAnivesariantes]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterCaixa]
as
  begin
    select 
	*
  from
    CaixaCadastro

  end

GO
/****** Object:  StoredProcedure [dbo].[spObterCaixaAberto]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterCaixaAberto]
as
 select * from Caixa
 where Caixa.Estado=0
GO
/****** Object:  StoredProcedure [dbo].[spObterCaixaMovimetoFiltro]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterCaixaMovimetoFiltro]
@DataInicio date,
@DataFim date,
@Tipo char(1),
--@CodFormaPagamento int,
@CodCaixa nvarchar(10)
as
  begin
    select 
	CX.Numero as 'Numero Caixa',
	CXM.DATA,
	CXM.Historico,
	CXM.NumeroDocumento,
	FP.Descricao AS 'FORMA PAGAMENTO',
	CXM.Valor,
	case  CXM.Tipo
	when 'E' THEN 'Entrada'
	when 'S' then 'Saida'
	end 
	as 
	 'Tipo Movimento'
	 
	 from CaixaMovimento  CXM
	 LEFT JOIN FormaPagamento FP ON FP.Codigo = CXM.CodFormaPagamento
	 LEFT JOIN Caixa          CX ON CX.Codigo = CXM.CodCaixa

where 
  CXM.CodCaixa          = @CodCaixa and
--  CXM.CodFormaPagamento = @CodFormaPagamento and
  CXM.Data BETWEEN @DataInicio AND @DataFim AND
  CXM.Tipo = @Tipo
  end
GO
/****** Object:  StoredProcedure [dbo].[spObterCancelamentoPorPessoa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterCancelamentoPorPessoa]
@Codigo int

as
  begin
    select 
	M.Nome as ' Motivo Cancelamento ' ,
	H.Motivo as ' Observação ',
	H.Data as ' Data '
	from HistoricoCancelamentos H
	left join MotivoCancelamento M on M.Codigo = H.CodMotivo 
	where H.CodPessoa = @Codigo
  end
GO
/****** Object:  StoredProcedure [dbo].[spObterClientesSemPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterCodigoMesa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterCodigoMesa]
@NumeroMesa int
 as 
   begin
    select Codigo,NumeroMesa from Mesas
	  where NumeroMesa = @NumeroMesa
   end


GO
/****** Object:  StoredProcedure [dbo].[spObterConfiguracao]    Script Date: 22/08/2015 12:02:33 ******/
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
 ,isnull(RepeteUltimoPedido,0) as RepeteUltimoPedido
 ,isnull(RegistraCancelamentos,0) as RegistraCancelamentos
 
 from Configuracao


GO
/****** Object:  StoredProcedure [dbo].[spObterDados]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterDados]
@Data date
as
select * from XSistemas
where Data= @Data

GO
/****** Object:  StoredProcedure [dbo].[spObterDadosCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterDadosCaixa]
@Data date,
@Numero varchar(10)
 as 
   begin
     select
	   ISNULL(Codigo,0) as Codigo,
	   ISNULL(Numero,1) as Numero,
	   ISNULL(Data,getdate()) as Data,
	   ISNULL(CodUsuario,0) as CodUsuario,
	   ISNULL(Historico,'') as Historico,
	   ISNULL(ValorAbertura,0) as ValorAbertura,
	   ISNULL(ValorFechamento,0) as ValorFechamento,
	   ISNULL(Estado,0) as Estado
   from 
   Caixa
  where Data = @Data
    and Numero = @Numero 
   end

GO
/****** Object:  StoredProcedure [dbo].[spObterDadosCaixaPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterDadosCaixaPorCodigo]
@Numero varchar(10),
@Data  datetime
 as 
   begin
     select
	  CX.Codigo,
	  CX.Data,
	  CX.Historico,
	  CX.Numero,
	  CX.ValorAbertura,
	  CX.Estado,
	  ISNULL(CX.ValorFechamento,0) AS 'ValorFechamento',
	  US.Nome as 'Usuario Abertura'
   from 
   Caixa CX
   left join Usuario US on US.Cod = CX.CodUsuario
  where 
   CX.Estado = 0 AND
   CX.Numero = @Numero and

   CX.Data =@Data

   end

GO
/****** Object:  StoredProcedure [dbo].[spObterEmpresa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterEmpresa]


as 

Select 
*
 from Empresa






GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPorCep]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterEntregadores]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterFormaPagamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterFormaPagamento]
as 
begin
select 
ISNull(Codigo,1) as Codigo,
ISNull(Descricao,'Dinheiro') as Descricao,
ISNull(ParcelaSN ,0) as ParcelaSN
from FormaPagamento
end



GO
/****** Object:  StoredProcedure [dbo].[spObterFPPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterFPPorCodigo]
@Codigo int
as
 select 
 ISNULL(Codigo,0) as Codigo,
 ISNULL(Descricao,'Dinheiro') as Descricao,
 ISNULL(ParcelaSN,0) as ParcelaSN
 from FormaPagamento
 where Codigo = @Codigo


 

GO
/****** Object:  StoredProcedure [dbo].[spObterGrupo]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterItemsExtrasPorPedido]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsExtrasPorPedido]
		@CodPedido int
	AS
		SELECT CodPedido,Descricao,Valor FROM ItemsExtras WHERE CodPedido = @CodPedido



GO
/****** Object:  StoredProcedure [dbo].[spObterItemsNaoImpresso]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterItemsNaoImpresso]
	@Codigo int	
as
	BEGIN
		SELECT *
		FROM ItemsPedido WHERE CodPedido = @Codigo 
		and ImpressoSN = 0
		ORDER BY Codigo asc
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterItemsPedido]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsPedido]
	@Codigo int	
as
	BEGIN
		SELECT *
		FROM ItemsPedido WHERE CodPedido = @Codigo ORDER BY Codigo asc
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterItemsPedidoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsPedidoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			i.Quantidade,
			i.Item,
			p.CodigoMesa
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo 
		ORDER BY Codigo
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsVendidos]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterItemsVendidos]
as
	BEGIN
	select * from vwObterItemsVendidos
 -- group by CodProduto,NomeProduto,RealizadoEm,Quantidade,PrecoItem
	END
	
	--select * from Pedido where RealizadoEm between '17-09-2014 20:32:55.347' and '17-09-2014 20:32:55.347'
	
	




GO
/****** Object:  StoredProcedure [dbo].[spObterMaiorCEP]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterMaiorCEP]
 as  
   begin
    select max(id) from base_cep
   end


GO
/****** Object:  StoredProcedure [dbo].[spObterMensages]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterMesas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[spObterMesas]
as
	BEGIN
		SELECT 
		* From 
		 Mesas
	   ORDER BY Codigo 
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterMesasAbertas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterMesasAbertas]
as
  begin
  select * from Mesas where StatusMesa=1
  end

GO
/****** Object:  StoredProcedure [dbo].[spObterMotivoCancelamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterMotivoCancelamento]
as
  begin
    select 
	ISNULL(Codigo,0) as Codigo,
	ISNULL(Nome ,'') as Nome,
	ISNULL(DataCadastro,Getdate()) as DataCadastro 
	from MotivoCancelamento
  end


GO
/****** Object:  StoredProcedure [dbo].[spObterNomeProduto]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterNumeroMesa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterNumeroMesa]
@Codigo int
 as 
   begin
    select NumeroMesa from Mesas
	  where Codigo = @Codigo
   end


GO
/****** Object:  StoredProcedure [dbo].[spObterPedido]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT P.Nome ,Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END


	select * from FormaPagamento
GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoCancelado]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidoEmAbertoApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedidoEmAbertoApp]
as
	BEGIN
		select 
			p.Codigo,
			p.CodPessoa,
			p.CodigoMesa,
			p.TotalPedido,
			p.RealizadoEm,
			p.CodigoMesa			
		from 
			Pedido p
			inner join Pessoa pe on p.CodPessoa = pe.Codigo
			inner join Mesas m on p.CodigoMesa = m.Codigo
		where
			p.Finalizado = 0
			and p.status = 'Aberto'
	END


GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoFinalizado]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedidoFinalizado]
as
	BEGIN
		SELECT PE.* ,P.Nome
		FROM Pedido PE
		Left join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 1 ORDER BY PE.Codigo DESC
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedidoPorCodigo]
@Codigo int
as
	BEGIN
		SELECT P.Nome ,
		ISNULL(Pe.Codigo,0) as Codigo,
		ISNULL(Pe.CodPessoa,0) as CodPessoa,
		ISNULL(Pe.TotalPedido,0) as TotalPedido,
		ISNULL(Pe.TrocoPara,0) as TrocoPara,
		ISNULL(Pe.FormaPagamento,'Dinheiro') as FormaPagamento,
		ISNULL(Pe.Finalizado,0) as Finalizado,
		ISNULL(Pe.RealizadoEm,GETDATE()) as RealizadoEm,
		ISNULL(Pe.Tipo,0) as Tipo,
		ISNULL(Pe.NumeroMesa,0) as NumeroMesa,
		ISNULL(Pe.status,'Aberto') as status,
		ISNULL(Pe.PedidoOrigem,'Balcao') as PedidoOrigem,
		ISNULL(Pe.CodigoMesa,0) as CodigoMesa,
		ISNULL(Pe.CodUsuario,0) as CodUsuario,
		ISNULL(Pe.DescontoValor,0) as DescontoValor,
		ISNULL(Pe.CodMotoboy,0) as CodMotoboy,
		ISNULL(PE.MargemGarcon,0) as MargemGarcon

		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorData]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidoSemMesas]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidosPessoaPorData]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterPedidosPessoaPorData]
@CodPessoa int ,
@DataInicio date,
@DataFim date 
as
 begin

select 
   P.Codigo as 'Codigo Pedido',
   cast(P.RealizadoEm as date) as 'Data do Pedido ',
   TotalPedido ,
   Tipo,
   [status]
from 
Pedido P
where 
  CodPessoa = @CodPessoa and
  RealizadoEm between  @DataInicio and @DataFim

end

GO
/****** Object:  StoredProcedure [dbo].[spObterPermissao]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterPermissao]
@CodUsuario int
as
 begin
  select * from UsuarioPermissao
  where CodUsuario = @CodUsuario
 end


GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,Telefone2,DataNascimento,TicketFidelidade,CodRegiao
  FROM Pessoa WHERE Codigo = @Codigo
 END


GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorTelefone]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone nvarchar(100)

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,Numero,DataNascimento,CodRegiao
  FROM Pessoa WHERE Telefone = @Telefone or Telefone2=@Telefone
 END


GO
/****** Object:  StoredProcedure [dbo].[spObterPessoas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoas]
as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,DataNascimento,CodRegiao,DataCadastro
  FROM Pessoa 
 END


GO
/****** Object:  StoredProcedure [dbo].[spObterPessoasApp]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoasApp]
as
 BEGIN
  SELECT Codigo,Nome,Telefone FROM Pessoa P
 Where P.Observacao ='Cadastrado via App' 
 END


-- select * from Pessoa

GO
/****** Object:  StoredProcedure [dbo].[spObterProduto]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProduto]
as
BEGIN
SELECT *
FROM Produto 
where AtivoSN=1 ORDER BY Codigo ASC
END


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoCompleto]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana
	
	FROM Produto WHERE AtivoSN= 1 and Codigo = @Codigo ;


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,DiaSemana,PrecoDesconto
	FROM Produto WHERE  AtivoSN= 1 and Codigo = @Codigo;


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorGrupo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoPorGrupo]
	@GrupoProduto nvarchar(50)
as
	SELECT Codigo,NomeProduto,PrecoProduto
	FROM Produto WHERE AtivoSN= 1 and GrupoProduto = @GrupoProduto;


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutosAtivosSemDesconto]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterProdutosAtivosSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN
FROM Produto 
where AtivoSN=1 ORDER BY Codigo ASC
END



GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoSemDesconto]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN
FROM Produto 
where AtivoSN=1 ORDER BY Codigo ASC
end


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutosInativos]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterProdutosInativos]
as
BEGIN
SELECT *
FROM Produto 
where AtivoSN=0 ORDER BY Codigo ASC
END



GO
/****** Object:  StoredProcedure [dbo].[spObterProdutosInativosSemDesconto]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutosInativosSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN
FROM Produto 
where AtivoSN=0 ORDER BY Codigo ASC
END



GO
/****** Object:  StoredProcedure [dbo].[spObterRegioes]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterRegioes]
   as
   begin
    Select * from RegiaoEntrega
   end


GO
/****** Object:  StoredProcedure [dbo].[spObterRegioesPorCodigo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterRegioesPorCodigo]
  @Codigo nvarchar(10)
   as
   begin
    Select * from RegiaoEntrega 
	where Codigo = @Codigo
   end


GO
/****** Object:  StoredProcedure [dbo].[spObterRegioesPorNome]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterRegioesPorNome]
  @NomeRegiao nvarchar(10)
   as
   begin
    Select * from RegiaoEntrega 
	where NomeRegiao = @NomeRegiao
   end


GO
/****** Object:  StoredProcedure [dbo].[spObterTaxaPorCliente]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterTaxaPorCliente]
@Codigo int
as
  begin
    select R.TaxaServico from RegiaoEntrega R
	left join Pessoa P on R.Codigo = P.CodRegiao
	where P.Codigo = @Codigo 
	    
	 end



GO
/****** Object:  StoredProcedure [dbo].[spObterUsuario]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterUsuario]
as begin
select 
Cod as Codigo,
ISNULL(Nome,0) as Nome,
ISNULL(Senha,0) as Senha,
ISNULL(CancelaPedidosSN,0) as CancelaPedidosSN,
ISNULL(AlteraProdutosSN,0) as AlteraProdutosSN,
ISNULL(AdministradorSN,0) as AdministradorSN,
ISNULL(AcessaRelatoriosSN,0) as AcessaRelatoriosSN,
ISNULL(DescontoPedidoSN,0) as DescontoPedidoSN,
ISNULL(FinalizaPedidoSN,0) as FinalizaPedidoSN,
ISNULL(DescontoMax,0) as DescontoMax
 from Usuario
end


GO
/****** Object:  StoredProcedure [dbo].[spObterUsuarioGrid]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterUsuarioGrid]
as begin
select 
ISNULL(Nome,0) as Nome,
ISNULL(Senha,0) as Senha,
--ISNULL(CancelaPedidosSN,0) as CancelaPedidosSN,
ISNULL(AlteraProdutosSN,0) as AlteraProdutosSN,
ISNULL(AdministradorSN,0) as AdministradorSN,
ISNULL(AcessaRelatoriosSN,0) as AcessaRelatoriosSN,
ISNULL(DescontoPedidoSN,0) as DescontoPedidoSN

 from Usuario
end


GO
/****** Object:  StoredProcedure [dbo].[spObterViasDoPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  StoredProcedure [dbo].[spRemoveDezPorCento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spRemoveDezPorCento]
@Codigo int ,
@MargemGarcon decimal(10,2),
@TotalPedido decimal(10,2)
as
 begin
  update Pedido 
  set 
  TotalPedido = @TotalPedido,
  MargemGarcon = @MargemGarcon
  where Codigo = @Codigo
 end
GO
/****** Object:  StoredProcedure [dbo].[spSinalizarPedidoConcluido]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int	
AS
	Update Pedido SET Finalizado = 1 WHERE Codigo = @Codigo



GO
/****** Object:  StoredProcedure [dbo].[spTotaisCaixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spTotaisCaixa]
@Numero int,
@Data   date
as
  begin
select 
 Cx.CodCaixa,
 Fp.Descricao ,
 sum(cx.Valor) as 'Total Somado'
 from CaixaMovimento CX
 left join FormaPagamento FP on FP.Codigo = Cx.CodFormaPagamento
 where 
 CX.CodCaixa = @Numero AND
 CX.Data   = @Data
 and CX.Tipo='E'
 group by CodCaixa,Fp.Descricao
 
 end


 --select * from CaixaMovimento
GO
/****** Object:  StoredProcedure [dbo].[spZerarFidelidade]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[APagar]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[APagarParcelas]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[AReceber]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[AReceberParcelas]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[base_cep]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_cep](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[cep] [nvarchar](10) NULL,
	[Logradouro] [nvarchar](max) NULL,
	[cidade] [nvarchar](100) NULL,
	[bairro] [nvarchar](100) NULL,
	[estado] [char](2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Caixa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Caixa](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NOT NULL,
	[CodUsuario] [int] NULL,
	[Historico] [nvarchar](max) NULL,
	[Numero] [nvarchar](10) NOT NULL,
	[ValorAbertura] [decimal](10, 2) NULL,
	[ValorFechamento] [decimal](10, 2) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK01_CAIXA] PRIMARY KEY CLUSTERED 
(
	[Numero] ASC,
	[Data] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CaixaBanco]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[CaixaCadastro]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaixaCadastro](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [nvarchar](10) NULL,
	[Nome] [nvarchar](50) NULL,
	[DataCadastro] [datetime] NULL,
 CONSTRAINT [PK01_CodCaixaCadastro] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK01_CodCaixaCadastro] UNIQUE NONCLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CaixaDiferenca]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CaixaDiferenca](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCaixa] [nvarchar](10) NULL,
	[Data] [date] NULL,
	[ValorSomado] [decimal](10, 2) NULL,
	[ValorInformado] [decimal](10, 2) NULL,
	[ValorDiferenca] [decimal](10, 2) NULL,
	[CodUsuario] [int] NOT NULL,
	[Tipo] [char](1) NULL,
 CONSTRAINT [PK01_CaixaDiferenca] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CaixaMovimento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CaixaMovimento](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodCaixa] [nvarchar](10) NULL,
	[Data] [datetime] NULL,
	[Historico] [nvarchar](100) NULL,
	[NumeroDocumento] [nvarchar](50) NULL,
	[CodFormaPagamento] [int] NULL,
	[Valor] [decimal](10, 2) NULL,
	[Tipo] [char](1) NULL,
	[CodUsuario] [int] NULL,
 CONSTRAINT [PK01_CAIXAMOVIMENTO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Configuracao]    Script Date: 22/08/2015 12:02:33 ******/
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
	[RepeteUltimoPedido] [bit] NULL,
	[RegistraCancelamentos] [bit] NULL,
 CONSTRAINT [Pk_cod_Config] PRIMARY KEY CLUSTERED 
(
	[cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 22/08/2015 12:02:33 ******/
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
	[strConnectionResult] [nvarchar](max) NOT NULL,
	[VersaoBanco] [varchar](max) NULL,
	[DataInicio] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Entregador]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[Estoque]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[EventosSistema]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventosSistema](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodUsuario] [int] NOT NULL,
	[TipoEvento] [nvarchar](10) NULL,
	[DataEvento] [datetime] NULL,
	[LocalEvento] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormaPagamento]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[Grupo]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NomeGrupo] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GrupoPessoas]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[HistoricoCancelamentos]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoricoCancelamentos](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NOT NULL,
	[Motivo] [nvarchar](100) NULL,
	[CodMotivo] [int] NULL,
	[Data] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemsPedido]    Script Date: 22/08/2015 12:02:33 ******/
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
	[Item] [nvarchar](max) NULL,
	[ImpressoSN] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mensagens]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[Mesas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesas](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NumeroMesa] [nvarchar](10) NOT NULL,
	[CodCliente] [int] NULL,
	[StatusMesa] [nvarchar](10) NOT NULL,
	[DataAtualizacao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MotivoCancelamento]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MotivoCancelamento](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[DataCadastro] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdemServico]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[Pago]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[Pedido]    Script Date: 22/08/2015 12:02:33 ******/
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
	[PedidoOrigem] [nvarchar](10) NULL,
	[CodigoMesa] [int] NULL,
	[CodUsuario] [int] NULL,
	[DescontoValor] [numeric](10, 2) NULL,
	[CodMotoboy] [int] NULL,
	[MargemGarcon] [decimal](10, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 22/08/2015 12:02:33 ******/
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
	[Telefone2] [nvarchar](20) NULL,
	[CodRegiao] [int] NULL,
 CONSTRAINT [PK_CODPESSOA] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 22/08/2015 12:02:33 ******/
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
	[AtivoSN] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produtos_Categoria]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos_Categoria](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[CodProduto] [int] NOT NULL,
	[Valor] [decimal](10, 2) NULL,
 CONSTRAINT [PK01_Produtos_Categoria] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Receber]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[Recebido]    Script Date: 22/08/2015 12:02:33 ******/
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
/****** Object:  Table [dbo].[RegiaoEntrega]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegiaoEntrega](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Bairro] [nvarchar](100) NULL,
	[TaxaServico] [decimal](10, 2) NULL,
	[NomeRegiao] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegioesEntrega]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegioesEntrega](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](20) NULL,
	[ValorEntrega] [decimal](10, 2) NULL,
	[DataCadastro] [datetime] NULL,
 CONSTRAINT [PK01_RegiaoEntrega] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Cod] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[Senha] [nvarchar](128) NULL,
	[CancelaPedidosSN] [bit] NULL,
	[AlteraProdutosSN] [bit] NULL,
	[AdministradorSN] [bit] NULL,
	[AcessaRelatoriosSN] [bit] NULL,
	[DescontoPedidoSN] [bit] NULL,
	[FinalizaPedidoSN] [bit] NULL,
	[DescontoMax] [numeric](10, 2) NULL,
 CONSTRAINT [Pk_Cod_Usuario] PRIMARY KEY CLUSTERED 
(
	[Cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[XSistemas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XSistemas](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NULL,
	[RegistroMD5] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[vwObterEmpresa]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vwObterEmpresa]
as 
  select Emp.Nome,Emp.Telefone,Telefone2,
  Endereco,Numero,Bairro,Cidade, PontoReferencia
   from Empresa Emp

GO
/****** Object:  View [dbo].[vwObterItemsVendidos]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vwObterItemsVendidos]
as
select 
(select Codigo from Produto P where P.Codigo = I.CodProduto ) as CodProduto,
(select P.NomeProduto from Produto P where P.Codigo = I.CodProduto) as NomeProduto,
Sum(Quantidade) as Quantidade,
Sum(I.PrecoTotalItem) as PrecoTotalItem,
cast(RealizadoEm as date) as RealizadoEM
from 
Pedido P
join ItemsPedido I on I.CodPedido = P.Codigo
where P.Finalizado=1 and P.status='Aberto'
group by CodProduto,cast(RealizadoEM as date)

  



GO
/****** Object:  View [dbo].[vwObterPedidosFinalizados]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create VIEW [dbo].[vwObterPedidosFinalizados]
as
select 
PD.Codigo,
P.Nome,
TotalPedido,
FormaPagamento,
RealizadoEm,
PD.Tipo
 from Pedido PD
 join Pessoa P on P.Codigo = PD.CodPessoa
where Finalizado=1 and [status]='Aberto'

  


GO
/****** Object:  View [dbo].[vwObterPedidosPorBoy]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vwObterPedidosPorBoy]
as
select 
count(P.CodMotoboy) as QuantidadeEntregas,
cast(P.RealizadoEm as date) as RealizadoEm,
(select Codigo from Entregador E where P.CodMotoboy = E.Codigo) as CodMotoboy, 
(select Nome from Entregador E where P.CodMotoboy = E.Codigo) as Nome 
from
Pedido P
where P.Finalizado =1
group by P.CodMotoboy,cast(P.RealizadoEm as date)






GO
/****** Object:  View [dbo].[vwObterXSistemas]    Script Date: 22/08/2015 12:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create view [dbo].[vwObterXSistemas]
 as
 select count(Codigo) as Contador from XSistemas

GO
ALTER TABLE [dbo].[AReceber] ADD  DEFAULT ((0)) FOR [Quitado]
GO
ALTER TABLE [dbo].[CaixaCadastro] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Configuracao] ADD  DEFAULT ((0)) FOR [CobraTaxaGarcon]
GO
ALTER TABLE [dbo].[Configuracao] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Estoque] ADD  DEFAULT ('0.00') FOR [QuantidadeEstoque]
GO
ALTER TABLE [dbo].[EventosSistema] ADD  DEFAULT (getdate()) FOR [DataEvento]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_Finalizado]  DEFAULT ((0)) FOR [Finalizado]
GO
ALTER TABLE [dbo].[Pessoa] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT ((0)) FOR [AtivoSN]
GO
ALTER TABLE [dbo].[Receber] ADD  DEFAULT ((0)) FOR [Quitado]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [DescontoPedidoSN]
GO
ALTER TABLE [dbo].[Caixa]  WITH CHECK ADD  CONSTRAINT [FK01_CODUSERCAIXA] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[Caixa] CHECK CONSTRAINT [FK01_CODUSERCAIXA]
GO
ALTER TABLE [dbo].[Caixa]  WITH CHECK ADD  CONSTRAINT [FK02_NUMCAIXA] FOREIGN KEY([Numero])
REFERENCES [dbo].[CaixaCadastro] ([Numero])
GO
ALTER TABLE [dbo].[Caixa] CHECK CONSTRAINT [FK02_NUMCAIXA]
GO
ALTER TABLE [dbo].[CaixaDiferenca]  WITH CHECK ADD  CONSTRAINT [FK01_NumCaixa] FOREIGN KEY([NumeroCaixa])
REFERENCES [dbo].[CaixaCadastro] ([Numero])
GO
ALTER TABLE [dbo].[CaixaDiferenca] CHECK CONSTRAINT [FK01_NumCaixa]
GO
ALTER TABLE [dbo].[CaixaDiferenca]  WITH CHECK ADD  CONSTRAINT [FK02_CodUsuario] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[CaixaDiferenca] CHECK CONSTRAINT [FK02_CodUsuario]
GO
ALTER TABLE [dbo].[CaixaMovimento]  WITH CHECK ADD  CONSTRAINT [FK02_CodFormaPagamento] FOREIGN KEY([CodFormaPagamento])
REFERENCES [dbo].[FormaPagamento] ([Codigo])
GO
ALTER TABLE [dbo].[CaixaMovimento] CHECK CONSTRAINT [FK02_CodFormaPagamento]
GO
ALTER TABLE [dbo].[CaixaMovimento]  WITH CHECK ADD  CONSTRAINT [FK03_CodUsuario] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[CaixaMovimento] CHECK CONSTRAINT [FK03_CodUsuario]
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_PRODUTO] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_CODIGO_PRODUTO]
GO
ALTER TABLE [dbo].[EventosSistema]  WITH CHECK ADD  CONSTRAINT [FK_CODUSEREVENTO] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[EventosSistema] CHECK CONSTRAINT [FK_CODUSEREVENTO]
GO
ALTER TABLE [dbo].[HistoricoCancelamentos]  WITH CHECK ADD  CONSTRAINT [FK_CODMOTIVO] FOREIGN KEY([CodMotivo])
REFERENCES [dbo].[MotivoCancelamento] ([Codigo])
GO
ALTER TABLE [dbo].[HistoricoCancelamentos] CHECK CONSTRAINT [FK_CODMOTIVO]
GO
ALTER TABLE [dbo].[HistoricoCancelamentos]  WITH CHECK ADD  CONSTRAINT [FK_CODPESSOA_CANCELAMENTO] FOREIGN KEY([CodPessoa])
REFERENCES [dbo].[Pessoa] ([Codigo])
GO
ALTER TABLE [dbo].[HistoricoCancelamentos] CHECK CONSTRAINT [FK_CODPESSOA_CANCELAMENTO]
GO
ALTER TABLE [dbo].[ItemsPedido]  WITH CHECK ADD  CONSTRAINT [FK_CODIGO_PRODUTO_ITEM] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[ItemsPedido] CHECK CONSTRAINT [FK_CODIGO_PRODUTO_ITEM]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([CodigoMesa])
REFERENCES [dbo].[Mesas] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([CodMotoboy])
REFERENCES [dbo].[Entregador] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_CODUSER] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_CODUSER]
GO
ALTER TABLE [dbo].[Produtos_Categoria]  WITH CHECK ADD  CONSTRAINT [FK01_Produtos_Categoria] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Produtos_Categoria] CHECK CONSTRAINT [FK01_Produtos_Categoria]
GO
USE [master]
GO
ALTER DATABASE [DBExpert_Teste] SET  READ_WRITE 
GO
