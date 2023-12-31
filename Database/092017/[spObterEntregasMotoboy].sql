
ALTER procedure [dbo].[spObterEntregasMotoboy]
@DataInicio datetime,
@DataFim datetime
as
select
E.Nome,
sum(R.TaxaServico) as 'Taxas',
count(P.Codigo) as 'QtdEntregas' 
 from Pedido P 
left join Pessoa_Endereco PE on PE.Codigo=P.CodEndereco
left join RegiaoEntrega   R  on R.Codigo =PE.CodRegiao
left join Entregador      E  on E.Codigo =P.CodMotoboy
where CodMotoboy is not null
and RealizadoEM between @DataInicio and @DataFim
group by E.Nome 





