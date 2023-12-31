
ALTER procedure [dbo].[spObterMediaVendas]
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
 
