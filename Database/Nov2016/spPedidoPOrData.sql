
ALTER PROCEDURE [dbo].[spObterPedidoPorData]
@DataInicio Datetime,
@DataFim Datetime
as
	BEGIN
		SELECT 
		Codigo,
		Tipo,
		TotalPedido
		FROM Pedido
	  WHERE Finalizado = 1  and 
	  RealizadoEm between @DataInicio and @DataFim  
	 
	 
	END

