USE [TaskManagmentDB]
GO 
/*INSERT [dbo].[Properties] ([PropName], [DataType])
	VALUES ('Favorite', 1)*/

INSERT [dbo].[PropValues] ([DataType], [TaskID], [ValueText], [PropID])
	VALUES (
		(SELECT [dbo].[Properties].[DataType] FROM [dbo].[Properties] 
			WHERE [dbo].[Properties].PropName = 'Favorite'),
		(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]	
			WHERE [dbo].[Tasks].[TaskName] = 'ГПН-Лаборатория'),
		'True',
		(SELECT [dbo].[Properties].ID FROM [dbo].[Properties] 
			WHERE [dbo].[Properties].PropName = 'Favorite'))

INSERT [dbo].[PropValues] ([DataType], [TaskID], [ValueText], [PropID])
	VALUES (
		(SELECT [dbo].[Properties].[DataType] FROM [dbo].[Properties] 
			WHERE [dbo].[Properties].PropName = 'Favorite'),
		(SELECT [dbo].[Tasks].ID FROM [dbo].[Tasks]	
			WHERE [dbo].[Tasks].[TaskName] = 'Тех.поддержка'),
		'True',
		(SELECT [dbo].[Properties].ID FROM [dbo].[Properties] 
			WHERE [dbo].[Properties].PropName = 'Favorite'))
