
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

			exec spAlterarTotalPedidoApp @CodPedido
		end
	END
GO
ALTER PROCEDURE [dbo].[spAlterarItemPedidoApp]
	@Codigo int,
	@CodUsuario int,	
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
AS
	BEGIN
		declare @CodPedido int
		--Busco pedidos em aberto
		set @CodPedido = (select CodPedido from ItemsPedido where Codigo = @Codigo)
		
		if (@CodPedido > 0 )
		begin
			UPDATE 
				ItemsPedido			
			SET 
				Quantidade = cast(@Quantidade as decimal(10,2)),
				CodUsuario = @CodUsuario,
				PrecoItem = @PrecoUnitario,
				PrecoTotalItem = @PrecoTotal,
				Item = @Item
			WHERE 
				Codigo = @Codigo


			exec spAlterarTotalPedidoApp @CodPedido
		end
		
	END
GO
ALTER PROCEDURE [dbo].[spObterItemsPedidoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			i.CodUsuario,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			cast(Quantidade as decimal(10,2)),
			i.Item,
			p.CodigoMesa
		FROM 
			ItemsPedido i
			inner join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo 
		ORDER BY Codigo
	END
