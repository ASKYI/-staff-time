USE[TaskManagmentDB]
GO

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, Date) VALUES ('�������� ������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 1, '24-04-2018')  

INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, Date) VALUES ('���� ������������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 2, '20-04-2018') 
	
INSERT INTO [dbo].[Works] (WorkName, TaskID, WorkTypeID, Date) VALUES ('�������� ������������ �� ��������', 
	(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]
	WHERE [Tasks].[TaskName] = '���-�����������'), 3, '27-04-2018')   