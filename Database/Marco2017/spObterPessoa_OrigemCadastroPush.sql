
alter procedure [dbo].[spObterPessoa_OrigemCadastroPush]
@Codigo int
as
begin
 
 select Nome,Telefone from Pessoa where CodOrigemCadastro=@Codigo
 and [user_id] is not null or [user_id]!=''
end