create procedure spObterClientesPorOrigem
 as
  begin
    select 
	PC.Nome,count(CodOrigemCadastro) as QuantidadeClientes
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	group by CodOrigemCadastro,PC.Nome
  end
  go
  create procedure spObterClientesPorOrigemCodigo
 @Codigo int
 as
  begin
    select 
	PC.Nome,count(CodOrigemCadastro) as QuantidadeClientes
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	where PC.Codigo=@Codigo
	group by CodOrigemCadastro,PC.Nome

  end
  go
    create procedure spObterPessoaPorCodOrigemCadastro
 @Codigo int
 as
  begin
    select 
	 P.Nome,P.Telefone
	from Pessoa P
	join Pessoa_OrigemCadastro PC on PC.Codigo=P.CodOrigemCadastro
	where PC.Codigo=@Codigo

  end
  