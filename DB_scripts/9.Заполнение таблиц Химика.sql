Insert into LABS(name_lab) values ('����������� �������������� ��������')

Insert into UZER(num_user, name_short, name_tiny, sity, addr, codes, pass_per_alert, pass_behaviour) values
('1','��� "�������"��� 2_0 �������� _2014_08_25', '������������', '�����', '������, 634045, �.�����,;��.���������� 9, ���. 16',
'���', '3', '1')

Insert into LOGIN(UZER, LAB, SML_RIGHTS, OTHER_RIGHTS, BLOCKED, doc_right) values 
('1', '1', '1', '282290900', '0', '249')

Insert into IGroups(grup) values ('������')
Insert into IGroups(grup) values ('������')
Insert into IGroups(grup) values ('������������')

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


insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ������', 'ID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', '������������ ������', 'TaskName', '1')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ���� ������', 'TaskTypeID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ������������ ������', 'ParentTaskID', '0')
insert into InvFields(num_inv, [name], field, type) values ('1', 'ID ���� ������', 'WorkTypeID', '0')

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