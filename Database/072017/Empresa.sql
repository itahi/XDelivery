alter table Empresa add Id_loja int;
go
update Empresa set Id_loja =1;
GO
ALTER procedure [dbo].[spAdicionarEmpresa]
@Nome nvarchar(100),
@CNPJ varchar(14),
@Telefone varchar(20),
@Telefone2 varchar(20),
@Contato nvarchar(50),
@Cep varchar(8),
@Endereco nvarchar(100),
@Cidade nvarchar(50),
@Bairro varchar(50),
@Numero varchar(10),
@UF char(2),
@PontoReferencia nvarchar(max),
@Servidor nvarchar(max),
@Banco nvarchar(max),
@DataInicio datetime,
@VersaoBanco char(2),
@CaminhoBackup nvarchar(max),
@UrlServidor nvarchar(max),
@HorarioFuncionamento nvarchar(max),
@ConfiguracaoSMS nvarchar(max),
@Id_loja int

as 
Insert into Empresa (nome,CNPJ,Telefone ,Telefone2,Contato,Cep,
					Endereco,Cidade,Bairro,Numero,UF,PontoReferencia,
					Servidor,Banco,DataInicio,VersaoBanco,CaminhoBackup,UrlServidor,
					HorarioFuncionamento,ConfiguracaoSMS,Id_loja)
            Values (@Nome,@CNPJ,@Telefone,@Telefone2,@Contato,@Cep,
			       @Endereco,@Cidade,@Bairro,@Numero,@UF,@PontoReferencia,
				   @Servidor,@Banco,@DataInicio,@VersaoBanco,@CaminhoBackup,@UrlServidor,
				   @HorarioFuncionamento,@ConfiguracaoSMS,@Id_loja)

GO
ALTER procedure [dbo].[spAlterarEmpresa]
--@Codigo int,
@Nome nvarchar(100),
@CNPJ varchar(14),
@Telefone varchar(20),
@Telefone2 varchar(20),
@Contato nvarchar(50),
@Cep varchar(8),
@Endereco nvarchar(100),
@Cidade nvarchar(50),
@Bairro varchar(50),
@Numero varchar(10),
@UF char(2),
@PontoReferencia nvarchar(max),
@Servidor nvarchar(max),
@Banco nvarchar(max),
@DataInicio datetime,
@VersaoBanco char(2),
@CaminhoBackup nvarchar(max),
@UrlServidor nvarchar(max),
@HorarioFuncionamento nvarchar(max),
@ConfiguracaoSMS nvarchar(max),
@Id_Loja int
as
begin
Update
Empresa set
nome = @Nome,
CNPJ =@CNPJ ,
Telefone=@Telefone ,
Telefone2=@Telefone2,
Contato=@Contato,
Cep=@Cep,
Endereco=@Endereco,
Cidade=@Cidade,
Bairro=@Bairro,
Numero=@Numero,
UF=@UF,
PontoReferencia=@PontoReferencia,
Servidor=@Servidor,
Banco=@Banco,
DataInicio = @DataInicio,
VersaoBanco = @VersaoBanco,
CaminhoBackup = @CaminhoBackup,
UrlServidor= @UrlServidor,
HorarioFuncionamento= @HorarioFuncionamento,
ConfiguracaoSMS= @ConfiguracaoSMS,
Id_Loja = @Id_Loja
end



