
ALTER PROCEDURE [dbo].[spExcluirItemPedido]

	@CodProduto int,
	@CodPedido int,
	@Codigo int,
	@NomeProduto nvarchar(max)

AS
	BEGIN
		DELETE FROM 
			ItemsPedido
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido  -- Código Pedido
			and Codigo =@Codigo   -- Código Sequencial
			and NomeProduto =@NomeProduto
     exec spDeletarEstoque @CodPedido,@CodProduto,@NomeProduto
	END



