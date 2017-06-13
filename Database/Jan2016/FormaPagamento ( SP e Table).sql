alter table FormaPagamento add CaminhoImagem nvarchar(max);
alter table FormaPagamento add DataFoto Datetime;
alter table FormaPagamento add AtivoSN bit;

GO
ALTER procedure [dbo].[spAdicionarFormaPagamento]
@Descricao nvarchar(100),
@DescontoSN bit,
@GeraFinanceiro bit,
@OnlineSN bit,
@DataAlteracao datetime,
@CaminhoImagem nvarchar(max)

as
begin 
Insert into FormaPagamento(Descricao,ParcelaSN,GeraFinanceiro,OnlineSN,DataAlteracao,CaminhoImagem)
            Values (@Descricao,@DescontoSN,@GeraFinanceiro,@OnlineSN,@DataAlteracao,@CaminhoImagem)

end
go
ALTER procedure [dbo].[spAlterarFormaPagamento]
@Codigo int,
@Descricao nvarchar(100),
@DescontoSN bit,
@GeraFinanceiro bit,
@OnlineSN bit,
@DataAlteracao datetime,
@CaminhoImagem nvarchar(max)

as 
begin
update FormaPagamento set 
     Descricao=@Descricao ,
	 ParcelaSN = @DescontoSN,
	 GeraFinanceiro = @GeraFinanceiro,
	 OnlineSN= @OnlineSN,
	 CaminhoImagem =@CaminhoImagem,
	 DataAlteracao =@DataAlteracao 
	         where Codigo=@Codigo
end
go
GO
ALTER procedure [dbo].[spObterFormaPagamento]
as 
begin
select 
ISNull(Codigo,1) as Codigo,
ISNull(Descricao,'Dinheiro') as Descricao,
ISNull(ParcelaSN ,0) as ParcelaSN,
ISNULL(GeraFinanceiro,0) AS GeraFinanceiro,
ISNULL(OnlineSN,0) AS OnlineSN,
IsNUll(CaminhoImagem,'') as CaminhoImagem
from FormaPagamento
end
GO
ALTER  procedure [dbo].[spObterFPNOme]
@Nome nvarchar(100)
as 
  begin
    select 
	ISNULL(Codigo,0) as Codigo,
	ISNULL(Descricao,0) as Descricao,
	ISNULL(ParcelaSn,0) as ParcelaSn,
	ISNULL(GeraFinanceiro,0) as GeraFinanceiro,
	IsNUll(CaminhoImagem,'') as CaminhoImagem
	from FormaPagamento

	where Descricao=@Nome
	
  end
  GO
ALTER procedure [dbo].[spObterFPPorCodigo]
@Codigo int
as
 select 
 ISNULL(Codigo,0) as Codigo,
 ISNULL(Descricao,'Dinheiro') as Descricao,
 ISNULL(ParcelaSN,0) as ParcelaSN,
 IsNUll(CaminhoImagem,'') as CaminhoImagem
 from FormaPagamento
 where Codigo = @Codigo
