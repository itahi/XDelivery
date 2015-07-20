create view vwObterPedidosPorBoy
as
select 
count(P.CodMotoboy) as QuantidadeEntregas,
cast(P.RealizadoEm as date) as RealizadoEm,
(select Codigo from Entregador E where P.CodMotoboy = E.Codigo) as CodMotoboy, 
(select Nome from Entregador E where P.CodMotoboy = E.Codigo) as Nome 
from
Pedido P
where P.Finalizado =1
group by P.CodMotoboy,cast(P.RealizadoEm as date)




