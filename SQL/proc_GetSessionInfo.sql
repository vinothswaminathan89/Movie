-- =============================================
-- Author:		Vinoth S
-- Create date: 30 SEP 2018
-- Description:	Get the Session Info
-- =============================================
USE Movie
GO
ALTER PROCEDURE proc_GetSessionInfo
	@MovieDate DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF(@MovieDate IS NULL)
	BEGIN
		SET @MovieDate = GETDATE()
	END
	SELECT MovieId, SessionId, MovieTime, Seats FROM Session WHERE CAST(MovieTime AS DATE) = CAST(@MovieDate AS DATE) 
END
GO
