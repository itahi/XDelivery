alter table Produto_OpcaoTipo add AtivoSN bit;
alter table Produto_OpcaoTipo add OnlineSN bit;
GO
ALTER procedure [dbo].[spAdicionarProduto_OpcaoTipo]
(
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime,
@AtivoSN bit,
@OnlineSN bit
--@DataSincronismo datetime

)
as 
begin
  insert into  Produto_OpcaoTipo (Nome,Tipo,MaximoOpcionais,MinimoOpcionais,OrdenExibicao,DataAlteracao,AtivoSN,OnlineSN)
                          values (@Nome,@Tipo,@MaximoOpcionais,@MinimoOpcionais,@OrdenExibicao,@DataAlteracao,@AtivoSN,@OnlineSN)
end
GO
ALTER procedure [dbo].[spAlterarProduto_OpcaoTipo]
(
@Codigo int,
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime,
@AtivoSN bit,
@OnlineSN bit
 )
 as 
   begin
     update Produto_OpcaoTipo set
	Nome=@Nome ,
	Tipo=@Tipo ,
	MaximoOpcionais= @MaximoOpcionais ,
	MinimoOpcionais= @MinimoOpcionais   ,
	OrdenExibicao =  @OrdenExibicao ,
	DataAlteracao = @DataAlteracao,
	AtivoSN = @AtivoSN,
	OnlineSN= @OnlineSN
       where Codigo =@Codigo    
   end
  go
  update Produto_OpcaoTipo set AtivoSN=1;
  update Produto_OpcaoTipo set OnlineSN=1;