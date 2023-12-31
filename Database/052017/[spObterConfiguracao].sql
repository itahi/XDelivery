
ALTER procedure [dbo].[spObterConfiguracao]
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






