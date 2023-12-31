
ALTER procedure [dbo].[spAlterarProduto_OpcaoTipo]
(
@Codigo int,
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime
 )
 as 
   begin
     update Produto_OpcaoTipo set
	Nome=@Nome ,
	Tipo=@Tipo ,
	MaximoOpcionais= @MaximoOpcionais ,
	MinimoOpcionais= @MinimoOpcionais   ,
	OrdenExibicao =  @OrdenExibicao ,
	DataAlteracao = @DataAlteracao 
       where Codigo =@Codigo    
   end
