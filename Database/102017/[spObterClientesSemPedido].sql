
ALTER procedure [dbo].[spObterClientesSemPedido]
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo, 
Telefone,PE.Nome,
(select top 1 Telefone from Empresa) as 'SeuTelefone'
from Pessoa PE 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
and SUBSTRING(Telefone,1,1)=9 and Len(Telefone)=9
end




