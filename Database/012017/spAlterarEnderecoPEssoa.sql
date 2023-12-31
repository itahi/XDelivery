alter table Pessoa_Endereco add Codigo int identity(1,1)
go
ALTER procedure [dbo].[spAlterarEndereco]
@Codigo int,
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
      where CodPessoa = @CodPessoa and Codigo=@Codigo
   end

