alter table Pedido add ImpressoSN bit;
go
ALTER PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT 
		case Pe.Tipo
		when '1 - Mesa' then  P.Nome +' - '+ Pe.NumeroMesa 
		when '2 - Balcao' then 'Cliente Balcao ' +PE.Senha +' ' + PE.Observacao
		when '0 - Entrega' then P.Nome 
		end as  'Nome Cliente',
		Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo,Pe.HorarioEntrega,isnull(Pe.ImpressoSN,1) as ImpressoSN
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END


