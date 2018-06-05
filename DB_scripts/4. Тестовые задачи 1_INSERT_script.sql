USE [TaskManagmentDB]
GO
INSERT [dbo].[Tasks] ([TaskName], [dbo].[TaskTypeID]) 
	VALUES (N'ГПН-Лаборатория', 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Тех.поддержка', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'ГПН-Лаборатория'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'ДС-6', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'ГПН-Лаборатория'), 3)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Обновление', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Тех.поддержка'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Тех.поддержка'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Дополнительные работы', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Тех.поддержка'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Доработка функционала по интеграции с системами', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'ДС-6'), 4)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Справочники', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Дополнительные работы'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Документы', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Дополнительные работы'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Инструкции', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Дополнительные работы'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Дополнительные работы'), 3)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Тестирование', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Доработка функционала по интеграции с системами'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Инструкции/описание', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Доработка функционала по интеграции с системами'), 4)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Консультации', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Доработка функционала по интеграции с системами'), 3)


INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Паспорт', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Документы'))
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID]) 
	VALUES (N'Протокол', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Документы'))

INSERT [dbo].[Tasks] ([TaskName], [dbo].[TaskTypeID]) 
	VALUES (N'Красноярские экологи', 2)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Договор 111/2017', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Красноярские экологи'), 4)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Справочники 0', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Договор 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Журналы', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Договор 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Документы 1', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Договор 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Инструкции', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Договор 111/2017'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'ДС-1', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Договор 111/2017'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'ОА', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Справочники 0'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Методики', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Справочники 0'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'КТ', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Справочники 0'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Интеграция', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'ДС-1'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Документы 2', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'ДС-1'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Справочники 1', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Документы'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Журналы', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Документы'), 1)
INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Справочники 2', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Документы'), 1)

INSERT [dbo].[Tasks] ([TaskName], [ParentTaskID], [dbo].[TaskTypeID]) 
	VALUES (N'Корректировка', (SELECT Tasks.ID FROM Tasks WHERE TaskName LIKE 'Справочники 0'), 1)




