ALTER TABLE Caixa ADD Constraint PK01_CodigoCaixa primary KEY (Codigo);
alter table CaixaMovimento alter column CodCaixa int;
ALTER TABLE CaixaMovimento ADD FOREIGN KEY (CodCaixa) REFERENCES Caixa(Codigo);
go 
ALTER procedure [dbo].[spInserirMovimentoCaixa]
@Data datetime,
@Historico nvarchar(100),
@NumeroDocumento nvarchar(50),
@CodFormaPagamento int,
@Valor decimal(10,2),
@Tipo char(1),
@CodUser int,
@Turno varchar(5)
as
  begin
     declare @CodCaixa int;
	 set @CodCaixa = ( select Codigo from Caixa where Estado=0 and Turno=@Turno)
     insert into CaixaMovimento (CodCaixa,Data,Historico,NumeroDocumento,CodFormaPagamento,Valor,Tipo,CodUsuario,Turno)
	        values (@CodCaixa,@Data,@Historico,@NumeroDocumento,@CodFormaPagamento,@Valor,@Tipo,@CodUser,@Turno)
  end
