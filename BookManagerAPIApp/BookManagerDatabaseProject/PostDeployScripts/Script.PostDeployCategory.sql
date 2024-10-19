/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS (SELECT * FROM Category)
BEGIN
    INSERT INTO Category ([Id],[Name]) VALUES (1,'Mystery')
    INSERT INTO Category ([Id],[Name]) VALUES (2,'Romance')
    INSERT INTO Category ([Id],[Name]) VALUES (3,'Biography')
    INSERT INTO Category ([Id],[Name]) VALUES (4,'Auto-Biography')
    INSERT INTO Category ([Id],[Name]) VALUES (5,'Thriller')
    INSERT INTO Category ([Id],[Name]) VALUES (6,'Self-help')
    INSERT INTO Category ([Id],[Name]) VALUES (7,'Comic')
    INSERT INTO Category ([Id],[Name]) VALUES (8,'Fiction')
END

IF NOT EXISTS (SELECT * FROM Language)
BEGIN
    INSERT INTO Language ([Id],[Name]) VALUES (1,'Kannada')
    INSERT INTO Language ([Id],[Name]) VALUES (2,'English')
    INSERT INTO Language ([Id],[Name]) VALUES (3,'Hindi')    
END