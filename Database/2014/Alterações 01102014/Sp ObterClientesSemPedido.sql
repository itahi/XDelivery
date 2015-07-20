create procedure spObterClientesSemPedido
@DataInicial datetime,
@DataFinal datetime
as 
begin
select Pe.Codigo , Telefone from Pessoa PE left join Pedido PD on PD.CodPessoa=PE.Codigo 
where Pe.Codigo  not in (select PD.CodPessoa from Pedido PD where PD.RealizadoEm between @DataInicial and @DataFinal)
end

