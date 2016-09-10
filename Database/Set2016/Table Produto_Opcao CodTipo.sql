alter table Produto_Opcao add CodTipo int ;
alter table Produto_Opcao add constraint FK03_CodTipoOpcao foreign key (CodTipo) references Produto_OpcaoTipo(Codigo);


go
update Produto_Opcao set CodTipo 
=(select Codigo from Produto_OpcaoTipo where Codigo =(select Tipo from Opcao where Codigo=Produto_Opcao.CodOpcao))
