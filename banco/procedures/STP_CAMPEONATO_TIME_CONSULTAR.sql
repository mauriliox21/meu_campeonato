IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.STP_CAMPEONATO_TIME_CONSULTAR'))
   DROP PROCEDURE STP_CAMPEONATO_TIME_CONSULTAR

GO
CREATE PROCEDURE STP_CAMPEONATO_TIME_CONSULTAR(
    @SQ_CAMPEONATO INT,
	@ST_ELIMINADO VARCHAR(1)
)
AS
BEGIN 
	SET DATEFORMAT DMY

	DECLARE @INSTRUCAO NVARCHAR(MAX)

	SET @INSTRUCAO = ' SELECT CAT.*, CAM.NM_CAMPEONATO, TIM.SQ_TIME, ''sucesso'' AS CT_TIPO_RETORNO
                       FROM TB_CAMPEONATO_TIME CAT WITH(NOLOCK)
                       INNER JOIN TB_CAMPEONATO CAM WITH(NOLOCK) ON CAT.SQ_CAMPEONATO = CAM.SQ_CAMPEONATO
                       INNER JOIN TB_TIME TIM WITH(NOLOCK) ON CAT.SQ_TIME = TIM.SQ_TIME 
					   WHERE CAT.SQ_CAMPEONATO = ''' + CAST(@SQ_CAMPEONATO AS VARCHAR(10)) + ''' '

	IF @ST_ELIMINADO <> ''
	    SET @INSTRUCAO = @INSTRUCAO + 'AND CAT.ST_ELIMINADO = ''' + @ST_ELIMINADO + ''' '

	SET @INSTRUCAO = @INSTRUCAO + ' ORDER BY CAT.NR_QUANTIDADE_VITORIA DESC'

	EXECUTE sp_executesql @INSTRUCAO
	
END
GO