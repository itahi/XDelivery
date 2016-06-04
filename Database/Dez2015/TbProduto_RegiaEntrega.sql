alter table Produto add DataFoto datetime ;
go
update Produto set DataFoto = GETDATE()
go
Alter table RegiaoEntrega_Bairros add AtivoSN bit;
Alter table RegiaoEntrega_Bairros add OnlineSN bit;
go
update RegiaoEntrega_Bairros set AtivoSN=1;
update RegiaoEntrega_Bairros set OnlineSN=1;
GO
ALTER procedure [dbo].[spAdicionaBairrosRegiao]
@CodRegiao int,
@Nome  nvarchar(100),
@CEP  nvarchar(10),
@DataCadastro datetime,
@AtivoSN bit,
@OnlineSN bit
	as
	 begin
	 Insert into RegiaoEntrega_Bairros (CodRegiao,Nome,Cep,DataCadastro,AtivoSN,OnlineSN) 
	        values (@CodRegiao,@Nome,@Cep,@DataCadastro,@AtivoSN,@OnlineSN)
	 end
	 GO
ALTER procedure [dbo].[spAlterarBairrosRegiao]
@CodRegiao int,
@Nome  nvarchar(100),
@CEP  nvarchar(10),
@DataCadastro datetime,
@AtivoSN bit,
@OnlineSN bit
as 
  begin
  update RegiaoEntrega_Bairros 
  set 
  CodRegiao = @CodRegiao,
  Nome = @Nome,
  CEP = @CEP,
  DataCadastro = @DataCadastro,
  AtivoSN = @AtivoSN,
  OnlineSN = @OnlineSN

  where 
  CodRegiao = @CodRegiao
  end
GO
ALTER procedure [dbo].[spObterRegiaoEntrega_BairrosPorCodigo]
@Codigo int
	 as
	   begin
	   select 
	   CodRegiao,
	   Nome,
	   Cep,
	   Isnull(AtivoSN,0) as AtivoSN,
	   Isnull(OnlineSN,0) as   OnlineSN
	   from RegiaoEntrega_Bairros
	   where 
	   CodRegiao = @Codigo
	   end
go
alter table RegiaoEntrega_Bairros drop constraint UK01_RegiaoEntrega
go
