
ALTER PROCEDURE [dbo].[spObterPedidoEmAbertoApp]
as
	BEGIN
		select 
			p.Codigo,
			p.CodPessoa,
			p.CodigoMesa,
			p.TotalPedido,
			p.RealizadoEm,
			p.NumeroMesa,
			p.CodUsuario			
		from 
			Pedido p
			inner join Pessoa pe on p.CodPessoa = pe.Codigo
     	    inner join Mesas m on p.CodigoMesa = m.Codigo
		where
			p.Finalizado = 0
			and p.status = 'Aberto' AND StatusMesa=2
	END


