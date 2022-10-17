IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.STP_CAMPEONATO_INCLUIR'))
   DROP PROCEDURE STP_CAMPEONATO_INCLUIR

GO
CREATE PROCEDURE STP_CAMPEONATO_INCLUIR(
   @NM_CAMPEONATO VARCHAR(100)
)
AS
BEGIN 
    SET DATEFORMAT DMY

	INSERT INTO TB_CAMPEONATO (NM_CAMPEONATO) VALUES (@NM_CAMPEONATO)

	SELECT 'sucesso' AS CT_TIPO_RETORNO, 'Registro inclu�do com sucesso' AS CT_MENSAGEM, SCOPE_IDENTITY() AS CT_CHAVE_REGISTRO
END
GO