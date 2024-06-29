CREATE PROCEDURE spAuthenticateUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT TOP 1  Username, Password
    FROM Users
    WHERE Username = @Username AND Password = @Password;
END;
