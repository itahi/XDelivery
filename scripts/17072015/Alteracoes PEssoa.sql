ALTER TABLE Pessoa ALTER COLUMN Telefone2 NVARCHAR(20) ;
UPDATE Pessoa SET Telefone= NULL WHERE Telefone=0
CREATE UNIQUE INDEX U_TEL1_PESSOA
   ON Pessoa (Telefone);
go
create UNIQUE INDEX U_TEL2_PESSOA
   ON Pessoa (Telefone2);

