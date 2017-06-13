create procedure spContaEstoquePorGrupo
@Codigo int
as    
begin
Select PO.NomeProduto,Sum(PO.Quantidade ) as EstoqueAtual
from Produto_Estoque PO 
 join Produto P on P.Codigo=PO.CodProduto 
 join Grupo G on G.Codigo = P.CodGrupo 
 where G.Codigo=@Codigo
group by PO.NomeProduto
end