update Usuario set FinalizaPedidoSN=0 where FinalizaPedidoSN is null
go
ALTER PROCEDURE [dbo].[spObterUsuarioApp]	
AS
BEGIN
	SELECT
		cod,
		Nome,
		Senha,
		AdministradorSN,
		FinalizaPedidoSN
	FROM
		Usuario
END




