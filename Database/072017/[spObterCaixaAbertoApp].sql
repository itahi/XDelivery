create procedure [dbo].[spObterCaixaAbertoApp]
as
 select * from Caixa
 where Estado=0
 and Data<cast(GETDATE() as date)



