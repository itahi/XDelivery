
ALTER procedure [dbo].[spObterCaixaAberto]
@Turno nvarchar(max)
as
 select * from Caixa
 where Estado=0
and Turno=@Turno and Data<cast(GETDATE() as date)



