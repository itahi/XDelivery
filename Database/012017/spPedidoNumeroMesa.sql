

ALTER procedure [dbo].[spObterPedidoPorNumeroMesa]
@Codigo int
as
begin
   select 
   I.Codigo,CodPedido,CodProduto,NomeProduto,Quantidade from ItemsPedido I
   join Pedido P on P.Codigo = I.CodPedido
    where (P.Finalizado=0 and P.[status]!='Cancelado')  and P.CodigoMesa = @Codigo
end
