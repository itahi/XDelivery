
ALTER procedure [dbo].[spAdicionarProdutoInsumo]
@CodProduto int ,
@CodInsumo int ,
@Quantidade decimal(10,2)
as
begin
 insert into Produto_Insumo (CodProduto,CodInsumo,Quantidade) 
        values (@CodProduto,@CodInsumo,@Quantidade) 
end


