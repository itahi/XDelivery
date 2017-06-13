alter table Configuracao alter column ImpViaCozinha nvarchar(max);
alter table Configuracao alter column ImprimeViaEntrega nvarchar(max);
alter table Configuracao drop column ImprimiSN;
alter table Configuracao drop column PedidosParaFidelidade;
alter table Configuracao drop column PrevisaoEntregaSN;
alter table Configuracao drop column PrevisaoEntrega;
alter table Configuracao drop column TamanhoFont;
alter table Configuracao drop column ImpLPT;
alter table Configuracao drop column PortaLPT;
alter table Configuracao drop column ViasEntrega;
alter table Configuracao drop column ViasCozinha;
alter table Configuracao drop column ViasBalcao;
alter table Configuracao drop column TipoImpressao;
alter table Configuracao drop column GCM;
alter table Configuracao drop column Pushapp_id;
alter table Configuracao drop column Pushauthorization
alter table Configuracao add  DadosPush nvarchar(max);
alter table Configuracao add  Impressoras nvarchar(max);
alter table Configuracao drop column  ImpressoraCozinha;
alter table Configuracao drop column  ImpressoraEntrega ;
GO
ALTER procedure [dbo].[spAdicionarConfiguracao]
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
ALTER procedure [dbo].[spAlterarConfiguracao]
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
ALTER procedure [dbo].[spObterConfiguracao]
as 
select isnull(cod,0) as cod 
 ,isnull(ImpViaCozinha,'{"ImprimeSM":false,"TipoAgrupamento":"Sem Agrupamento"}') as ImpViaCozinha 
 ,isnull(UsaDataNascimento,0) as UsaDataNascimento 
 ,isnull(UsaLoginSenha,0) as UsaLoginSenha 
 ,isnull(QtdCaracteresImp,0) as QtdCaracteresImp 
 ,isnull(ControlaEntregador,0) as ControlaEntregador
 ,isnull(ProdutoPorCodigo,'{"PorCodigo":false,"TipoCodigo":"Personalizado"}') as ProdutoPorCodigo 
 ,isnull(Usa2Telefones,0) as Usa2Telefones 
 ,isnull(UsaControleMesa,0) as UsaControleMesa
 ,isnull(ImprimeViaEntrega,'{"ImprimeSM":false,"TipoAgrupamento":"Sem Agrupamento"}') as ImprimeViaEntrega
  ,isnull(ImpressoraCopaBalcao,'{"ImprimeSM":false,"TipoAgrupamento":"Sem Agrupamento"}') as ImprimeViaBalcao
 ,isnull(ControlaFidelidade,'') as ControlaFidelidade
 ,isnull(DescontoDiaSemana,0) as DescontoDiaSemana
 ,isnull(CobraTaxaGarcon,0) as CobraTaxaGarcon
 ,isnull(EnviaSMS,0) as EnviaSMS
 ,isnull(RepeteUltimoPedido,0) as RepeteUltimoPedido
 ,isnull(RegistraCancelamentos,0) as RegistraCancelamentos
 ,isnull(DadosApp,0) as DadosApp
 ,isnull(CidadesAtendidas,0) as CidadesAtendidas
 ,isnull(ExigeVendedorSN,0) as ExigeVendedorSN
 ,isnull(CobrancaProporcionalSN,0) as CobrancaProporcionalSN
 ,isnull(DadosPush,'') as DadosPush
 ,isnull(Impressoras,'') as Impressoras
 from Configuracao






