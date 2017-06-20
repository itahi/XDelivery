create procedure spObterVendasPorVendedor
@DataI datetime,
@DataF datetime
as
select 
TotalPedido , RealizadoEm, U.Nome,U.Cod
 from Pedido P
join Usuario U  on U.Cod=P.CodUsuario
 where 
 Finalizado=1 and CodigoPedidoWS is null
 and RealizadoEM between @DataI and @DataF


