create procedure spObterEnderecoCompletoPessoa
@Codigo int
as 
begin
 select Codigo ,Endereco +' N� '+Numero + ' ' +Bairro +
' '+ Complemento + ' '+ PontoReferencia  +' '+ Cidade as EnderecoCompleto
  from Pessoa_Endereco where CodPessoa = @Codigo
  end



 -- select Codigo ,Endereco +' N� '+Numero + ' ' +Bairro +
-- ' '+ Complemento + ' '+ PontoReferencia  +' '+ Cidade as EnderecoCompleto
--  from Pessoa_Endereco where CodPessoa = @Codigo
--  end
