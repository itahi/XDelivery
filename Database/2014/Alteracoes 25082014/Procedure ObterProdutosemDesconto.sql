
create PROCEDURE [dbo].spObterProdutoSemDesconto
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto
FROM Produto ORDER BY Codigo ASC
END


