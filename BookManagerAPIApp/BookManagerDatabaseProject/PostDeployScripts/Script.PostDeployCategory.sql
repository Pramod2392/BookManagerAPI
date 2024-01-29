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
    INSERT INTO Category ([Name]) VALUES ('Mystery')
    INSERT INTO Category ([Name]) VALUES ('Romance')
    INSERT INTO Category ([Name]) VALUES ('Biography')
    INSERT INTO Category ([Name]) VALUES ('Auto-Biography')
    INSERT INTO Category ([Name]) VALUES ('Thriller')
    INSERT INTO Category ([Name]) VALUES ('Self-help')
    INSERT INTO Category ([Name]) VALUES ('Comic')
    INSERT INTO Category ([Name]) VALUES ('Fiction')
END