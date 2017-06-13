create procedure spObterClientesPorRegiaoPush
@Codigo int
as 
  begin
   select * from Pessoa where CodRegiao=@Codigo 
   and [user_id] is not null
  end
