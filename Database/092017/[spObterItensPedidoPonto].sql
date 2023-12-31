ALTER procedure [dbo].[spObterItensPedidoPonto]
@Codigo int
as 
select 
P.Codigo,cast((P.PontoFidelidadeVenda*I.Quantidade) as int) as PontoFidelidadeVenda,
(select CodPessoa from Pedido where Codigo=@Codigo) as CodPessoa,
(select sum(PrecoTotalItem) from ItemsPedido where CodPedido=@Codigo) as TotalItens,
PrecoTotalItem,
PontoFidelidadeTroca,
FidelidadeSN
 from ItemsPedido I
join Produto P on P.Codigo=I.CodProduto
where P.PontoFidelidadeVenda>0
and I.CodPedido=@Codigo