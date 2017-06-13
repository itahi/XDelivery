create procedure spEntregasPorMotoboyAgrupado
@DataInicio datetime,
@DataFim datetime
as
select 
Count(PD.CodMotoboy) QtdEntregas,
E.Nome as NomeEntregador,
R.NomeRegiao
 from Pedido PD
join Entregador E on E.Codigo=PD.CodMotoboy
join Pessoa P on P.Codigo = PD.CodPessoa
join RegiaoEntrega R on R.Codigo = P.CodRegiao
---Where PD.Finalizado=1 and PD.RealizadoEm between @DataInicio and @DataFim
group by R.NomeRegiao,E.Nome