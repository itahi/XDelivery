create procedure spObterMensages
as 
begin
select * from Mensagens
end
go
-------------------------------------
create procedure spAlterarMensagens
@Codigo int,
@Conteudo  nvarchar(150)
as 
begin
update Mensagens set Conteudo=@Conteudo
where Codigo=@Codigo
end
go

-------------------------------------
create procedure spAdicionarMensagen
@Tipo char(2),
@Conteudo nvarchar(150)
as
begin
insert into Mensagens (Conteudo,Tipo) values (@Conteudo,@Tipo)
end