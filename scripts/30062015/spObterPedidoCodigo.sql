
create PROCEDURE [dbo].[spObterPedidoPorCodigo]
@Codigo int
as
	BEGIN
		SELECT P.Nome ,Pe.* 
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END

