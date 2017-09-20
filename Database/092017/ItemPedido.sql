alter table ItemsPedido add FidelidadeSN bit;
alter table ItemsPedido add DescontoPorcetagem numeric(10,2);
go
update ItemsPedido set FidelidadeSN=0;
update ItemsPedido set DescontoPorcetagem=0;
go
ALTER PROCEDURE [dbo].[spAdicionarItemAoPedido]
	@CodPedido int,
	@CodProduto int,
	@NomeProduto nvarchar(max),
	@Quantidade decimal(10,2),
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max),
	@ImpressoSN bit	,
	@DataAtualizacao datetime,
	@DescontoPorcetagem decimal,
	@FidelidadeSN bit
as
	BEGIN
		INSERT INTO ItemsPedido(CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN,DescontoPorcetagem,FidelidadeSN)
		VALUES(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN,@DescontoPorcetagem,@FidelidadeSN)
		
		exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
	END




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
@DataAtualizacao date,
@DescontoPorcetagem decimal,
@FidelidadeSN bit
AS
INSERT INTO dbo.ItemsPedido (CodPedido,CodProduto,NomeProduto,Quantidade,PrecoItem,PrecoTotalItem,Item,ImpressoSN,DescontoPorcetagem,FidelidadeSN)
					  Values(@CodPedido,@CodProduto,@NomeProduto,@Quantidade,@PrecoUnitario,@PrecoTotal,@Item,@ImpressoSN,@DescontoPorcetagem,@FidelidadeSN)
exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido
GO

ALTER PROCEDURE [dbo].[spAlterarItemPedido]

	@CodProduto int,
	@CodPedido int,
	@Codigo int,
	@NomeProduto nvarchar(max),
	@Quantidade int,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max),
	@ImpressoSN bit,
	@DataAtualizacao date,
	@DescontoPorcetagem numeric(10,2),
	@FidelidadeSN bit
AS
	BEGIN
		UPDATE ItemsPedido

		SET 
		NomeProduto = @NomeProduto,
		Quantidade = @Quantidade,
		PrecoItem = @PrecoUnitario,
		PrecoTotalItem = @PrecoTotal,
		ImpressoSN=0,
		Item = @Item,
		DescontoPorcetagem=@DescontoPorcetagem
		WHERE 
			CodProduto = @CodProduto --Codigo Produto
			and CodPedido = @CodPedido  -- Código Pedido
			and Codigo = @Codigo;
        exec spDeletarEstoque @CodPedido,@CodProduto,@NomeProduto;
		exec spBaixarEstoque @CodProduto,@NomeProduto,@Quantidade,@CodPedido;
	END




