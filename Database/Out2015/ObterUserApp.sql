
ALTER PROCEDURE [dbo].[spObterUsuarioApp]	
AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN
	FROM
		Usuario
END