IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.STP_FASE_CONSULTAR'))
   DROP PROCEDURE STP_FASE_CONSULTAR

GO
CREATE PROCEDURE STP_FASE_CONSULTAR(
    @SQ_CAMPEONATO INT
)
AS
BEGIN 
	SET DATEFORMAT DMY

	DECLARE @INSTRUCAO NVARCHAR(MAX)

	SET @INSTRUCAO = ' SELECT FAS.*, ''sucesso'' AS CT_TIPO_RETORNO
                       FROM TB_FASE FAS WITH(NOLOCK)
                       WHERE FAS.SQ_CAMPEONATO = ' + CAST(@SQ_CAMPEONATO AS VARCHAR(10))

	EXECUTE sp_executesql @INSTRUCAO
	
END
GO