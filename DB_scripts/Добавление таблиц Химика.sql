create table [UZER]([NUM_USER] int,[NAME_SHORT] nvarchar(100),[NAME_FULL] nvarchar(255),[NAME_TINY] nvarchar(50),[NAME_MICRO] nvarchar(15),[SITY] nvarchar(30),[ADDR] nvarchar(255),[TELEFON] nvarchar(255),[FAX] nvarchar(50),[EMAIL] nvarchar(50),[CODES] nvarchar(255),[BANK] nvarchar(255),[DESCRIPTIONS] nvarchar(255),[OWNERMODE] smallint,[PLUSCITY] bit,[UROPT] nvarchar(200),[PASS_PER_ALERT] int,[PASS_BEHAVIOUR] int , CONSTRAINT PKCU_UZER_NUM_USER PRIMARY KEY ([NUM_USER]))

create table [UZERDB]([NUM_REC] int IDENTITY(1, 1),[NAME] nvarchar(255),[DT1] datetime,[DT2] datetime,[DT3] datetime , CONSTRAINT PKCU_UZERDB_NUM_REC PRIMARY KEY ([NUM_REC]))

create table [GLOBALOPTIONS]([NUM_REC] int IDENTITY(1, 1),[OPTNAME] nvarchar(255),[OPTVALUE] IMAGE , CONSTRAINT PKCU_GLOBALOPTIONS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_GLOBALOPTIONS_OPTNAME] on [GLOBALOPTIONS]([OPTNAME])

create table [STATISTIKA]([NUM_REC] int IDENTITY(1, 1),[DATE_ST] datetime,[TYPE_ST] int,[NAME_ST] nvarchar(255),[COUNT_ST] int , CONSTRAINT PKCU_STATISTIKA_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_STATISTIKA_NAME_ST_TYPE_ST] on [STATISTIKA]([NAME_ST],[TYPE_ST])
create UNIQUE index [U_STATISTIKA_NUM_REC_DATE_ST] on [STATISTIKA]([NUM_REC],[DATE_ST],[COUNT_ST])

create table [LABS]([NUM_LAB] int IDENTITY(1, 1),[NAME_LAB] nvarchar(255),[LAB_MNGR_JOB] nvarchar(30),[LAB_MNGR] nvarchar(80),[METROLOG] nvarchar(80),[ATTESTAT] TEXT,[ADDRESS] nvarchar(255),[PHONES] nvarchar(100),[LAB] nvarchar(20),[EMAIL] nvarchar(255) , CONSTRAINT PKCU_LABS_NUM_LAB PRIMARY KEY ([NUM_LAB]))
create UNIQUE index [U_LABS_NUM_LAB_NAME_LAB] on [LABS]([NUM_LAB],[NAME_LAB])
create UNIQUE index [U_LABS_NAME_LAB] on [LABS]([NAME_LAB])

create table [IGROUPS]([NUM_REC] int IDENTITY(1, 1),[GRUP] nvarchar(50) , CONSTRAINT PKCU_IGROUPS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_IGROUPS_GRUP] on [IGROUPS]([GRUP])
create UNIQUE index [U_IGROUPS_NUM_REC_GRUP] on [IGROUPS]([NUM_REC],[GRUP])

create table [LOGIN]([NUM_REC] int IDENTITY(1, 1),[UZER] nvarchar(100),[PASSWORD] nvarchar(30),[LAB] int,[comment] nvarchar(255),[OKT] int,[FIOISP] nvarchar(50),[DT] datetime,[HIM_RIGHT] int,[UNI_RIGHT] int,[DOC_RIGHT] int,[JUR_RIGHT] smallint,[SML_RIGHTS] smallint,[OTHER_RIGHTS] int,[SMK_RIGHTS] int,[MNG_RIGHTS] nvarchar(50),[LAB_RIGHTS] int,[WORK_SP] int,[TJ_RIGHTS] int,[OTHER_RIGHTS2] int,[NUM_IMG] int,[PASS_PER] int,[PASS_END] datetime,[HIM2_RIGHTS] nvarchar(50),[BLOCKED] bit,[JRO2_RIGHTS] nvarchar(30),[ADUSER] nvarchar(255) , CONSTRAINT PKCU_LOGIN_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_UZER] on [LOGIN]([UZER])

create table [LJ_LOCKF]([NUM_REC] int IDENTITY(1, 1),[NUM_LJT] int,[NUM_N] int,[NUM_FIO1] int,[NUM_FIO2] int,[DT] datetime,[TM] datetime , CONSTRAINT PKCU_LJ_LOCKF_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LJ_LOCKF_NUM_LJT_NUM_N] on [LJ_LOCKF]([NUM_LJT],[NUM_N])

create table [LOGIN_JURNAL_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_JURNAL] int,[RIGHTS] int,[BAN] bit , CONSTRAINT PKCU_LOGIN_JURNAL_RIGHTS_NUM PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_JURNAL_RIGHTS_NUM_LO] on [LOGIN_JURNAL_RIGHTS]([NUM_LOGIN],[NUM_JURNAL])

create table [LOGIN_CLJJ_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_LJ] int , CONSTRAINT PKCU_LOGIN_CLJJ_RIGHTS_NUM_R PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_CLJJ_RIGHTS_NUM_LOGI] on [LOGIN_CLJJ_RIGHTS]([NUM_LOGIN],[NUM_LJ])

create table [LOGIN_JGROUP_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_JR] int,[GR_NAME] nvarchar(255),[RIGHTS] int , CONSTRAINT PKCU_LOGIN_JGROUP_RIGHTS_NUM PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_JGROUP_RIGHTS_NUM_LO] on [LOGIN_JGROUP_RIGHTS]([NUM_LOGIN],[GR_NAME],[NUM_JR])


create table [LOGIN_UNIV_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_UNIBOOK] int,[RIGHTS] int , CONSTRAINT PKCU_LOGIN_UNIV_RIGHTS_NUM_R PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_UNIV_RIGHTS_NUM_LOGI] on [LOGIN_UNIV_RIGHTS]([NUM_LOGIN],[NUM_UNIBOOK])

create table [LOGIN_UNIV2_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_US] int,[RIGHTS] int , CONSTRAINT PKCU_LOGIN_UNIV2_RIGHTS_NUM_ PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_UNIV2_RIGHTS_NUM_LOG] on [LOGIN_UNIV2_RIGHTS]([NUM_LOGIN],[NUM_US])


create table [LOGIN_JRO_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_SERVICE] int,[RIGHTS] int , CONSTRAINT PKCU_LOGIN_JRO_RIGHTS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_JRO_RIGHTS_NUM_LOGIN] on [LOGIN_JRO_RIGHTS]([NUM_LOGIN],[NUM_SERVICE])

create table [LOGIN_JUHR_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_JUHR] int,[RIGHTS] int , CONSTRAINT PKCU_LOGIN_JUHR_RIGHTS_NUM_R PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_JUHR_RIGHTS_NUM_LOGI] on [LOGIN_JUHR_RIGHTS]([NUM_LOGIN],[NUM_JUHR])

create table [LOGIN_TJ_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_TJ] int,[RIGHTS] int , CONSTRAINT PKCU_LOGIN_TJ_RIGHTS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_TJ_RIGHTS_NUM_LOGIN_] on [LOGIN_TJ_RIGHTS]([NUM_LOGIN],[NUM_TJ])


create table [LOGIN_NDOCS_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_DOCS] int,[RIGHTS] int , CONSTRAINT PKCU_LOGIN_NDOCS_RIGHTS_NUM_ PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_NDOCS_RIGHTS_NUM_LOG] on [LOGIN_NDOCS_RIGHTS]([NUM_LOGIN],[NUM_DOCS])

create table [DFORMS]([NUM_REC] int IDENTITY(1, 1),[CLASS] nvarchar(50),[NAME] nvarchar(200),[DOCFORM] IMAGE,[DOCVARS] IMAGE,[DESCR] TEXT,[DT] datetime,[USERID] nvarchar(100),[FINISHLOCK] bit,[DOCVIS] IMAGE , CONSTRAINT PKCU_DFORMS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMS_NAME_NUM_REC] on [DFORMS]([NAME],[NUM_REC])
create UNIQUE index [U_DFORMS_NAME] on [DFORMS]([NAME])
create UNIQUE index [U_DFORMS_CLASS_NAME] on [DFORMS]([CLASS],[NAME])

create table [LOGIN_CLJL_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_LAB] int , CONSTRAINT PKCU_LOGIN_CLJL_RIGHTS_NUM_R PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_CLJL_RIGHTS_NUM_LOGI] on [LOGIN_CLJL_RIGHTS]([NUM_LOGIN],[NUM_LAB])


create table [REGNUMS]([NUM_REC] int IDENTITY(1, 1),[NAME] nvarchar(20),[COMM] nvarchar(255),[TEMPL] nvarchar(50),[COUNTER] int,[USELABS] bit,[USEINDOCS] bit , CONSTRAINT PKCU_REGNUMS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_REGNUMS_NAME] on [REGNUMS]([NAME])


create table [LOGIN_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_IGROUPS] int,[RIGHTS] bit , CONSTRAINT PKCU_LOGIN_RIGHTS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_RIGHTS_NUM_LOGIN_NUM] on [LOGIN_RIGHTS]([NUM_LOGIN],[NUM_IGROUPS])
ALTER TABLE [LOGIN_RIGHTS] ADD CONSTRAINT [FK_LOGIN_LOGIN_RIGHTS] FOREIGN KEY ([NUM_LOGIN]) REFERENCES [LOGIN] ([NUM_REC])  ON DELETE CASCADE  ON UPDATE CASCADE 
ALTER TABLE [LOGIN_RIGHTS] ADD CONSTRAINT [FK_IGROUPS_LOGIN_RIGHTS] FOREIGN KEY ([NUM_IGROUPS]) REFERENCES [IGROUPS] ([NUM_REC])  ON DELETE CASCADE  ON UPDATE CASCADE 

create table [INVENTS]([NUM_REC] int IDENTITY(1, 1),[SNAME] nvarchar(50),[TABLENAME] nvarchar(50),[NEEDTOSHOW] bit , CONSTRAINT PKCU_INVENTS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_INVENTS_SNAME] on [INVENTS]([SNAME])
create UNIQUE index [U_INVENTS_TABLENAME] on [INVENTS]([TABLENAME])
create UNIQUE index [U_INVENTS_NUM_REC_TABLENAME] on [INVENTS]([NUM_REC],[TABLENAME])

create table [INVFIELDS]([NUM_INV] int,[NUM_REC] int IDENTITY(1, 1),[NAME] nvarchar(50),[FIELD] nvarchar(50),[TYPE] smallint , CONSTRAINT PKCU_INVFIELDS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_INVFIELDS_NUM_INV_NUM_REC] on [INVFIELDS]([NUM_INV],[NUM_REC])
create UNIQUE index [U_INVFIELDS_NUM_INV_NAME] on [INVFIELDS]([NUM_INV],[NAME])
create UNIQUE index [U_INVFIELDS_NUM_INV_FIELD] on [INVFIELDS]([NUM_INV],[FIELD])

create table [IGLIST]([NUM_GROUP] int,[NUM_REC] int IDENTITY(1, 1),[NUM_INV] int , CONSTRAINT PKCU_IGLIST_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_IGLIST_NUM_GROUP_NUM_INV] on [IGLIST]([NUM_GROUP],[NUM_INV])
create UNIQUE index [U_IGLIST_NUM_INV] on [IGLIST]([NUM_INV])

create table [CONPROC_MAIN_FILTER_N]([NUM_USER] int,[DTBEFORE] datetime,[DTAFTER] datetime,[EXECUTOR] nvarchar(50),[NUM_OBJECT] int,[NUM_PARAM] int,[NUM_METH] int,[NUM_ALGTYPE] int,[NUM_ALGGROUP] int,[POSX] int,[POSY] int,[SIZEX] int,[USELAST] int,[ALG_VER] nvarchar(10),[NAME_ALG] nvarchar(200),[NAME_GROUP] nvarchar(255) , CONSTRAINT PKCU_CONPROC_MAIN_FILTER_N_N PRIMARY KEY ([NUM_USER]))

create table [SML]([NUM_REC] int IDENTITY(1, 1),[LAB_ID] int,[NAME] nvarchar(255),[SHORTNAME] nvarchar(20),[NUM] int,[NUM_GRP] int,[ADD_LJ] bit,[DOP] TEXT,[ISREADY] bit , CONSTRAINT PKCU_SML_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_SML_NAME] on [SML]([NAME])
create UNIQUE index [U_SML_NUM_REC_NAME] on [SML]([NUM_REC],[NAME])

create table [LOGIN_SML_RIGHTS]([NUM_REC] int IDENTITY(1, 1),[NUM_LOGIN] int,[NUM_SML] int , CONSTRAINT PKCU_LOGIN_SML_RIGHTS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LOGIN_SML_RIGHTS_NUM_LOGIN] on [LOGIN_SML_RIGHTS]([NUM_LOGIN],[NUM_SML])

create table [SR_DOC_ND]([NUM_REC] int IDENTITY(1, 1),[NUM_DOC] int,[NAME] nvarchar(200),[NUM_DFORMS] int , CONSTRAINT PKCU_SR_DOC_ND_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_SR_DOC_ND_NUM_DOC_NAME] on [SR_DOC_ND]([NUM_DOC],[NAME])

create table [DFORMSQUERY]([NUM_FORM] int,[NUM_REC] int IDENTITY(1, 1),[NUMBR] int,[NAME] nvarchar(100),[QUERY] IMAGE,[PARAMS] IMAGE , CONSTRAINT PKCU_DFORMSQUERY_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMSQUERY_NUM_FORM_NAME] on [DFORMSQUERY]([NUM_FORM],[NAME])
create UNIQUE index [U_DFORMSQUERY_NUM_FORM_NUMBR] on [DFORMSQUERY]([NUM_FORM],[NUMBR])

create table [DFORMSOLE]([NUM_REC] int IDENTITY(1, 1),[NUM_DOC] int,[OLEDATA] IMAGE , CONSTRAINT PKCU_DFORMSOLE_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMSOLE_NUM_DOC] on [DFORMSOLE]([NUM_DOC])

create table [DFORMSHISTORY]([NUM_FORM] int,[NUM_REC] int IDENTITY(1, 1),[NAME_PRM] nvarchar(255),[VALUE_PRM] nvarchar(255),[TIME_STAMP] datetime , CONSTRAINT PKCU_DFORMSHISTORY_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMSHISTORY_NUM_FORM_NAM] on [DFORMSHISTORY]([NUM_FORM],[NAME_PRM],[TIME_STAMP])
create UNIQUE index [U_DFORMSHISTORY_NUM_FORM_NAM9] on [DFORMSHISTORY]([NUM_FORM],[NAME_PRM],[VALUE_PRM])

create table [DFATT]([NUM_FORM] int,[NUM_REC] int IDENTITY(1, 1),[VARGROUP] nvarchar(255),[VARNAME] nvarchar(255),[NAME] nvarchar(50) , CONSTRAINT PKCU_DFATT_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFATT_NUM_FORM_NUM_REC] on [DFATT]([NUM_FORM],[NUM_REC])
create index [I_DFATT_NUM_FORM_VARGROUP_VA] on [DFATT]([NUM_FORM],[VARGROUP],[VARNAME])

create table [DFORMSDOX]([NUM_DOC] int,[NUM_REC] int IDENTITY(1, 1),[DT] datetime,[TM] datetime,[DOC] IMAGE,[FINAL] int,[FIO_ID] nvarchar(50),[LAB_ID] int , CONSTRAINT PKCU_DFORMSDOX_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMSDOX_NUM_REC_NUM_DOC_] on [DFORMSDOX]([NUM_REC],[NUM_DOC],[DT],[TM],[FIO_ID],[LAB_ID],[FINAL])
create UNIQUE index [U_DFORMSDOX_NUM_DOC_DT_TM] on [DFORMSDOX]([NUM_DOC],[DT],[TM])

create table [DFORMSDOXATT]([NUM_REC] int IDENTITY(1, 1),[NUM_DOX_FORM] int,[NAME_P] nvarchar(255),[VALUE_P] nvarchar(255) , CONSTRAINT PKCU_DFORMSDOXATT_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMSDOXATT_NUM_DOX_FORM_] on [DFORMSDOXATT]([NUM_DOX_FORM],[NUM_REC])
create index [I_DFORMSDOXATT_NUM_DOX_FORM_] on [DFORMSDOXATT]([NUM_DOX_FORM],[NAME_P])

create table [DFORMSDOXP]([NUM_REC] int IDENTITY(1, 1),[NUM_DOX_FORM] int,[NAME_P] nvarchar(255),[VALUE_P] nvarchar(255) , CONSTRAINT PKCU_DFORMSDOXP_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_DFORMSDOXP_NUM_DOX_FORM_NU] on [DFORMSDOXP]([NUM_DOX_FORM],[NUM_REC])
create UNIQUE index [U_DFORMSDOXP_NUM_DOX_FORM_NA] on [DFORMSDOXP]([NUM_DOX_FORM],[NAME_P])

create table [SPR_HEADERS]([NUM_REC] int IDENTITY(1, 1),[SID] nvarchar(50),[NAMEFORM] nvarchar(255),[COMM] nvarchar(255),[H] int,[W] int,[NUM_GROUP] int,[LIMITTXT] int , CONSTRAINT PKCU_SPR_HEADERS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_SPR_HEADERS_SID] on [SPR_HEADERS]([SID])
create UNIQUE index [U_SPR_HEADERS_NAMEFORM] on [SPR_HEADERS]([NAMEFORM])
create UNIQUE index [U_SPR_HEADERS_SID_NUM_REC] on [SPR_HEADERS]([SID],[NUM_REC])

create table [SPR_VALS]([NUM_REC] int IDENTITY(1, 1),[NUM_SPR] int,[VAL] nvarchar(255) , CONSTRAINT PKCU_SPR_VALS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_SPR_VALS_NUM_SPR_VAL] on [SPR_VALS]([NUM_SPR],[VAL])

create table [LJS]([NUM_REC] int IDENTITY(1, 1),[JOURNAL] nvarchar(255),[FORMULABEFORE] TEXT,[FORMULAAFTER] TEXT,[SHORTNAME] nvarchar(30),[INSTRUCTION] TEXT,[NORCHECK] bit,[NOXMED] bit,[USE_GR] bit,[FULLSCR] bit,[BRAKSCAN] bit,[NUM_N_SHOW] int,[USE_REGNUM] bit,[REGNUM] int,[USE_REAGENT_WRITEOFF] bit,[USEMETHLAB] bit,[VIEWTYPE] int,[STOREFILES] bit,[CLAS] nvarchar(60),[NUMB] int,[SHORTJOURNAL] nvarchar(255),[FBEFOREDESC] nvarchar(255),[FAFTERDESC] nvarchar(255),[NUMTEXTDOC] int,[CIPHERFIELD] nvarchar(11),[MNGPARAMS] bit,[DOCVIS] IMAGE,[USE_VERIF] bit,[VERIFKOEF] float,[DATETAKENTXT] nvarchar(100),[TIMETAKENTXT] nvarchar(100),[ISDTSHOW] bit,[ISLOCKINSERT] bit,[COPYTAKINGDTTM] bit,[NUM_C_CM] int,[USEMULTIINS] bit,[USE_SELECTNORM] bit , CONSTRAINT PKCU_LJS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_LJS_NUM_REC_JOURNAL] on [LJS]([NUM_REC],[JOURNAL])
create UNIQUE index [U_LJS_JOURNAL] on [LJS]([JOURNAL])


create table [JOURNAL]([NUM_REC] int IDENTITY(1, 1),[NUM_FORM] int,[NUM_LAB] int , CONSTRAINT PKCU_JOURNAL_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_JOURNAL_NUM_FORM_NUM_LAB_N] on [JOURNAL]([NUM_FORM],[NUM_LAB],[NUM_REC])

create table [UNIBOOK]([NUM_REC] int IDENTITY(1, 1),[NAMEBOOK] nvarchar(255),[NAMEBOOKSM] nvarchar(30),[FORMTABLE] smallint,[DOP] TEXT,[HASTABLE] bit , CONSTRAINT PKCU_UNIBOOK_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_UNIBOOK_NAMEBOOK] on [UNIBOOK]([NAMEBOOK])
create UNIQUE index [U_UNIBOOK_NAMEBOOKSM] on [UNIBOOK]([NAMEBOOKSM])
create UNIQUE index [U_UNIBOOK_NUM_REC_NAMEBOOK] on [UNIBOOK]([NUM_REC],[NAMEBOOK])

create table [UNILAB]([NUM_REC] int IDENTITY(1, 1),[NUM_LAB] int,[NUM_UNI] int , CONSTRAINT PKCU_UNILAB_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_UNILAB_NUM_LAB_NUM_UNI] on [UNILAB]([NUM_LAB],[NUM_UNI])



create table [SMK2_JFORMS]([NUM_REC] int IDENTITY(1, 1),[NUM_SMK] int,[NAME] nvarchar(255),[SHORTNAME] nvarchar(25),[INSTRUCTION] TEXT,[USECONFIRM] bit , CONSTRAINT PKCU_SMK2_JFORMS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_SMK2_JFORMS_NAME] on [SMK2_JFORMS]([NAME])
create UNIQUE index [U_SMK2_JFORMS_NUM_SMK_NAME_N] on [SMK2_JFORMS]([NUM_SMK],[NAME],[NUM_REC])

create table [SMK2_JOURNALS]([NUM_REC] int IDENTITY(1, 1),[NUM_JFORM] int,[NUM_LAB] int , CONSTRAINT PKCU_SMK2_JOURNALS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_SMK2_JOURNALS_NUM_LAB_NUM_] on [SMK2_JOURNALS]([NUM_LAB],[NUM_JFORM])

create table [US]([NUM_REC] int IDENTITY(1, 1),[NAM] nvarchar(255),[SNAM] nvarchar(100),[TYPVIEW] int,[DOP] TEXT,[EXPAND] bit,[TYPUSE] int,[DOCVIS] IMAGE , CONSTRAINT PKCU_US_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_US_NAM] on [US]([NAM])
create UNIQUE index [U_US_SNAM] on [US]([SNAM])
create UNIQUE index [U_US_NUM_REC_NAM] on [US]([NUM_REC],[NAM])


create table [DOPLINKS]([NUM_REC] int IDENTITY(1, 1),[TBL1] nvarchar(50),[FLD1] nvarchar(50),[RELT1T2] smallint,[TBL2] nvarchar(50),[FLD2] nvarchar(50) , CONSTRAINT PKCU_DOPLINKS_NUM_REC PRIMARY KEY ([NUM_REC]))

create table [METHPARVARS]([NUM_PRM] int,[NUM_REC] int IDENTITY(1, 1),[VARIABLE] nvarchar(255),[V2] nvarchar(255),[DESCR] nvarchar(255),[UNIT] nvarchar(15),[CONSTANT] float,[MAXVAL] float,[CALIBR] nvarchar(14),[MINVAL] float,[ISMETROL] bit,[NUM_JPR] int,[NUM_MTD] int,[CONST_LOCK] bit,[IS_LEFT] bit,[NUM_JRPV] int,[COPY_LOCK] bit,[NUM_QUERY] int,[NUM_CALC] int,[ISVSP] bit,[NUM_JPR2] int,[NUM_CRM] int,[ISRAZB] bit , CONSTRAINT PKCU_METHPARVARS_NUM_REC PRIMARY KEY ([NUM_REC]))
create UNIQUE index [U_METHPARVARS_NUM_PRM_NUM_REC] on [METHPARVARS]([NUM_PRM],[NUM_REC])
create UNIQUE index [U_METHPARVARS_NUM_PRM_VARIAB] on [METHPARVARS]([NUM_PRM],[VARIABLE])