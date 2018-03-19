//delete from Tasks

USE [TaskManagmentDB]
GO
INSERT [dbo].[Tasks] ([TaskName]) 
	VALUES (N'ГПН-Лаборатория')

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Тех.поддержка', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'ГПН-Лаборатория'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'ДС-6', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'ГПН-Лаборатория'))

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Обновление', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Тех.поддержка'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Тех.поддержка'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Дополнительные работы', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Тех.поддержка'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Доработка функционала по интеграции с системами', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'ДС-6'))

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Справочники', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Документы', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Инструкции', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Тестирование', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Доработка функционала по интеграции с системами'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Инструкции/описание', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Доработка функционала по интеграции с системами'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Доработка функционала по интеграции с системами'))


INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Паспорт', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Документы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Протокол', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Документы'))




