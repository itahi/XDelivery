
ALTER procedure [dbo].[spAtualizaEstoque]
 @CodProduto int,
 @Quantidade decimal(10,2),
 @DataAtualizacao datetime,
 @PrecoCompra decimal(10,2),
 @NomeProduto nvarchar(max),
 @CodPedido int
 as
  begin
   --declare @Contador int
   --set @Contador = (select COUNT(CodProduto) as CodProduto  from Produto_Estoque where CodProduto = @CodProduto and DataAtualizacao =@DataAtualizacao)
   
  --  if (@Contador > 0)
		--begin
		--  update Produto_Estoque set Quantidade= Quantidade + @Quantidade , DataAtualizacao=GETDATE()
		--  where CodProduto=@CodProduto
		--end  
  -- else
		 begin
		   insert into Produto_Estoque values (@CodProduto,@Quantidade,GETDATE(),@PrecoCompra,@NomeProduto,@CodPedido);
		 end
     
 end
 go
 create procedure spBaixarEstoque
 @CodProduto int,
 @NomeProduto nvarchar(max),
 @Quantidade decimal(10,2),
 @CodPedido int
 as 
 begin
     insert into Produto_Estoque (CodProduto,NomeProduto,Quantidade,DataAtualizacao,CodPedido)
	             values (@CodProduto,@NomeProduto,-@Quantidade,GETDATE(),@CodPedido)
 end



