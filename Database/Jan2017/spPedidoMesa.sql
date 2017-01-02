create procedure spObterPedidoPorNumeroMesa
@Codigo int
as
begin
   select 
   I.Codigo,CodPedido,CodProduto,NomeProduto,Quantidade,P.TotalPedido from ItemsPedido I
   join Pedido P on P.Codigo = I.CodPedido
    where P.Finalizado=0  and P.CodigoMesa = @Codigo
end
