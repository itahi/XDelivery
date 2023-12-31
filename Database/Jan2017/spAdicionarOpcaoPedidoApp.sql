
ALTER procedure [dbo].[spAdicionarOpcaoPedidoApp]
 @CodProduto int,
 @NumeroMesa nvarchar(max),
 @CodOpcao   int,
 @Quantidade decimal,
 @Preco      decimal(10,2),
 @Observacao nvarchar(100)
 as
  
   begin
    declare  @CodPedido  int;
    set @CodPedido = (select Codigo from Pedido where Finalizado=0 and NumeroMesa=@NumeroMesa);

      insert into Pedido_Opcao (CodProduto,CodOpcao,CodPedido,Quantidade,Preco,Observacao)
	         values (@CodProduto,@CodOpcao,@CodPedido,@Quantidade,@Preco,@Observacao)
   end


