USE [TaskManagmentDB]
GO

INSERT INTO [dbo].[Users] ([UserName]) Values('�������� ������������')

INSERT INTO [dbo].[UserTasks] ([TaskID], [UserID]) 
VALUES ( (SELECT [dbo].[Users].[ID] FROM [dbo].Users WHERE [dbo].[Users].UserName = '�������� ������������'),
		 (SELECT [dbo].[Tasks].[ID] FROM [dbo].[Tasks] WHERE [dbo].[Tasks].[TaskName] = '���-�����������'))
