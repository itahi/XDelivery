

ALTER PROCEDURE [dbo].[spAdicionarProduto]
	@Codigo int output,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@DiaSemana nvarchar(100),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@MaximoAdicionais int,
	@UrlImagem nvarchar(max),
	@DataInicioPromocao date,
	@DataFimPromocao date
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,
		GrupoProduto,DiaSemana,PrecoDesconto,AtivoSN,OnlineSN,DataAlteracao,MaximoAdicionais,UrlImagem,DataInicioPromocao,DataFimPromocao)
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
			@MaximoAdicionais,
			@UrlImagem,
			@DataInicioPromocao,
			@DataFimPromocao
		)
		SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
	END
