USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID) VALUES ('Тестовая ошибка', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 1)  

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID) VALUES ('Тест рефракторинг', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 2) 
	
INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID) VALUES ('Тестовая Консультация по телефону', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 3)   