USE[TaskManagmentDB]
GO

INSERT INTO [dbo].Attributes (Name, DateType) VALUES ('Описание', '1')
INSERT INTO [dbo].Attributes (Name, DateType) VALUES ('Комментарий', '1')

INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (0, (SELECT ID FROM Attributes WHERE Name = 'Описание'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (1, (SELECT ID FROM Attributes WHERE Name = 'Описание'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (2, (SELECT ID FROM Attributes WHERE Name = 'Описание'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (3, (SELECT ID FROM Attributes WHERE Name = 'Описание'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (4, (SELECT ID FROM Attributes WHERE Name = 'Описание'))

INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (0, (SELECT ID FROM Attributes WHERE Name = 'Комментарий'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (1, (SELECT ID FROM Attributes WHERE Name = 'Комментарий'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (2, (SELECT ID FROM Attributes WHERE Name = 'Комментарий'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (3, (SELECT ID FROM Attributes WHERE Name = 'Комментарий'))
INSERT INTO [dbo].WorkTypeAttrs (WorkTypeID, AttrID) VALUES (4, (SELECT ID FROM Attributes WHERE Name = 'Комментарий'))