USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, StartDate) VALUES ('Тестовая ошибка', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 1, GETDATE())

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, StartDate) VALUES ('Тестовая ошибка 1', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 1, GETDATE())  
	
INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, StartDate) VALUES ('Тестовая Консультация по телефону', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 3, GETDATE())   