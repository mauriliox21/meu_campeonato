DECLARE @NR_VERSAO INT
SET @NR_VERSAO = 6

IF ISNULL((SELECT NR_VERSAO_SISTEMA FROM TB_SISTEMA), 0) = (@NR_VERSAO - 1)
BEGIN 
	BEGIN TRANSACTION

	ALTER TABLE dbo.TB_CAMPEONATO_TIME ADD
	    NR_QUANTIDADE_VITORIA int NULL,
	    NR_COLOCACAO int NULL 

	COMMIT
END
ELSE
BEGIN 
	RAISERROR ('A vers�o do Banco de dados n�o � a adequada para execu��o deste procedimento', 16, 1)
END