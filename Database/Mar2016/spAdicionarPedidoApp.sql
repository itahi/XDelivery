

ALTER PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@CodigoMesa int,
	@NumeroMesa nvarchar(100)
	
as
	BEGIN
	--	declare @NumMesa nvarchar(20);
	--	set @NumMesa = (select NumeroMesa from Mesas where Codigo = @CodigoMesa)
			
		INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa, CodUsuario)
	--	Values(@CodPessoa, @TotalPedido, @RealizadoEm, @NumMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa, @CodUsuario);
	    Values(@CodPessoa, @TotalPedido, @RealizadoEm, @NumeroMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa, @CodUsuario);
		SET @Codigo = SCOPE_IDENTITY()
			
		--Atualizando status da mesa
		update Mesas set StatusMesa = 2 where Codigo = @CodigoMesa
			
		RETURN @Codigo
	END

GO

