 alter table Pessoa_endereco alter column Endereco nvarchar(max);
 alter table Pessoa_endereco alter column Complemento nvarchar(max);
 alter table Pessoa_endereco alter column PontoReferencia nvarchar(max);
  alter table Pessoa_endereco alter column Bairro nvarchar(max);
  alter table Pessoa_endereco alter column Cidade nvarchar(max);
 go
 insert into 
 Pessoa_Endereco (CodPessoa,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,NomeEndereco)  
 select Codigo,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,'Principal' from Pessoa
