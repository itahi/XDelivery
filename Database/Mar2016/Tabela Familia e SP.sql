
alter procedure spAdicionarFamilia
@Nome   nvarchar(max) ,
@AtivoSN bit,
@OnlineSN bit,
@DataAlteracao datetime,
@PaiSN bit
as 
  begin
     insert into Grupo (NomeGrupo,AtivoSN,OnlineSN,DataAlteracao,PaiSN)
           values (@Nome,@AtivoSN,@OnlineSN,@DataAlteracao,@PaiSN)
  end
go
alter procedure spAlterarFamilia
@Codigo int,
@Nome   nvarchar(max) ,
@AtivoSN bit,
@OnlineSN bit,
@DataAlteracao datetime,
@PaiSN bit

as 
  begin
     update Grupo set 
     NomeGrupo=@Nome,
     AtivoSN=@AtivoSN,
     OnlineSN=@OnlineSN,
     DataAlteracao=@DataAlteracao,
     PaiSN = @PaiSN

     where Codigo=@Codigo
  end
go
alter procedure spExcluirFamilia 
 @Codigo int
 as
   begin
     delete from Grupo where Codigo=@Codigo
   end  
go
alter procedure spObterFamilia
as
  begin
    select Codigo,NomeGrupo from Grupo where PaiSN=1
  end   
go
alter procedure spObterFamiliaPorCodigo
@Codigo int
as 
  begin
    select * from Grupo where CodFamilia is not null and Codigo=@Codigo
  end  
  go
  ---------------------------------------------------
Alter table Grupo add CodFamilia int null
go
alter table Grupo add Constraint FK01_CodFamilia  foreign key (CodFamilia) references Familia(Codigo)
GO
ALTER PROCEDURE [dbo].[spAdicionarGrupo]

	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit ,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int 
	
as
	BEGIN
		INSERT INTO Grupo(NomeGrupo,ImprimeCozinhaSN,OnlineSN,DataAlteracao,AtivoSN,NomeImpressora,CodFamilia)
		Values(@NomeGrupo,@ImprimeCozinhaSN,@OnlineSN,@DataAlteracao,@AtivoSN,@NomeImpressora,@CodFamilia )
	END
GO

ALTER PROCEDURE [dbo].[spAlterarGrupo]

	@Codigo int,
	@NomeGrupo nvarchar(50),
	@ImprimeCozinhaSN bit,
	@OnlineSN bit,
	@DataAlteracao datetime,
	@AtivoSN bit,
	@NomeImpressora nvarchar(max),
	@CodFamilia int
AS
	BEGIN
		UPDATE Grupo

		SET 
		NomeGrupo = @NomeGrupo,
		ImprimeCozinhaSN =@ImprimeCozinhaSN,
		OnlineSN= @OnlineSN,
		DataAlteracao =@DataAlteracao ,
		AtivoSN =@AtivoSN,
		NomeImpressora =@NomeImpressora,
		CodFamilia = @CodFamilia
		WHERE Codigo = @Codigo

		update Produto set 
		AtivoSN = @AtivoSN ,
		OnlineSN=@OnlineSN,
		DataAlteracao = @DataAlteracao
		
		   where 
		   Produto.GrupoProduto = @NomeGrupo and Produto.AtivoSN = @AtivoSN AND Produto.OnlineSN=@OnlineSN
	END	

GO
ALTER PROCEDURE [dbo].[spObterGrupo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		ISNULL(AtivoSN,0) AS AtivoSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia 
			FROM Grupo 
		where PaiSN is null or PaiSN=0	
		ORDER BY NomeGrupo ASC
go
ALTER PROCEDURE [dbo].[spObterGrupoAtivo]
	as
		SELECT 
		IsNull(Codigo,0) as Codigo,
		IsNull(NomeGrupo,'Padrao') as NomeGrupo,
		IsNull(ImprimeCozinhaSN,0) as ImprimeCozinhaSN,
		ISNULL(OnlineSN,0) AS OnlineSN,
		Isnull(NomeImpressora,'') as NomeImpressora,
		IsNull(CodFamilia,0) as CodFamilia 

			FROM Grupo 
			where AtivoSN=1
		ORDER BY NomeGrupo ASC
		go
