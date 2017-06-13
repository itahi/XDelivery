DECLARE @retorno int;


SELECT @retorno = COUNT(COLUMN_NAME) FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Produto' AND  COLUMN_NAME = 'UrlImagem'

IF (@retorno <= 0)
BEGIN
   alter table Produto add UrlImagem nvarchar(max);
END
go
DECLARE @retorno2 int;
SELECT @retorno2 = COUNT(COLUMN_NAME) FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Produto' AND  COLUMN_NAME = 'DataInicioPromocao'

IF (@retorno2 <= 0)
BEGIN
   alter table Produto add DataInicioPromocao date
   alter table Produto add DataFimPromocao date;
   
   --update Produto set DataInicioPromocao=GetDate()
   --update Produto set DataFimPromocao=GetDate()
END


go

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
    @MaximoAdicionais int,
    @UrlImagem nvarchar(max),
    @DataInicioPromocao date,
	@DataFimPromocao date

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
		MaximoAdicionais= @MaximoAdicionais,
		UrlImagem =@UrlImagem ,
		DataInicioPromocao = @DataInicioPromocao,
		DataFimPromocao = @DataFimPromocao
		WHERE Codigo = @Codigo;
	END

go

ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT 
	NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,
	DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais,UrlImagem,DataInicioPromocao,DataFimPromocao
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;


