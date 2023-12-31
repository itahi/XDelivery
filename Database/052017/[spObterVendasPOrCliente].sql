ALTER procedure [dbo].[spObterVendasPOrCliente]
@DataInicio date,
@DataFim date
as
select 
Count(P.Codigo) as NumeroPedidos,
(Select Nome +'-'+Telefone From Pessoa where Codigo=P.CodPessoa) as NOme,
Sum(P.TotalPedido) as TotalPedidos
from Pessoa PE 
join Pedido P on P.CodPessoa = PE.Codigo
where PE.Tipo<>'1 - Mesa'and REalizadoEm between @DataInicio  and @DataFim 
group by CodPessoa
order by TotalPedidos desc



