
ALTER PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@CodigoMesa int
	
as
	BEGIN
     	declare @NumeroMesa nvarchar(20);
		set @NumeroMesa = (select NumeroMesa from Mesas where Codigo = @CodigoMesa and StatusMesa=1)
		
		if @NumeroMesa!=''
		begin	
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa, CodUsuario)
			Values            (@CodPessoa, @TotalPedido, Getdate(),@NumeroMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa,@CodUsuario);
			SET @Codigo = SCOPE_IDENTITY()
			--Atualizando status da mesa
			exec spAlteraStatusMesa @CodigoMesa,2
		end	
		RETURN @Codigo
	END


	select * from Pedido where Codigo=2072 
