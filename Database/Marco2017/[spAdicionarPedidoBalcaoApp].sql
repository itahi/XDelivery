
create PROCEDURE [dbo].[spAdicionarPedidoBalcaoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@NomeCliente nvarchar(max)
as
	BEGIN
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, Tipo, [Status], PedidoOrigem, CodUsuario,Observacao)
			Values            (@CodPessoa, @TotalPedido, Getdate(), '2 - Balcao', 'Aberto', 'Aplicativo',@CodUsuario,@NomeCliente);
			SET @Codigo = SCOPE_IDENTITY()
		RETURN @Codigo
	END

