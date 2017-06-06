create procedure spObterEnderecoPessoaCEP
@CodPessoa int,
@Cep varchar(9)
as
 begin
  select * from Pessoa_Endereco where CodPessoa=@CodPessoa and @Cep=@Cep
 end