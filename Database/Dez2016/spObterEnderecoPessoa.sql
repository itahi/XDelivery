
ALTER procedure [dbo].[spObterEnderecoPessoa]
@Codigo int
 as 
 begin
   select * from Pessoa_Endereco where CodPessoa=@Codigo
 end

