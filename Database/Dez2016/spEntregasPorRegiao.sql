
ALTER procedure [dbo].[spObterEntregasPorRegiao]
@DataInicio datetime,
@DataFim datetime
as
select 
R.NomeRegiao,
sum(R.TaxaServico) as 'Valor das Entregas',
Count(P.Codigo) 'Qtd Entregas'
From Pedido P
join Pessoa PE on PE.Codigo = P.CodPessoa
join RegiaoEntrega R on R.Codigo = PE.CodRegiao
where Finalizado=1 and RealizadoEm between @DataInicio and @DataFim
group by R.NomeRegiao