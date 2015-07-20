
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
@ImprimeViaEntrega bit,
@ControlaFidelidade bit,
@PedidosParaFidelidade int ,
@DescontoDiaSemana bit,
@PrevisaoEntrega nvarchar(10),
@PrevisaoEntregaSN bit

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
    PrevisaoEntregaSN= @PrevisaoEntregaSN
	where cod=@cod
	end
