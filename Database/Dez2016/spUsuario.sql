
ALTER procedure [dbo].[spAdicionarUsuarioDefault]
@Nome nvarchar(max),
@Senha nvarchar(128),
@AdministradorSN bit,
@AbreFechaCaixaSN bit

as begin

insert into Usuario (Nome,Senha,AdministradorSN,AbreFechaCaixaSN ) values
					(@Nome,@Senha,@AdministradorSN,@AbreFechaCaixaSN)
  end

