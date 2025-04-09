IF
    ( NOT EXISTS 
        ( select object_id from sys.objects where object_id = OBJECT_ID(N'[dbo].[Users]') and type = 'U')
    )
BEGIN
    CREATE TABLE Users
    (
        Id int IDENTITY(1,1) NOT NULL,
        Email varchar(30) NOT NULL,
        Name varchar(50) NOT NULL,
        Password varchar(30) NOT NULL,
    )
END;