update Pedido set CodEndereco= ( Select top 1 Codigo from Pessoa_Endereco where Pessoa_Endereco.CodPessoa=Pedido.CodPessoa)