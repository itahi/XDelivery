
ALTER PROCEDURE [dbo].[spObterUsuarioLogin]	

@nome nvarchar(100),
@senha nvarchar(32)

AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN
	FROM
		Usuario where (Nome = @nome) AND (Senha = @senha)
END

