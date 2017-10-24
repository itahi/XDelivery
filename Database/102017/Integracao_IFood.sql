create table IntegracaoIFood
(
Codigo int identity(1,1),
UserName nvarchar(max),
Senha nvarchar(max),
Data datetime
)
go
create procedure spAdicionar_Integracao
@UserName nvarchar(max),
@Senha nvarchar(max)
as
begin
 insert into IntegracaoIFood (UserName,Senha,Data) 
          values ( @UserName,@Senha,GETDATE())
end
go
create procedure spAlterar_Integracao
@Codigo int,
@UserName nvarchar(max),
@Senha nvarchar(max)
as
begin
 update IntegracaoIFood set 
 UserName=@UserName,
 Senha=@Senha,
 Data=GETDATE()
 where Codigo=@Codigo
end
go
create procedure spExcluir_Integracao
@Codigo int
as
 delete from IntegracaoIFood where Codigo=@Codigo
go
create procedure spObter_Integracao
as
select * from IntegracaoIFood
