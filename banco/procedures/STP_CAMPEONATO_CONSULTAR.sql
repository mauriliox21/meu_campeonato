IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.STP_CAMPEONATO_CONSULTAR'))
   DROP PROCEDURE STP_CAMPEONATO_CONSULTAR

GO
CREATE PROCEDURE STP_CAMPEONATO_CONSULTAR(
    @NM_CAMPEONATO VARCHAR(100)
)
AS
BEGIN 
	SET DATEFORMAT DMY

	DECLARE @INSTRUCAO NVARCHAR(MAX)

	SET @INSTRUCAO = ' SELECT CAM.SQ_CAMPEONATO, CAM.NM_CAMPEONATO, ''sucesso'' AS CT_TIPO_RETORNO
	                   FROM TB_CAMPEONATO CAM WITH(NOLOCK) '

    IF @NM_CAMPEONATO <> ''
	    SET @INSTRUCAO = @INSTRUCAO + ' WHERE CAM.NM_CAMPEONATO LIKE ''%'+@NM_CAMPEONATO+'%'' '

	EXECUTE sp_executesql @INSTRUCAO
	
END
GO