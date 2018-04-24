USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, Date) VALUES ('Тестовая ошибка', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 1, '24-04-2018')  

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, Date) VALUES ('Тест рефракторинг', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 2, '20-04-2018') 
	
INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, Date) VALUES ('Тестовая Консультация по телефону', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'), 3, '27-04-2018')   