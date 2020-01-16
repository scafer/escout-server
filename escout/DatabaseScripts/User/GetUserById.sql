DECLARE @userId NVARCHAR(50)

SET @userId = {0}

SELECT [userId]
      ,[username]
      ,[email]
FROM [Users] WITH(NOLOCK)
WHERE userId = @userId