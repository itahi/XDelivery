create procedure spVendasPorVendedor
@DataInicio date,
@DataFim date
as
begin
select 
P.*, U.Nome from Pedido P
left join Usuario U on U.Cod=P.CodUsuario
where 
P.CodUsuario is not null
and P.Finalizado=1 and P.status<>'Cancelado'
and P.RealizadoEm between @DataInicio and @DataFim
end
