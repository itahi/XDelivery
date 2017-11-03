
create table IntegracaoIFood
(
Codigo int identity(1,1),
UserName nvarchar(max),
Senha nvarchar(max),
Data datetime,
CodOrigem int not null
)
go
create procedure spAdicionar_Integracao
@UserName nvarchar(max),
@Senha nvarchar(max),
@CodOrigem int
as
begin
 insert into IntegracaoIFood (UserName,Senha,Data,CodOrigem) 
          values ( @UserName,@Senha,GETDATE(),@CodOrigem)
end
go
create procedure spAlterar_Integracao
@Codigo int,
@UserName nvarchar(max),
@Senha nvarchar(max),
@CodOrigem int
as
begin
 update IntegracaoIFood set 
 UserName=@UserName,
 Senha=@Senha,
 Data=GETDATE(),
 CodOrigem=@CodOrigem
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
go
create procedure spObter_IntegracaoPorCodigo
@Codigo int
as
select i.*,P.Nome as NomeOrigem from IntegracaoIFood i
join Pessoa_OrigemCadastro P on P.Codigo=i.CodOrigem
where i.Codigo=@Codigo


