create procedure spObterItemsVendidosPeriodoNovo
@DataInicio datetime,
@DataFim datetime
as
begin
select 
It.CodProduto,
It.NomeProduto,
Quantidade,
Item,
Pr.GrupoProduto
 From ItemsPedido It
 join Pedido Pe on Pe.Codigo = It.CodPedido
 left join Produto Pr on Pr.Codigo = It.CodProduto
 where Pe.RealizadoEm between  @DataInicio  and @DataFim
end