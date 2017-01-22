
create procedure [dbo].[spObterClientesSemPedidoPush]
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo , Telefone 
from Pessoa PE 
left join Pedido PD on PD.CodPessoa=PE.Codigo 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
and PE.user_id is not null
end


