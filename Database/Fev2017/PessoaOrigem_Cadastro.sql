create table Pessoa_OrigemCadastro
(
Codigo int not null identity(1,1),
Nome nvarchar(max) not null,
AtivoSN bit default 1,
Constraint PK01_Pessoa_OrigemCadastro primary key (Codigo)
)
go
create procedure spAdicionarPessoa_OrigemCadastro
@Nome nvarchar(max),
@AtivoSN bit
as 
begin
  insert into Pessoa_OrigemCadastro (Nome,AtivoSN) values (@Nome,@AtivoSN)
end
go
create procedure spAlterarPessoa_OrigemCadastro 
@Codigo int ,
@Nome nvarchar(max),
@AtivoSN bit
as
 begin
   update Pessoa_OrigemCadastro set
   Nome=@Nome,
   AtivoSN =@AtivoSN
   where Codigo=@Codigo
 end
go
create procedure spExcluirPessoa_OrigemCadastro
@Codigo int
as 
  begin
   delete from Pessoa_OrigemCadastro where Codigo=@Codigo
  end
go
create procedure spObterPessoa_OrigemCadastro
as
begin
 select * from Pessoa_OrigemCadastro
end
go
create procedure spObterPessoa_OrigemCadastroPorCodigo
@Codigo int
as
begin
 select * from Pessoa_OrigemCadastro where Codigo=@Codigo
end

