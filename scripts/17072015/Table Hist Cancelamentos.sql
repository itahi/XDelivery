
Create table HistoricoCancelamentos
(
Codigo int primary key identity(1,1),
CodPessoa int not null,
Motivo nvarchar(100),
CodMotivo int ,
Data date,

Constraint FK_CODPESSOA_CANCELAMENTO FOREIGN KEY (CodPessoa) REFERENCES Pessoa(Codigo), 
constraint FK_CODMOTIVO FOREIGN KEY (CodMotivo) REFERENCES MotivoCancelamento(Codigo) 
)



