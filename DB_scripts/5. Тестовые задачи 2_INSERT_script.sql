USE [TaskManagmentDB]
GO

INSERT [dbo].[Tasks] ([TaskName], [dbo].[TaskTypeID]) 
	VALUES (N'������������ �������', 2)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'������� 111/2017', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '������������ �������'), 4)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'�����������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '������� 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'�������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '������� 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'��������� 1', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '������� 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'����������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '������� 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'��-1', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '������� 111/2017'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'��', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '�����������'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'��������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '�����������'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'��', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '�����������'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'����������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '��-1'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'��������� 2', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '��-1'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'����������� 1', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '���������'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'�������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '���������'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'����������� 2', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '���������'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'�������������', (SELECT Tasks.ID FROM Tasks WHERE TaskName = '�����������'), 1)