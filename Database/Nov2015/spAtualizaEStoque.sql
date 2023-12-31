

ALTER procedure [dbo].[spAtualizaEstoque]
 @CodProduto int,
 @Quantidade decimal(10,2),
 @DataAtualizacao datetime
 
 as
  begin
   declare @Contador int
   set @Contador = (select COUNT(CodProduto) as CodProduto  from Produto_Estoque where CodProduto = @CodProduto and DataAtualizacao =@DataAtualizacao)
   
    if (@Contador > 0)
		begin
		  update Produto_Estoque set Quantidade= Quantidade + @Quantidade , DataAtualizacao=GETDATE()
		  where CodProduto=@CodProduto
		end  
   else
		 begin
		   insert into Produto_Estoque values (@CodProduto,@Quantidade,GETDATE());
		 end
     
 end;