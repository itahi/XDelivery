create procedure [dbo].[spObterTaxaPorClienteEndereco]
@Codigo int
as
  begin
    select R.TaxaServico  
	from RegiaoEntrega R
	left join Pessoa_Endereco P on P.CodRegiao=R.Codigo
	where P.Codigo=@Codigo    
	 end
