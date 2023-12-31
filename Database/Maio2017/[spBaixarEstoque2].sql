
 ALTER procedure [dbo].[spBaixarEstoque]
 @CodProduto int,
 @NomeProduto nvarchar(max),
 @Quantidade decimal(10,2),
 @CodPedido int
 as 
 begin
   declare @controlaEstoque bit ;
    set @controlaEstoque =( select ControlaEstoque from Produto where Codigo=@CodProduto);
	 if @controlaEstoque =1 
	   begin
     insert into Produto_Estoque (CodProduto,NomeProduto,Quantidade,DataAtualizacao,CodPedido)
	             values (@CodProduto,@NomeProduto,-@Quantidade,GETDATE(),@CodPedido)
      end
 end