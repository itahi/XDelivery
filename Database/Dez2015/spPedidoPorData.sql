
ALTER PROCEDURE [dbo].[spObterPedidoPorData]
@DataInicio Datetime,
@DataFim Datetime
as
	BEGIN
		SELECT *
		FROM Pedido
		
	  WHERE Finalizado = 1  and RealizadoEm between @DataInicio and @DataFim  ORDER BY Codigo DESC
	END




