USE [master]
GO
/****** Object:  Database [xSistemas_Desenvolvimento]    Script Date: 06/06/2017 14:26:21 ******/
CREATE DATABASE [xSistemas_Desenvolvimento]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'xSistemas_Desenvolvimento', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\xSistemas_Desenvolvimento.mdf' , SIZE = 12352KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'xSistemas_Desenvolvimento_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\xSistemas_Desenvolvimento_log.ldf' , SIZE = 1344KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [xSistemas_Desenvolvimento].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET ARITHABORT OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET  DISABLE_BROKER 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET  MULTI_USER 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET DB_CHAINING OFF 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [xSistemas_Desenvolvimento]
GO
/****** Object:  User [digital]    Script Date: 06/06/2017 14:26:21 ******/
CREATE USER [digital] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  StoredProcedure [dbo].[AlteraFidelidade]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[ObterFidelidade]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[sbObterUltimoPedido]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sbObterUltimoPedido]
  @CodPessoa int
  as
   begin
	select 
	top 1 P.Codigo,
		 P.CodPessoa,
		 TotalPedido,
		 (select FormaPagamento from Pedido PS where PS.Codigo = p.Codigo) as FP, 
		RealizadoEm,
		(select top 1 Codigo from Pessoa_Endereco where CodPessoa = P.CodPessoa order by Codigo asc) as CodEndereco
	from Pedido P
	left join Pessoa_Endereco PE on PE.CodPessoa=P.CodPessoa
	where P.CodPessoa =@CodPessoa and Finalizado =1
	order by RealizadoEm desc
  end




GO
/****** Object:  StoredProcedure [dbo].[spAbrirCaixa]    Script Date: 06/06/2017 14:26:21 ******/
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
 @Numero varchar(10),
 @Turno varchar(5),
 @HorarioFechamento datetime
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura,Numero,Turno,HorarioAbertura,HorarioFechamento) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura,@Numero,@Turno,GetDate(),@HorarioFechamento) 
   end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionaBairrosRegiao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionaBairrosRegiao]
@CodRegiao int,
@Nome  nvarchar(100),
@CEP  nvarchar(10),
@DataCadastro datetime,
@AtivoSN bit,
@OnlineSN bit
	as
	 begin
	 Insert into RegiaoEntrega_Bairros (CodRegiao,Nome,Cep,DataCadastro,AtivoSN,OnlineSN) 
	        values (@CodRegiao,@Nome,@Cep,@DataCadastro,@AtivoSN,@OnlineSN)
	 end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionaDiferenca]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionaHistorico]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionaHistorico]
@CodPessoa int,
@Tipo char(1),
@Valor     decimal(10,2), 
@Data      date,
@Historico nvarchar(100),
@CodUsuario int 
as 
 begin
   insert into HistoricoPessoa (CodPessoa,Tipo,Valor,Data,Historico,CodUsuario)
               values (@CodPessoa,@Tipo,@Valor,@Data,@Historico,@CodUsuario)
 end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionaHistoricoCancelamento]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionaHistoricoCancelamento]
@CodPessoa int,
@Motivo nvarchar(100),
@CodMotivo int,
@Data date,
@CodPedido int
as
  begin
  insert into HistoricoCancelamentos (CodPessoa,Motivo,CodMotivo,Data,CodPedido)
         values (@CodPessoa,@Motivo,@CodMotivo,@Data,@CodPedido)

  end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionaMotivoCancelamento]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionar_InsumoEstoque]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionar_InsumoEstoque]
@CodInsumo int,
@Quantidade decimal
as 
 begin
    insert into Insumo_Estoque (CodInsumo,Quantidade,DataAlteracao)
	                    values (@CodInsumo,@Quantidade,Getdate()) 
 end
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarCaixa]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarCep]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarClienteDelivery]    Script Date: 06/06/2017 14:26:21 ******/
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
@DataCadastro Datetime,
@user_id nvarchar(max),
@DDD char(2),
@Sexo char(2),
@PFPJ char(1),
@CodOrigemCadastro int
as 
begin
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2,DataNascimento,
			TicketFidelidade,CodRegiao,DataCadastro,[user_id],DDD,Sexo,PFPJ,CodOrigemCadastro)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2,@DataNascimento,
			@TicketFidelidade,@CodRegiao,@DataCadastro,@user_id,@DDD,@Sexo,@PFPJ,@CodOrigemCadastro)
SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo

end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarConfiguracao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarConfiguracao]
@ImpViaCozinha nvarchar(max),
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo nvarchar(max),
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega nvarchar(max),
@ImprimeViaBalcao nvarchar(max),
@ControlaFidelidade nvarchar(max),
@DescontoDiaSemana bit,
@CobraTaxaGarcon bit,
@EnviaSMS bit,
@RepeteUltimoPedido bit,
@RegistraCancelamentos bit,
@DadosApp nvarchar(max),
@CidadesAtendidas nvarchar(max),
@ExigeVendedorSN bit,
@CobrancaProporcionalSN bit,
@DadosPush nvarchar(max),
@Impressoras nvarchar(max)
as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,
                          ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,
						  ImprimeViaEntrega,ImpressoraCopaBalcao ,ControlaFidelidade,DescontoDiaSemana,
						  CobraTaxaGarcon ,EnviaSMS,
						  RepeteUltimoPedido,RegistraCancelamentos,DadosApp,
						  CidadesAtendidas,ExigeVendedorSN,
						  CobrancaProporcionalSN,DadosPush,Impressoras)
						  values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,
							@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,
							@ImprimeViaEntrega,@ImprimeViaBalcao,@ControlaFidelidade,@DescontoDiaSemana,
							@CobraTaxaGarcon,@EnviaSMS,
							@RepeteUltimoPedido,@RegistraCancelamentos,@DadosApp,
							@CidadesAtendidas,@ExigeVendedorSN,
							@CobrancaProporcionalSN,@DadosPush,@Impressoras)
	end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarDescontoSemana]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionaRegiao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionaRegiao]

	@NomeRegiao nvarchar(8),
	@TaxaServico decimal(10,2),
	@DataAlteracao datetime,
	@OnlineSN bit,
	@AtivoSN bit,
	@valorMinimoFreteGratis decimal(10,2),
	@PrevisaoEntrega nvarchar(10)
	
as
	BEGIN
		INSERT INTO RegiaoEntrega(NomeRegiao,TaxaServico,DataAlteracao,OnlineSN,AtivoSN,valorMinimoFreteGratis,PrevisaoEntrega)
		Values(@NomeRegiao,@TaxaServico,@DataAlteracao,@OnlineSN,@AtivoSN,@valorMinimoFreteGratis,@PrevisaoEntrega)
	END



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEmpresa]    Script Date: 06/06/2017 14:26:21 ******/
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
@CaminhoBackup nvarchar(max),
@UrlServidor nvarchar(max),
@HorarioFuncionamento nvarchar(max),
@ConfiguracaoSMS nvarchar(max)

as 
Insert into Empresa (nome,CNPJ,Telefone ,Telefone2,Contato,Cep,
					Endereco,Cidade,Bairro,Numero,UF,PontoReferencia,
					Servidor,Banco,DataInicio,VersaoBanco,CaminhoBackup,UrlServidor,HorarioFuncionamento,ConfiguracaoSMS)
            Values (@Nome,@CNPJ,@Telefone,@Telefone2,@Contato,@Cep,
			       @Endereco,@Cidade,@Bairro,@Numero,@UF,@PontoReferencia,
				   @Servidor,@Banco,@DataInicio,@VersaoBanco,@CaminhoBackup,@UrlServidor,@HorarioFuncionamento,@ConfiguracaoSMS)



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEmpresa_HorarioEntrega]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarEmpresa_HorarioEntrega]
@Limite_horario_pedido nvarchar(6),
@Horario_entrega nvarchar(20),
@OnlineSN bit
as
 begin
 insert into Empresa_HorarioEntrega (Limite_horario_pedido,Horario_entrega,OnlineSN)
         values  (@Limite_horario_pedido,@Horario_entrega,@OnlineSN)
 end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEndereco]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionarEndereco]
@Codigo int output,
@CodPessoa int,
@Cep char(9),
@Endereco nvarchar(max),
@Complemento nvarchar(50),
@PontoReferencia nvarchar(100),
@Bairro nvarchar(100),
@Cidade nvarchar(100),
@UF char(2),
@Numero nvarchar(10),
@CodRegiao int,
@NomeEndereco nvarchar(max)
as
 begin
  insert into Pessoa_Endereco (CodPessoa,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,NomeEndereco)
      values (@CodPessoa,@Cep,@Endereco,@Complemento,@PontoReferencia,@Bairro,@Cidade,@UF,@Numero,@CodRegiao,@NomeEndereco)
	    SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
 end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarEntregador]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarEvento]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarFamilia]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarFamilia]
@Nome   nvarchar(max) ,
@AtivoSN bit,
@OnlineSN bit,
@DataAlteracao datetime,
@PaiSN bit
as 
  begin
     insert into Grupo (NomeGrupo,AtivoSN,OnlineSN,DataAlteracao,PaiSN)
           values (@Nome,@AtivoSN,@OnlineSN,@DataAlteracao,@PaiSN)
  end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarFinalizaPedido_Pedido]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarFinalizaPedido_Pedido]
@CodPedido int,
@CodPagamento int,
@ValorPagamento decimal(10,2)
as 
 begin
   insert into Pedido_Finalizacao values (@CodPedido,@CodPagamento,@ValorPagamento)
 end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarFormaPagamento]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarFormaPagamento]
@Descricao nvarchar(100),
@DescontoSN bit,
@GeraFinanceiro bit,
@OnlineSN bit,
@DataAlteracao datetime,
@CaminhoImagem nvarchar(max),
@AtivoSN bit

as
begin 
Insert into FormaPagamento(Descricao,ParcelaSN,GeraFinanceiro,OnlineSN,DataAlteracao,CaminhoImagem,AtivoSN)
            Values (@Descricao,@DescontoSN,@GeraFinanceiro,@OnlineSN,@DataAlteracao,@CaminhoImagem,@AtivoSN)

end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarGrupo]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarGrupo]

	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int,
	@MultiploSabores nvarchar(max)
	
as
	BEGIN
		INSERT INTO Grupo(NomeGrupo,ImprimeCozinhaSN,OnlineSN,DataAlteracao,AtivoSN,NomeImpressora,CodFamilia,MultiploSabores)
		Values(@NomeGrupo,@ImprimeCozinhaSN,@OnlineSN,@DataAlteracao,@AtivoSN,@NomeImpressora,@CodFamilia ,@MultiploSabores)
	END



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarInsumo]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarInsumo]
@Nome nvarchar(max),
@UnidadeMedida char(4),
@AtivoSN bit,
@Preco decimal
as 
 begin
  insert into Insumo (Nome,UnidadeMedida,AtivoSN,DataCadastro,DataAlteracao,Preco) 
           values (@Nome,@UnidadeMedida,@AtivoSN,Getdate(),Getdate(),@Preco) 
 end

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedido]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAdicionarItemAoPedido]
	@CodPedido int,
	@CodProduto int,
	@NomeProduto nvarchar(max),
	@Quantidade decimal(10,2),
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max),
	@ImpressoSN bit	,
	@DataAtualizacao datetime
as
	BEGIN
		INSERT INTO ItemsPedido(CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
		VALUES(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN)
		
		exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
	END




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedidoApp]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAdicionarItemAoPedidoApp]
	@CodigoMesa int,
	@CodProduto int,
	@CodUsuario int,
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
			INSERT INTO ItemsPedido(CodPedido, CodProduto, CodUsuario, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN)
			VALUES(@CodPedido, @CodProduto, @CodUsuario, @NomeProduto, cast(@Quantidade as decimal(10,2)), @PrecoUnitario, @PrecoTotal, @Item,0)
			exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
			exec spAlterarTotalPedidoApp @CodPedido
		end
	END
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemAoPedidoBalcaoApp]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAdicionarItemAoPedidoBalcaoApp]
	@CodProduto int,
	@Senha nvarchar(100),
	@CodUsuario int,
	@NomeProduto nvarchar(max),
	@Quantidade decimal(10,2),
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
	as
	BEGIN
		declare @CodPedido int
		set @CodPedido = (select max(Codigo) from Pedido where Senha =@Senha and Finalizado =0 and [status] = 'Aberto' );
		--se achou o pedido
		if (@CodPedido > 0)
		begin
			INSERT INTO ItemsPedido(CodPedido, CodProduto, CodUsuario, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN)
		                	VALUES (@CodPedido, @CodProduto, @CodUsuario, @NomeProduto, @Quantidade, @PrecoUnitario, @PrecoTotal, @Item,0)

            exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
			exec spAlterarTotalPedidoApp @CodPedido
		end
	END


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarItemExtra]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarItemVenda]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarMensagen]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarMesas]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarMesas]
@NumeroMesa nvarchar(10),
@StatusMesa int,
@AtivoSN bit,
@OnlineSN bit
  as
    begin
	 Insert into Mesas (NumeroMesa,StatusMesa,AtivoSN,OnlineSN)
	             Values (@NumeroMesa,@StatusMesa,@AtivoSN,@OnlineSN)
	end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarOpcao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarOpcao]
--@Codigo int output,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit,
@SinalOpcao nvarchar(10),
@DiasDisponivel nvarchar(max)
as
  begin
    insert into Opcao (Nome,Tipo,DataAlteracao,OnlineSN,AtivoSN,SinalOpcao,DiasDisponivel) 
	   values (@Nome,@Tipo,@DataAlteracao,@OnlineSN,@AtivoSN,@SinalOpcao,@DiasDisponivel) 
	    
  end



GO
/****** Object:  StoredProcedure [dbo].[spAdicionarOpcaoPedido]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionarOpcaoPedido]
 @CodProduto int,
 @CodPedido  int,
 @CodOpcao   int,
 @Quantidade decimal,
 @Preco      decimal(10,2),
 @Observacao nvarchar(100)
 as
   begin
      insert into Pedido_Opcao (CodProduto,CodOpcao,CodPedido,Quantidade,Preco,Observacao)
	         values (@CodProduto,@CodOpcao,@CodPedido,@Quantidade,@Preco,@Observacao)
   end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarOpcaoPedidoApp]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionarOpcaoPedidoApp]
 @CodProduto int,
 @NumeroMesa nvarchar(max),
 @CodOpcao   int,
 @Quantidade decimal,
 @Preco      decimal(10,2),
 @Observacao nvarchar(100)
 as
  
   begin
    declare  @CodPedido  int;
    set @CodPedido = (select Codigo from Pedido where Finalizado=0 and NumeroMesa=@NumeroMesa);

      insert into Pedido_Opcao (CodProduto,CodOpcao,CodPedido,Quantidade,Preco,Observacao)
	         values (@CodProduto,@CodOpcao,@CodPedido,@Quantidade,@Preco,@Observacao)
   end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarOpcaProduto]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarOpcaProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@DataAlteracao datetime,
@PrecoProcomocao decimal(10,2),
@CodTipo int
as 
  begin
     insert into Produto_Opcao ( CodProduto,CodOpcao,Preco,DataAlteracao,PrecoProcomocao,CodTipo)
	        values  ( @CodProduto,@CodOpcao,@Preco,@DataAlteracao,@PrecoProcomocao,@CodTipo) 
	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarOpcoesPedido]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarOpcoesPedido]

@CodPedido int,
@CodProduto int,
@CodOpcao  int,
@Quantidade decimal(10,2),
@Observacao nvarchar(max)
as
 begin

  insert into Pedido_OpcoesProduto 
  values (@CodPedido,@CodProduto,@CodOpcao,@Quantidade,@Observacao)
end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedido]    Script Date: 06/06/2017 14:26:21 ******/
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
    @NumeroMesa nvarchar(100),
    @Status     nvarchar(20),
    @PedidoOrigem nvarchar(10),
	@CodigoMesa int	,
	@DescontoValor decimal(10,2),
	@CodigoPedidoWS int,
	@CodUsuario int,
	@HorarioEntrega nvarchar(max),
	@Observacao nvarchar(max),
	@CodEndereco int,
    @Senha nvarchar(max),
	@PagoFidelidade bit
as
        BEGIN
		set @CodigoMesa= (select Codigo from Mesas where Codigo = @CodigoMesa)
		 if @CodigoMesa>0
		 begin
		     exec spAlteraStatusMesa @CodigoMesa,2;
		 end
		
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS,CodUsuario,HorarioEntrega,
			Observacao,CodEndereco,Senha,PagoFidelidade)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS,@CodUsuario,@HorarioEntrega,
				@Observacao,@CodEndereco,@Senha,@PagoFidelidade
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo

        END


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedidoApp]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@CodigoMesa int
	
as
	BEGIN
     	declare @NumeroMesa nvarchar(20);
		set @NumeroMesa = (select NumeroMesa from Mesas where Codigo = @CodigoMesa and StatusMesa=1)
		
		if @NumeroMesa!=''
		begin	
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa, CodUsuario)
			Values            (@CodPessoa, @TotalPedido, Getdate(),@NumeroMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa,@CodUsuario);
			SET @Codigo = SCOPE_IDENTITY()
			--Atualizando status da mesa
			exec spAlteraStatusMesa @CodigoMesa,2
		end	
		RETURN @Codigo
	END


	select * from Pedido where Codigo=2072 


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedidoBalcaoApp]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAdicionarPedidoBalcaoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@NomeCliente nvarchar(max),
	@Senha nvarchar(max)
as
	BEGIN
	   declare @DataAtual datetime;
	   set @DataAtual = Getdate();
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, Tipo, [Status], PedidoOrigem, CodUsuario,Observacao,Senha,ImpressoSN)
			Values            (@CodPessoa, @TotalPedido, @DataAtual, '2 - Balcao', 'Aberto', 'Aplicativo',@CodUsuario,@NomeCliente,@Senha,0);
			SET @Codigo = SCOPE_IDENTITY()
		RETURN @Codigo
	END

GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedidoStatus]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarPedidoStatus]
-- @Codigo int,
 @Nome nvarchar(100),
 @Status int,
  @AlertarSN bit,
 @EnviarSN bit
 --@DataAlteracao datetime
 
  as
   begin
      insert into PedidoStatus (Nome,EnviarSN,AlertarSN,[Status],DataAlteracao) 
                          values (@Nome,@EnviarSN,@AlertarSN,@Status,Getdate())
   end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedidoStatusMovimento]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarPedidoStatusMovimento]
  @CodPedido int,
  @CodStatus int,
  @CodUsuario int,
  @DataAlteracao datetime
  as 
    begin
      insert into PedidoStatusMovimento values (@CodPedido,@CodStatus,@CodUsuario,@DataAlteracao)
    end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPermissao]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoa]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoa_Fidelidade]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarPessoa_Fidelidade]
@CodPessoa int ,
@CodPedido int ,
@Ponto   int,
@Tipo     char
as
  begin
    insert into Pessoa_Fidelidade (CodPessoa,CodPedido,Ponto,Tipo) 
	       values (@CodPessoa,@CodPedido,@Ponto,@Tipo)
  end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoa_OrigemCadastro]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarPessoa_OrigemCadastro]
@Nome nvarchar(max),
@AtivoSN bit
as 
begin
  insert into Pessoa_OrigemCadastro (Nome,AtivoSN) values (@Nome,@AtivoSN)
end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPessoaApp]    Script Date: 06/06/2017 14:26:21 ******/
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




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarProduto]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdicionarProduto]
	@Codigo int output,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@CodGrupo int,
	@DiaSemana nvarchar(max),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@MaximoAdicionais int,
	@UrlImagem nvarchar(max),
	@DataInicioPromocao date,
	@DataFimPromocao date,
	@CodigoPersonalizado varchar(5),
	@Markup decimal,
	@PrecoSugerido decimal(10,2),
	@PrecoCusto decimal(10,2),
	@PontoFidelidadeVenda int,
	@PontoFidelidadeTroca int,
	@ControlaEstoque bit,
	@EstoqueMinimo decimal(8,2)
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,
		GrupoProduto,CodGrupo,DiaSemana,PrecoDesconto,AtivoSN,OnlineSN,DataAlteracao,MaximoAdicionais,
		UrlImagem,DataInicioPromocao,DataFimPromocao,CodigoPersonalizado,Markup,PrecoSugerido,PrecoCusto,
		PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo)
		Values(
			@Nome,
			@Descricao,
			@Preco,
			@GrupoProduto,
			@CodGrupo,
			@DiaSemana,
			@PrecoDesconto,
			@AtivoSN,
			@OnlineSN,
			@DataAlteracao,
			@MaximoAdicionais,
			@UrlImagem,
			@DataInicioPromocao,
			@DataFimPromocao,
			@CodigoPersonalizado,
			@Markup ,
			@PrecoSugerido ,
			@PrecoCusto,
			@PontoFidelidadeVenda,
			@PontoFidelidadeTroca,
			@ControlaEstoque,
			@EstoqueMinimo
				)
		SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
	END




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarProduto_OpcaoTipo]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAdicionarProduto_OpcaoTipo]
(
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime,
@AtivoSN bit,
@OnlineSN bit
--@DataSincronismo datetime

)
as 
begin
  insert into  Produto_OpcaoTipo (Nome,Tipo,MaximoOpcionais,MinimoOpcionais,OrdenExibicao,DataAlteracao,AtivoSN,OnlineSN)
                          values (@Nome,@Tipo,@MaximoOpcionais,@MinimoOpcionais,@OrdenExibicao,@DataAlteracao,@AtivoSN,@OnlineSN)
end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarProdutoInsumo]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionarProdutoInsumo]
@CodProduto int ,
@CodInsumo int ,
@Quantidade decimal
as
begin
 insert into Produto_Insumo (CodProduto,CodInsumo,Quantidade) 
        values (@CodProduto,@CodInsumo,@Quantidade) 
end


GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuario]    Script Date: 06/06/2017 14:26:21 ******/
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
@DescontoPedidoSN bit,
@EditaPedidoSN bit,
@VisualizaDadosClienteSN bit,
@AbreFechaCaixaSN bit,
@AlteraDadosClienteSN bit
as begin

insert into Usuario (Nome,Senha,CancelaPedidosSN,AlteraProdutosSN,AdministradorSN,AcessaRelatoriosSN,
						FinalizaPedidoSN,DescontoMax,DescontoPedidoSN,EditaPedidoSN,VisualizaDadosClienteSN,
						AbreFechaCaixaSN,AlteraDadosClienteSN)
					values
					(@Nome,@Senha,@CancelaPedidosSN,@AlteraProdutosSN,@AdministradorSN,@AcessaRelatoriosSN,
					@FinalizaPedidoSN,@DescontoMax,@DescontoPedidoSN,@EditaPedidoSN,@VisualizaDadosClienteSN,
						@AbreFechaCaixaSN,@AlteraDadosClienteSN )
  end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarUsuarioDefault]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAdicionarUsuarioDefault]
@Nome nvarchar(max),
@Senha nvarchar(128),
@AdministradorSN bit,
@AbreFechaCaixaSN bit

as begin

insert into Usuario (Nome,Senha,AdministradorSN,AbreFechaCaixaSN ) values
					(@Nome,@Senha,@AdministradorSN,@AbreFechaCaixaSN)
  end




GO
/****** Object:  StoredProcedure [dbo].[spAdicionarVenda]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAdicionaX]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAdicionaX]
@Data date
as
 insert into XSistemas (Data) values (@Data)




GO
/****** Object:  StoredProcedure [dbo].[spAltera]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlteraCAixa]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlteraFidelidade]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlteraFinalizaPedido_Pedido]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraFinalizaPedido_Pedido]
@CodPedido int,
@CodPagamento int,
@ValorPagamento decimal(10,2)
 as
   begin
    declare @JaExiste int
    set @JaExiste = (select COUNT(CodPedido) from Pedido_Finalizacao where /*CodPagamento=@CodPagamento and */CodPedido=@CodPedido)
     if @JaExiste>0
      begin
       update Pedido_Finalizacao set
      CodPagamento = @CodPagamento,
      ValorPagamento = @ValorPagamento
      where CodPedido = @CodPedido /*and CodPagamento=@CodPagamento*/
      end
       else
         begin
          exec spAdicionarFinalizaPedido_Pedido @CodPedido,@CodPagamento,@ValorPagamento
         end
   end




GO
/****** Object:  StoredProcedure [dbo].[spAlteraMesas]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraMesas]
@Codigo int,
@StatusMesa int,
@NumeroMesa nvarchar(10),
@AtivoSN bit,
@OnlineSN bit
 as
   begin
    Update Mesas set 
	  StatusMesa = @StatusMesa,
	  NumeroMesa = @NumeroMesa,
	  AtivoSN = @AtivoSN,
	  OnlineSN= @OnlineSN,
	  DataAlteracao = GETDATE()

	  where Codigo = @Codigo

   end





GO
/****** Object:  StoredProcedure [dbo].[spAlteraMotivoCancelamento]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAlteraMotivoCancelamento]
@Nome nvarchar(50),
@DataCadastro date,
@Codigo int
as
  begin
    Update MOtivoCancelamento 
	set 
	Nome=@Nome
	where 
	Codigo = @Codigo 
  end




GO
/****** Object:  StoredProcedure [dbo].[spAlteraOpcao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraOpcao]
@Codigo int,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit,
@SinalOpcao nvarchar(10),
@DiasDisponivel nvarchar(max)
as
begin
  update Opcao set 
  Nome = @Nome,
  Tipo = @Tipo,
  DataAlteracao =@DataAlteracao,
  OnlineSN =@OnlineSN,
  AtivoSN = @AtivoSN,
  SinalOpcao=@SinalOpcao,
  DiasDisponivel=@DiasDisponivel
  where Codigo =@Codigo
end



GO
/****** Object:  StoredProcedure [dbo].[spAlteraPedidoApp]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlteraPedidoBalcaoApp]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlteraPedidoBalcaoApp]
	@Codigo int output,
	@NomeCliente nvarchar(max),
	@Senha	nvarchar(max)
as
	BEGIN			
		update Pedido 
		set Observacao=@NomeCliente ,
		Senha=@Senha
		where Codigo = @Codigo
		exec spAlteraTotalPedido @Codigo
	END

GO
/****** Object:  StoredProcedure [dbo].[spAlteraPedidoStatusMovimento]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlteraPedidoStatusMovimento]
@CodPedido int,
@CodStatus int,
@CodUsuario int,
@DataAlteracao datetime
as
 update PedidoStatusMovimento set
 DataAlteracao=@DataAlteracao,
 CodUsuario = @CodUsuario
 where CodPedido=@CodPedido and CodStatus=@CodStatus
 


GO
/****** Object:  StoredProcedure [dbo].[spAlteraPermissao]    Script Date: 06/06/2017 14:26:21 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarBairrosRegiao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAlterarBairrosRegiao]
@CodRegiao int,
@Nome  nvarchar(100),
@CEP  nvarchar(10),
@DataCadastro datetime,
@AtivoSN bit,
@OnlineSN bit
as 
  begin
  update RegiaoEntrega_Bairros 
  set 
  CodRegiao = @CodRegiao,
  Nome = @Nome,
  CEP = @CEP,
  DataCadastro = @DataCadastro,
  AtivoSN = @AtivoSN,
  OnlineSN = @OnlineSN

  where 
  --CodRegiao = @CodRegiao
  Cep = @CEp
  end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarConfiguracao]    Script Date: 06/06/2017 14:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarConfiguracao]
@cod int,
@ImpViaCozinha nvarchar(max),
@UsaDataNascimento bit,
@UsaLoginSenha bit,
@QtdCaracteresImp int,
@ControlaEntregador bit,
@ProdutoPorCodigo nvarchar(max),
@Usa2Telefones bit,
@UsaControleMesa bit,
@ImprimeViaEntrega nvarchar(max),
@ImprimeViaBalcao nvarchar(max),
@ControlaFidelidade nvarchar(max),
@DescontoDiaSemana bit,
@CobraTaxaGarcon bit,
@EnviaSMS bit,
@RepeteUltimoPedido bit,
@RegistraCancelamentos bit,
@DadosApp nvarchar(max),
@CidadesAtendidas nvarchar(max),
@ExigeVendedorSN bit,
@CobrancaProporcionalSN bit,
@DadosPush nvarchar(max),
@Impressoras nvarchar(max)
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
	ImpressoraCopaBalcao = @ImprimeViaBalcao,
	ControlaFidelidade=@ControlaFidelidade,
	DescontoDiaSemana = @DescontoDiaSemana,
    CobraTaxaGarcon = @CobraTaxaGarcon,
    EnviaSMS =@EnviaSMS,
	RepeteUltimoPedido = @RepeteUltimoPedido,
	RegistraCancelamentos = @RegistraCancelamentos,
	DadosApp= @DadosApp,
	CidadesAtendidas=@CidadesAtendidas,
	ExigeVendedorSN = @ExigeVendedorSN,
	CobrancaProporcionalSN=@CobrancaProporcionalSN,
	DadosPush = @DadosPush,
	Impressoras =@Impressoras
	where cod=@cod
end


GO
/****** Object:  StoredProcedure [dbo].[spAlteraRegiao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[spAlteraRegiao]
  @Codigo int,
  @NomeRegiao nvarchar(20),
  @TaxaServico decimal(10,2),
  @DataAlteracao datetime,
  @OnlineSN bit,
  @AtivoSN bit,
  @valorMinimoFreteGratis decimal(10,2),
  @PrevisaoEntrega nvarchar(10)
as
  begin
    update RegiaoEntrega 
	set
	  NomeRegiao = @NomeRegiao,
	  TaxaServico = @TaxaServico,
	  DataAlteracao = @DataAlteracao,
	  OnlineSN =@OnlineSN,
	  valorMinimoFreteGratis =@valorMinimoFreteGratis,
	  PrevisaoEntrega = @PrevisaoEntrega,
	  AtivoSN = @AtivoSN
	  
    where 
	  Codigo = @Codigo
	  begin
	   update RegiaoEntrega_Bairros set OnlineSN=@OnlineSN , AtivoSN=@AtivoSN , RegiaoEntrega_Bairros.DataCadastro=Getdate()
	  where
	   RegiaoEntrega_Bairros.CodRegiao = @Codigo
	   end 
  end



GO
/****** Object:  StoredProcedure [dbo].[spAlterarEmpresa]    Script Date: 06/06/2017 14:26:22 ******/
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
@CaminhoBackup nvarchar(max),
@UrlServidor nvarchar(max),
@HorarioFuncionamento nvarchar(max),
@ConfiguracaoSMS nvarchar(max)

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
CaminhoBackup = @CaminhoBackup,
UrlServidor= @UrlServidor,
HorarioFuncionamento= @HorarioFuncionamento,
ConfiguracaoSMS= @ConfiguracaoSMS

end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarEmpresa_HorarioEntrega]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarEmpresa_HorarioEntrega]
@Codigo int ,
@Limite_horario_pedido nvarchar(6),
@Horario_entrega nvarchar(20),
@OnlineSN bit
as
  begin
   update Empresa_HorarioEntrega set
   Limite_horario_pedido=  @Limite_horario_pedido ,
   Horario_entrega  = @Horario_entrega,
   OnlineSN =@OnlineSN
    where Codigo=@Codigo 
  end


GO
/****** Object:  StoredProcedure [dbo].[spAlterarEndereco]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAlterarEndereco]
@Codigo int,
@CodPessoa int,
@Cep char(9),
@Endereco nvarchar(max),
@Complemento nvarchar(50),
@PontoReferencia nvarchar(100),
@Bairro nvarchar(100),
@Cidade nvarchar(100),
@UF char(2),
@Numero nvarchar(10),
@CodRegiao int,
@NomeEndereco nvarchar(max)
 as 
   begin
     update Pessoa_Endereco set 
      Cep=@Cep,
      Endereco=@Endereco,
      Complemento=@Complemento,
      PontoReferencia=@PontoReferencia,
      Bairro=@Bairro,
      Cidade=@Cidade,
      UF=@UF,
      Numero=@Numero,
      CodRegiao=@CodRegiao,
      NomeEndereco=@NomeEndereco
      where CodPessoa = @CodPessoa and Codigo=@Codigo
   end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarEntregador]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarFamilia]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarFamilia]
@Codigo int,
@Nome   nvarchar(max) ,
@AtivoSN bit,
@OnlineSN bit,
@DataAlteracao datetime,
@PaiSN bit

as 
  begin
     update Grupo set 
     NomeGrupo=@Nome,
     AtivoSN=@AtivoSN,
     OnlineSN=@OnlineSN,
     DataAlteracao=@DataAlteracao,
     PaiSN = @PaiSN

     where Codigo=@Codigo
  end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarFormaPagamento]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarFormaPagamento]
@Codigo int,
@Descricao nvarchar(100),
@DescontoSN bit,
@GeraFinanceiro bit,
@OnlineSN bit,
@DataAlteracao datetime,
@CaminhoImagem nvarchar(max),
@AtivoSN bit

as 
begin
update FormaPagamento set 
     Descricao=@Descricao ,
	 ParcelaSN = @DescontoSN,
	 GeraFinanceiro = @GeraFinanceiro,
	 OnlineSN= @OnlineSN,
	 CaminhoImagem =@CaminhoImagem,
	 DataAlteracao =@DataAlteracao ,
	 AtivoSN = @AtivoSN
	         where Codigo=@Codigo
end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarGrupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int,
	@MultiploSabores nvarchar(max)
AS
	BEGIN
		UPDATE Grupo

		SET 
		NomeGrupo = @NomeGrupo,
		ImprimeCozinhaSN =@ImprimeCozinhaSN,
		OnlineSN= @OnlineSN,
		DataAlteracao =@DataAlteracao ,
		AtivoSN =@AtivoSN,
		NomeImpressora =@NomeImpressora,
		CodFamilia = @CodFamilia,
		MultiploSabores=@MultiploSabores
		WHERE Codigo = @Codigo
         exec spAlterarProdutoPorGrupo @AtivoSN,@OnlineSN,@Codigo,@NomeGrupo;
	END


GO
/****** Object:  StoredProcedure [dbo].[spAlterarInsumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarInsumo]
@Codigo int,
@Nome nvarchar(max),
@UnidadeMedida char(4),
@AtivoSN bit,
@Preco decimal
as 
 begin
  update  Insumo 
  set
  Nome=@Nome,
  UnidadeMedida=@UnidadeMedida,
  AtivoSN=@AtivoSN,
  DataCadastro=Getdate(),
  DataAlteracao=Getdate(),
  Preco = @Preco 
  where Codigo=@Codigo
 end

GO
/****** Object:  StoredProcedure [dbo].[spAlterarItemExtraPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarItemPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAlterarItemPedido]

	@CodProduto int,
	@CodPedido int,
	@Codigo int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max),
	@ImpressoSN bit,
	@DataAtualizacao date
AS
	BEGIN
		UPDATE ItemsPedido

		SET 
		NomeProduto = @NomeProduto,
		Quantidade = @Quantidade,
		PrecoItem = @PrecoUnitario,
		PrecoTotalItem = @PrecoTotal,
		ImpressoSN=0,
		Item = @Item
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido  -- Código Pedido
			and Codigo = @Codigo;
        exec spDeletarEstoque @CodPedido,@CodProduto,@NomeProduto;
		exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido;
	END




GO
/****** Object:  StoredProcedure [dbo].[spAlterarItemPedidoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAlterarItemPedidoApp]
	@Codigo int,
	@CodUsuario int,	
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
AS
	BEGIN
		declare @CodPedido int
		--Busco pedidos em aberto
		--set @CodPedido = (select CodPedido from ItemsPedido where Codigo = @Codigo)
		
		if (@CodPedido > 0 )
		begin
			UPDATE 
				ItemsPedido			
			SET 
				Quantidade = cast(@Quantidade as decimal(10,2)),
				CodUsuario = @CodUsuario,
				PrecoItem = @PrecoUnitario,
				PrecoTotalItem = @PrecoTotal,
				Item = @Item
			WHERE 
				Codigo = @Codigo

			exec spAlterarTotalPedidoApp @Codigo
		end
		
	END



GO
/****** Object:  StoredProcedure [dbo].[spAlterarMensagens]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarMultiploOpcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarMultiploOpcao]
 @Preco decimal(10,2),
 @CodOpcao int,
 --@DataAlteracao datetime,
 @OnlineSN bit
  as
    begin
      update Produto_Opcao set
      Preco = @Preco,
      DataAlteracao = Getdate(),
      OnlineSn = @OnlineSN
      where CodOpcao = @CodOpcao
    end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarMultiploPessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarMultiploPessoa]
@Codigo int,
@CodRegiao int,
@Nome nvarchar(max),
@Cidade nvarchar(max),
@Bairro nvarchar(max),
@Telefone nvarchar(20)
 as 
 begin
   update Pessoa 
   set CodRegiao =@CodRegiao,
       Nome =@Nome,
       Cidade=@Cidade,
       Bairro=@Bairro,
       Telefone=@Telefone
       where Codigo=@Codigo
 end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarMultiploProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarMultiploProduto]
@Codigo int,
@Nome nvarchar(max),
@DataAlteracao datetime,
@Preco decimal(10,2),
@DataInicioPromocao date ,
@DataFimPromocao date,
@PrecoDesconto decimal(10,2),
@DiasDesconto nvarchar(max)
as
begin
  update 
  Produto 
  set
   PrecoProduto = @Preco,
   NomeProduto = @Nome,
   DataAlteracao = @DataAlteracao,
   DataInicioPromocao = @DataInicioPromocao,
   DataFimPromocao = @DataFimPromocao,
   PrecoDesconto = @PrecoDesconto,
   DiaSemana = @DiasDesconto
   where Codigo = @Codigo 
end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarOpcaoProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAlterarOpcaoProduto]
@CodProduto int,
@CodOpcao   int,
@Preco      decimal(10,2),
@PrecoProcomocao decimal(10,2),
@DataAlteracao datetime,
@CodTipo int
as 
  begin
    Update Produto_Opcao set
	CodOpcao = @CodOpcao,
	Preco    =  @Preco,
	DataAlteracao = @DataAlteracao,
	PrecoProcomocao = @PrecoProcomocao,
	CodTipo =@CodTipo
	where 
	CodProduto = @CodProduto and 
	CodOpcao = @CodOpcao

	 update Produto set DataAlteracao = @DataAlteracao
	 where Produto.Codigo =@CodProduto
  end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarPedidoStatus]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarPedidoStatus]
 @Codigo int,
 @Nome nvarchar(100),
 @EnviarSN bit,
 @AlertarSN bit,
 @Status int 
   as 
     begin
       update PedidoStatus set 
         Nome = @Nome,
         EnviarSN =@EnviarSN,
         AlertarSN = @AlertarSN,
         [Status] = @Status, 
         DataAlteracao =Getdate()
           where Codigo =@Codigo
     end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarPessoa]    Script Date: 06/06/2017 14:26:22 ******/
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
 @DataCadastro datetime,
 @user_id nvarchar(max),
 @DDD char(2),
@Sexo char(2),
@PFPJ char(1),
@CodOrigemCadastro int
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
  CodRegiao =@CodRegiao,
  [user_id] = @user_id,
   DDD= @DDD ,
   Sexo = @Sexo,
   PFPJ =@PFPJ,
   CodOrigemCadastro=@CodOrigemCadastro
  where Codigo=@Codigo
END



GO
/****** Object:  StoredProcedure [dbo].[spAlterarPessoa_OrigemCadastro]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarPessoa_OrigemCadastro] 
@Codigo int ,
@Nome nvarchar(max),
@AtivoSN bit
as
 begin
   update Pessoa_OrigemCadastro set
   Nome=@Nome,
   AtivoSN =@AtivoSN
   where Codigo=@Codigo
 end


GO
/****** Object:  StoredProcedure [dbo].[spAlterarPessoaApp]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarProduto]    Script Date: 06/06/2017 14:26:22 ******/
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
	@CodGrupo int,
	@DiaSemana nvarchar(max),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
    @MaximoAdicionais int,
    @UrlImagem nvarchar(max),
    @DataInicioPromocao date,
	@DataFimPromocao date,
	@CodigoPersonalizado varchar(5),
	@Markup decimal,
	@PrecoSugerido decimal (10,2),
	@PrecoCusto decimal (10,2),
	@PontoFidelidadeVenda int,
	@PontoFidelidadeTroca int,
	@ControlaEstoque bit,
	@EstoqueMinimo decimal(8,2)

AS
	BEGIN
		UPDATE Produto

		SET 
		
		NomeProduto = @Nome,
		DescricaoProduto = @Descricao,
		PrecoProduto = @Preco,
		GrupoProduto = @GrupoProduto,
		CodGrupo =@CodGrupo,
		DiaSemana = @DiaSemana,
		PrecoDesconto = @PrecoDesconto,
		AtivoSN = @AtivoSN ,
		OnlineSN =@OnlineSN,
		DataAlteracao =@DataAlteracao ,
		MaximoAdicionais= @MaximoAdicionais,
		UrlImagem =@UrlImagem ,
		DataInicioPromocao = @DataInicioPromocao,
		DataFimPromocao = @DataFimPromocao,
		CodigoPersonalizado =@CodigoPersonalizado,
		Markup=@Markup ,
		PrecoSugerido=@PrecoSugerido ,
		PrecoCusto=@PrecoCusto,
		PontoFidelidadeVenda=@PontoFidelidadeVenda ,
	    PontoFidelidadeTroca= @PontoFidelidadeTroca ,
		ControlaEstoque = @ControlaEstoque,
		EstoqueMinimo= @EstoqueMinimo
		WHERE Codigo = @Codigo;
	END



GO
/****** Object:  StoredProcedure [dbo].[spAlterarProduto_OpcaoTipo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarProduto_OpcaoTipo]
(
@Codigo int,
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime,
@AtivoSN bit,
@OnlineSN bit
 )
 as 
   begin
     update Produto_OpcaoTipo set
	Nome=@Nome ,
	Tipo=@Tipo ,
	MaximoOpcionais= @MaximoOpcionais ,
	MinimoOpcionais= @MinimoOpcionais   ,
	OrdenExibicao =  @OrdenExibicao ,
	DataAlteracao = @DataAlteracao,
	AtivoSN = @AtivoSN,
	OnlineSN= @OnlineSN
       where Codigo =@Codigo    
   end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarProdutoInsumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spAlterarProdutoInsumo]
@Codigo int,
@Quantidade decimal
as
begin
 update Produto_Insumo
  set Quantidade=@Quantidade 
  where Codigo=@Codigo
end


GO
/****** Object:  StoredProcedure [dbo].[spAlterarProdutoPorGrupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlterarProdutoPorGrupo]
@AtivoSN bit,
@OnlineSN bit,
@CodGrupo int,
@NomeGrupo nvarchar(max)
as
  begin
  update Produto set 
		AtivoSN = @AtivoSN ,
		OnlineSN=@OnlineSN,
		GrupoProduto = @NomeGrupo,
		DataAlteracao = Getdate()
	   where 
       CodGrupo = @CodGrupo
  end




GO
/****** Object:  StoredProcedure [dbo].[spAlterarSenha]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarTotalPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2),
	@Tipo nvarchar(20),
	@NumeroMesa nvarchar(20),
	@CodUsuario int,
	@HorarioEntrega nvarchar(max),
	@DescontoValor decimal(10,2),
	@Observacao nvarchar(max),
	@CodEndereco int
AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido,
		Tipo = @Tipo,
		NumeroMesa = @NumeroMesa,
		CodUsuario =@CodUsuario,
		HorarioEntrega= @HorarioEntrega,
		DescontoValor= @DescontoValor,
		Observacao=@Observacao,
		CodEndereco = @CodEndereco
		WHERE 
			Codigo = @Codigo --Codigo Produto
	END

GO
/****** Object:  StoredProcedure [dbo].[spAlterarTotalPedidoApp]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarTrocoParaFormaPagamento]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spAlterarUsuario]    Script Date: 06/06/2017 14:26:22 ******/
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
@DescontoMax decimal,
@EditaPedidoSN bit,
@VisualizaDadosClienteSN bit,
@AbreFechaCaixaSN bit,
@AlteraDadosClienteSN bit

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
DescontoPedidoSN = @DescontoPedidoSN,
EditaPedidoSN=@EditaPedidoSN ,
VisualizaDadosClienteSN= @VisualizaDadosClienteSN ,
AbreFechaCaixaSN =@AbreFechaCaixaSN ,
AlteraDadosClienteSN=@AlteraDadosClienteSN 
where cod=@Codigo
   END




GO
/****** Object:  StoredProcedure [dbo].[spAlteraStatusMesa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAlteraStatusMesa]
@Codigo int,
--@NumeroMesa nvarchar(100),
@StatusMesa int
  as
    begin
	update Mesas set StatusMesa = @StatusMesa
	 where Codigo = @Codigo --and Codigo = @Codigo
	end




GO
/****** Object:  StoredProcedure [dbo].[spAtualizaEstoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spAtualizaEstoque]
 @CodProduto int,
 @Quantidade decimal(10,2),
 @DataAtualizacao datetime,
 @PrecoCompra decimal(10,2),
 @NomeProduto nvarchar(max),
 @CodPedido int
 as
  begin
   --declare @Contador int
   --set @Contador = (select COUNT(CodProduto) as CodProduto  from Produto_Estoque where CodProduto = @CodProduto and DataAtualizacao =@DataAtualizacao)
   
  --  if (@Contador > 0)
		--begin
		--  update Produto_Estoque set Quantidade= Quantidade + @Quantidade , DataAtualizacao=GETDATE()
		--  where CodProduto=@CodProduto
		--end  
  -- else
		 begin
		   insert into Produto_Estoque values (@CodProduto,@Quantidade,GETDATE(),@PrecoCompra,@NomeProduto,@CodPedido);
		 end
     
 end
GO
/****** Object:  StoredProcedure [dbo].[spBaixarEstoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE procedure [dbo].[spBaixarEstoque]
 @CodProduto int,
 @NomeProduto nvarchar(max),
 @Quantidade decimal(10,2),
 @CodPedido int
 as 
 begin
   declare @controlaEstoque bit ;
    set @controlaEstoque =( select ControlaEstoque from Produto where Codigo=@CodProduto);
	 if @controlaEstoque =1 
	   begin
     insert into Produto_Estoque (CodProduto,NomeProduto,Quantidade,DataAtualizacao,CodPedido)
	             values (@CodProduto,@NomeProduto,-@Quantidade,GETDATE(),@CodPedido)
      end
 end
GO
/****** Object:  StoredProcedure [dbo].[spCalculaSistema]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spCalculaSistema]
as
delete from XSistemas




GO
/****** Object:  StoredProcedure [dbo].[spCancelarHistoricoPessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spCancelarHistoricoPessoa]
@CodPedido int
as
  begin
     delete from HistoricoPessoa where
	 Historico like '%'+@CodPedido
  end
GO
/****** Object:  StoredProcedure [dbo].[spCancelarPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCancelarPedido]

 @Codigo int,
 @Status nvarchar(100),
 @CodUsuario int
AS
 BEGIN
 declare  @CodigoMesa int
	set @CodigoMesa = ( select CodigoMesa from Pedido where Codigo=@Codigo)
	if (@CodigoMesa >0)
	 begin
	  exec spAlteraStatusMesa @CodigoMesa,1
	 end
  UPDATE Pedido

  SET   
  [status] = @Status,
  RealizadoEm = GetDate(),
  CodUsuario = @CodUsuario,
  Finalizado = 1
  where Codigo = @Codigo
  exec spExcluirFinalizaPedido_Pedido @Codigo
  exec spCancelarHistoricoPessoa @Codigo
END






GO
/****** Object:  StoredProcedure [dbo].[spContaCancelamentos]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spContaEstoqueAtual]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spContaEstoqueAtual]
@NomeProduto nvarchar(max)
as
begin
select NomeProduto,Sum(Quantidade)  as EstoqueAtual 
from Produto_Estoque 
where NomeProduto=@NomeProduto
group by NomeProduto
end
GO
/****** Object:  StoredProcedure [dbo].[spContaEstoquePorGrupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spContaEstoquePorGrupo]
@Codigo int
as    
begin
Select PO.NomeProduto,Sum(PO.Quantidade ) as EstoqueAtual
from Produto_Estoque PO 
 join Produto P on P.Codigo=PO.CodProduto 
 join Grupo G on G.Codigo = P.CodGrupo 
 where G.Codigo=@Codigo
group by PO.NomeProduto
end
GO
/****** Object:  StoredProcedure [dbo].[spContaPedidosPorCliente]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spCriarPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCriarPedido]
@CodPedido int,
@CodProduto int,
@NomeProduto nvarchar(max),
@Quantidade decimal(10,2),
@PrecoUnitario decimal(10,2),
@PrecoTotal decimal(10,2),
@Item nvarchar(max),
@ImpressoSN bit,
@DataAtualizacao date
AS
INSERT INTO dbo.ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
					  Values(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN)
exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido




GO
/****** Object:  StoredProcedure [dbo].[spDeletarEstoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spDeletarEstoque]
@CodPedido int,
@CodProduto int,
@NomeProduto nvarchar(max)
as
  begin
     delete from Produto_Estoque 
	 where CodProduto=@CodProduto and
	 CodPedido=@CodPedido and NomeProduto=@NomeProduto
  end
GO
/****** Object:  StoredProcedure [dbo].[spEntregasPorBoyData]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spEntregasPorBoyData]
@CodPessoa int,
@DataInicio   date,
@DataFim  date
as
 begin
 select 
count(P.CodMotoboy) as QuantidadeEntregas,
cast(P.RealizadoEm as date) as RealizadoEm,
(select Codigo from Entregador E where P.CodMotoboy = E.Codigo) as CodMotoboy, 
(select Nome from Entregador E where P.CodMotoboy = E.Codigo) as Nome 
from
Pedido P
where P.Finalizado =1
and P.CodMotoboy = @CodPessoa and 
P.RealizadoEm between @DataInicio and @DataFim

group by P.CodMotoboy,cast(P.RealizadoEm as date)

 end




GO
/****** Object:  StoredProcedure [dbo].[spEntregasPorMotoboyAgrupado]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spEntregasPorMotoboyAgrupado]
@DataInicio datetime,
@DataFim datetime
as
select 
Count(PD.CodMotoboy) QtdEntregas,
E.Nome as NomeEntregador,
R.NomeRegiao
 from Pedido PD
join Entregador E on E.Codigo=PD.CodMotoboy
join Pessoa P on P.Codigo = PD.CodPessoa
join RegiaoEntrega R on R.Codigo = P.CodRegiao
Where PD.Finalizado=1 and PD.RealizadoEm between @DataInicio and @DataFim
group by R.NomeRegiao,E.Nome


GO
/****** Object:  StoredProcedure [dbo].[spExcluirBairroRegiao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirBairroRegiao]
@CodRegiao int,
@Cep       nvarchar(10)
as
  begin
     delete from RegiaoEntrega_Bairros 
     where CodRegiao=@CodRegiao and CEP=@Cep
  end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirCaixa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirCliente]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluiRegiao]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirEndereco]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirEndereco]
 @CodPessoa int,
 @NomeEndereco nvarchar(max)
  as
  begin
    delete from Pessoa_Endereco where
    CodPessoa =@CodPessoa and NomeEndereco=@NomeEndereco
  end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirFamilia]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spExcluirFamilia] 
 @Codigo int
 as
   begin
     delete from Grupo where Codigo=@Codigo
   end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirFinalizaPedido_Pedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirFinalizaPedido_Pedido]
@CodPedido int 
  as 
   begin
     delete from Pedido_Finalizacao where CodPedido = @CodPedido
   end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirFormaPagamento]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirGrupo]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirInsumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create procedure [dbo].[spExcluirInsumo]
  @Codigo int
  as
  begin
  delete from Insumo where Codigo=@Codigo
  end


GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemExtraPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExcluirItemExtraPorCodigo]
		@Codigo int
	AS
		DELETE FROM ItemsExtras WHERE Codigo = @Codigo




GO
/****** Object:  StoredProcedure [dbo].[spExcluirItemPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spExcluirItemPedido]

	@CodProduto int,
	@CodPedido int,
	@Codigo int,
	@NomeProduto nvarchar(max)

AS
	BEGIN
		DELETE FROM 
			ItemsPedido
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido  -- Código Pedido
			and Codigo =@Codigo   -- Código Sequencial
			and NomeProduto =@NomeProduto
     exec spDeletarEstoque @CodPedido,@CodProduto,@NomeProduto
	END




GO
/****** Object:  StoredProcedure [dbo].[spExcluirMotivoCancelamento]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirMovimentoCaixa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirOpcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirOpcao]
@Codigo int
as
begin
  delete from Opcao 
  where Codigo =@Codigo
end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirOpcaoProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spExcluirOpcaoProduto]
  @CodProduto int,
  @CodOpcao   int
   as
     begin
	  delete from Produto_Opcao 
	    where
		CodProduto = @CodProduto and 
		CodOpcao   = @CodOpcao
 update Produto set DataAlteracao = Getdate()
	 where Produto.Codigo =@CodProduto

	 end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirOpcoesPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirOpcoesPedido]
@CodPedido int,
@CodProduto int,
@CodOpcao  int
 as
 begin
  delete from Pedido_OpcoesProduto
   where CodPedido=@CodPedido and 
   CodProduto=@CodProduto and
   CodOpcao=@CodOpcao
 end




GO
/****** Object:  StoredProcedure [dbo].[spExcluirPessoa_OrigemCadastro]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirPessoa_OrigemCadastro]
@Codigo int
as 
  begin
   delete from Pessoa_OrigemCadastro where Codigo=@Codigo
  end


GO
/****** Object:  StoredProcedure [dbo].[spExcluirProduto]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spExcluirProdutoInsumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spExcluirProdutoInsumo]
@Codigo int
  as
   begin
    delete from Produto_Insumo where Codigo=@Codigo
   end

GO
/****** Object:  StoredProcedure [dbo].[spFechamentoTotalCaixa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spFechamentoTotalCaixa]
@DataInicio datetime,
@DataFim    datetime
as
select
F.Codigo,
F.Descricao,
ValorPagamento as ValorTotal,
P.Codigo as CountPedido
,E.Codigo CodMotoboy
,E.Nome
from 
Pedido_Finalizacao PF 
join Pedido P on P.Codigo = PF.CodPedido
join FormaPagamento F on F.Codigo = PF.CodPagamento
left join Entregador E on E.Codigo = P.CodMotoboy
where P.RealizadoEm between @DataInicio and @DataFim



GO
/****** Object:  StoredProcedure [dbo].[spFecharCaixa]    Script Date: 06/06/2017 14:26:22 ******/
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
@Numero varchar(10),
@Turno varchar(5)
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
	  and Turno= @Turno
  end




GO
/****** Object:  StoredProcedure [dbo].[spImprimeOpcoesProdutoPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spImprimeOpcoesProdutoPedido]
@CodPedido int,
@CodProduto int,
@CodOpcao int 
as
begin
select * from Pedido_OpcoesProduto
where 
CodProduto=@CodProduto and
CodOpcao = @CodOpcao and
CodPedido = @CodPedido
end




GO
/****** Object:  StoredProcedure [dbo].[spImprimePedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spImprimePedido]
@Codigo int,
@CodEndereco int
as
  begin
    select 
	Pd.Codigo, 
	Pd.NumeroMesa,
	Pd.CodPessoa,
	pd.FormaPagamento,
	Pd.TotalPedido,
	Pd.Tipo,
	
	 (select 
count(Codigo)   
from
Pedido
where Tipo =Pd.Tipo
and Cast(RealizadoEm as date) =cast(Pd.RealizadoEm as date)
group by Cast(RealizadoEM as  date),Tipo) as NumeroVenda,
	Pd.RealizadoEm,
	Pd.TrocoPara ,
	It.CodProduto,
	It.NomeProduto,
	Isnull(IT.Item,'') as Item,
	It.PrecoTotalItem,
	It.Quantidade,
	Isnull(RG.TaxaServico,0) as TaxaServico,
	IsNull(Pd.DescontoValor,0) as  DescontoValor,
	(select CodGrupo from Produto where Produto.Codigo=It.CodProduto) as CodGrupo,
	HorarioEntrega,
	Isnull((select Nome from Usuario where Cod=Pd.CodUsuario),'Online' )as 'Atendente',
	Pd.Observacao,
	CodEndereco,
	Convert(char(8),cast(HorarioFechamento-RealizadoEM as datetime),114) as TempoPermanencia, 
		case Pd.Tipo
		when '1 - Mesa' then  P.Nome +' - '+ Pd.NumeroMesa 
		when '2 - Balcao' then 'Cliente Balcao ' +Pd.Senha +' ' + Pd.Observacao
		when '0 - Entrega' then P.Nome 
		end as  Nome
	
	,P.Telefone,P.Telefone2,PE.Endereco+' '+'Nº'+PE.Numero as EndereComNumero,PE.Complemento,PE.PontoReferencia,PE.Bairro,PE.Cidade,
	TotalPedido/NumeroPessoas as ValorPorPessoa,
	(Select Nome from Empresa ) as NOmeEmpresa,
    ( select TaxaServico from RegiaoEntrega  where Codigo=P.CodRegiao) as TaxaServico,
    (select Convert(char(8),Dateadd(n, cast(RG.PrevisaoEntrega as int),PD.RealizadoEM),114) from RegiaoEntrega where Codigo=P.CodRegiao) as PrevisaoEntrega,
	(select count(Codigo) from Pedido where CodPessoa=P.Codigo) as ClienteNovo,
	Senha,
	isnull(PagoFidelidade,0) as PagoFidelidade
	from Pedido Pd
	left join ItemsPedido It on It.CodPedido = Pd.Codigo
    left join Pessoa P on P.Codigo = Pd.CodPessoa
	left join Pessoa_Endereco PE on Pe.CodPessoa = Pd.CodPessoa and PE.Codigo=@CodEndereco
	left join RegiaoEntrega RG on RG.Codigo = P.CodRegiao
	where Pd.Codigo = @Codigo 
	
  end


GO
/****** Object:  StoredProcedure [dbo].[spImprimePedidoMesa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spImprimePedidoMesa]
@Codigo int,
@CodGrupo int
as
  begin
    select 
	Pd.Codigo, 
	Pd.NumeroMesa,
	Pd.CodPessoa,
	pd.FormaPagamento,
	Pd.TotalPedido,
	Pd.Tipo,
	
	 (select 
count(Codigo)   
from
Pedido
where Tipo =Pd.Tipo
and Cast(RealizadoEm as date) =cast(Pd.RealizadoEm as date)
group by Cast(RealizadoEM as  date),Tipo) as NumeroVenda,
	Pd.RealizadoEm,
	Isnull(Pd.TrocoPara,'S/ Troco') as  TrocoPara,
	It.CodProduto,
	It.NomeProduto,
	Isnull(IT.Item,'') as Item,
	It.PrecoTotalItem,
	It.Quantidade,
	Isnull(RG.TaxaServico,0) as TaxaServico,
	IsNull(Pd.DescontoValor,0) as  DescontoValor,
	(SELECT PrevisaoEntrega from Configuracao) as PrevisaoEntrega,
	(SELECT PrevisaoEntregaSN from Configuracao) as PrevisaoEntregaSN,
	(select CodGrupo from Produto where Produto.Codigo=It.CodProduto and CodGrupo=@CodGrupo) as CodGrupo
	from Pedido Pd
	left join ItemsPedido It on It.CodPedido = Pd.Codigo
	left join Pessoa P on P.Codigo = Pd.CodPessoa
	left join RegiaoEntrega RG on RG.Codigo = P.CodRegiao
	where Pd.Codigo = @Codigo 
  end




GO
/****** Object:  StoredProcedure [dbo].[spInformaItemImpresso]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spInsereBoyPedido]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spInsereFidelidade]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spInsereFidelidade]
@CodPessoa int,
@CodPedido int,
@Ponto int
as 
  begin
     
     insert into Pessoa_Fidelidade (CodPessoa,CodPedido,Ponto,Tipo,Data)
	        values   (@CodPessoa,@CodPedido,@Ponto,'C',Getdate())
	  
  end

GO
/****** Object:  StoredProcedure [dbo].[spInsereRegistro]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spInserirMovimentoCaixa]    Script Date: 06/06/2017 14:26:22 ******/
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
@CodUser int,
@Turno varchar(5)
as
  begin
     insert into CaixaMovimento (CodCaixa,Data,Historico,NumeroDocumento,CodFormaPagamento,Valor,Tipo,CodUsuario,Turno)
	        values (@CodCaixa,@Data,@Historico,@NumeroDocumento,@CodFormaPagamento,@Valor,@Tipo,@CodUser,@Turno)
  end




GO
/****** Object:  StoredProcedure [dbo].[spLimparEstoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spLimparEstoque]
as
begin
 delete from Produto_Estoque
end




GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spLogin]
@Nome nvarchar(max),
@Senha nvarchar(max)
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
	ISNULL(DescontoMax,0) as DescontoMax,
	ISNULL(EditaPedidoSN,0) as EditaPedidoSN,
	ISNULL(VisualizaDadosClienteSN,0) as VisualizaDadosClienteSN,
	ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN,
	ISNULL(AlteraDadosClienteSN,0) as AlteraDadosClienteSN
	 from Usuario
	 where Nome=@Nome and Senha=@Senha
end




GO
/****** Object:  StoredProcedure [dbo].[spMargemGarcon]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spMovimentoCaixa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMovimentoCaixa]
@Caixa int,
@Turno varchar(5),
@DataI date,
@DataF date
as
select 
case Tipo 
when 'E' then 'Entradas'
when 'S' then 'Saidas'
end
as 'Tipo Movimento', 
                        
Fp.Descricao ,
sum(cx.Valor) as 'Total Somado'
from CaixaMovimento CX
left join FormaPagamento FP on FP.Codigo = Cx.CodFormaPagamento
left join Caixa C on C.Codigo = CX.CodCaixa
where 
CX.CodCaixa = @Caixa AND
C.Turno = @Turno and 
CX.Data BETWEEN @DataI  AND @DataF 

group by CodCaixa,Fp.Descricao,Tipo




GO
/****** Object:  StoredProcedure [dbo].[spObterAnivesariantes]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterAnivesariantes]
@DataInicial char(5),
@DataFinal char(5)
as
 BEGIN
  SELECT Telefone,Nome
  FROM Pessoa 
 WHERE RIGHT(CONVERT(VARCHAR,DataNascimento,112),4)
   between RIGHT(@DataInicial,2) + LEFT(@DataInicial,2) 
	AND RIGHT(@DataFinal,2) + LEFT(@DataFinal,2)
 END




GO
/****** Object:  StoredProcedure [dbo].[spObterAnivesariantesPush]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterAnivesariantesPush]
@DataInicial char(5),
@DataFinal char(5)
as
 BEGIN
  SELECT Nome,[user_id]
  FROM Pessoa 
 WHERE 	RIGHT(CONVERT(VARCHAR,DataNascimento,112),4)
   between RIGHT(@DataInicial,2) + LEFT(@DataInicial,2) 
	AND RIGHT(@DataFinal,2) + LEFT(@DataFinal,2)
	and [user_id] is not null and [user_id] !=''
 END



GO
/****** Object:  StoredProcedure [dbo].[spObterBairro_RegiaoPorNome]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterBairro_RegiaoPorNome]
@Bairro nvarchar(max)
as
select
 * from RegiaoEntrega_Bairros 
where AtivoSN=1 
and Nome=@Bairro





GO
/****** Object:  StoredProcedure [dbo].[spObterCaixa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterCaixaAberto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterCaixaAberto]
as
 select * from Caixa
 where Caixa.Estado=0




GO
/****** Object:  StoredProcedure [dbo].[spObterCaixaMovimetoFiltro]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterCancelamentoPorPessoa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterClientesPorOrigem]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterClientesPorOrigem]
 as
  begin
    select 
	PC.Nome,count(CodOrigemCadastro) as QuantidadeClientes
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	group by CodOrigemCadastro,PC.Nome
  end
  

GO
/****** Object:  StoredProcedure [dbo].[spObterClientesPorOrigemCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterClientesPorOrigemCodigo]
 @Codigo int
 as
  begin
    select 
	PC.Nome,count(CodOrigemCadastro) as QuantidadeClientes
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	where PC.Codigo=@Codigo
	group by CodOrigemCadastro,PC.Nome

  end

GO
/****** Object:  StoredProcedure [dbo].[spObterClientesPorRegiao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spObterClientesPorRegiao]
@Codigo int
as 
  begin
   select Nome,Telefone from Pessoa where CodRegiao=@Codigo 
  end


GO
/****** Object:  StoredProcedure [dbo].[spObterClientesPorRegiaoPush]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterClientesPorRegiaoPush]
@Codigo int
as 
  begin
   select Nome,Telefone from Pessoa where CodRegiao=@Codigo 
   and [user_id] is not null  or [user_id]!='' 
  end


GO
/****** Object:  StoredProcedure [dbo].[spObterClientesSemPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterClientesSemPedido]
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo , Telefone,PE.Nome
from Pessoa PE 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
end




GO
/****** Object:  StoredProcedure [dbo].[spObterClientesSemPedidoPush]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterClientesSemPedidoPush]
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo , Telefone ,Pe.Nome,Pe.user_id
from Pessoa PE 
--left join Pedido PD on PD.CodPessoa=PE.Codigo 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
and PE.user_id is not null and PE.user_id!=''
end




GO
/****** Object:  StoredProcedure [dbo].[spObterCodigoMesa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterCodigoMesa]
@NumeroMesa nvarchar(100)
 as 
   begin
    select Codigo,NumeroMesa from Mesas
	  where NumeroMesa = @NumeroMesa
   end




GO
/****** Object:  StoredProcedure [dbo].[spObterConfiguracao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterConfiguracao]
as 
select isnull(cod,0) as cod 
 ,isnull(ImpViaCozinha,'{"ImprimeSN":false,"TipoAgrupamento":"Sem Agrupamento","ViaCozinha":"0"}') as ImpViaCozinha 
 ,isnull(UsaDataNascimento,0) as UsaDataNascimento 
 ,isnull(UsaLoginSenha,0) as UsaLoginSenha 
 ,isnull(QtdCaracteresImp,0) as QtdCaracteresImp 
 ,isnull(ControlaEntregador,0) as ControlaEntregador
 ,isnull(ProdutoPorCodigo,'{"PorCodigo":false,"TipoCodigo":"Personalizado"}') as ProdutoPorCodigo 
 ,isnull(Usa2Telefones,0) as Usa2Telefones 
 ,isnull(UsaControleMesa,0) as UsaControleMesa
 ,isnull(ImprimeViaEntrega,'{"ImprimeSN":true,"TipoAgrupamento":"Sem Agrupamento","ViaDelivery":"1"}') as ImprimeViaEntrega
  ,isnull(ImpressoraCopaBalcao,'{"ImprimeSN":true,"TipoAgrupamento":"Sem Agrupamento","ViaBalcao":"1"}') as ImprimeViaBalcao
 ,isnull(ControlaFidelidade,'') as ControlaFidelidade
 ,isnull(DescontoDiaSemana,0) as DescontoDiaSemana
 ,isnull(CobraTaxaGarcon,0) as CobraTaxaGarcon
 ,isnull(EnviaSMS,0) as EnviaSMS
 ,isnull(RepeteUltimoPedido,0) as RepeteUltimoPedido
 ,isnull(RegistraCancelamentos,0) as RegistraCancelamentos
 ,isnull(DadosApp,'{"plataforma":"","url":""}') as DadosApp
 ,isnull(CidadesAtendidas,0) as CidadesAtendidas
 ,isnull(ExigeVendedorSN,0) as ExigeVendedorSN
 ,isnull(CobrancaProporcionalSN,0) as CobrancaProporcionalSN
 ,isnull(DadosPush,'{"GCM":"","Pushapp_id":"","Pushauthorization":""}') as DadosPush
 ,isnull(Impressoras,'{"ImpressoraDelivery":"","ImpressoraBalcao":"","ImpressoraContaMesa":""}') as Impressoras
 from Configuracao







GO
/****** Object:  StoredProcedure [dbo].[spObterCrediario]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[spObterCrediario]
 as
 select 
CodPessoa,
(Select NOme from Pessoa where Codigo=H.CodPessoa) as Nome,
(Select Telefone from Pessoa where Codigo=H.CodPessoa) as Telefone,
Sum(Valor) 'Valor Devedor'
  from HistoricoPessoa  H
  join Pessoa P on P.Codigo=H.CodPessoa
group by CodPessoa



--select * from HistoricoPessoa





GO
/****** Object:  StoredProcedure [dbo].[spObterCrediarioData]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterCrediarioData]
@DataInicio date,
@DataFim date
as
begin
  select 
H.Tipo,
Data,
CodPessoa,
Historico,
Valor,
P.Nome ,
P.Telefone
  from HistoricoPessoa  H
  join Pessoa P on P.Codigo=H.CodPessoa
 where 
 Data between @DataInicio and @DataFim
end


GO
/****** Object:  StoredProcedure [dbo].[spObterCrediarioDataResumido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterCrediarioDataResumido]
@DataI date,
@DataF date
 as
 select 
CodPessoa,
(Select NOme from Pessoa where Codigo=H.CodPessoa) as Nome,
(Select Telefone from Pessoa where Codigo=H.CodPessoa) as Telefone,
Sum(Valor) 'Valor Devedor'
  from HistoricoPessoa  H
  join Pessoa P on P.Codigo=H.CodPessoa
where Data between @DataI and @DataF
group by CodPessoa



GO
/****** Object:  StoredProcedure [dbo].[spObterDados]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterDadosCaixa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterDadosCaixa]
@Data date,
@Numero varchar(10),
@Turno varchar(5)
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
    and Turno = @Turno
   end




GO
/****** Object:  StoredProcedure [dbo].[spObterDadosCaixaPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterDadosOpcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterDadosOpcao]
@Codigo int,
@CodOpcao int 
  as 
    begin
	  select Pr.Preco,OP.Nome,PO.Tipo from Produto_Opcao Pr
	  left join Opcao OP on OP.Codigo = Pr.CodOpcao 
	  JOIN Produto_OpcaoTipo PO ON PO.Codigo = OP.Tipo
	  where CodProduto =@Codigo
	  and CodOpcao=@CodOpcao

	end




GO
/****** Object:  StoredProcedure [dbo].[spObterEmpresa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterEmpresa_HorarioEntrega]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[spObterEmpresa_HorarioEntrega]
   as
   begin
  select * from Empresa_HorarioEntrega
  end


GO
/****** Object:  StoredProcedure [dbo].[spObterEmpresa_HorarioEntregaPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create procedure [dbo].[spObterEmpresa_HorarioEntregaPorCodigo]
  @Codigo int
   as
   begin
  select * from Empresa_HorarioEntrega where Codigo=@Codigo
  end


GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoCompletoPessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterEnderecoCompletoPessoa]
@Codigo int
as 
begin
 select Codigo ,Endereco +' Nº '+Numero + ' ' +Bairro +
' '+ Complemento + ' '+ PontoReferencia  +' '+ Cidade as EnderecoCompleto
  from Pessoa_Endereco where CodPessoa = @Codigo
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterEnderecoPessoa]
@Codigo int
 as 
 begin
   select * from Pessoa_Endereco where CodPessoa=@Codigo
 end




GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPessoaCEP]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterEnderecoPessoaCEP]
@CodPessoa int,
@Cep varchar(9)
as
 begin
  select * from Pessoa_Endereco where CodPessoa=@CodPessoa and @Cep=@Cep
 end


GO
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPorCep]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterEnderecoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterEnderecoPorCodigo]
@Codigo int
as 
begin
 select PE.Codigo,P.Nome,PE.Endereco,PE.Bairro,PE.Cidade,PE.UF,PE.PontoReferencia,
  PE.Complemento as Observacao,PE.Numero,Telefone,Telefone2,DataNascimento,
  isNull(TicketFidelidade,0) as TicketFidelidade ,
  PE.CodRegiao,DataCadastro,isnull([user_id],'') as [user_id] 
  ,isnull(DDD,'') as DDD
  ,isnull(Sexo,'') as Sexo,PFPJ,PE.Cep
  from Pessoa_Endereco PE
  join Pessoa P on P.Codigo = PE.CodPessoa 
  where PE.Codigo = @Codigo
  
  end



GO
/****** Object:  StoredProcedure [dbo].[spObterEntregadores]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterEntregasMotoboy]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterEntregasMotoboy]
@DataInicio date,
@DataFim date
as
select 
E.Codigo,
E.Nome,
PS.Nome as 'Cliente',
PS.Telefone as 'Telefone',
 DATEPART(hour, P.RealizadoEM) as 'Hora'
from Pedido P
join Entregador E  on E.Codigo=P.CodMotoboy 
join Pessoa PS on PS.Codigo = P.CodPessoa
where CodMotoboy is not null
and RealizadoEM between @DataInicio and @DataFim





GO
/****** Object:  StoredProcedure [dbo].[spObterEntregasMotoboyResumido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterEntregasMotoboyResumido]
@DataInicio date,
@DataFim date
as
select 
count(Pd.Codigo) as QtdEntregas,
E.Nome as NomeEntregador
 from Pedido Pd
join Pessoa P on P.Codigo = Pd.CodPessoa
join Entregador E on E.Codigo = PD.CodMotoboy
where CodMotoboy is not null
and RealizadoEM between @DataInicio and @DataFim
group by E.Nome



GO
/****** Object:  StoredProcedure [dbo].[spObterEntregasPorRegiao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterEntregasPorRegiao]
@DataInicio datetime,
@DataFim datetime
as
select 
R.NomeRegiao,
sum(R.TaxaServico) as 'Valor das Entregas',
Count(P.Codigo) 'Qtd Entregas'
From Pedido P
join Pessoa PE on PE.Codigo = P.CodPessoa
join RegiaoEntrega R on R.Codigo = PE.CodRegiao
where Finalizado=1 and RealizadoEm between @DataInicio and @DataFim
group by R.NomeRegiao


GO
/****** Object:  StoredProcedure [dbo].[spObterFamilia]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterFamilia]
as
  begin
    select Codigo,NomeGrupo from Grupo where PaiSN=1
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterFamiliaPorCodFamilia]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterFamiliaPorCodFamilia]
@Codigo int
as 
  begin
    select * from Grupo where  CodFamilia=@Codigo
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterFamiliaPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterFamiliaPorCodigo]
@Codigo int
as 
  begin
    select * from Grupo where CodFamilia is null and Codigo=@Codigo
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterFidelidadePessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterFidelidadePessoa]
@Codigo int 
as
  select isnull(sum(Ponto),0) as Pontos from Pessoa_Fidelidade where CodPessoa=@Codigo


GO
/****** Object:  StoredProcedure [dbo].[spObterFormaPagamento]    Script Date: 06/06/2017 14:26:22 ******/
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
ISNull(ParcelaSN ,0) as ParcelaSN,
ISNULL(GeraFinanceiro,0) AS GeraFinanceiro,
ISNULL(OnlineSN,0) AS OnlineSN,
IsNUll(CaminhoImagem,'') as CaminhoImagem,
isnull(AtivoSN,0) as AtivoSN
from FormaPagamento
end




GO
/****** Object:  StoredProcedure [dbo].[spObterFormaPagamentoAtivo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterFormaPagamentoAtivo]
as 
begin
select 
ISNull(Codigo,1) as Codigo,
ISNull(Descricao,'Dinheiro') as Descricao,
ISNull(ParcelaSN ,0) as ParcelaSN,
ISNULL(GeraFinanceiro,0) AS GeraFinanceiro,
ISNULL(OnlineSN,0) AS OnlineSN,
IsNUll(CaminhoImagem,'') as CaminhoImagem,
isnull(AtivoSN,0) as AtivoSN

from FormaPagamento
where AtivoSN=1
end



GO
/****** Object:  StoredProcedure [dbo].[spObterFPNOme]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[spObterFPNOme]
@Nome nvarchar(100)
as 
  begin
    select 
	ISNULL(Codigo,0) as Codigo,
	ISNULL(Descricao,0) as Descricao,
	ISNULL(ParcelaSn,0) as ParcelaSn,
	ISNULL(GeraFinanceiro,0) as GeraFinanceiro,
	IsNUll(CaminhoImagem,'') as CaminhoImagem,
	isnull(AtivoSN,0) as AtivoSN
	from FormaPagamento

	where Descricao=@Nome
	
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterFPPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
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
 ISNULL(ParcelaSN,0) as ParcelaSN,
 IsNUll(CaminhoImagem,'') as CaminhoImagem,
 isnull(AtivoSN,0) as AtivoSN
 from FormaPagamento
 where Codigo = @Codigo




GO
/****** Object:  StoredProcedure [dbo].[spObterGrupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterGrupo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia ,
		IsNull(MultiploSabores,0) as MultiploSabores 
			FROM Grupo 
		where PaiSN is null or PaiSN=0	
		ORDER BY NomeGrupo ASC


GO
/****** Object:  StoredProcedure [dbo].[spObterGrupoAtivo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterGrupoAtivo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia ,
		IsNull(MultiploSabores,0) as MultiploSabores 
			FROM Grupo 
			where AtivoSN=1 and (PaiSN=0 or PaiSN is null)
		ORDER BY NomeGrupo ASC


GO
/****** Object:  StoredProcedure [dbo].[spObterGrupoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterGrupoPorCodigo]
@Codigo int
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia  ,
		IsNull(MultiploSabores,0) as MultiploSabores 
			FROM Grupo 
		where Codigo = @Codigo
		ORDER BY NomeGrupo ASC


GO
/****** Object:  StoredProcedure [dbo].[spObterGrupoPorNome]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterGrupoPorNome]
@Nome nvarchar(100)
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN
		,
		IsNull(MultiploSabores,0) as MultiploSabores 
			FROM Grupo 
	    where NomeGrupo = @Nome



GO
/****** Object:  StoredProcedure [dbo].[spObterHistoricoPorPessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterHistoricoPorPessoa]
@CodPessoa int 
as
begin
 select 
 Tipo,
 Data,
Historico,
Valor
  from HistoricoPessoa  H
 where 
 CodPessoa=@CodPessoa
 END




GO
/****** Object:  StoredProcedure [dbo].[spObterHistoricoPorPessoaPorData]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterHistoricoPorPessoaPorData]
@CodPessoa int ,
@DataInicio date,
@DataFim date
as
begin
  select 
H.Tipo,
Data,
CodPessoa,
P.Nome,
P.Telefone,
Historico,
Valor
  from HistoricoPessoa  H
  Join Pessoa P on P.Codigo=H.CodPessoa
 where 
 CodPessoa=@CodPessoa  
 and Data between @DataInicio and @DataFim
 
end



GO
/****** Object:  StoredProcedure [dbo].[spObterInsumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[spObterInsumo]
 as
  select * from Insumo


GO
/****** Object:  StoredProcedure [dbo].[spObterInsumoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[spObterInsumoPorCodigo]
 @Codigo int
 as
 begin
  select * from Insumo where Codigo=@Codigo
 end 


GO
/****** Object:  StoredProcedure [dbo].[spObterInsumoPorCodProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterInsumoPorCodProduto]
 @CodProduto int
 as
 begin
  select P.Codigo,I.Nome,P.Quantidade,I.Preco from Produto_Insumo P 
  join Insumo I on I.Codigo=P.CodInsumo
  where CodProduto=@CodProduto
 end 

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsExtrasPorPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterItemsExtrasPorPedido]
		@CodPedido int
	AS
		SELECT CodPedido,Descricao,Valor FROM ItemsExtras WHERE CodPedido = @CodPedido




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsNaoImpresso]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterItemsNaoImpresso]
	@Codigo int,
	@NomeImpressora nvarchar(max)
as
	BEGIN
		SELECT  IT.* , Pe.NumeroMesa, G.NomeImpressora
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.Codigo =P.CodGrupo 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		IT.CodPedido =@Codigo
		and G.NomeImpressora =@NomeImpressora
		and IT.ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsNaoImpressoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spObterItemsNaoImpressoPorCodigo]
	@Codigo int
as
	BEGIN
		SELECT  IT.* , 
		(select NumeroMesa from Pedido where Codigo=@Codigo) as NumeroMesa, 
		G.NomeImpressora
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.NomeGrupo =P.GrupoProduto 
		WHERE 
		IT.CodPedido =@Codigo
		and IT.ImpressoSN = 0	
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsNaoImpressoPorGrupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterItemsNaoImpressoPorGrupo]
	@Codigo int,
	@CodGrupo int
as
	BEGIN
		SELECT  IT.* , Pe.NumeroMesa, 
		G.NomeImpressora,
		G.NomeGrupo
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.NomeGrupo =P.GrupoProduto 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		IT.CodPedido =@Codigo
		and G.Codigo =@CodGrupo
		and IT.ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsNaoImpressoPorImpressora]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterItemsNaoImpressoPorImpressora]
	@Codigo int,
	@NomeImpressora nvarchar(max)
as
	BEGIN
		SELECT  IT.* , Pe.NumeroMesa, G.NomeImpressora
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.Codigo =P.CODGRUPO 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		IT.CodPedido =@Codigo
		and G.NomeImpressora =@NomeImpressora
		and IT.ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsPedido]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterItemsPedidoApp]    Script Date: 06/06/2017 14:26:22 ******/
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
			isnull(i.CodUsuario,1) as CodUsuario,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			Quantidade ,
			i.Item,
			p.CodigoMesa
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo and CodigoMesa is not null and P.Finalizado=0
		ORDER BY Codigo
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsPedidoBalcaoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterItemsPedidoBalcaoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			isnull(i.CodUsuario,1) as CodUsuario,
			i.NomeProduto,
				i.PrecoItem as PrecoUnitario,
			i.PrecoTotalItem as PrecoTotal,
			Quantidade ,
			i.Item,
			P.Senha
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo and Senha is not null and P.Finalizado=0
		ORDER BY Codigo
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterItemsVendidos]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterItemsVendidosPeriodo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterItemsVendidosPeriodo]
@DataInicio date,
@DataFim    date
as 
  begin
  select 
(select Codigo from Produto P where P.Codigo = I.CodProduto ) as CodProduto,
(select P.NomeProduto from Produto P where P.Codigo = I.CodProduto) as NomeProduto,
Sum(Quantidade) as Quantidade,
Sum(I.PrecoTotalItem) as PrecoTotalItem,
cast(RealizadoEm as date) as 'Data Venda'
from 
Pedido P
join ItemsPedido I on I.CodPedido = P.Codigo
where 
P.Finalizado=1 and
cast(P.RealizadoEm as date) between @DataInicio and @DataFim
group by CodProduto,cast(RealizadoEM as date)
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterItemsVendidosPeriodoNovo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterItemsVendidosPeriodoNovo]
@DataInicio datetime,
@DataFim datetime
as
begin
select 
It.NomeProduto,
SUM(Quantidade) Quantidade,
Pr.GrupoProduto
 From ItemsPedido It
 join Pedido Pe on Pe.Codigo = It.CodPedido
 left join Produto Pr on Pr.Codigo = It.CodProduto
 where Pe.RealizadoEm between  @DataInicio  and @DataFim
 GROUP BY iT.NomeProduto,PR.GrupoProduto
 order by Quantidade desc
 
end




GO
/****** Object:  StoredProcedure [dbo].[spObterMediaVendas]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterMediaVendas]
@DataInicio datetime,
@DataFim datetime
as
select 
case Tipo 
when '0 - Entrega' then 'Delivery/Entregas'
when '1 - Mesa' then 'Mesas'
when '2 - Balcao' then 'Vendas Balcão' 
end as 'Tipo de venda',
Sum(TotalPedido) TotalVendas
,count(Codigo) as QuantidadeVendas,
cast(sum(TotalPedido)/Count(Codigo) as decimal(10,2))  MédiaPorTipo
 from Pedido
 where 
 Finalizado =1 and
 [status]!='Cancelado' and 
 RealizadoEm between @DataInicio and @DataFim
 group by Tipo
 

GO
/****** Object:  StoredProcedure [dbo].[spObterMenorPontuacaoProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterMenorPontuacaoProduto]
as
  select Isnull(Min(PontoFidelidadeTroca),0) PontoFidelidadeTroca from Produto where PontoFidelidadeTroca>0


GO
/****** Object:  StoredProcedure [dbo].[spObterMensages]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterMesas]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterMesasAbertas]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterMesasPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterMesasPorCodigo]
@Codigo int
as
 select * from Mesas where Codigo=@Codigo


GO
/****** Object:  StoredProcedure [dbo].[spObterMotivoCancelamento]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterMovimentoCaixa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterMovimentoCaixa]
@Turno nvarchar(10),
@DataI datetime,
@DataF datetime,
@CodCaixa int,
@EntradaSaida nvarchar(max)
as
 select CX.Numero , 
 CXM.DATA, 
 CXM.Historico, 
 CXM.NumeroDocumento, 
 FP.Descricao ,
 CXM.Valor,
case  
CXM.Tipo
when 'E' THEN 'Entrada'
when 'S' then 'Saida'
end  
 from CaixaMovimento  CXM
LEFT JOIN FormaPagamento FP ON FP.Codigo = CXM.CodFormaPagamento 
LEFT JOIN Caixa         CX ON CX.Codigo = CXM.CodCaixa    
where CXM.Turno =@Turno
and CXM.CodCaixa=@CodCaixa
and CXM.Data BETWEEN @DataI AND @DataF
and CXM.Tipo in (@EntradaSaida)



                               


GO
/****** Object:  StoredProcedure [dbo].[spObterMovimentoCaixa2]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterMovimentoCaixa2]
@Turno nvarchar(10),
@DataI datetime,
@DataF datetime,
@CodCaixa int
as
 select CX.Numero , 
 CXM.DATA, 
 CXM.Historico, 
 CXM.NumeroDocumento, 
 FP.Descricao ,
 CXM.Valor,
case  
CXM.Tipo
when 'E' THEN 'Entrada'
when 'S' then 'Saida'
end  
 from CaixaMovimento  CXM
LEFT JOIN FormaPagamento FP ON FP.Codigo = CXM.CodFormaPagamento 
LEFT JOIN Caixa         CX ON CX.Codigo = CXM.CodCaixa    
where CXM.Turno =@Turno
and CXM.CodCaixa=@CodCaixa
and CXM.Data BETWEEN @DataI AND @DataF




                               


GO
/****** Object:  StoredProcedure [dbo].[spObterNomeImpressoraPorCodigoPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterNomeImpressoraPorCodigoPedido]
	@Codigo int
as
	BEGIN
		SELECT  G.NomeImpressora,G.Codigo as CodGrupo
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G   on G.Codigo =P.CodGrupo 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		CodPedido =@Codigo
		and IT.ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY P.CodGrupo asc
		
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterNomeProduto]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterNumeroMesa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterNumeroVenda]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterNumeroVenda]
@Tipo nvarchar(100),
@Data datetime
  as
   begin
   select 
count(Codigo) +1 as NumeroVenda ,
cast(RealizadoEm as date) as DataVenda,
Tipo
from
Pedido
where Tipo =@Tipo
and RealizadoEm =@Data
group by Cast(RealizadoEM as  date),Tipo
   end




GO
/****** Object:  StoredProcedure [dbo].[spObterOpcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterOpcao]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  o.Nome as Nome,
  O.AtivoSN,
  O.OnlineSN,
  isnull(SinalOpcao,'') as SinalOpcao,
  isnull(DiasDisponivel,'') as  DiasDisponivel
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
end



GO
/****** Object:  StoredProcedure [dbo].[spObterOpcaoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterOpcaoApp]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  ISNULL(o.Nome,0) as Nome,
  O.AtivoSN,
  O.OnlineSN,
  O.DataSincronismo,
  O.DataAlteracao,
  O.SinalOpcao,
  O.DiasDisponivel
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
order by PO.OrdenExibicao asc
end




GO
/****** Object:  StoredProcedure [dbo].[spObterOpcaoPedidoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterOpcaoPedidoApp]
as
select * from Pedido_Opcao

GO
/****** Object:  StoredProcedure [dbo].[spObterOpcaoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterOpcaoPorCodigo]
@Codigo int
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  Nome,
  O.AtivoSN,
  O.OnlineSN,
   isnull(SinalOpcao,'') as SinalOpcao
from Opcao O
where O.Codigo=@Codigo and AtivoSN=1
end




GO
/****** Object:  StoredProcedure [dbo].[spObterOpcaoPorTipo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterOpcaoPorTipo]
@Codigo int
as
 begin
    select Codigo,Nome,Tipo,AtivoSN,OnlineSN,SinalOpcao,DiasDisponivel from Opcao where Tipo=@Codigo
 end



GO
/****** Object:  StoredProcedure [dbo].[spObterOpcaoProdutoCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterOpcaoProdutoCodigo]
@Codigo int
  as 
    begin
	  select
	  CodProduto,
	  CodOpcao,
	  ISNULL(Preco,0) as Preco,
	  ISNULL(PO.DataAlteracao,GETDATE()) as DataAlteracao,
	  ISNULL(PO.DataSincronismo,GETDATE()+1) as DataSincronismo,
	  ISNULL(PO.OnlineSN,0) as OnlineSN,
	  ISNULL(PrecoProcomocao,0) as PrecoProcomocao,
	  ISNULL(P.DataFimPromocao,GETDATE()) as DataFimPromocao
	  from Produto_Opcao PO 
	  join Produto P on P.Codigo = PO.CodProduto
	  join Opcao  O on O.Codigo = Po.CodOpcao and O.OnlineSN=1 and O.AtivoSN=1
	 where CodProduto =@Codigo
	  
	end




GO
/****** Object:  StoredProcedure [dbo].[spObterOpcaoProdutoPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterOpcaoProdutoPedido]
@CodPedido  int
as
select 
PEO.CodPedido,
PO.Nome as 'Tipo',PEO.Observacao
 ,PEO.Quantidade,PEO.Preco
 from Pedido_Opcao PEO
left join Opcao O on O.Codigo = PEO.CodOpcao
left join Produto_OpcaoTipo PO on PO.Codigo=O.Tipo
where  PEO.CodPedido=@CodPedido




GO
/****** Object:  StoredProcedure [dbo].[spObterOpcoesProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterOpcoesProduto]
@Codigo int
as 
  begin
select PO.CodOpcao,PO.Preco,
O.Nome,O.Tipo,isnull(SinalOpcao,'') as SinalOpcao
from 
Produto_Opcao PO
join Opcao O on O.Codigo = PO.CodOpcao
where PO.CodProduto = @Codigo
end




GO
/****** Object:  StoredProcedure [dbo].[spObterOpcoesProdutoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterOpcoesProdutoApp]
as
begin
  select 
  ISNULL(PO.Codigo,0) as Codigo,
  ISNULL(CodProduto,0) as CodProduto,
  ISNULL(CodOpcao,0) as CodOpcao,
  ISNULL(Preco,0) as Preco,
  isnull(CodTipo,0) as CodTipo
from Produto_Opcao PO
join Produto_OpcaoTipo POT on POT.Codigo=PO.CodTipo
order by POT.OrdenExibicao asc
end




GO
/****** Object:  StoredProcedure [dbo].[spObterPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT 
		case Pe.Tipo
		when '1 - Mesa' then  P.Nome +' - '+ Pe.NumeroMesa 
		when '2 - Balcao' then 'Cliente Balcao ' +PE.Senha +' ' + PE.Observacao
		when '0 - Entrega' then P.Nome 
		end as  'Nome Cliente',
		Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo,Pe.HorarioEntrega,isnull(Pe.ImpressoSN,1) as ImpressoSN
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoBalcaoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedidoBalcaoApp]
as
	BEGIN
		select 
			p.Codigo,
			p.CodPessoa,
			p.CodigoMesa,
			p.TotalPedido,
			p.RealizadoEm,
			p.CodUsuario,
			isnull(p.Observacao,'Cliente Balcão') as NomeCliente,
			Senha 			
		from 
			Pedido p
			inner join Pessoa pe on p.CodPessoa = pe.Codigo
		where
			p.Finalizado = 0 and P.Tipo='2 - Balcao' and Senha is not null
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoCancelado]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidoEmAbertoApp]    Script Date: 06/06/2017 14:26:22 ******/
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
			p.NumeroMesa,
			p.CodUsuario			
		from 
			Pedido p
			inner join Pessoa pe on p.CodPessoa = pe.Codigo
     	    inner join Mesas m on p.CodigoMesa = m.Codigo
		where
			p.Finalizado = 0
			and p.status = 'Aberto' AND StatusMesa=2
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoFinalizado]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidoFinalizadoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPedidoFinalizadoPorCodigo]
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
		ISNULL(PE.MargemGarcon,0) as MargemGarcon,
		ISNULL(PE.HorarioEntrega,0) as HorarioEntrega
		,
		ISNULL(PE.Observacao,' ') as Observacao
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 1 --and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END



GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoOnline]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterPedidoOnline]
@Codigo int
as
  begin
    select * from Pedido where CodigoPedidoWS!=0 and
    Codigo = @Codigo
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoOnlinePorData]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterPedidoOnlinePorData]
@DataInicio datetime,
@DataFim datetime
as
  begin
    select PS.Nome,  
	CodigoPedidoWS,FormaPagamento,TotalPedido from Pedido P
	join Pessoa PS on PS.Codigo = P.CodPessoa
	where CodigoPedidoWS!=0 and Finalizado=1 and [status]!='Cancelado' and
    RealizadoEm between @DataInicio and @DataFim  
    end




GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
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
		ISNULL(PE.MargemGarcon,0) as MargemGarcon,
        ISNULL(PE.CodUsuario,0) as CodUsuario,
		ISNULL(PE.HorarioEntrega,0) as HorarioEntrega,
		ISNULL(PE.Observacao,' ') as Observacao,
		isnull(PE.CodEndereco,0) as CodEndereco,
		isnull(PE.Senha,0) as Senha
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorData]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterPedidoPorData]
@DataInicio Datetime,
@DataFim Datetime
as
	BEGIN
		SELECT 
		Codigo,
		Tipo,
		TotalPedido
		FROM Pedido
	  WHERE Finalizado = 1  and 
	  RealizadoEm between @DataInicio and @DataFim  
	 
	 
	END




GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorNumeroMesa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[spObterPedidoPorNumeroMesa]
@Codigo int
as
begin
   select 
   I.Codigo,CodPedido,CodProduto,NomeProduto,Quantidade from ItemsPedido I
   join Pedido P on P.Codigo = I.CodPedido
    where (P.Finalizado=0 and P.[status]='Aberto')  and P.CodigoMesa = @Codigo
end



GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoPorSenha]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterPedidoPorSenha]
@Senha nvarchar(100)
as
  select Codigo,Senha,CodPessoa,TotalPedido,CodUsuario,Observacao as NomeCliente from Pedido where Senha=@Senha and Finalizado=0
  and [status]='Aberto' 


GO
/****** Object:  StoredProcedure [dbo].[spObterPedidosCanceladosPeriodo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterPedidosCanceladosPeriodo]
@DataI datetime,
@DataF datetime
as
select 
U.Nome as 'Usuario', 
FormaPagamento,
TotalPedido ,
M.Nome as 'Motivo',
H.Motivo 'Observacao',
PE.Nome ' Cliente',
PE.Telefone 
 from Pedido P
 left join Usuario U on U.Cod=P.CodUsuario
 left join HistoricoCancelamentos H on H.CodPedido=P.Codigo
 left join MotivoCancelamento  M on M.Codigo = H.CodMotivo
 join Pessoa PE on PE.Codigo = P.CodPessoa
 where 
 status='Cancelado'
 and RealizadoEm between @DataI and @DataF



GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoSemMesas]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidosPessoaPorData]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPedidoStatus]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterPedidoStatus]
 as 
   begin
     select P.Codigo,P.Nome,P.Status from PedidoStatus P
   end




GO
/****** Object:  StoredProcedure [dbo].[spObterPedidoStatusPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterPedidoStatusPorCodigo]
 @Codigo int
 as 
   begin
     select * from PedidoStatus 
      where Codigo =@Codigo
   end




GO
/****** Object:  StoredProcedure [dbo].[spObterPermissao]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterPessoa_OrigemCadastro]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterPessoa_OrigemCadastro]
as
begin
 select * from Pessoa_OrigemCadastro
end


GO
/****** Object:  StoredProcedure [dbo].[spObterPessoa_OrigemCadastroPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterPessoa_OrigemCadastroPorCodigo]
@Codigo int
as
begin
 select * from Pessoa_OrigemCadastro where Codigo=@Codigo
end

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoa_OrigemCadastroPush]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterPessoa_OrigemCadastroPush]
@Codigo int
as
begin
 
 select Nome,Telefone from Pessoa where CodOrigemCadastro=@Codigo
 and [user_id] is not null or [user_id]!=''
end

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,isnull(PontoReferencia,'') PontoReferencia,
  isnull(Observacao,'')Observacao ,Numero,Telefone,Telefone2,DataNascimento,isnull(TicketFidelidade,0) TicketFidelidade,
  CodRegiao,DataCadastro,isnull([user_id],'') as[user_id] ,isnull(DDD,'') as DDD , isnull(Sexo,'') as Sexo ,PFPJ,
  (select top 1 Codigo from Pessoa_Endereco where CodPessoa = Pessoa.Codigo order by Codigo asc) as CodEndereco,
  isnull(CodOrigemCadastro,0) as CodOrigemCadastro
  FROM Pessoa WHERE Codigo =@Codigo
 END


GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorCodOrigemCadastro]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterPessoaPorCodOrigemCadastro]
 @Codigo int
 as
  begin
    select 
	 P.Nome,P.Telefone
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	where PC.Codigo=@Codigo

  end
  

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoaPorTelefone]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[spObterPessoaPorTelefone]
 @Telefone nvarchar(100)
as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,
  Numero,DataNascimento,CodRegiao,TicketFidelidade,Telefone2,[user_id],DDD,Sexo,PFPJ,
  (Select top 1 Codigo from Pessoa_Endereco where CodPessoa = Pessoa.Codigo order by Codigo asc) as CodEndereco,
  isnull(CodOrigemCadastro,0) as CodOrigemCadastro
  FROM Pessoa 
  WHERE 
  Telefone = @Telefone or Telefone2 =@Telefone
 END



GO
/****** Object:  StoredProcedure [dbo].[spObterPessoas]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterPessoas]
as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,DataNascimento,CodRegiao,DataCadastro,PFPJ,
  isnull(CodOrigemCadastro,0) as CodOrigemCadastro
  FROM Pessoa 
 END

GO
/****** Object:  StoredProcedure [dbo].[spObterPessoasApp]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterProdutividadeGarcon]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterProdutividadeGarcon]
@DataI date,
@DataF date
as
select 
I.Quantidade,
I.PrecoTotalItem ,
 I.CodProduto, 
 I.NomeProduto
 , U.Cod
,U.Nome as Garcon
 from
ItemsPedido I
join Pedido P on P.Codigo = I.CodPedido
left join Usuario U on U.Cod = I.CodUsuario
where RealizadoEm between @DataI and @DataF and Tipo='1 - Mesa'




GO
/****** Object:  StoredProcedure [dbo].[spObterProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProduto]
as
BEGIN
SELECT 
P.Codigo,
NomeProduto as Nome,
DescricaoProduto as Descricao,
PrecoProduto as Preco,
GrupoProduto,
PrecoDesconto,
DiaSemana,
P.AtivoSN,
CodGrupo,
CodigoPersonalizado,
DataInicioPromocao,
DataFimPromocao,
UrlImagem,
Markup ,
PrecoSugerido ,
PrecoCusto,
PontoFidelidadeVenda,
PontoFidelidadeTroca,
ControlaEstoque,
EstoqueMinimo
FROM Produto P 
join Grupo G on G.Codigo = P.CodGrupo and G.AtivoSN=1
 where P.AtivoSN=1 ORDER BY Codigo ASC
END

GO
/****** Object:  StoredProcedure [dbo].[spObterProduto_OpcaoTipoGeral]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterProduto_OpcaoTipoGeral]
--@Codigo int
as 
  begin
    select * from Produto_OpcaoTipo
    --where Codigo =@Codigo
    order by OrdenExibicao asc
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterProduto_OpcaoTipoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterProduto_OpcaoTipoPorCodigo]
@Codigo int
as 
  begin
    select * from Produto_OpcaoTipo
    where Codigo =@Codigo
  end




GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoAlteracao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoAlteracao]
as
BEGIN
SELECT 
P.Codigo,
NomeProduto ,
DescricaoProduto,
P.PrecoProduto,
GrupoProduto,
PrecoDesconto,
DiaSemana,
P.AtivoSN,
CodGrupo,
CodigoPersonalizado,
DataInicioPromocao,
DataFimPromocao,
UrlImagem
FROM Produto P 
join Grupo G on G.Codigo = P.CodGrupo and G.AtivoSN=1
 where P.AtivoSN=1 ORDER BY Codigo ASC
END



GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoCodigoInterno]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterProdutoCodigoInterno]
	@Codigo nvarchar(5),
	@CodProduto int
	as
	select * from Produto where CodigoPersonalizado=@Codigo and Codigo !=@CodProduto


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoCompleto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int,
	@AtivoSN bit
as
begin
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana,CodGrupo,CodigoPersonalizado,Markup ,
	PrecoSugerido ,PrecoCusto ,PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo
	
	FROM Produto WHERE AtivoSN= @AtivoSN and Codigo = @Codigo 
end

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoCompletoPorCodPersonalizado]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoCompletoPorCodPersonalizado]
	@Codigo int,
	@AtivoSN bit
as
	SELECT Codigo,NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana,CodGrupo,CodigoPersonalizado
	,ControlaEstoque,EstoqueMinimo
	
	FROM Produto WHERE AtivoSN= @AtivoSN and CodigoPersonalizado = @Codigo ;

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoEstoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterProdutoEstoque]
	@Codigo int
as
	SELECT 
	P.Codigo,
	P.NomeProduto,
	P.PrecoProduto
	FROM Produto P
	 WHERE P.AtivoSN= 1  and P.ControlaEstoque=1 and CODGRUPO = @Codigo





GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCliente]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spObterProdutoPorCliente]
@DataInicio datetime,
@DataFim datetime,
@CodGrupo int
as
  begin
    select 
	P.Nome,P.Telefone
	from Pessoa P 
	join Pedido PE on PE.CodPessoa = P.Codigo
	join ItemsPedido I on I.CodPedido = PE.Codigo
	join Produto PR on PR.Codigo = I.CodProduto
	where 
	PE.RealizadoEM between @DataInicio and @DataFim and 
	 PE.Tipo!='1 - Mesa' and 
	Pr.CodGrupo=@CodGrupo
  end

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorClientePush]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterProdutoPorClientePush]
@DataInicio datetime,
@DataFim datetime,
@CodGrupo int
as
  begin
    select 
	P.Nome,P.Telefone
	from Pessoa P 
	join Pedido PE on PE.CodPessoa = P.Codigo
	join ItemsPedido I on I.CodPedido = PE.Codigo
	join Produto PR on PR.Codigo = I.CodProduto
	where 
	P.user_id is not null or P.user_id!='' and
	PE.RealizadoEM between @DataInicio and @DataFim and 
	 PE.Tipo!='1 - Mesa' and 
	Pr.CodGrupo=@CodGrupo
  end

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodGrupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoPorCodGrupo]
	@Codigo int
as
	SELECT *
	FROM Produto WHERE AtivoSN= 1 and CodGrupo = @Codigo;




GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT 
	NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,
	DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais,
	UrlImagem,DataInicioPromocao,DataFimPromocao,CodGrupo,AtivoSN,
	CodigoPersonalizado,Markup ,PrecoSugerido ,
	PrecoCusto,PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;
GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorCodPai]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spObterProdutoPorCodPai]
	@Codigo int
as
	SELECT *
	FROM Produto P
	left join Grupo G on G.Codigo = P.CODGRUPO
	WHERE P.AtivoSN= 1 and G.CodFamilia=@Codigo


GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoPorGrupo]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterProdutosAtivos]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterProdutosAtivos]
as
BEGIN
SELECT 
P.*
FROM Produto P 
join Grupo G on G.Codigo = P.CodGrupo and G.AtivoSN=1
 where P.AtivoSN=1 ORDER BY Codigo ASC
END




GO
/****** Object:  StoredProcedure [dbo].[spObterProdutosAtivosSemDesconto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutosAtivosSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN,CodGrupo
FROM Produto 
where AtivoSN=1 ORDER BY Codigo ASC
END




GO
/****** Object:  StoredProcedure [dbo].[spObterProdutosDisponivelsPratroca]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterProdutosDisponivelsPratroca]
@Codigo int
  as
   select NomeProduto from Produto where PontoFidelidadeTroca>=@Codigo

GO
/****** Object:  StoredProcedure [dbo].[spObterProdutoSemDesconto]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterProdutosInativos]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterProdutosInativosSemDesconto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterProdutosInativosSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN,CodGrupo
FROM Produto 
where AtivoSN=0 ORDER BY Codigo ASC
END




GO
/****** Object:  StoredProcedure [dbo].[spObterRegiaoEntrega_Bairros]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterRegiaoEntrega_Bairros]
	 as
	   begin
	   select CodRegiao,Nome,Cep  from RegiaoEntrega_Bairros
	   end




GO
/****** Object:  StoredProcedure [dbo].[spObterRegiaoEntrega_BairrosPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterRegiaoEntrega_BairrosPorCodigo]
@Codigo int
	 as
	   begin
	   select 
	   CodRegiao,
	   Nome,
	   Cep,
	   Isnull(AtivoSN,0) as AtivoSN,
	   Isnull(OnlineSN,0) as   OnlineSN
	   from RegiaoEntrega_Bairros
	   where 
	   CodRegiao = @Codigo
	   end




GO
/****** Object:  StoredProcedure [dbo].[spObterRegiaoPorNome]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterRegiaoPorNome]
@Nome nvarchar(max)
as 
begin

select * from RegiaoEntrega_Bairros where Nome=@Nome
end




GO
/****** Object:  StoredProcedure [dbo].[spObterRegioes]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterRegioesPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterRegioesPorNome]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterResumoCaixa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spObterResumoCaixa] 
@DataInicio datetime,
@DataFim datetime
as
begin
select 
F.Descricao,
Sum(ValorPagamento) 
from 
Pedido_Finalizacao PF 
join Pedido P on P.Codigo = PF.CodPedido
join FormaPagamento F on F.Codigo = PF.CodPagamento

where P.RealizadoEm between @DataInicio and @DataFim
group by(Descricao)
end




GO
/****** Object:  StoredProcedure [dbo].[spObterTaxaPorCliente]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterTaxaPorClienteEndereco]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterTaxaPorClienteEndereco]
@Codigo int
as
  begin
    select R.TaxaServico  
	from RegiaoEntrega R
	left join Pessoa_Endereco P on P.CodRegiao=R.Codigo
	where P.Codigo=@Codigo    
	 end



GO
/****** Object:  StoredProcedure [dbo].[spObterTipoOpcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterTipoOpcao]
as
begin
   select * from Produto_OpcaoTipo WHERE ATIVOSN=1
end




GO
/****** Object:  StoredProcedure [dbo].[spObterTodosItemsPedidoBalcaoApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE	 PROCEDURE [dbo].[spObterTodosItemsPedidoBalcaoApp]
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			isnull(i.CodUsuario,1) as CodUsuario,
			i.NomeProduto,
			i.PrecoItem as PrecoUnitario,
			i.PrecoTotalItem as PrecoTotal,
			Quantidade ,
			i.Item,
			P.Senha 
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			Senha is not null and P.Finalizado=0
		ORDER BY Codigo
	END

GO
/****** Object:  StoredProcedure [dbo].[spObterUsuario]    Script Date: 06/06/2017 14:26:22 ******/
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
ISNULL(DescontoMax,0) as DescontoMax,
ISNULL(EditaPedidoSN,0) as EditaPedidoSN,
ISNULL(VisualizaDadosClienteSN,0) as VisualizaDadosClienteSN,
ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN,
ISNULL(AlteraDadosClienteSN,0) as AlteraDadosClienteSN
 from Usuario
end




GO
/****** Object:  StoredProcedure [dbo].[spObterUsuarioApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spObterUsuarioApp]	
AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN
	FROM
		Usuario
END




GO
/****** Object:  StoredProcedure [dbo].[spObterUsuarioGrid]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spObterUsuarioLogin]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObterUsuarioLogin]	

@nome nvarchar(100),
@senha nvarchar(32)

AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN
	FROM
		Usuario where (Nome = @nome) AND (Senha = @senha)
END




GO
/****** Object:  StoredProcedure [dbo].[spObterUsuarioPorCodigo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterUsuarioPorCodigo]
@Codigo int
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
ISNULL(DescontoMax,0) as DescontoMax,
ISNULL(EditaPedidoSN,0) as EditaPedidoSN,
ISNULL(VisualizaDadosClienteSN,0) as VisualizaDadosClienteSN,
ISNULL(AbreFechaCaixaSN,0) as AbreFechaCaixaSN,
ISNULL(AlteraDadosClienteSN,0) as AlteraDadosClienteSN
 from Usuario
 where Cod=@Codigo
end




GO
/****** Object:  StoredProcedure [dbo].[spObterVendasPOrCliente]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spObterVendasPOrCliente]
@DataInicio date,
@DataFim date
as
select 
Count(P.Codigo) as NumeroPedidos,
(Select Nome +'-'+Telefone From Pessoa where Codigo=P.CodPessoa) as NOme,
Sum(P.TotalPedido) as TotalPedidos
from Pessoa PE 
join Pedido P on P.CodPessoa = PE.Codigo
where  REalizadoEm between @DataInicio  and @DataFim 
group by CodPessoa
order by TotalPedidos desc




GO
/****** Object:  StoredProcedure [dbo].[spObterVendasPorVendedor]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spObterVendasPorVendedor]
@DataInicio datetime,
@DataFim datetime
as
select 
Sum(TotalPedido ) as TotalPedido, 
U.Nome,U.Cod
 from Pedido P
 join Usuario U  on U.Cod=P.CodUsuario
 where 
 Finalizado=1 and (CodigoPedidoWS is null or CodigoPedidoWS=0)
 and RealizadoEM between @DataInicio and @DataFim
 group by U.Cod,U.Nome




GO
/****** Object:  StoredProcedure [dbo].[spObterViasDoPedido]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spOpcoesItensVendidos]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spOpcoesItensVendidos]
@DataInicio date,
@DataFim date
as
begin
select 
It.Item ,It.NomeProduto from
ItemsPedido It
join Pedido P on P.Codigo=It.CodPedido
where P.RealizadoEM between @DataInicio and @DataFim
and P.Finalizado=1
end




GO
/****** Object:  StoredProcedure [dbo].[spReabrirPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spReabrirPedido]
@CodPedido int
as
begin
delete from CaixaMOvimento where NumeroDocumento =@CodPedido
delete from Pedido_Finalizacao where CodPedido=@CodPedido;
delete from Pedido_OpcoesProduto where CodPedido=@CodPedido;
delete from HistoricoCancelamentos where CodPedido=@CodPedido;
update Pedido set Finalizado=0 , [status]='Aberto' , RealizadoEm=GetDate() where Codigo=@CodPedido;
update Mesas set StatusMesa =2 where Codigo=(select top 1 CodigoMesa from Pedido where Codigo=@CodPedido order by RealizadoEm desc);

end


GO
/****** Object:  StoredProcedure [dbo].[spRemoveDezPorCento]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spSinalizarPedidoConcluido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int,
	@NumeroPessoas int	
AS
    declare  @TipoMesa int
	set @TipoMesa = ( select CodigoMesa from Pedido where Codigo=@Codigo)
	if (@TipoMesa >0)
	 begin
	  exec spAlteraStatusMesa @TipoMesa,1
	 end
	Update Pedido 
	SET Finalizado = 1 , 
	HorarioFechamento =GetDate(),
	NumeroPessoas=@NumeroPessoas,
	[status]='Finalizado'
	 WHERE Codigo = @Codigo

GO
/****** Object:  StoredProcedure [dbo].[spStatusPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spStatusPedido] 
@Codigo int
 as 
   begin
     select PSM.CodStatus,PS.Nome from PedidoStatusMovimento PSM
     join PedidoStatus PS on PS.Status=PSM.CodStatus
     where CodPedido=@Codigo
     order by PSM.DataAlteracao desc
   end




GO
/****** Object:  StoredProcedure [dbo].[spTotaisCaixa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  StoredProcedure [dbo].[spTransfeMesa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spTransfeMesa]
@Codigo int,
@NumeroMesa nvarchar(max),
@CodUsuario int,
@TotalPedido decimal(10,2),
@CodigoMesa int,
@CodPessoa int,
@NewMesa  int
as
begin
  declare @CodPedidNovo int;
  declare @CodPedidoDestino int;
  declare @CodPedidoOrigem int;
  declare @Cout int;
  declare @Data datetime;
  declare @StatusMesa nvarchar(max);
  set @StatusMesa ='Transferida p/ mesa' +@NumeroMesa;
  set @Data =Getdate();
  set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @NewMesa and Finalizado=0 and status='Aberto');
  set @CodPedidoOrigem = ( select Codigo from Pedido where CodigoMesa = 1 and Finalizado=0 and status='Aberto');
  set @Cout = ( select count(Codigo) from Pedido where CodigoMesa = @NewMesa and Finalizado=0 and status='Aberto')
  if (@Cout =0)
  begin
	  -- Cria o pedido 
	  Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,@TotalPedido,0.00,'Dinheiro',@Data,'1 - Mesa',@NumeroMesa,
	  'Aberto','Balcao',@NewMesa,0,0,@CodUsuario,'','',0,''
      
	  -- Insere items no pedido baseando-se no pedido que foi cancelado
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN from ItemsPedido 
			  where CodPedido=@Codigo
      exec spCancelarPedido  @Codigo,@StatusMesa,@CodUsuario;
  end
  else
     begin
	  Exec spCancelarPedido  @CodPedidoOrigem,@StatusMesa,@CodUsuario;
      insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @Codigo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesa + Item,ImpressoSN from ItemsPedido 
			  where CodPedido=@CodPedidoOrigem
     end

	 exec spAlterarTotalPedidoApp @Codigo;
end

GO
/****** Object:  StoredProcedure [dbo].[spTransfeMesaApp]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spTransfeMesaApp]
@CodigoMesaOrigem int,
@CodigoMesaDestino int
as
begin
-- Declarando váriaveis temporarias ---
  declare @DataAtual datetime;
  declare @CodPedidNovo int;
  declare @CodigoPedidoOrigem int ;
  declare @CodUser int;
  declare @CodPessoa int;
  declare @TotalPedido decimal
  declare @CodPedidoDestino int;
  declare @NumeroMesaOrigem nvarchar(max);
  declare @NumeroMesaDestino nvarchar(max);
  declare @Cout int;
  declare @Data datetime;
  declare @StatusMesa nvarchar(max);
 
  ----- Sets ----
  set @DataAtual = Getdate()
  set @CodPessoa = (select CodPessoa from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @TotalPedido = (select TotalPedido from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodUser = (select CodUsuario from Pedido where CodigoMesa=@CodigoMesaOrigem and Finalizado=0 and [status]='Aberto');
  set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto');
  set @CodigoPedidoOrigem = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaOrigem and Finalizado=0 and status='Aberto');
  set @NumeroMesaDestino = (select NumeroMesa from Mesas where Codigo=@CodigoMesaDestino);
  set @NumeroMesaOrigem = (select NumeroMesa from Mesas where Codigo=@CodigoMesaOrigem);
   set @StatusMesa ='Transferida p/ mesa' +@NumeroMesaDestino;
  set @Data =Getdate();
  set @Cout = ( select count(Codigo) from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto')
  if (@Cout =0)
	  begin
	    Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,@TotalPedido,0.00,'Dinheiro',@DataAtual,'1 - Mesa',@NumeroMesaDestino,
	         'Aberto','Balcao',@CodigoMesaDestino,0,0,@CodUser,'','',0,''
	 
	  -- Insere items no pedido baseando-se no pedido que foi cancelado
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,isnull(ImpressoSN,1) as ImpressoSN from ItemsPedido 
			  where CodPedido=@CodigoPedidoOrigem
      exec spCancelarPedido  @CodigoPedidoOrigem,@StatusMesa,@CodUser;
	  end
  else
     begin
      insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidoDestino,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN from ItemsPedido 
			where CodPedido=@CodigoPedidoOrigem
	   
	   Exec spCancelarPedido  @CodigoPedidoOrigem,@StatusMesa,@CodUser;
     end

	 exec spAlterarTotalPedidoApp @CodPedidoDestino;
end

GO
/****** Object:  StoredProcedure [dbo].[spTransfereItemMesa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spTransfereItemMesa]
@Codigo int,
@CodigoMesaDestino int
as
begin
    declare @CodPedidoOrigem int;
	declare @CodPedidoDestino int;
	declare @NumeroMesaOrigem nvarchar(max);
	declare @CodPedidNovo int;
	declare @CodPessoa int;
	declare @CodUsuario int;
	declare @DataAtual datetime;
	declare @Count int;
	declare @CodigoMesOrigem int;
	declare @NumeroMesaDestino nvarchar(max);
	
	set @DataAtual = Getdate();
	set @CodPedidoOrigem = ( select CodPedido from ItemsPedido where Codigo=@Codigo);
	set @CodPessoa = ( select CodPessoa from Pedido where Codigo=@CodPedidoOrigem);
	set @CodUsuario = ( select CodUsuario from Pedido where Codigo=@CodPedidoOrigem);
	set @CodigoMesOrigem = ( select CodigoMesa from Pedido where Codigo=@CodPedidoOrigem);
	set @NumeroMesaOrigem = ( select NumeroMesa from Pedido where Codigo = @CodPedidoOrigem);
	set @CodPedidoDestino = ( select Codigo from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto'); 
	set @NumeroMesaDestino = ( select NumeroMesa from Mesas where Codigo=@CodigoMesaDestino)
	set @Count = ( select count(Codigo) from Pedido where CodigoMesa = @CodigoMesaDestino and Finalizado=0 and status='Aberto');
  
  if (@Count =0)
  begin
	  Exec spAdicionarPedido @CodPedidNovo output ,@CodPessoa,0,'0,00','Dinheiro',@DataAtual,'1 - Mesa',@NumeroMesaDestino,
	  'Aberto','Balcao',@CodigoMesaDestino,0,0,@CodUsuario,'','',0,''
	  -- Insere items no pedido baseando-se no pedido
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidNovo,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN 
	         from ItemsPedido 
	   where Codigo=@Codigo
   end
   else
     begin
	   -- Insere items no pedido baseando-se no pedido
	  insert into ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
	  select                   @CodPedidoDestino,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,'Trans. Mesa '+ @NumeroMesaOrigem + Item,ImpressoSN 
	         from ItemsPedido 
	   where Codigo=@Codigo
	 end
	    -- Remove o item do Pedido antigo
     delete from ItemsPedido where Codigo=@Codigo;
	 exec [spAlterarTotalPedidoApp] @Codigo;
	 exec [spAlterarTotalPedidoApp] @CodPedidoDestino;
end

GO
/****** Object:  StoredProcedure [dbo].[spVendasPorVendedor]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spVendasPorVendedor]
@DataInicio date,
@DataFim date
as
begin
select 
P.*, U.Nome from Pedido P
left join Usuario U on U.Cod=P.CodUsuario
where 
P.CodUsuario is not null
and P.Finalizado=1 and P.status<>'Cancelado'
and P.RealizadoEm between @DataInicio and @DataFim
end




GO
/****** Object:  StoredProcedure [dbo].[spZerarFidelidade]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  UserDefinedFunction [dbo].[F_NUMEROS]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[F_NUMEROS] (@STRING varchar(100)) Returns varchar(100) As
Begin
Declare @MAX int, @CARAC char(1), @NUM varchar(100)
Set @MAX = (Select Len(@STRING))
Set @NUM = ''
While @MAX > 0
Begin
Set @CARAC = (Select Right(Left(@STRING, Len(@STRING) - @MAX + 1), 1))
If @CARAC <> ''
Begin
If IsNumeric(@CARAC) = 1
Begin
Set @NUM = @NUM + @CARAC
End
End
Set @MAX = @MAX - 1
End
Return @NUM
End




GO
/****** Object:  Table [dbo].[base_cep]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[Caixa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	[Turno] [varchar](5) NULL,
	[HorarioAbertura] [time](7) NULL,
	[HorarioFechamento] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CaixaCadastro]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[CaixaDiferenca]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[CaixaMovimento]    Script Date: 06/06/2017 14:26:22 ******/
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
	[Turno] [varchar](5) NULL,
 CONSTRAINT [PK01_CAIXAMOVIMENTO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Configuracao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuracao](
	[cod] [int] IDENTITY(1,1) NOT NULL,
	[ImpViaCozinha] [nvarchar](max) NULL,
	[UsaDataNascimento] [bit] NULL,
	[UsaLoginSenha] [bit] NULL,
	[QtdCaracteresImp] [int] NULL,
	[ControlaEntregador] [bit] NULL,
	[ProdutoPorCodigo] [nvarchar](max) NULL,
	[Usa2Telefones] [bit] NULL,
	[UsaControleMesa] [bit] NULL,
	[ImprimeViaEntrega] [nvarchar](max) NULL,
	[ControlaFidelidade] [nvarchar](max) NULL,
	[DescontoDiaSemana] [bit] NULL,
	[ImpressoraCozinha] [nvarchar](100) NULL,
	[ImpressoraEntrega] [nvarchar](100) NULL,
	[ImpressoraCopaBalcao] [nvarchar](100) NULL,
	[CobraTaxaGarcon] [bit] NULL,
	[EnviaSMS] [bit] NULL,
	[DataAtualizacao] [datetime] NULL,
	[RepeteUltimoPedido] [bit] NULL,
	[RegistraCancelamentos] [bit] NULL,
	[DadosApp] [nvarchar](max) NULL,
	[CidadesAtendidas] [nvarchar](max) NULL,
	[ExigeVendedorSN] [bit] NULL,
	[CobrancaProporcionalSN] [bit] NULL,
	[DadosPush] [nvarchar](max) NULL,
	[Impressoras] [nvarchar](max) NULL,
 CONSTRAINT [Pk_cod_Config] PRIMARY KEY CLUSTERED 
(
	[cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 06/06/2017 14:26:22 ******/
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
	[CaminhoBackup] [nvarchar](max) NOT NULL,
	[VersaoBanco] [varchar](max) NULL,
	[DataInicio] [datetime] NULL,
	[UrlServidor] [nvarchar](max) NULL,
	[HorarioFuncionamento] [nvarchar](max) NULL,
	[ConfiguracaoSMS] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Empresa_HorarioEntrega]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa_HorarioEntrega](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Limite_horario_pedido] [nvarchar](6) NULL,
	[Horario_entrega] [nvarchar](20) NULL,
	[OnlineSN] [bit] NULL,
 CONSTRAINT [PK01_Empresa_HorarioEntrega] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entregador]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[Estoque]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[EventosSistema]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[FormaPagamento]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormaPagamento](
	[Codigo] [int] IDENTITY(1,2) NOT NULL,
	[Descricao] [nvarchar](max) NULL,
	[ParcelaSN] [bit] NULL,
	[GeraFinanceiro] [bit] NULL,
	[OnlineSN] [bit] NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[CaminhoImagem] [nvarchar](max) NULL,
	[DataFoto] [datetime] NULL,
	[AtivoSN] [bit] NULL,
 CONSTRAINT [PK_CODIGO_FPAGAMENTO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Grupo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NomeGrupo] [nvarchar](50) NULL,
	[ImprimeCozinhaSN] [bit] NULL,
	[OnlineSN] [bit] NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[AtivoSN] [bit] NULL,
	[NomeImpressora] [nvarchar](max) NULL,
	[CodFamilia] [int] NULL,
	[PaiSN] [bit] NULL,
	[MultiploSabores] [int] NULL,
 CONSTRAINT [PK01_GRUPO] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HistoricoCancelamentos]    Script Date: 06/06/2017 14:26:22 ******/
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
	[CodPedido] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HistoricoPessoa]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HistoricoPessoa](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NOT NULL,
	[Tipo] [char](1) NULL,
	[Valor] [decimal](10, 2) NULL,
	[Data] [date] NULL,
	[Historico] [nvarchar](100) NULL,
	[CodUsuario] [int] NULL,
 CONSTRAINT [PK01_HistoricoPessoa] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Insumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Insumo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[UnidadeMedida] [char](4) NULL,
	[AtivoSN] [bit] NULL,
	[DataCadastro] [datetime] NULL,
	[DataAlteracao] [datetime] NULL,
	[Preco] [decimal](10, 2) NULL,
 CONSTRAINT [PK01_CodInsumo] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Insumo_Estoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insumo_Estoque](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodInsumo] [int] NOT NULL,
	[Quantidade] [decimal](10, 2) NOT NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK01_CodInsumoEstoque] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemsPedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsPedido](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPedido] [int] NULL,
	[CodProduto] [int] NULL,
	[NomeProduto] [nvarchar](max) NULL,
	[Quantidade] [decimal](10, 2) NULL,
	[PrecoItem] [decimal](10, 2) NULL,
	[PrecoTotalItem] [decimal](10, 2) NULL,
	[Item] [nvarchar](max) NULL,
	[ImpressoSN] [bit] NULL,
	[CodUsuario] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mensagens]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[Mesas]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesas](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NumeroMesa] [nvarchar](10) NOT NULL,
	[CodCliente] [int] NULL,
	[StatusMesa] [nvarchar](10) NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[AtivoSN] [bit] NULL,
	[OnlineSN] [bit] NULL,
	[DataSincronismo] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_NumeroMesa] UNIQUE NONCLUSTERED 
(
	[NumeroMesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MotivoCancelamento]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  Table [dbo].[Opcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opcao](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Tipo] [int] NULL,
	[DataSincronismo] [datetime] NULL,
	[OnlineSN] [bit] NULL,
	[DataAlteracao] [datetime] NULL,
	[AtivoSN] [bit] NULL,
	[SinalOpcao] [nvarchar](10) NULL,
	[DiasDisponivel] [nvarchar](max) NULL,
 CONSTRAINT [PK01_Opcao] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NULL,
	[TotalPedido] [decimal](10, 2) NULL,
	[TrocoPara] [decimal](18, 2) NULL,
	[FormaPagamento] [nvarchar](100) NULL,
	[Finalizado] [bit] NULL,
	[RealizadoEm] [datetime] NULL,
	[Tipo] [nvarchar](20) NULL,
	[status] [nvarchar](max) NULL,
	[PedidoOrigem] [nvarchar](10) NULL,
	[CodigoMesa] [int] NULL,
	[CodUsuario] [int] NULL,
	[DescontoValor] [numeric](10, 2) NULL,
	[CodMotoboy] [int] NULL,
	[MargemGarcon] [decimal](10, 2) NULL,
	[CodigoPedidoWS] [int] NULL,
	[NumeroMesa] [nvarchar](10) NULL,
	[HorarioEntrega] [nvarchar](max) NULL,
	[Observacao] [nvarchar](max) NULL,
	[CodEndereco] [int] NULL,
	[HorarioFechamento] [datetime] NULL,
	[NumeroPessoas] [int] NULL,
	[Senha] [nvarchar](max) NULL,
	[ImpressoSN] [bit] NULL,
	[PagoFidelidade] [bit] NULL,
 CONSTRAINT [PK01_Pedido] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido_Finalizacao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido_Finalizacao](
	[CodPedido] [int] NOT NULL,
	[CodPagamento] [int] NOT NULL,
	[ValorPagamento] [decimal](10, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido_Opcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido_Opcao](
	[CodProduto] [int] NULL,
	[CodOpcao] [int] NULL,
	[CodPedido] [int] NULL,
	[Quantidade] [decimal](18, 0) NULL,
	[Preco] [decimal](10, 2) NULL,
	[Observacao] [nvarchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido_OpcoesProduto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido_OpcoesProduto](
	[CodPedido] [int] NULL,
	[CodProduto] [int] NULL,
	[CodOpcao] [int] NULL,
	[Quantidade] [decimal](10, 2) NULL,
	[Observacao] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PedidoStatus]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoStatus](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[EnviarSN] [bit] NULL,
	[AlertarSN] [bit] NULL,
	[Status] [int] NOT NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK01_PedidoStatus] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK01_PedidoStatus] UNIQUE NONCLUSTERED 
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PedidoStatusMovimento]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoStatusMovimento](
	[CodPedido] [int] NOT NULL,
	[CodStatus] [int] NOT NULL,
	[CodUsuario] [int] NOT NULL,
	[DataAlteracao] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 06/06/2017 14:26:22 ******/
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
	[Credito] [decimal](10, 2) NULL,
	[user_id] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[Pessoa] ADD [DDD] [char](2) NULL
ALTER TABLE [dbo].[Pessoa] ADD [Sexo] [char](1) NULL
ALTER TABLE [dbo].[Pessoa] ADD [CodOrigemCadastro] [int] NULL
 CONSTRAINT [PK_CODPESSOA] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pessoa_Endereco]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pessoa_Endereco](
	[CodPessoa] [int] NOT NULL,
	[Cep] [char](9) NULL,
	[Endereco] [nvarchar](max) NULL,
	[Complemento] [nvarchar](50) NULL,
	[PontoReferencia] [nvarchar](100) NULL,
	[Bairro] [nvarchar](100) NULL,
	[Cidade] [nvarchar](100) NULL,
	[UF] [char](2) NULL,
	[Numero] [nvarchar](10) NULL,
	[CodRegiao] [int] NOT NULL,
	[NomeEndereco] [nvarchar](50) NULL,
	[Codigo] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pessoa_Fidelidade]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pessoa_Fidelidade](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodPessoa] [int] NULL,
	[CodPedido] [int] NULL,
	[Ponto] [int] NULL,
	[Tipo] [char](1) NULL,
	[Data] [datetime] NULL,
 CONSTRAINT [PK01_Codigo_Fidelidade] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pessoa_OrigemCadastro]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa_OrigemCadastro](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[AtivoSN] [bit] NULL,
 CONSTRAINT [PK01_Pessoa_OrigemCadastro] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produto]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Produto](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[NomeProduto] [nvarchar](255) NULL,
	[DescricaoProduto] [nvarchar](max) NULL,
	[PrecoProduto] [decimal](10, 2) NULL,
	[GrupoProduto] [nvarchar](50) NULL,
	[PrecoDesconto] [decimal](5, 2) NULL,
	[AtivoSN] [bit] NULL,
	[OnlineSN] [bit] NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[MaximoAdicionais] [int] NULL,
	[UrlImagem] [nvarchar](max) NULL,
	[DataInicioPromocao] [date] NULL,
	[DataFimPromocao] [date] NULL,
	[DataFoto] [datetime] NULL,
	[CODGRUPO] [int] NULL,
	[DiaSemana] [nvarchar](max) NULL,
	[PercentualDesconto] [decimal](3, 2) NULL,
	[CodigoPersonalizado] [varchar](5) NULL,
	[Markup] [decimal](18, 0) NULL,
	[PrecoSugerido] [decimal](10, 2) NULL,
	[PrecoCusto] [decimal](10, 2) NULL,
	[PontoFidelidadeVenda] [int] NULL,
	[PontoFidelidadeTroca] [int] NULL,
	[ControlaEstoque] [bit] NULL,
	[EstoqueMinimo] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Produto_Estoque]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto_Estoque](
	[CodProduto] [int] NOT NULL,
	[Quantidade] [decimal](10, 2) NULL,
	[DataAtualizacao] [date] NULL,
	[PrecoCompra] [decimal](10, 2) NULL,
	[NomeProduto] [nvarchar](max) NULL,
	[CodPedido] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produto_Insumo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto_Insumo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodProduto] [int] NOT NULL,
	[CodInsumo] [int] NOT NULL,
	[Quantidade] [decimal](10, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produto_Opcao]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto_Opcao](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodProduto] [int] NOT NULL,
	[CodOpcao] [int] NOT NULL,
	[Preco] [decimal](10, 2) NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[OnlineSN] [bit] NULL,
	[PrecoProcomocao] [decimal](10, 2) NULL,
	[CodTipo] [int] NULL,
 CONSTRAINT [PK01_Produto_Opcao] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produto_OpcaoTipo]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Produto_OpcaoTipo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](30) NOT NULL,
	[Tipo] [char](2) NULL,
	[MaximoOpcionais] [int] NOT NULL,
	[MinimoOpcionais] [int] NOT NULL,
	[OrdenExibicao] [int] NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[AtivoSN] [bit] NULL,
	[OnlineSN] [bit] NULL,
 CONSTRAINT [PK_OpcaoTipo01] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegiaoEntrega]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegiaoEntrega](
	[Codigo] [int] IDENTITY(0,1) NOT NULL,
	[TaxaServico] [decimal](10, 2) NULL,
	[NomeRegiao] [nvarchar](20) NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[OnlineSN] [bit] NULL,
	[AtivoSN] [bit] NULL,
	[valorMinimoFreteGratis] [decimal](10, 2) NULL,
	[PrevisaoEntrega] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegiaoEntrega_Bairros]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegiaoEntrega_Bairros](
	[CodRegiao] [int] NULL,
	[Nome] [nvarchar](100) NULL,
	[CEP] [nvarchar](10) NULL,
	[DataCadastro] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
	[AtivoSN] [bit] NULL,
	[OnlineSN] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Cod] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NOT NULL,
	[Senha] [nvarchar](128) NULL,
	[CancelaPedidosSN] [bit] NULL,
	[AlteraProdutosSN] [bit] NULL,
	[AdministradorSN] [bit] NULL,
	[AcessaRelatoriosSN] [bit] NULL,
	[DescontoPedidoSN] [bit] NULL,
	[FinalizaPedidoSN] [bit] NULL,
	[DescontoMax] [numeric](10, 2) NULL,
	[EditaPedidoSN] [bit] NULL,
	[VisualizaDadosClienteSN] [bit] NULL,
	[AbreFechaCaixaSN] [bit] NULL,
	[AlteraDadosClienteSN] [bit] NULL,
 CONSTRAINT [Pk_Cod_Usuario] PRIMARY KEY CLUSTERED 
(
	[Cod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[XSistemas]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  View [dbo].[vwObterEmpresa]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  View [dbo].[vwObterItemsVendidos]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  View [dbo].[vwObterPedidosFinalizados]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  View [dbo].[vwObterPedidosPorBoy]    Script Date: 06/06/2017 14:26:22 ******/
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
/****** Object:  View [dbo].[vwObterXSistemas]    Script Date: 06/06/2017 14:26:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vwObterXSistemas]
 as
 select count(Codigo) as Contador from XSistemas




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
ALTER TABLE [dbo].[Insumo_Estoque] ADD  DEFAULT (getdate()) FOR [DataAlteracao]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_Finalizado]  DEFAULT ((0)) FOR [Finalizado]
GO
ALTER TABLE [dbo].[Pessoa] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Pessoa_Fidelidade] ADD  DEFAULT (getdate()) FOR [Data]
GO
ALTER TABLE [dbo].[Pessoa_OrigemCadastro] ADD  DEFAULT ((1)) FOR [AtivoSN]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT ((0)) FOR [AtivoSN]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT ((0)) FOR [PontoFidelidadeVenda]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT ((0)) FOR [PontoFidelidadeTroca]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT ((0)) FOR [ControlaEstoque]
GO
ALTER TABLE [dbo].[Produto] ADD  DEFAULT ((0)) FOR [EstoqueMinimo]
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
ALTER TABLE [dbo].[CaixaMovimento]  WITH NOCHECK ADD  CONSTRAINT [FK02_CodFormaPagamento] FOREIGN KEY([CodFormaPagamento])
REFERENCES [dbo].[FormaPagamento] ([Codigo])
GO
ALTER TABLE [dbo].[CaixaMovimento] NOCHECK CONSTRAINT [FK02_CodFormaPagamento]
GO
ALTER TABLE [dbo].[CaixaMovimento]  WITH NOCHECK ADD  CONSTRAINT [FK03_CodUsuario] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[CaixaMovimento] NOCHECK CONSTRAINT [FK03_CodUsuario]
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
ALTER TABLE [dbo].[HistoricoCancelamentos]  WITH CHECK ADD  CONSTRAINT [FK01_CodPedido_Cancelado] FOREIGN KEY([CodPedido])
REFERENCES [dbo].[Pedido] ([Codigo])
GO
ALTER TABLE [dbo].[HistoricoCancelamentos] CHECK CONSTRAINT [FK01_CodPedido_Cancelado]
GO
ALTER TABLE [dbo].[HistoricoPessoa]  WITH CHECK ADD  CONSTRAINT [FK01_HistoricoPessoa] FOREIGN KEY([CodPessoa])
REFERENCES [dbo].[Pessoa] ([Codigo])
GO
ALTER TABLE [dbo].[HistoricoPessoa] CHECK CONSTRAINT [FK01_HistoricoPessoa]
GO
ALTER TABLE [dbo].[HistoricoPessoa]  WITH CHECK ADD  CONSTRAINT [FK02_HistoricoPessoa] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[HistoricoPessoa] CHECK CONSTRAINT [FK02_HistoricoPessoa]
GO
ALTER TABLE [dbo].[Insumo_Estoque]  WITH CHECK ADD  CONSTRAINT [FK01_CodInsumo] FOREIGN KEY([CodInsumo])
REFERENCES [dbo].[Insumo] ([Codigo])
GO
ALTER TABLE [dbo].[Insumo_Estoque] CHECK CONSTRAINT [FK01_CodInsumo]
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
ALTER TABLE [dbo].[Pedido_Finalizacao]  WITH CHECK ADD  CONSTRAINT [FK01_CodPedidoFinalizacao] FOREIGN KEY([CodPedido])
REFERENCES [dbo].[Pedido] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_Finalizacao] CHECK CONSTRAINT [FK01_CodPedidoFinalizacao]
GO
ALTER TABLE [dbo].[Pedido_Finalizacao]  WITH CHECK ADD  CONSTRAINT [FK02CodPagamento] FOREIGN KEY([CodPagamento])
REFERENCES [dbo].[FormaPagamento] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_Finalizacao] CHECK CONSTRAINT [FK02CodPagamento]
GO
ALTER TABLE [dbo].[Pedido_Opcao]  WITH CHECK ADD  CONSTRAINT [FK01_Pedido_Opcao] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_Opcao] CHECK CONSTRAINT [FK01_Pedido_Opcao]
GO
ALTER TABLE [dbo].[Pedido_Opcao]  WITH CHECK ADD  CONSTRAINT [FK02_Pedido_Opcao] FOREIGN KEY([CodOpcao])
REFERENCES [dbo].[Opcao] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_Opcao] CHECK CONSTRAINT [FK02_Pedido_Opcao]
GO
ALTER TABLE [dbo].[Pedido_Opcao]  WITH CHECK ADD  CONSTRAINT [FK03_Pedido_Opcao] FOREIGN KEY([CodPedido])
REFERENCES [dbo].[Pedido] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_Opcao] CHECK CONSTRAINT [FK03_Pedido_Opcao]
GO
ALTER TABLE [dbo].[Pedido_OpcoesProduto]  WITH CHECK ADD  CONSTRAINT [FK01_CodPedidoOpcoes] FOREIGN KEY([CodPedido])
REFERENCES [dbo].[Pedido] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_OpcoesProduto] CHECK CONSTRAINT [FK01_CodPedidoOpcoes]
GO
ALTER TABLE [dbo].[Pedido_OpcoesProduto]  WITH CHECK ADD  CONSTRAINT [FK02_CodPedidoOpcoes] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_OpcoesProduto] CHECK CONSTRAINT [FK02_CodPedidoOpcoes]
GO
ALTER TABLE [dbo].[Pedido_OpcoesProduto]  WITH CHECK ADD  CONSTRAINT [FK03_CodPedidoOpcoes] FOREIGN KEY([CodOpcao])
REFERENCES [dbo].[Opcao] ([Codigo])
GO
ALTER TABLE [dbo].[Pedido_OpcoesProduto] CHECK CONSTRAINT [FK03_CodPedidoOpcoes]
GO
ALTER TABLE [dbo].[PedidoStatusMovimento]  WITH CHECK ADD  CONSTRAINT [FK01_PedidoStatusMovimento] FOREIGN KEY([CodPedido])
REFERENCES [dbo].[Pedido] ([Codigo])
GO
ALTER TABLE [dbo].[PedidoStatusMovimento] CHECK CONSTRAINT [FK01_PedidoStatusMovimento]
GO
ALTER TABLE [dbo].[PedidoStatusMovimento]  WITH CHECK ADD  CONSTRAINT [FK02_PedidoStatusMovimento] FOREIGN KEY([CodStatus])
REFERENCES [dbo].[PedidoStatus] ([Status])
GO
ALTER TABLE [dbo].[PedidoStatusMovimento] CHECK CONSTRAINT [FK02_PedidoStatusMovimento]
GO
ALTER TABLE [dbo].[PedidoStatusMovimento]  WITH CHECK ADD  CONSTRAINT [FK03_PedidoStatusMovimento] FOREIGN KEY([CodUsuario])
REFERENCES [dbo].[Usuario] ([Cod])
GO
ALTER TABLE [dbo].[PedidoStatusMovimento] CHECK CONSTRAINT [FK03_PedidoStatusMovimento]
GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK01_PessoaOrigemCadastro] FOREIGN KEY([CodOrigemCadastro])
REFERENCES [dbo].[Pessoa_OrigemCadastro] ([Codigo])
GO
ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK01_PessoaOrigemCadastro]
GO
ALTER TABLE [dbo].[Pessoa_Endereco]  WITH CHECK ADD  CONSTRAINT [FK01_CodPessoa] FOREIGN KEY([CodPessoa])
REFERENCES [dbo].[Pessoa] ([Codigo])
GO
ALTER TABLE [dbo].[Pessoa_Endereco] CHECK CONSTRAINT [FK01_CodPessoa]
GO
ALTER TABLE [dbo].[Pessoa_Endereco]  WITH CHECK ADD  CONSTRAINT [FK02_CodRegiao] FOREIGN KEY([CodRegiao])
REFERENCES [dbo].[RegiaoEntrega] ([Codigo])
GO
ALTER TABLE [dbo].[Pessoa_Endereco] CHECK CONSTRAINT [FK02_CodRegiao]
GO
ALTER TABLE [dbo].[Pessoa_Fidelidade]  WITH CHECK ADD  CONSTRAINT [FK01_CodPessoa_Fidelidade] FOREIGN KEY([CodPessoa])
REFERENCES [dbo].[Pessoa] ([Codigo])
GO
ALTER TABLE [dbo].[Pessoa_Fidelidade] CHECK CONSTRAINT [FK01_CodPessoa_Fidelidade]
GO
ALTER TABLE [dbo].[Pessoa_Fidelidade]  WITH CHECK ADD  CONSTRAINT [FK02_CodPedido_Fidelidade] FOREIGN KEY([CodPedido])
REFERENCES [dbo].[Pedido] ([Codigo])
GO
ALTER TABLE [dbo].[Pessoa_Fidelidade] CHECK CONSTRAINT [FK02_CodPedido_Fidelidade]
GO
ALTER TABLE [dbo].[Produto]  WITH CHECK ADD  CONSTRAINT [FK01_CodGrupo] FOREIGN KEY([CODGRUPO])
REFERENCES [dbo].[Grupo] ([Codigo])
GO
ALTER TABLE [dbo].[Produto] CHECK CONSTRAINT [FK01_CodGrupo]
GO
ALTER TABLE [dbo].[Produto_Opcao]  WITH CHECK ADD  CONSTRAINT [FK01_Produto] FOREIGN KEY([CodProduto])
REFERENCES [dbo].[Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Produto_Opcao] CHECK CONSTRAINT [FK01_Produto]
GO
ALTER TABLE [dbo].[Produto_Opcao]  WITH CHECK ADD  CONSTRAINT [FK02_CodOpcao] FOREIGN KEY([CodOpcao])
REFERENCES [dbo].[Opcao] ([Codigo])
GO
ALTER TABLE [dbo].[Produto_Opcao] CHECK CONSTRAINT [FK02_CodOpcao]
GO
ALTER TABLE [dbo].[Produto_Opcao]  WITH CHECK ADD  CONSTRAINT [FK03_CodTipoOpcao] FOREIGN KEY([CodTipo])
REFERENCES [dbo].[Produto_OpcaoTipo] ([Codigo])
GO
ALTER TABLE [dbo].[Produto_Opcao] CHECK CONSTRAINT [FK03_CodTipoOpcao]
GO
ALTER TABLE [dbo].[RegiaoEntrega_Bairros]  WITH NOCHECK ADD  CONSTRAINT [FK01_RegiaoEntrega] FOREIGN KEY([CodRegiao])
REFERENCES [dbo].[RegiaoEntrega] ([Codigo])
GO
ALTER TABLE [dbo].[RegiaoEntrega_Bairros] NOCHECK CONSTRAINT [FK01_RegiaoEntrega]
GO
USE [master]
GO
ALTER DATABASE [xSistemas_Desenvolvimento] SET  READ_WRITE 
GO
