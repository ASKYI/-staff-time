USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, StartDate) VALUES ('�������� ������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 1, GETDATE())

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, StartDate) VALUES ('�������� ������ 1', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 1, GETDATE())  
	
INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, StartDate) VALUES ('�������� ������������ �� ��������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 3, GETDATE())   