DECLARE @NR_VERSAO INT
SET @NR_VERSAO = 2

IF ISNULL((SELECT NR_VERSAO_SISTEMA FROM TB_SISTEMA), 0) = (@NR_VERSAO - 1)
BEGIN 
	BEGIN TRANSACTION

	UPDATE TB_SISTEMA SET NR_VERSAO_SISTEMA = @NR_VERSAO 
	

    CREATE TABLE dbo.TB_CAMPEONATO(
    	SQ_CAMPEONATO int NOT NULL PRIMARY KEY IDENTITY (1, 1),
    	NM_CAMPEONATO varchar(100) NOT NULL
    )  

	COMMIT
END
ELSE
BEGIN 
	RAISERROR ('A vers?o do Banco de dados n?o ? a adequada para execu??o deste procedimento', 16, 1)
END