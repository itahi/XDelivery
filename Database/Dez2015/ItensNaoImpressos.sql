alter table Grupo add NomeImpressora nvarchar(max);
go
ALTER PROCEDURE [dbo].[spAdicionarGrupo]

	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max)
	
as
	BEGIN
		INSERT INTO Grupo(NomeGrupo,ImprimeCozinhaSN,OnlineSN,DataAlteracao,AtivoSN,NomeImpressora)
		Values(@NomeGrupo,@ImprimeCozinhaSN,@OnlineSN,@DataAlteracao,@AtivoSN,@NomeImpressora )
	END
go
ALTER PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max)
AS
	BEGIN
		UPDATE Grupo

		SET 
		NomeGrupo = @NomeGrupo,
		ImprimeCozinhaSN =@ImprimeCozinhaSN,
		OnlineSN= @OnlineSN,
		DataAlteracao =@DataAlteracao ,
		AtivoSN =@AtivoSN,
		NomeImpressora =@NomeImpressora
		WHERE Codigo = @Codigo

		update Produto set AtivoSN = @AtivoSN ,OnlineSN=@OnlineSN, DataAlteracao = @DataAlteracao
		   where Produto.GrupoProduto = @NomeGrupo
	END	
go
ALTER PROCEDURE [dbo].[spObterItemsNaoImpresso]
	@Codigo int	
as
	BEGIN
		SELECT top 1 IT.* , Pe.NumeroMesa, G.NomeImpressora
		FROM ItemsPedido IT 
		left join Produto P on P.Codigo = IT.CodProduto 
		left join Grupo G on G.NomeGrupo =P.GrupoProduto 
		left join Pedido Pe on Pe.Codigo = IT.CodPedido
		WHERE 
		IT.CodPedido = @Codigo
		and ImpressoSN = 0
		and G.ImprimeCozinhaSN=1
		ORDER BY IT.Codigo asc
	END
