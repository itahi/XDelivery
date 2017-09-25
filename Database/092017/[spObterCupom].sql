
ALTER procedure [dbo].[spObterCupom]
as
begin
select Codigo,CodCupom,Quantidade from Cupom
end
go
create procedure [dbo].[spObterCupomPorCodigo]
@Codigo int
as
begin
select * from Cupom where Codigo=@Codigo
end