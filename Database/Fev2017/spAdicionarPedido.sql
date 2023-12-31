

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
		set @NumeroMesa = (select top 1 NumeroMesa from Mesas where Codigo = @CodigoMesa and StatusMesa=1)
		
	    set @CodPessoa = (select top 1 Codigo from Pessoa where Observacao='Cadastrado via App')
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


