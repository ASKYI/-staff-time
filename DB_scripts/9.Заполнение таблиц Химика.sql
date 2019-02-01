Insert into LABS(name_lab) values ('Лаборатория аналитического контроля')

Insert into UZER(num_user, name_short, name_tiny, sity, addr, codes, pass_per_alert, pass_behaviour) values
('1','ОАО "Химсофт"ВЛК 2_0 СТАНДАРТ _2014_08_25', 'Газпромнефть', 'Томск', 'Россия, 634045, г.Томск,;ул.Моркрушина 9, стр. 16',
'прр', '3', '1')

Insert into LOGIN(UZER, LAB, SML_RIGHTS, OTHER_RIGHTS, BLOCKED, doc_right) values 
('1', '1', '1', '282290900', '0', '249')

Insert into IGroups(grup) values ('Задачи')
Insert into IGroups(grup) values ('Работы')
Insert into IGroups(grup) values ('Пользователи')
Insert into IGroups(grup) values ('Статистика')

Insert into Invents(Sname, TableName, NeedToShow) values('Задачи', 'Tasks', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Типы задач', 'TaskTypes', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Свойства задач', 'Properties', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Значения свойств задач', 'PropValues', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Типы данных свойств задач', 'TaskTypeProps', 'true')

Insert into Invents(Sname, TableName, NeedToShow) values('Работы', 'Works', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Типы работ', 'WorkTypes', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Атрибуты типов работ', 'WorkTypeAttrs', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Атрибуты работ', 'Attributes', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Значения атрибутов работ', 'AttrValues', 'true')


Insert into Invents(Sname, TableName, NeedToShow) values('Пользователи', 'Users', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Пользователи задачи', 'UserTasks', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Трудозатраты по задачам', 'TaskResults', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('Трудозатраты по задачам по дням', 'TaskResults2', 'true')

insert into IGList (num_group, num_inv) values('1', '1')
insert into IGList (num_group, num_inv) values('1', '2')
insert into IGList (num_group, num_inv) values('1', '3')
insert into IGList (num_group, num_inv) values('1', '4')
insert into IGList (num_group, num_inv) values('1', '5')

insert into IGList (num_group, num_inv) values('2', '6')
insert into IGList (num_group, num_inv) values('2', '7')
insert into IGList (num_group, num_inv) values('2', '8')
insert into IGList (num_group, num_inv) values('2', '9')
insert into IGList (num_group, num_inv) values('2', '10')

insert into IGList (num_group, num_inv) values('3', '11')
insert into IGList (num_group, num_inv) values('3', '12')
insert into IGList (num_group, num_inv) values('4', '13')
insert into IGList (num_group, num_inv) values('4', '14')


insert into InvFields(num_inv, [name], field, type) values ('1', 'ID задачи', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'Наименование задачи', 'TaskName', '1')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID типа задачи', 'TaskTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID родительской задачи', 'ParentTaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID типа работы', 'WorkTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID уровня задачи', 'LevelID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'Главная задача', 'IsMain', '0')

insert into InvFields(num_inv, [name], field, type) values ('2', 'ID типа задачи', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('2', 'Наименование типа задачи', 'TypeName', '1')

insert into InvFields(num_inv, [name], field, type) values ('3', 'ID свойства задачи', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('3', 'Тип данных свойства задачи', 'DataType', '0')
insert into InvFields(num_inv, [name], field, type) values ('3', 'Наименование свойства задачи', 'PropName', '1')

insert into InvFields(num_inv, [name], field, type) values ('4', 'ID значения свойства задачи', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'Тип данных значения свойства задачи', 'DataType', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'ID свойства задачи', 'PropID', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'ID задачи', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'Текстовое значение свойства задачи', 'ValueText', '1')
insert into InvFields(num_inv, [name], field, type) values ('4', 'Целочисленное значение свойства задачи', 'ValueInt', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'Значение свойства задачи дата', 'ValueDate', '3')
insert into InvFields(num_inv, [name], field, type) values ('4', 'Значение свойства задачи время', 'ValueTime', '4')

insert into InvFields(num_inv, [name], field, type) values ('5', 'ID свойства - задача', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('5', 'ID типа задачи', 'TaskTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('5', 'ID свойства', 'PropID', '0')

insert into InvFields(num_inv, [name], field, type) values ('6', 'ID работы', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', 'Наименование работы', 'WorkName', '1')
insert into InvFields(num_inv, [name], field, type) values ('6', 'ID задачи', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', 'ID типа работы', 'WorkTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', 'Дата создания работы', 'Date', '3')
insert into InvFields(num_inv, [name], field, type) values ('6', 'Время создания работы', 'StartTime', '4')
insert into InvFields(num_inv, [name], field, type) values ('6', 'Длительность, минуты', 'Minutes', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', 'Идентификатор пользователя', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', 'Описание', 'Description', '1')

insert into InvFields(num_inv, [name], field, type) values ('7', 'ID Типа работ', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('7', 'Наименование типа работ', 'TypeName', '1')

insert into InvFields(num_inv, [name], field, type) values ('8', 'ID атрибута типа работы', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('8', 'ID типа работы', 'WorkTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('8', 'ID атрибута', 'AttrID', '0')

insert into InvFields(num_inv, [name], field, type) values ('9', 'ID атрибута', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('9', 'Наименование атрибута', 'Name', '1')

insert into InvFields(num_inv, [name], field, type) values ('10', 'ID значения атрибута', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'ID атрибута', 'AttrID', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'ID работы', 'WorkID', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'Тип данных значения свойства работы', 'DataType', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'Текстовое значение свойства работы', 'ValueText', '1')
insert into InvFields(num_inv, [name], field, type) values ('10', 'Целочисленное значение свойства работы', 'ValueInt', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'Значение свойства работы дата', 'ValueDate', '3')
insert into InvFields(num_inv, [name], field, type) values ('10', 'Значение свойства работы время', 'ValueTime', '4')

insert into InvFields(num_inv, [name], field, type) values ('11', 'ID пользователя', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('11', 'Наименование пользователя', 'UserName', '1')

insert into InvFields(num_inv, [name], field, type) values ('12', 'ID пользовательской задачи', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('12', 'ID пользователя', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('12', 'ID задачи', 'TaskID', '0')

insert into InvFields(num_inv, [name], field, type) values ('13', 'ID трудозатрат', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'ID задачи', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'ID пользователя', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'Уровень задачи в дереве', 'Level', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'Номер по порядку в дереве', 'Ord', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'Суммарные трудозатраты, минуты', 'Minutes', '0')

insert into InvFields(num_inv, [name], field, type) values ('14', 'ID трудозатрат', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', 'Дата', 'Dt', '3')
insert into InvFields(num_inv, [name], field, type) values ('14', 'ID задачи', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', 'ID пользователя', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', 'Уровень задачи в дереве', 'TreeLevel', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', 'Номер по порядку в дереве', 'Ord', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', 'Суммарные трудозатраты, минуты', 'Minutes', '0')

insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Works', 'ID', 'AttrValues', 'WorkID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Properties', 'ID', 'TaskTypeProps', 'PropID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Properties', 'ID', 'PropValues', 'PropID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Tasks', 'ID', 'PropValues', 'TaskID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('TaskTypes', 'ID', 'Tasks', 'TaskTypeID')

insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('WorkTypes', 'ID', 'Tasks', 'WorkTypeID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('TaskTypes', 'ID', 'TaskTypeProps', 'TaskTypeID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Tasks', 'ID', 'UserTasks', 'TaskID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Tasks', 'ID', 'Works', 'TaskID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('WorkTypes', 'ID', 'Works', 'WorkTypeID')

insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Attributes', 'ID', 'WorkTypeAttrs', 'AttrID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Attributes', 'ID', 'AttrValues', 'AttrID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('WorkTypes', 'ID', 'WorkTypeAttrs', 'WorkTypeID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Users', 'ID', 'UserTasks', 'UserID')

insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Tasks', 'ID', 'TaskResults', 'TaskID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Users', 'ID', 'TaskResults', 'UserID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Users', 'ID', 'TaskResults2', 'UserID')
insert into DOPLINKS (tbl1, fld1, tbl2, fld2) values('Tasks', 'ID', 'TaskResults2', 'TaskID')

--Периоды
 INSERT INTO IGROUPS(GRUP) Values('Периоды')
 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('Периоды','PERIODS',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор записи', 'ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Группа', 'SUPERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Наименование периода', 'PERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата начала периода', 'B', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата конца периода', 'D', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODS')) and (UPPER(ig.GRUP) = UPPER('Периоды')))

 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('Смены периодов','PERIODSSMENA',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор записи', 'NUM_REC', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор периода', 'PERIOD_ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор группы периодов', 'SUPERIOD', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор смены периодов', 'TYPSMENA', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата начала', 'DT1', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Время начала', 'TM1', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата конца', 'DT2', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Время конца', 'TM2', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODSSMENA')) and (UPPER(ig.GRUP) = UPPER('Периоды')))

 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('Подпериоды смены периодов','PERIODSSMENASUB',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор записи', 'NUM_REC', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор смены периода', 'NUM_SMENA', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата начала', 'DT1', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Время начала', 'TM1', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата конца', 'DT2', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Время конца', 'TM2', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата', 'DT_MAIN', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODSSMENASUB')) and (UPPER(ig.GRUP) = UPPER('Периоды')))

 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('Подпериоды','PERIODSSUB',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор периода', 'PERIOD_ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Идентификатор записи', 'ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Группа', 'SUPERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Наименование', 'PERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата начала', 'B', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Дата конца', 'D', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, 'Номер периода', 'NUMBR', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODSSUB')) and (UPPER(ig.GRUP) = UPPER('Периоды')))
 
 insert into login_rights(num_login, num_igroups, rights) values (1, 5, 1)

