
ALTER procedure [dbo].[spAlterarProdutoInsumo]
@Codigo int,
@Quantidade decimal(10,2)
as
begin
 update Produto_Insumo
  set Quantidade=@Quantidade 
  where Codigo=@Codigo
end
