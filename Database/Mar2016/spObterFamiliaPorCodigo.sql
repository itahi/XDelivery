
ALTER procedure [dbo].[spObterFamiliaPorCodigo]
@Codigo int
as 
  begin
    select * from Grupo where CodFamilia is null and Codigo=@Codigo
  end  

