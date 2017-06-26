update Caixa set Estado=1;
go
create procedure spCaixaAbertoAnterior
@Turno nvarchar(max) 
as
  select 
  * from
  Caixa where Estado=0 and
   Turno=@Turno and 
   Data<cast(Getdate() as date) 