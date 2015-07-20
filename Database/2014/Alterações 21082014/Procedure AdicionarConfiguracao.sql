
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
@PrevisaoEntregaSN bit

as 
begin
insert into Configuracao (ImpViaCozinha,UsaDataNascimento,UsaLoginSenha,QtdCaracteresImp,
                          ControlaEntregador,ProdutoPorCodigo,Usa2Telefones,UsaControleMesa,
						  ImprimeViaEntrega,ControlaFidelidade,PedidosParaFidelidade,DescontoDiaSemana,PrevisaoEntrega,PrevisaoEntregaSN) 
						  values
                            (@ImpViaCozinha,@UsaDataNascimento,@UsaLoginSenha,@QtdCaracteresImp,
							@ControlaEntregador,@ProdutoPorCodigo,@Usa2Telefones,@UsaControleMesa,
							@ImprimeViaEntrega,@ControlaFidelidade,@PedidosParaFidelidade,@DescontoDiaSemana,@PrevisaoEntrega,@PrevisaoEntregaSN)
end




