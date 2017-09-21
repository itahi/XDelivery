
create procedure [dbo].[spObterItensPedidoPorValor]
@Codigo int
as 
select 
I.CodProduto,
(select CodPessoa from Pedido where Codigo=@Codigo) as CodPessoa,
(select sum(PrecoTotalItem) from ItemsPedido where CodPedido=@Codigo) as TotalItens
 from ItemsPedido I
where 
 I.CodPedido=@Codigo

