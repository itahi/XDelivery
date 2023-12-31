
ALTER PROCEDURE [dbo].[spAlterarItemPedido]

	@CodProduto int,
	@CodPedido int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max),
	@ImpressoSN bit,
	@DataAtualizacao date
AS
	BEGIN
		UPDATE ItemsPedido

		SET 
		NomeProduto = @NomeProduto,
		Quantidade = @Quantidade,
		PrecoItem = @PrecoUnitario,
		PrecoTotalItem = @PrecoTotal,
		ImpressoSN=@ImpressoSN,
		Item = @Item
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido ; -- Código Pedido
		exec spAtualizaEstoque @CodProduto,@Quantidade,@DataAtualizacao	
	END




