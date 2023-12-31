

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
		--set @CodPedido = (select CodPedido from ItemsPedido where Codigo = @Codigo)
		
		if (@CodPedido > 0 )
		begin
			UPDATE 
				ItemsPedido			
			SET 
				Quantidade = cast(@Quantidade as decimal(10,2)),
				CodUsuario = @CodUsuario,
				PrecoItem = @PrecoUnitario,
				PrecoTotalItem = @PrecoTotal,
				Item = @Item,
				ImpressoSN=0
			WHERE 
				Codigo = @Codigo

			exec spAlterarTotalPedidoApp @Codigo
		end
		
	END


