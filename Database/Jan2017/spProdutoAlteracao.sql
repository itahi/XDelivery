create PROCEDURE [dbo].[spObterProdutoAlteracao]
as
BEGIN
SELECT 
P.Codigo,
NomeProduto ,
DescricaoProduto,
P.PrecoProduto,
GrupoProduto,
PrecoDesconto,
DiaSemana,
P.AtivoSN,
CodGrupo,
CodigoPersonalizado,
DataInicioPromocao,
DataFimPromocao,
UrlImagem
FROM Produto P 
join Grupo G on G.Codigo = P.CodGrupo and G.AtivoSN=1
 where P.AtivoSN=1 ORDER BY Codigo ASC
END

