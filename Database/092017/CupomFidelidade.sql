create table Cupom
(
Codigo int identity(1,1),
CodCupom nvarchar(max),
Desconto decimal(10,2),
DataCadastro datetime,
DataValidade_Inicio date,
DataValidade_Fim date,
Quantidade  int,
AtivoSN     bit,
QuantidadePessoa int,
Constraint PK01_Cupom primary key (Codigo)
)
go
create table Pedido_Cupom 
(
CodPedido int not null,
CodCupom  int not null,
DataCadastro datetime
Constraint FK01_CodCupom foreign key (CodPedido) references Pedido(Codigo),
Constraint FK02_CodPedidoCupom foreign key (CodCupom) references Cupom(Codigo)
)
go
create procedure spAdicionarCupom
@CodCupom nvarchar(max),
@Desconto decimal(10,2),
@DataValidade_Inicio date,
@DataValidade_Fim date,
@Quantidade  int,
@AtivoSN     bit,
@QuantidadePessoa int
as
  begin
    insert into Cupom (CodCupom,Desconto,DataValidade_Inicio,DataValidade_Fim,Quantidade,DataCadastro,AtivoSN,QuantidadePessoa)
	       values (@CodCupom,@Desconto,@DataValidade_Inicio,@DataValidade_Fim,@Quantidade,GETDATE(),@AtivoSN,@QuantidadePessoa)
  end
go
create procedure spExcluirCupom
@Codigo int
as
begin
   delete from Cupom where Codigo=@Codigo
end
go
create procedure spAlterarCupom
@Codigo int,
@CodCupom nvarchar(max),
@Desconto decimal(10,2),
@DataValidade_Inicio date,
@DataValidade_Fim date,
@Quantidade  int,
@AtivoSN     bit,
@QuantidadePessoa int
as
 begin
    update Cupom set
	CodCupom=@CodCupom,
	Desconto=@Desconto,
	DataValidade_Inicio=@DataValidade_Inicio,
	DataValidade_Fim =@DataValidade_Fim,
	Quantidade=@Quantidade,
	AtivoSN=@AtivoSN,
	QuantidadePessoa=@QuantidadePessoa,
	DataCadastro=GETDATE()
	where Codigo=@Codigo
 end
go
create procedure spAdicionarPedido_Cupom
@CodPedido int,
@CodCupom int 
as
 begin
   insert into Pedido_Cupom (CodPedido,CodCupom,DataCadastro)
               values (@CodPedido,@CodCupom,Getdate())
 end