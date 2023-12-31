
ALTER procedure [dbo].[spVendasPorVendedor]
@DataInicio date,
@DataFim date
as
begin
select 
sum(P.TotalPedido) as 'Total dos Pedidos', 
U.Nome
from Pedido P
left join Usuario U on U.Cod=P.CodUsuario
where 
P.CodUsuario is not null
and P.Finalizado=1 and P.status<>'Cancelado'
and cast(P.RealizadoEm as date) between @DataInicio and @DataFim
group by U.Nome
end




