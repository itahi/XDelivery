
ALTER PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT P.Nome ,Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END
