create table CaixaCadastro
(
Codigo int     not null identity(1,1),
Numero nvarchar(10),
Nome   nvarchar(50),
DataCadastro datetime default getdate(),

constraint PK01_CodCaixaCadastro primary key(Codigo) ,
constraint UK01_CodCaixaCadastro unique(Numero) 

)

GO
  insert into CaixaCadastro(DataCadastro,Numero,Nome) values (GETDATE(),1,'Caixa Padrão')

go
create procedure spAdicionarCaixa
@Numero nvarchar(10),
@Nome   nvarchar(50),
@DataCadastro datetime
as 
  begin
   insert into CaixaCadastro (Numero,Nome,DataCadastro) values (@Numero,@Nome,@DataCadastro)
  end
go
create procedure spAlteraCAixa
@Numero nvarchar(10),
@Nome   nvarchar(50),
@Codigo int
 as
  begin
    update CaixaCadastro 
	   set
	   Numero = @Numero,
	   Nome   = @Nome

	   where Codigo = @Codigo
  end
go
create procedure spExcluirCaixa
@Codigo int
 as 
   begin
    delete from CaixaCadastro where Codigo = @Codigo
   end
go
create procedure spObterCaixa
as
  begin
    select 
	ISNULL(Codigo,1) as Codigo,
	ISNULL(Numero,1) as Numero,
	ISNULL(Nome,'Caixa Padrão') as Nome
  from
    CaixaCadastro

  end
