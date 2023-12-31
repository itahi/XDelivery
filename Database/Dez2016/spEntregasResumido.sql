create procedure [dbo].[spObterEntregasMotoboyResumido]
@DataInicio date,
@DataFim date
as
select 
count(Pd.Codigo) as QtdEntregas,
E.Nome as NomeEntregador
 from Pedido Pd
join Pessoa P on P.Codigo = Pd.CodPessoa
join Entregador E on E.Codigo = PD.CodMotoboy
where CodMotoboy is not null
and RealizadoEM between @DataInicio and @DataFim
group by E.Nome
