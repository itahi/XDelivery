
ALTER PROCEDURE [dbo].[spExcluirItemPedido]

	@CodProduto int,
	@CodPedido int,
	@Codigo int

AS
	BEGIN
		DELETE FROM 
			ItemsPedido
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido  -- Código Pedido
			and Codigo =@Codigo;   -- Código Sequencial
	END
