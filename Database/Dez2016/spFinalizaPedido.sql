
ALTER procedure [dbo].[spAlteraFinalizaPedido_Pedido]
@CodPedido int,
@CodPagamento int,
@ValorPagamento decimal(10,2)
 as
   begin
    declare @JaExiste int
    set @JaExiste = (select COUNT(CodPedido) from Pedido_Finalizacao where /*CodPagamento=@CodPagamento and */CodPedido=@CodPedido)
     if @JaExiste>0
      begin
       update Pedido_Finalizacao set
      CodPagamento = @CodPagamento,
      ValorPagamento = @ValorPagamento
      where CodPedido = @CodPedido /*and CodPagamento=@CodPagamento*/
      end
       else
         begin
          exec spAdicionarFinalizaPedido_Pedido @CodPedido,@CodPagamento,@ValorPagamento
         end
   end

