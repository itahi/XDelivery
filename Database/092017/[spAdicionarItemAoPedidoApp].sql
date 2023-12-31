
ALTER PROCEDURE [dbo].[spAdicionarItemAoPedidoApp]
	@CodigoMesa int,
	@CodProduto int,
	@CodUsuario int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)	
	as
	BEGIN
		declare @CodPedido int
		--Busco pedidos em aberto
		set @CodPedido = (select Codigo from Pedido where CodigoMesa = @CodigoMesa and Finalizado = 0 and status = 'Aberto')

		--se achou o pedido
		if (@CodPedido > 0)
		begin
			INSERT INTO ItemsPedido(CodPedido, CodProduto, CodUsuario, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN,FidelidadeSN,DescontoPorcetagem)
			VALUES(@CodPedido, @CodProduto, @CodUsuario, @NomeProduto, cast(@Quantidade as decimal(10,2)), @PrecoUnitario, @PrecoTotal, @Item,0,0,0)
			exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
			exec spAlterarTotalPedidoApp @CodPedido
		end
	END
GO
ALTER PROCEDURE [dbo].[spAdicionarItemAoPedidoBalcaoApp]
	@CodProduto int,
	@Senha nvarchar(100),
	@CodUsuario int,
	@NomeProduto nvarchar(max),
	@Quantidade decimal(10,2),
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
	as
	BEGIN
		declare @CodPedido int
		set @CodPedido = (select max(Codigo) from Pedido where Senha =@Senha and Finalizado =0 and [status] = 'Aberto' );
		--se achou o pedido
		if (@CodPedido > 0)
		begin
			INSERT INTO ItemsPedido(CodPedido, CodProduto, CodUsuario, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN,FidelidadeSN,DescontoPorcetagem)
		                	VALUES (@CodPedido, @CodProduto, @CodUsuario, @NomeProduto, @Quantidade, @PrecoUnitario, @PrecoTotal, @Item,0,0,0)

            exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
			exec spAlterarTotalPedidoApp @CodPedido
		end
	END

