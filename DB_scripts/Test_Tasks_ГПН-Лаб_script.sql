USE [TaskManagmentDB]
GO
INSERT [dbo].[Tasks] ([TaskName], [dbo].[TaskTypeID]) 
	VALUES (N'ГПН-Лаборатория', 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Тех.поддержка', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'ГПН-Лаборатория'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'ДС-6', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'ГПН-Лаборатория'), 3)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Обновление', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Тех.поддержка'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Тех.поддержка'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Дополнительные работы', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Тех.поддержка'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Доработка функционала по интеграции с системами', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'ДС-6'), 4)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Справочники', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Документы', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Инструкции', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Дополнительные работы'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Тестирование', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Доработка функционала по интеграции с системами'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Инструкции/описание', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Доработка функционала по интеграции с системами'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Доработка функционала по интеграции с системами'), 3)


INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Паспорт', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Документы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Протокол', (SELECT Tasks.ID FROM Tasks WHERE TaskName = 'Документы'))




