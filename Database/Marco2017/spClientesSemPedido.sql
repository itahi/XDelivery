
ALTER procedure [dbo].[spObterClientesSemPedido]
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo , Telefone,PE.Nome
from Pessoa PE 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
end


