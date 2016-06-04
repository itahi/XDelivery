create table Pessoa_Endereco
(
 CodPessoa int not null,
 Cep char(9),
 Endereco nvarchar(max),
 Complemento nvarchar(50),
 PontoReferencia nvarchar(100),
 Bairro nvarchar(100),
 Cidade nvarchar(100),
 UF char(2),
 Numero nvarchar(10),
 CodRegiao int not null,
 NomeEndereco nvarchar(50)
 
 Constraint FK01_CodPessoa foreign key (CodPessoa) references Pessoa(Codigo),
 Constraint FK02_CodRegiao foreign key (CodRegiao) references RegiaoEntrega(Codigo),

)
go
create procedure spAdicionarEndereco
@CodPessoa int,
@Cep char(9),
@Endereco nvarchar(max),
@Complemento nvarchar(50),
@PontoReferencia nvarchar(100),
@Bairro nvarchar(100),
@Cidade nvarchar(100),
@UF char(2),
@Numero nvarchar(10),
@CodRegiao int,
@NomeEndereco nvarchar(max)
as
 begin
  insert into Pessoa_Endereco (CodPessoa,Cep,Endereco,Complemento,PontoReferencia,Bairro,Cidade,UF,Numero,CodRegiao,NomeEndereco)
      values (@CodPessoa,@Cep,@Endereco,@Complemento,@PontoReferencia,@Bairro,@Cidade,@UF,@Numero,@CodRegiao,@NomeEndereco)
 end

go
create procedure spAlterarEndereco
@CodPessoa int,
@Cep char(9),
@Endereco nvarchar(max),
@Complemento nvarchar(50),
@PontoReferencia nvarchar(100),
@Bairro nvarchar(100),
@Cidade nvarchar(100),
@UF char(2),
@Numero nvarchar(10),
@CodRegiao int,
@NomeEndereco nvarchar(max)
 as 
   begin
     update Pessoa_Endereco set 
      Cep=@Cep,
      Endereco=@Endereco,
      Complemento=@Complemento,
      PontoReferencia=@PontoReferencia,
      Bairro=@Bairro,
      Cidade=@Cidade,
      UF=@UF,
      Numero=@Numero,
      CodRegiao=@CodRegiao,
      NomeEndereco=@NomeEndereco
      where CodPessoa = @CodPessoa and NomeEndereco=@NomeEndereco
   end
go
create procedure spExcluirEndereco
 @CodPessoa int,
 @NomeEndereco nvarchar(max)
  as
  begin
    delete from Pessoa_Endereco where
    CodPessoa =@CodPessoa and NomeEndereco=@NomeEndereco
  end
go
create procedure spObterEnderecoPessoa
@CodPessoa int
 as 
 begin
   select * from Pessoa_Endereco where CodPessoa=@CodPessoa
 end  