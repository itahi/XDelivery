
ALTER procedure [dbo].[spObterClientesPorRegiao]
@Codigo int
as 
  begin
   select Nome,Telefone from Pessoa where CodRegiao=@Codigo 
   and SUBSTRING(Telefone,1,1)=9 and Len(Telefone)=9
  end


