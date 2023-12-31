
ALTER procedure [dbo].[spObterPedidosCanceladosPeriodo]
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
