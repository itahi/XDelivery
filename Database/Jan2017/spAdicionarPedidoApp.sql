

ALTER PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@NumeroMesa nvarchar(100)
	
as
	BEGIN
     	declare @NumMesa nvarchar(20);
		set @NumMesa = (select Codigo from Mesas where NumeroMesa = @NumeroMesa)
			
		INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa, CodUsuario)
	    Values(@CodPessoa, @TotalPedido, @RealizadoEm, @NumeroMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @NumMesa, 1);
		SET @Codigo = SCOPE_IDENTITY()
			
		--Atualizando status da mesa
		exec spAlteraStatusMesa @NumMesa,2
			
		RETURN @Codigo
	END

