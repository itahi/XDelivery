alter table Pedido add HorarioFechamento datetime ;
GO
ALTER PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int	
AS
	Update Pedido SET Finalizado = 1 , HorarioFechamento =GetDate() WHERE Codigo = @Codigo

