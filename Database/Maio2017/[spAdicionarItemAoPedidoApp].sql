
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
			INSERT INTO ItemsPedido(CodPedido, CodProduto, CodUsuario, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN)
			VALUES(@CodPedido, @CodProduto, @CodUsuario, @NomeProduto, cast(@Quantidade as decimal(10,2)), @PrecoUnitario, @PrecoTotal, @Item,0)
			exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
			exec spAlterarTotalPedidoApp @CodPedido
		end
	END