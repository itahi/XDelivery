
ALTER procedure [dbo].[spObterItemsVendidosPeriodoNovo]
@DataInicio datetime,
@DataFim datetime
as
begin
select 
It.NomeProduto,
SUM(Quantidade) Quantidade,
Pr.GrupoProduto
 From ItemsPedido It
 join Pedido Pe on Pe.Codigo = It.CodPedido
 left join Produto Pr on Pr.Codigo = It.CodProduto
 where Pe.RealizadoEm between  @DataInicio  and @DataFim
 GROUP BY iT.NomeProduto,PR.GrupoProduto
 order by Quantidade desc
 
end

