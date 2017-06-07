insert into Pessoa_Endereco 
(CodPessoa,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,NomeEndereco)
select Codigo,Cep,Endereco,Observacao,PontoReferencia,Bairro,Cidade,Uf,Numero,CodRegiao,'Principal' from Pessoa
