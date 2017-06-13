
create procedure [dbo].[spObterFamiliaPorCodFamilia]
@Codigo int
as 
  begin
    select * from Grupo where  CodFamilia=@Codigo
  end  


