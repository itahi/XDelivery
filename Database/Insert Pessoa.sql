insert into Pessoa_Endereco 
(CodPessoa,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,NomeEndereco)
select Codigo,Cep,Endereco,Observacao,PontoReferencia,Bairro,Cidade,Uf,Numero,CodRegiao,'Principal' from Pessoa


008DE2947D00000000001DBAFAA0000000000000000FFF8DA4FE06BBDA6