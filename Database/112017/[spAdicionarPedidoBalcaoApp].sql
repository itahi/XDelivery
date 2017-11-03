ALTER PROCEDURE [dbo].[spAdicionarPedidoBalcaoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@NomeCliente nvarchar(max),
	@Senha nvarchar(max)
as
	BEGIN
	   declare @DataAtual datetime;
	   set @DataAtual = Getdate();
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, Tipo, [Status], PedidoOrigem, CodUsuario,Observacao,Senha,ImpressoSN,idiFood )
			Values            (@CodPessoa, @TotalPedido, @DataAtual, '2 - Balcao', 'Aberto', 'Aplicativo',@CodUsuario,@NomeCliente,@Senha,0,'');
			SET @Codigo = SCOPE_IDENTITY()
		RETURN @Codigo
	END

