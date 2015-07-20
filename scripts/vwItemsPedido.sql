CREATE VIEW vwObterItemsVendidos
as
select 
(select Codigo from Produto P where P.Codigo = I.CodProduto ) as CodProduto,
(select P.NomeProduto from Produto P where P.Codigo = I.CodProduto) as NomeProduto,
Sum(Quantidade) as Quantidade,
Sum(I.PrecoTotalItem) as PrecoTotalItem
from 
Pedido P
join ItemsPedido I on I.CodPedido = P.Codigo
group by CodProduto

  