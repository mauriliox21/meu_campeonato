IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.STP_CAMPEONATO_TIME_INCLUIR'))
   DROP PROCEDURE STP_CAMPEONATO_TIME_INCLUIR

GO
CREATE PROCEDURE STP_CAMPEONATO_TIME_INCLUIR(
   @SQ_CAMPEONATO INT,
   @SQ_TIME INT
)
AS
BEGIN 
    SET DATEFORMAT DMY

	INSERT INTO TB_CAMPEONATO_TIME ( SQ_CAMPEONATO,  SQ_TIME, DT_INSCRICAO, NR_PONTUACAO, ST_ELIMINADO) 
	                        VALUES (@SQ_CAMPEONATO, @SQ_TIME,    GETDATE(),            0,          'N')

	SELECT 'sucesso' AS CT_TIPO_RETORNO, 'Registro inclu�do com sucesso' AS CT_MENSAGEM, SCOPE_IDENTITY() AS CD_CHAVE_REGISTRO
END
GO
