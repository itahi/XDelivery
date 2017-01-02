 insert into 
 Pessoa_Endereco (CodPessoa,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,NomeEndereco)  
 select Codigo,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,'Principal' from Pessoa


