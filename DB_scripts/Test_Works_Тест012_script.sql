USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID) VALUES ('�������� ������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 1)  

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID) VALUES ('���� ������������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 2) 
	
INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID) VALUES ('�������� ������������ �� ��������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 3)   