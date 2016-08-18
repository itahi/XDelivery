alter table ItemsPedido alter column Quantidade decimal(10,2);
GO
ALTER PROCEDURE [dbo].[spCriarPedido]
@CodPedido int,
@CodProduto int,
@NomeProduto nvarchar(max),
@Quantidade decimal(10,2),
@PrecoUnitario decimal(10,2),
@PrecoTotal decimal(10,2),
@Item nvarchar(max),
@ImpressoSN bit,
@DataAtualizacao date
AS
INSERT INTO dbo.ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
					  Values(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN)
exec spAtualizaEstoque @CodProduto,@Quantidade,@DataAtualizacao;
GO
ALTER PROCEDURE [dbo].[spAdicionarItemAoPedido]
	@CodPedido int,
	@CodProduto int,
	@NomeProduto nvarchar(max),
	@Quantidade decimal(10,2),
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max),
	@ImpressoSN bit	,
	@DataAtualizacao datetime
as
	BEGIN
		INSERT INTO ItemsPedido(CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN)
		VALUES(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN)
		
		exec spAtualizaEstoque @CodProduto,@Quantidade,@DataAtualizacao;
	END
