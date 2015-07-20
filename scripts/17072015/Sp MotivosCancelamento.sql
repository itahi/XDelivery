create procedure spAdicionaMotivoCancelamento
@Nome nvarchar(50),
@DataCadastro date
 as 
 begin
    insert into MotivoCancelamento(Nome,DataCadastro) values (@Nome,@DataCadastro)
 end

go
create procedure spAlteraMotivoCancelamento
@Nome nvarchar(50),
@DataCadastro date,
@Codigo int
as
  begin
    Update MotivoCancalemento 
	set 
	Nome=@Nome
	where 
	Codigo = @Codigo 
  end
go
create procedure spExcluirMotivoCancelamento
@Codigo int
as 
  begin
    Delete from MotivoCancelamento where Codigo = @Codigo
  end
go
alter procedure spObterMotivoCancelamento
as
  begin
    select 
	ISNULL(Codigo,0) as Codigo,
	ISNULL(Nome ,'') as Nome,
	ISNULL(DataCadastro,Getdate()) as DataCadastro 
	from MotivoCancelamento
  end

