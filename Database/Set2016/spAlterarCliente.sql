
ALTER PROCEDURE [dbo].[spAlterarPessoa]

 @Codigo int,
 @Nome nvarchar(100),
 @Cep nvarchar(max),
 @Endereco nvarchar(100),
 @Numero nvarchar(20),
 @Bairro nvarchar(100),
 @Cidade nvarchar(100),
 @Uf char(2),
 @PontoReferencia nvarchar(200),
 @Observacao nvarchar(max),
 @Telefone nvarchar(20),
 @Telefone2 nvarchar(20),
 @DataNascimento datetime,
 @TicketFidelidade int,
 @CodRegiao int,
 @DataCadastro datetime,
 @user_id nvarchar(max)
 
AS
 BEGIN
  UPDATE Pessoa

  SET   
  Nome = @Nome,
  Cep = @Cep,
  Endereco=@Endereco,
  Numero = @Numero,
  Bairro = @Bairro,
  Cidade = @Cidade,
  Uf = @UF,
  PontoReferencia = @PontoReferencia,
  Observacao = @Observacao,
  Telefone = @Telefone,
  Telefone2 = @Telefone2,
  DataNascimento = @DataNascimento,
  DataCadastro = DataCadastro,
  TicketFidelidade= TicketFidelidade,
  CodRegiao =@CodRegiao,
  [user_id] = @user_id 
  where Codigo=@Codigo
END

