create table Produto_OpcaoTipo
(
Codigo int identity (1,1),
Nome nvarchar(30) not null,
Tipo char(2),
MaximoOpcionais int not null,
MinimoOpcionais int not null,
OrdenExibicao int ,
DataAlteracao datetime,
DataSincronismo datetime

Constraint PK_OpcaoTipo01 primary key (Codigo)

)
go
create procedure spAdicionarProduto_OpcaoTipo
(
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime
--@DataSincronismo datetime

)
as 
begin
  insert into  Produto_OpcaoTipo (Nome,Tipo,MaximoOpcionais,MinimoOpcionais,OrdenExibicao,DataAlteracao)
                          values (@Nome,@Tipo,@MaximoOpcionais,@MinimoOpcionais,@OrdenExibicao,@DataAlteracao)
end
go
create procedure spAlterarProduto_OpcaoTipo
(
@Codigo int,
@Nome nvarchar(30) ,
@Tipo char(2),
@MaximoOpcionais int ,
@MinimoOpcionais int ,
@OrdenExibicao int ,
@DataAlteracao datetime
 )
 as 
   begin
     update Produto_OpcaoTipo set
	Nome=@Nome ,
	Tipo=@Tipo ,
	MaximoOpcionais= @MaximoOpcionais ,
	MinimoOpcionais= @MinimoOpcionais   ,
	OrdenExibicao =  @OrdenExibicao ,
	DataAlteracao = @DataAlteracao 
       where Codigo =@Codigo    
   end
go
create procedure spObterProduto_OpcaoTipoPorCodigo
@Codigo int
as 
  begin
    select * from Produto_OpcaoTipo
    where Codigo =@Codigo
  end

    
