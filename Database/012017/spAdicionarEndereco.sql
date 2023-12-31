
ALTER procedure [dbo].[spAdicionarEndereco]
@Codigo int output,
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
	    SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
 end

