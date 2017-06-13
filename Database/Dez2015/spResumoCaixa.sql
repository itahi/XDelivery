create procedure spObterResumoCaixa 
@DataInicio datetime,
@DataFim datetime
as
begin
select 
F.Descricao,
Sum(ValorPagamento) 
from 
Pedido_Finalizacao PF 
join Pedido P on P.Codigo = PF.CodPedido
join FormaPagamento F on F.Codigo = PF.CodPagamento

where P.RealizadoEm between @DataInicio and @DataFim
group by(Descricao)
end


