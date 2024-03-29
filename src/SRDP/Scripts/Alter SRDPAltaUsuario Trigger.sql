USE [PersonalLBC_Sanitizada]
GO
/****** Object:  Trigger [dbo].[SRDPAltaUsuario]    Script Date: 19/08/2020 1:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Guido Caballero
-- Create date: 23/03/2019
-- Description:	Inserta notificacion en SRDP
-- =============================================
ALTER TRIGGER [dbo].[SRDPAltaUsuario] 
   ON  [dbo].[ESP_USUARIO]	 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    set xact_abort, nocount on
	if not exists(select * from inserted) return;
	begin try
		INSERT INTO SRDP.DBO.NotificationQ
		(ID, [FuncionarioID],[UserName], [ActionType], [Status], [CreateDate])
		SELECT 
		NEWID(), I.codigo, I.username, 'Alta', 'Queued', GETDATE()  
		FROM inserted I inner Join dbo.RH_EMPLOYEE E
		  on I.codigo = E.CODIGO 
		where E.TIPO_ROL not in (7, 8)
	END TRY
	BEGIN CATCH
	IF XACT_STATE() <> 0 ROLLBACK TRANSACTION
	END CATCH
END
