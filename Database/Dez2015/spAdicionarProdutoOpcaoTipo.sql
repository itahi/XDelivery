
ALTER procedure [dbo].[spAdicionarProduto_OpcaoTipo]
(
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime
--@DataSincronismo datetime

)
as 
begin
  insert into  Produto_OpcaoTipo (Nome,Tipo,MaximoOpcionais,MinimoOpcionais,OrdenExibicao,DataAlteracao)
                          values (@Nome,@Tipo,@MaximoOpcionais,@MinimoOpcionais,@OrdenExibicao,@DataAlteracao)
end