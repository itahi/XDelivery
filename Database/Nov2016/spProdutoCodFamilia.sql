
create PROCEDURE [dbo].[spObterProdutoPorCodPai]
	@Codigo int
as
	SELECT *
	FROM Produto P
	left join Grupo G on G.Codigo = P.CODGRUPO
	WHERE P.AtivoSN= 1 and G.CodFamilia=@Codigo