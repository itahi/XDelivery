
ALTER procedure [dbo].[spObterOpcoesProdutoApp]
as
begin
  select 
  ISNULL(Codigo,0) as Codigo,
  ISNULL(CodProduto,0) as CodProduto,
  ISNULL(CodOpcao,0) as CodOpcao,
  ISNULL(Preco,0) as Preco,
  isnull(CodTipo,0) as CodTipo
from Produto_Opcao

end

