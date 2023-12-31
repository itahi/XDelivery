
ALTER procedure [dbo].[spObterVendasPorVendedor]
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

