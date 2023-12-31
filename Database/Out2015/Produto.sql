alter table Produto add MaximoAdicionais int 
go
ALTER PROCEDURE [dbo].[spAdicionarProduto]
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@DiaSemana nvarchar(100),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@MaximoAdicionais int 
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,
		GrupoProduto,DiaSemana,PrecoDesconto,AtivoSN,OnlineSN,DataAlteracao,MaximoAdicionais)
		Values(
			@Nome,
			@Descricao,
			@Preco,
			@GrupoProduto,
			@DiaSemana,
			@PrecoDesconto,
			@AtivoSN,
			@OnlineSN,
			@DataAlteracao,
			@MaximoAdicionais
		)
	END
	GO
ALTER PROCEDURE [dbo].[spAlterarProduto]

	@Codigo int,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@DiaSemana nvarchar(100),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
   @MaximoAdicionais int

AS
	BEGIN
		UPDATE Produto

		SET 
		
		NomeProduto = @Nome,
		DescricaoProduto = @Descricao,
		PrecoProduto = @Preco,
		GrupoProduto = @GrupoProduto,
		DiaSemana = @DiaSemana,
		PrecoDesconto = @PrecoDesconto,
		AtivoSN = @AtivoSN ,
		OnlineSN =@OnlineSN,
		DataAlteracao =@DataAlteracao ,
		MaximoAdicionais= @MaximoAdicionais
		WHERE Codigo = @Codigo;
	END
	GO
GO
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;


