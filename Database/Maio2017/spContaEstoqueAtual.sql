create procedure spContaEstoqueAtual
@NomeProduto nvarchar(max)
as
begin
select NomeProduto,Sum(Quantidade)  as EstoqueAtual 
from Produto_Estoque 
where NomeProduto=@NomeProduto
group by NomeProduto
end