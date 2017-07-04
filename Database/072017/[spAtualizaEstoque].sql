
ALTER procedure [dbo].[spAtualizaEstoque]
 @CodProduto int,
 @Quantidade decimal(10,2),
 @DataAtualizacao datetime,
 @NomeProduto nvarchar(max),
 @CodPedido int
 as
  begin
  
  insert into Produto_Estoque  values (@CodProduto,@Quantidade,GETDATE(),@CodPedido,@NomeProduto);
     
 end
