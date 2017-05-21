
ALTER procedure [dbo].[spObterInsumoPorCodProduto]
 @CodProduto int
 as
 begin
  select P.Codigo,I.Nome,P.Quantidade from Produto_Insumo P 
  join Insumo I on I.Codigo=P.CodInsumo
  where CodProduto=@CodProduto
 end 
