ALTER PROCEDURE [dbo].[spSinalizarPedidoConcluido]
	@Codigo int,
	@NumeroPessoas int	
AS
	Update Pedido 
	SET Finalizado = 1 , 
	HorarioFechamento =GetDate(),
	NumeroPessoas=@NumeroPessoas
	 WHERE Codigo = @Codigo
