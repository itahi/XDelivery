create table Insumo 
(
Codigo int identity(1,1),
Nome nvarchar(max),
Preco decimal(10,2),
UnidadeMedida char(4),
AtivoSN bit,
DataCadastro datetime,
DataAlteracao datetime

Constraint PK01_CodInsumo primary key(Codigo)
)
go
create procedure spAdicionarInsumo
@Nome nvarchar(max),
@UnidadeMedida char(4),
@Preco decimal(10,2),
@AtivoSN bit
as 
 begin
  insert into Insumo (Nome,UnidadeMedida,Preco,AtivoSN,DataCadastro,DataAlteracao) 
           values (@Nome,@UnidadeMedida,@Preco,@AtivoSN,Getdate(),Getdate()) 
 end
 
go
create procedure spAlterarInsumo
@Codigo int,
@Nome nvarchar(max),
@UnidadeMedida char(4),
@Preco decimal(10,2),
@AtivoSN bit
as 
 begin
  update  Insumo 
  set
  Nome=@Nome,
  UnidadeMedida=@UnidadeMedida,
  Preco=@Preco,
  AtivoSN=@AtivoSN,
  DataCadastro=Getdate(),
  DataAlteracao=Getdate() 
  where Codigo=@Codigo
 end
 go
 create procedure spObterInsumo
 as
  select * from Insumo
  go
 create procedure spObterInsumoPorCodigo
 @Codigo int
 as
 begin
  select * from Insumo where Codigo=@Codigo
 end 
  go
  create procedure spExcluirInsumo
  @Codigo int
  as
  begin
  delete from Insumo where Codigo=@Codigo
  end
go

create table Produto_Insumo
(
Codigo int identity(1,1),
CodProduto int not null,
CodInsumo int not null,
Quantidade decimal(10,2)

)
go
create procedure spAdicionarProdutoInsumo
@CodProduto int ,
@CodInsumo int ,
@Quantidade decimal
as
begin
 insert into Produto_Insumo (CodProduto,CodInsumo,Quantidade) 
        values (@CodProduto,@CodInsumo,@Quantidade) 
end
go
create procedure spAlterarProdutoInsumo
@Codigo int,
@Quantidade decimal
as
begin
 update Produto_Insumo
  set Quantidade=@Quantidade 
  where Codigo=@Codigo
end
go
create procedure spExcluirProdutoInsumo
@Codigo int
  as
   begin
    delete from Produto_Insumo where Codigo=@Codigo
   end
  go
  create procedure spExcluirInsumo
  @Codigo int
  as
  begin
  delete from Insumo where Codigo=@Codigo
  end
go
go
create procedure spObterInsumoPorCodProduto
 @CodProduto int
 as
 begin
  select P.Codigo,I.Nome,I.Preco,P.Quantidade from Produto_Insumo P 
  join Insumo I on I.Codigo=P.CodInsumo
  where CodProduto=@CodProduto
 end 