
ALTER TABLE PRODUTO ADD CODGRUPO int;
alter table Produto add Constraint FK01_CodGrupo foreign key(CodGrupo) references Grupo(Codigo)
GO

ALTER PROCEDURE [dbo].[spAdicionarProduto]
	@Codigo int output,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@CodGrupo int,
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
		GrupoProduto,CodGrupo,DiaSemana,PrecoDesconto,AtivoSN,OnlineSN,DataAlteracao,MaximoAdicionais,UrlImagem,DataInicioPromocao,DataFimPromocao)
		Values(
			@Nome,
			@Descricao,
			@Preco,
			@GrupoProduto,
			@CodGrupo,
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
	@CodGrupo int,
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
		CodGrupo =@CodGrupo,
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
GO
ALTER PROCEDURE [dbo].[spObterProduto]
as
BEGIN
SELECT 
Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,PrecoDesconto,DiaSemana,AtivoSN,CodGrupo
FROM Produto 
where AtivoSN=1 ORDER BY Codigo ASC
END
GO
ALTER PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int,
	@AtivoSN bit
as
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana,CodGrupo
	
	FROM Produto WHERE AtivoSN= @AtivoSN and Codigo = @Codigo ;
GO
ALTER PROCEDURE [dbo].[spObterProdutosAtivosSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN,CodGrupo
FROM Produto 
where AtivoSN=1 ORDER BY Codigo ASC
END
go
ALTER PROCEDURE [dbo].[spObterProdutosInativosSemDesconto]
as
BEGIN
SELECT Codigo,NomeProduto,DescricaoProduto,PrecoProduto,GrupoProduto,AtivoSN,CodGrupo
FROM Produto 
where AtivoSN=0 ORDER BY Codigo ASC
END
GO
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT 
	NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,
	DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais,UrlImagem,DataInicioPromocao,DataFimPromocao,CodGrupo
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;