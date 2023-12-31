
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
@Sexo char(2),
@PFPJ char(1),
@CodOrigemCadastro int,
@longitude float ,
@latitude float ,
@email nvarchar(max)
as 
begin
Insert into Pessoa(nome,Cep,Endereco,Numero,Bairro,Cidade,Uf,PontoReferencia,Telefone,Observacao,Telefone2,DataNascimento,
			TicketFidelidade,CodRegiao,DataCadastro,[user_id],DDD,Sexo,PFPJ,CodOrigemCadastro,latitude,longitude,email)
            Values (@nome,@Cep,@Endereco,@Numero,@Bairro,@Cidade,@Uf,@PontoReferencia,@Telefone,@Observacao,@Telefone2,@DataNascimento,
			@TicketFidelidade,@CodRegiao,@DataCadastro,@user_id,@DDD,@Sexo,@PFPJ,@CodOrigemCadastro,@latitude,@longitude,@email)
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
@Sexo char(2),
@PFPJ char(1),
@CodOrigemCadastro int,
@longitude float ,
@latitude float ,
@email nvarchar(max)
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
   Sexo = @Sexo,
   PFPJ =@PFPJ,
   CodOrigemCadastro=@CodOrigemCadastro,
    longitude= @longitude  ,
   latitude= @latitude ,
   email=@Email
  where Codigo=@Codigo
END





GO
ALTER PROCEDURE [dbo].[spObterPessoaPorCodigo]

 @Codigo int

as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,isnull(PontoReferencia,'') PontoReferencia,
  isnull(Observacao,'')Observacao ,Numero,Telefone,Telefone2,DataNascimento,isnull(TicketFidelidade,0) TicketFidelidade,
  CodRegiao,DataCadastro,isnull([user_id],'') as[user_id] ,isnull(DDD,'') as DDD , isnull(Sexo,'') as Sexo ,PFPJ,
  (select top 1 Codigo from Pessoa_Endereco where CodPessoa = Pessoa.Codigo order by Codigo asc) as CodEndereco,
  isnull(CodOrigemCadastro,0) as CodOrigemCadastro,latitude,longitude,isnull(Email,'') as Email
  FROM Pessoa WHERE Codigo =@Codigo
 END
 GO
 ALTER PROCEDURE [dbo].[spObterPessoaPorTelefone]
 @Telefone nvarchar(100)
as
 BEGIN
  SELECT Codigo,Nome,Endereco,Bairro,Cidade,PontoReferencia,Observacao,
  Numero,DataNascimento,CodRegiao,TicketFidelidade,Telefone2,[user_id],DDD,Sexo,PFPJ,
  (Select top 1 Codigo from Pessoa_Endereco where CodPessoa = Pessoa.Codigo order by Codigo asc) as CodEndereco,
  isnull(CodOrigemCadastro,0) as CodOrigemCadastro,latitude,longitude,isnull(Email,'') as Email
  FROM Pessoa 
  WHERE 
  Telefone = @Telefone or Telefone2 =@Telefone
 END
 GO
ALTER PROCEDURE [dbo].[spObterPessoas]
as
 BEGIN
  SELECT Codigo,Nome,Cep,Endereco,Bairro,Cidade,UF,PontoReferencia,Observacao,Numero,Telefone,DataNascimento,CodRegiao,DataCadastro,PFPJ,
  isnull(CodOrigemCadastro,0) as CodOrigemCadastro,latitude,longitude,isnull(Email,'') as Email
  FROM Pessoa 
 END
