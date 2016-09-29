alter table Pessoa add DDD char(2);
alter table Pessoa add Sexo char(1);
go
GO
ALTER procedure [dbo].[spAdicionarClienteDelivery]
@Codigo int output,
@Nome nvarchar(100),
@Cep varchar(8),
@Endereco nvarchar(100),
@Numero varchar(10),
@Bairro varchar(50),
@Cidade nvarchar(100),
@UF char(2),
@PontoReferencia nvarchar(max),
@Telefone varchar(20),
@Observacao nvarchar(max),
@Telefone2 varchar(20),
@DataNascimento datetime,
@TicketFidelidade int ,
@CodRegiao int,
@DataCadastro Datetime,
@user_id nvarchar(max),
@DDD char(2),
@Sexo char(2)
as 
begin
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2,DataNascimento,
			TicketFidelidade,CodRegiao,DataCadastro,[user_id],DDD,Sexo)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2,@DataNascimento,
			@TicketFidelidade,@CodRegiao,@DataCadastro,@user_id,@DDD,@Sexo)
SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo

end
GO
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
 @user_id nvarchar(max),
 @DDD char(2),
@Sexo char(2)
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
  [user_id] = @user_id,
   DDD= @DDD ,
   Sexo = @Sexo 
  where Codigo=@Codigo
END
GO
ALTER PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,
  Observacao,Numero,Telefone,Telefone2,DataNascimento,TicketFidelidade,
  CodRegiao,DataCadastro,[user_id],DDD,Sexo
  FROM Pessoa WHERE Codigo =@Codigo
 END
 go
 ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]

 @Telefone nvarchar(100)

as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,
  Numero,DataNascimento,CodRegiao,TicketFidelidade,Telefone2,[user_id],DDD,Sexo
  FROM Pessoa WHERE 
  Telefone = @Telefone or Telefone2 =@Telefone
 END



