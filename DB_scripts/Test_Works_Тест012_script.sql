USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID) VALUES ('���� 0', 
	(SELECT [dbo].[Tasks].ID FROM [Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'))  

INSERT INTO [dbo].[Works] (WorkName, TaskID) VALUES ('���� 1', 
	(SELECT [dbo].[Tasks].ID FROM [Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������')) 
	
INSERT INTO [dbo].[Works] (WorkName, TaskID) VALUES ('���� 1', 
	(SELECT [dbo].[Tasks].ID FROM [Tasks]
	WHERE [Tasks].[TaskName] = '���.���������'))   