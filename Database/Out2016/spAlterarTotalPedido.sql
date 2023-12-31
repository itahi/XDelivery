
ALTER PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2),
	@Tipo nvarchar(20),
	@NumeroMesa nvarchar(20),
	@CodUsuario int,
	@HorarioEntrega nvarchar(max),
	@DescontoValor decimal(10,2)
AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido,
		Tipo = @Tipo,
		NumeroMesa = @NumeroMesa,
		CodUsuario =@CodUsuario,
		HorarioEntrega= @HorarioEntrega,
		DescontoValor= @DescontoValor
		WHERE 
			Codigo = @Codigo --Codigo Produto
	END

