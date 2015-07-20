go
begin
alter table Pedido add CodUsuario int null
 
alter table Pedido
ADD CONSTRAINT FK_CODUSER Foreign KEY (CodUsuario) references Usuario(Cod)
end

create table EventosSistema
(
Codigo int not null identity (1,1) primary key,
CodUsuario int not null, 
TipoEvento nvarchar(10),
DataEvento datetime default getdate(),
LocalEvento nvarchar(100)

constraint FK_CODUSEREVENTO foreign key(CodUsuario) references Usuario(Cod)
)


