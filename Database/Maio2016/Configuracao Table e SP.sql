

alter table Configuracao add [Pushauthorization] nvarchar(max);
alter table Configuracao add [Pushapp_id] nvarchar(max);
GO
update Configuracao set Pushapp_id='' , Pushauthorization=''
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
@RegistraCancelamentos bit,
@DadosApp nvarchar(max),
@Pushauthorization nvarchar(max),
@Pushapp_id nvarchar(max)

as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,
                          ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,
						  ImprimeViaEntrega,ControlaFidelidade,PedidosParaFidelidade,DescontoDiaSemana,
						  PrevisaoEntrega,PrevisaoEntregaSN,CobraTaxaGarcon ,TamanhoFont,ImpLPT,PortaLPT,EnviaSMS,
						  ViasEntrega,ViasCozinha,ViasBalcao,RepeteUltimoPedido,RegistraCancelamentos,DadosApp,Pushauthorization,Pushapp_id)
						  values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,
							@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,
							@ImprimeViaEntrega,@ControlaFidelidade,@PedidosParaFidelidade,@DescontoDiaSemana,
							@PrevisaoEntrega,@PrevisaoEntregaSN,@CobraTaxaGarcon,@TamanhoFont,@ImpLPT,@PortaLPT,@EnviaSMS,
							@ViasEntrega,@ViasCozinha,@ViasBalcao,@RepeteUltimoPedido,@RegistraCancelamentos,@DadosApp,@Pushauthorization,@Pushapp_id)
	end
GO
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
@RegistraCancelamentos bit,
@DadosApp nvarchar(max),
@Pushauthorization nvarchar(max),
@Pushapp_id nvarchar(max)
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
	RegistraCancelamentos = @RegistraCancelamentos,
	DadosApp= @DadosApp,
	Pushauthorization= @Pushauthorization,
	Pushapp_id=@Pushapp_id
	where cod=@cod
	end
GO
ALTER procedure [dbo].[spObterConfiguracao]
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
 ,isnull(DadosApp,0) as DadosApp
  ,isnull(Pushauthorization,0) as Pushauthorization
 ,isnull(Pushapp_id,0) as Pushapp_id
 from Configuracao
