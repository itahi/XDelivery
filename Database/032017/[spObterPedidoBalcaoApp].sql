create PROCEDURE [dbo].[spObterPedidoBalcaoApp]
as
	BEGIN
		select 
			p.Codigo,
			p.CodPessoa,
			p.CodigoMesa,
			p.TotalPedido,
			p.RealizadoEm,
			p.CodUsuario,
			isnull(p.Observacao,'Cliente Balcão') as NomeCliente 			
		from 
			Pedido p
			inner join Pessoa pe on p.CodPessoa = pe.Codigo
		where
			p.Finalizado = 0 and P.Tipo='2 - Balcao'
	END


