
ALTER procedure [dbo].[spObterOpcoesProdutoApp]
as
begin
  select 
  ISNULL(PO.Codigo,0) as Codigo,
  ISNULL(CodProduto,0) as CodProduto,
  ISNULL(CodOpcao,0) as CodOpcao,
  ISNULL(Preco,0) as Preco,
  isnull(CodTipo,0) as CodTipo
from Produto_Opcao PO
join Produto_OpcaoTipo POT on POT.Codigo=PO.CodTipo
order by POT.OrdenExibicao asc
end


