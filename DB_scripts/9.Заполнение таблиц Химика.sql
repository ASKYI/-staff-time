Insert into LABS(name_lab) values ('����������� �������������� ��������')

Insert into UZER(num_user, name_short, name_tiny, sity, addr, codes, pass_per_alert, pass_behaviour) values
('1','��� "�������"��� 2_0 �������� _2014_08_25', '������������', '�����', '������, 634045, �.�����,;��.���������� 9, ���. 16',
'���', '3', '1')

Insert into LOGIN(UZER, LAB, SML_RIGHTS, OTHER_RIGHTS, BLOCKED, doc_right) values 
('1', '1', '1', '282290900', '0', '249')

Insert into IGroups(grup) values ('������')
Insert into IGroups(grup) values ('������')
Insert into IGroups(grup) values ('������������')
Insert into IGroups(grup) values ('����������')

Insert into Invents(Sname, TableName, NeedToShow) values('������', 'Tasks', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('���� �����', 'TaskTypes', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('�������� �����', 'Properties', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('�������� ������� �����', 'PropValues', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('���� ������ ������� �����', 'TaskTypeProps', 'true')

Insert into Invents(Sname, TableName, NeedToShow) values('������', 'Works', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('���� �����', 'WorkTypes', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('�������� ����� �����', 'WorkTypeAttrs', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('�������� �����', 'Attributes', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('�������� ��������� �����', 'AttrValues', 'true')


Insert into Invents(Sname, TableName, NeedToShow) values('������������', 'Users', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('������������ ������', 'UserTasks', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('������������ �� �������', 'TaskResults', 'true')
Insert into Invents(Sname, TableName, NeedToShow) values('������������ �� ������� �� ����', 'TaskResults2', 'true')

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


insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', '������������ ������', 'TaskName', '1')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ���� ������', 'TaskTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ������������ ������', 'ParentTaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ���� ������', 'WorkTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ������ ������', 'LevelID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', '������� ������', 'IsMain', '0')

insert into InvFields(num_inv, [name], field, type) values ('2', 'ID ���� ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('2', '������������ ���� ������', 'TypeName', '1')

insert into InvFields(num_inv, [name], field, type) values ('3', 'ID �������� ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('3', '��� ������ �������� ������', 'DataType', '0')
insert into InvFields(num_inv, [name], field, type) values ('3', '������������ �������� ������', 'PropName', '1')

insert into InvFields(num_inv, [name], field, type) values ('4', 'ID �������� �������� ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', '��� ������ �������� �������� ������', 'DataType', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'ID �������� ������', 'PropID', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', 'ID ������', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', '��������� �������� �������� ������', 'ValueText', '1')
insert into InvFields(num_inv, [name], field, type) values ('4', '������������� �������� �������� ������', 'ValueInt', '0')
insert into InvFields(num_inv, [name], field, type) values ('4', '�������� �������� ������ ����', 'ValueDate', '3')
insert into InvFields(num_inv, [name], field, type) values ('4', '�������� �������� ������ �����', 'ValueTime', '4')

insert into InvFields(num_inv, [name], field, type) values ('5', 'ID �������� - ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('5', 'ID ���� ������', 'TaskTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('5', 'ID ��������', 'PropID', '0')

insert into InvFields(num_inv, [name], field, type) values ('6', 'ID ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', '������������ ������', 'WorkName', '1')
insert into InvFields(num_inv, [name], field, type) values ('6', 'ID ������', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', 'ID ���� ������', 'WorkTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', '���� �������� ������', 'Date', '3')
insert into InvFields(num_inv, [name], field, type) values ('6', '����� �������� ������', 'StartTime', '4')
insert into InvFields(num_inv, [name], field, type) values ('6', '������������, ������', 'Minutes', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', '������������� ������������', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('6', '��������', 'Description', '1')

insert into InvFields(num_inv, [name], field, type) values ('7', 'ID ���� �����', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('7', '������������ ���� �����', 'TypeName', '1')

insert into InvFields(num_inv, [name], field, type) values ('8', 'ID �������� ���� ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('8', 'ID ���� ������', 'WorkTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('8', 'ID ��������', 'AttrID', '0')

insert into InvFields(num_inv, [name], field, type) values ('9', 'ID ��������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('9', '������������ ��������', 'Name', '1')

insert into InvFields(num_inv, [name], field, type) values ('10', 'ID �������� ��������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'ID ��������', 'AttrID', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', 'ID ������', 'WorkID', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', '��� ������ �������� �������� ������', 'DataType', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', '��������� �������� �������� ������', 'ValueText', '1')
insert into InvFields(num_inv, [name], field, type) values ('10', '������������� �������� �������� ������', 'ValueInt', '0')
insert into InvFields(num_inv, [name], field, type) values ('10', '�������� �������� ������ ����', 'ValueDate', '3')
insert into InvFields(num_inv, [name], field, type) values ('10', '�������� �������� ������ �����', 'ValueTime', '4')

insert into InvFields(num_inv, [name], field, type) values ('11', 'ID ������������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('11', '������������ ������������', 'UserName', '1')

insert into InvFields(num_inv, [name], field, type) values ('12', 'ID ���������������� ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('12', 'ID ������������', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('12', 'ID ������', 'TaskID', '0')

insert into InvFields(num_inv, [name], field, type) values ('13', 'ID �����������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'ID ������', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', 'ID ������������', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', '������� ������ � ������', 'Level', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', '����� �� ������� � ������', 'Ord', '0')
insert into InvFields(num_inv, [name], field, type) values ('13', '��������� ������������, ������', 'Minutes', '0')

insert into InvFields(num_inv, [name], field, type) values ('14', 'ID �����������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', '����', 'Dt', '3')
insert into InvFields(num_inv, [name], field, type) values ('14', 'ID ������', 'TaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', 'ID ������������', 'UserID', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', '������� ������ � ������', 'TreeLevel', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', '����� �� ������� � ������', 'Ord', '0')
insert into InvFields(num_inv, [name], field, type) values ('14', '��������� ������������, ������', 'Minutes', '0')

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

--�������
 INSERT INTO IGROUPS(GRUP) Values('�������')
 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('�������','PERIODS',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ������', 'ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������', 'SUPERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������ �������', 'PERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� ������ �������', 'B', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� ����� �������', 'D', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODS')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODS')) and (UPPER(ig.GRUP) = UPPER('�������')))

 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('����� ��������','PERIODSSMENA',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ������', 'NUM_REC', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� �������', 'PERIOD_ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ������ ��������', 'SUPERIOD', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ����� ��������', 'TYPSMENA', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� ������', 'DT1', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '����� ������', 'TM1', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� �����', 'DT2', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '����� �����', 'TM2', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENA')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODSSMENA')) and (UPPER(ig.GRUP) = UPPER('�������')))

 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('���������� ����� ��������','PERIODSSMENASUB',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ������', 'NUM_REC', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ����� �������', 'NUM_SMENA', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� ������', 'DT1', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '����� ������', 'TM1', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� �����', 'DT2', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '����� �����', 'TM2', 4 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '����', 'DT_MAIN', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSMENASUB')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODSSMENASUB')) and (UPPER(ig.GRUP) = UPPER('�������')))

 INSERT INTO INVENTS(SNAME, TABLENAME, NEEDTOSHOW) Values('����������','PERIODSSUB',1)
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� �������', 'PERIOD_ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������� ������', 'ID', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������', 'SUPERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '������������', 'PERIOD', 1 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� ������', 'B', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '���� �����', 'D', 3 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 INSERT INTO INVFIELDS(NUM_INV, NAME, FIELD, TYPE) SELECT NUM_REC, '����� �������', 'NUMBR', 0 from INVENTS WHERE UPPER(TABLENAME) = UPPER('PERIODSSUB')
 
 INSERT INTO IGLIST(NUM_INV,NUM_GROUP) select inv.NUM_REC, ig.NUM_REC from INVENTS inv inner join IGROUPS ig on ((UPPER(inv.TABLENAME) = UPPER('PERIODSSUB')) and (UPPER(ig.GRUP) = UPPER('�������')))
 
 insert into login_rights(num_login, num_igroups, rights) values (1, 5, 1)

