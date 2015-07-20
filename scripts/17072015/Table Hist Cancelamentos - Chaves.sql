Alter table HistoricoCancelamentos add CodMotivo int not null;
Alter table HistoricoCancelamentos add Constraint FK_CODMOTIVO FOREIGN KEY (CodMotivo) REFERENCES MotivoCancelamento(Codigo) 