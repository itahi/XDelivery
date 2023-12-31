
ALTER procedure [dbo].[spCaixaAbertoAnterior]
@Turno nvarchar(max)
as
  select 
  * from
  Caixa 
  where 
  Estado=0 and 
  Turno=@Turno and 
  Data<cast(Getdate() as date) AND 
  Codigo not in ( select TOP 1 CodCaixa from CaixaMovimento where Turno=@Turno order by CodCaixa desc )