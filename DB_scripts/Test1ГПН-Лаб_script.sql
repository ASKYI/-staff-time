/*Конфликт с внешними ключами. Выполнить несколько раз.*/
USE [TaskManagmentDB]
GO
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'71db33ee-7d9e-4a3f-a5ab-0d4e2bfd15c2', N'Обновление', N'991248ed-8fd3-4756-8b0d-686e1b274d75')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'63e3db68-0576-4e46-90e4-16a8873b4ff2', N'Консультации', N'2c0e6520-3071-49bd-a774-6aade88376be')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'ac241b6a-5751-4994-939f-1bd1d55fa906', N'Паспорт', N'4a4b2bb9-eddc-4691-8ce8-984a818b30fc')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'd387b229-4293-4299-b305-22b7db6456c4', N'Справочники', N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'72563651-9c9a-42e3-9f55-396c8faca74e', N'Тестирование ', N'2c0e6520-3071-49bd-a774-6aade88376be')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'1f6b2f04-ecba-4905-a182-5d742625ae83', N'Инструкции', N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'991248ed-8fd3-4756-8b0d-686e1b274d75', N'Тех.поддержка ', N'4a52f107-2bcd-44ad-a9cf-a156485e5e7e')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'2c0e6520-3071-49bd-a774-6aade88376be', N'Доработка функционала по интеграции с системами ', N'fff68f02-1998-4412-a674-dd020803e5b3')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'defbbf03-c6be-484a-af3c-6bc126d62542', N'Протокол', N'4a4b2bb9-eddc-4691-8ce8-984a818b30fc')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4a4b2bb9-eddc-4691-8ce8-984a818b30fc', N'Документы', N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'4a52f107-2bcd-44ad-a9cf-a156485e5e7e', N'ГПН-Лаборатория', NULL)
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'fff68f02-1998-4412-a674-dd020803e5b3', N'ДС-6 ', N'4a52f107-2bcd-44ad-a9cf-a156485e5e7e')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'3e324acc-d9d3-46fd-a11b-e01e1e187dc0', N'Консультации', N'991248ed-8fd3-4756-8b0d-686e1b274d75')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'14b6c2e1-bc0f-4392-9eb8-e15db95ba7ca', N'Дополнительные работы', N'991248ed-8fd3-4756-8b0d-686e1b274d75')
INSERT [dbo].[Tasks] ([TaskID], [TaskName], [ParentTaskID]) VALUES (N'75e13f32-9f1e-4bc3-9023-ec1bd10b0863', N'Инструкции/описание', N'2c0e6520-3071-49bd-a774-6aade88376be')
