USE [TaskManagmentDB]
GO

INSERT INTO [dbo].[Users] ([UserName]) Values('Тестовый пользователь')

INSERT INTO [dbo].[UserTasks] ([TaskID], [UserID]) 
VALUES ( (SELECT [dbo].[Users].[ID] FROM [dbo].Users WHERE [dbo].[Users].UserName = 'Тестовый пользователь'),
		 (SELECT [dbo].[Tasks].[ID] FROM [dbo].[Tasks] WHERE [dbo].[Tasks].[TaskName] = 'ГПН-Лаборатория'))
