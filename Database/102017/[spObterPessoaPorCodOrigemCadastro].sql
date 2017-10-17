
ALTER procedure [dbo].[spObterPessoaPorCodOrigemCadastro]
 @Codigo int
 as
  begin
    select 
	 P.Nome,P.Telefone,
(select top 1 Telefone from Empresa) as 'SeuTelefone'
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	where PC.Codigo=@Codigo

  end
  

