create PROCEDURE [dbo].spObterPedidoFinalizado
as
	BEGIN
		SELECT *
		FROM Pedido
		
	  WHERE Finalizado = 1 ORDER BY Codigo DESC
	END

