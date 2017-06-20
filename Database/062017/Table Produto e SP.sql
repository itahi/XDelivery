alter table Produto add  PalavrasChaves nvarchar(max);
GO
update Produto set PalavrasChaves ='';
go
ALTER PROCEDURE [dbo].[spAdicionarProduto]
	@Codigo int output,
	@Nome nvarchar(50),
	@Descricao nvarchar(max),
	@Preco decimal(10,2),
	@GrupoProduto nvarchar(50),
	@CodGrupo int,
	@DiaSemana nvarchar(max),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@MaximoAdicionais int,
	@UrlImagem nvarchar(max),
	@DataInicioPromocao date,
	@DataFimPromocao date,
	@CodigoPersonalizado varchar(5),
	@Markup decimal,
	@PrecoSugerido decimal(10,2),
	@PrecoCusto decimal(10,2),
	@PontoFidelidadeVenda int,
	@PontoFidelidadeTroca int,
	@ControlaEstoque bit,
	@EstoqueMinimo decimal(8,2),
	@PalavrasChaves nvarchar(max)
	
as
	BEGIN
		INSERT INTO Produto(NomeProduto,DescricaoProduto,PrecoProduto,
		GrupoProduto,CodGrupo,DiaSemana,PrecoDesconto,AtivoSN,OnlineSN,DataAlteracao,MaximoAdicionais,
		UrlImagem,DataInicioPromocao,DataFimPromocao,CodigoPersonalizado,Markup,PrecoSugerido,PrecoCusto,
		PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo,PalavrasChaves)
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
			@DataFimPromocao,
			@CodigoPersonalizado,
			@Markup ,
			@PrecoSugerido ,
			@PrecoCusto,
			@PontoFidelidadeVenda,
			@PontoFidelidadeTroca,
			@ControlaEstoque,
			@EstoqueMinimo,
			@PalavrasChaves
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
	@DiaSemana nvarchar(max),
	@PrecoDesconto decimal(5,2),
	@AtivoSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
    @MaximoAdicionais int,
    @UrlImagem nvarchar(max),
    @DataInicioPromocao date,
	@DataFimPromocao date,
	@CodigoPersonalizado varchar(5),
	@Markup decimal,
	@PrecoSugerido decimal (10,2),
	@PrecoCusto decimal (10,2),
	@PontoFidelidadeVenda int,
	@PontoFidelidadeTroca int,
	@ControlaEstoque bit,
	@EstoqueMinimo decimal(8,2),
	@PalavrasChaves nvarchar(max)

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
		DataFimPromocao = @DataFimPromocao,
		CodigoPersonalizado =@CodigoPersonalizado,
		Markup=@Markup ,
		PrecoSugerido=@PrecoSugerido ,
		PrecoCusto=@PrecoCusto,
		PontoFidelidadeVenda=@PontoFidelidadeVenda ,
	    PontoFidelidadeTroca= @PontoFidelidadeTroca ,
		ControlaEstoque = @ControlaEstoque,
		EstoqueMinimo= @EstoqueMinimo,
		PalavrasChaves=@PalavrasChaves
		WHERE Codigo = @Codigo;
	END


GO
ALTER PROCEDURE [dbo].[spObterProduto]
as
BEGIN
SELECT 
P.Codigo,
NomeProduto as Nome,
DescricaoProduto as Descricao,
PrecoProduto as Preco,
GrupoProduto,
PrecoDesconto,
DiaSemana,
P.AtivoSN,
CodGrupo,
CodigoPersonalizado,
DataInicioPromocao,
DataFimPromocao,
UrlImagem,
Markup ,
PrecoSugerido ,
PrecoCusto,
PontoFidelidadeVenda,
PontoFidelidadeTroca,
ControlaEstoque,
EstoqueMinimo,
PalavrasChaves
FROM Produto P 
join Grupo G on G.Codigo = P.CodGrupo and G.AtivoSN=1
 where P.AtivoSN=1 ORDER BY Codigo ASC
END
GO
ALTER PROCEDURE [dbo].[spObterProdutoCompleto]
	@Codigo int,
	@AtivoSN bit
as
begin
	SELECT NomeProduto,PrecoProduto,DescricaoProduto,PrecoDesconto,DiaSemana,CodGrupo,CodigoPersonalizado,Markup ,
	PrecoSugerido ,PrecoCusto ,PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo,PalavrasChaves
	
	FROM Produto WHERE AtivoSN= @AtivoSN and Codigo = @Codigo 
end
GO
ALTER PROCEDURE [dbo].[spObterProdutoPorCodigo]
	@Codigo int,
	@AtivoSN bit
as
	SELECT 
	NomeProduto,PrecoProduto,DescricaoProduto,GrupoProduto,
	DiaSemana,PrecoDesconto,OnlineSN,Codigo,MaximoAdicionais,
	UrlImagem,DataInicioPromocao,DataFimPromocao,CodGrupo,AtivoSN,
	CodigoPersonalizado,Markup ,PrecoSugerido ,
	PrecoCusto,PontoFidelidadeVenda,PontoFidelidadeTroca,ControlaEstoque,EstoqueMinimo,PalavrasChaves
	FROM Produto WHERE  AtivoSN= @AtivoSN and Codigo = @Codigo;