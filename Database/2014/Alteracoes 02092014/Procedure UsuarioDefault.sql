
create procedure [dbo].[spAdicionarUsuarioDefault]
@Nome nvarchar(max),
@Senha nvarchar(128)

as begin

insert into Usuario (Nome,Senha) values
					(@Nome,@Senha)
  end



