USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID) VALUES ('Тест 0', 
	(SELECT [dbo].[Tasks].ID FROM [Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория'))  

INSERT INTO [dbo].[Works] (WorkName, TaskID) VALUES ('Тест 1', 
	(SELECT [dbo].[Tasks].ID FROM [Tasks]
	WHERE [Tasks].[TaskName] = 'ГПН-Лаборатория')) 
	
INSERT INTO [dbo].[Works] (WorkName, TaskID) VALUES ('Тест 1', 
	(SELECT [dbo].[Tasks].ID FROM [Tasks]
	WHERE [Tasks].[TaskName] = 'Тех.поддержка'))   