
ALTER procedure [dbo].[spObterClientesPorRegiaoPush]
@Codigo int
as 
  begin
   select Nome,Telefone from Pessoa where CodRegiao=@Codigo 
   and [user_id] is not null  or [user_id]!='' 
  end
