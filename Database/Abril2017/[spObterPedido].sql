
ALTER PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT P.Nome,
		case Pe.Tipo
		when '1 - Mesa' then  P.Nome +' '+ Pe.NumeroMesa 
		end as 'Nome Cliente',
		Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo,Pe.HorarioEntrega
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END


