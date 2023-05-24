--------------------------------------------------------
--  文件已创建 - 星期三-五月-24-2023   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table PET
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET" 
   (	"PET_ID" VARCHAR2(20 BYTE), 
	"PET_NAME" VARCHAR2(20 BYTE), 
	"BREED" VARCHAR2(20 BYTE), 
	"AGE" NUMBER(*,0), 
	"PICTURE" VARCHAR2(50 BYTE), 
	"HEALTH_STATE" NUMBER(*,0), 
	"VACCINE" NUMBER(1,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_ACCOMMODATE
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_ACCOMMODATE" 
   (	"DURATION" NUMBER(*,0), 
	"PET_ID" VARCHAR2(20 BYTE), 
	"ROOM_ID" VARCHAR2(5 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_ADOPT
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_ADOPT" 
   (	"TIME_ID" VARCHAR2(20 BYTE), 
	"ADDRESS" VARCHAR2(20 BYTE), 
	"ADOPTER_ID" VARCHAR2(20 BYTE), 
	"PET_ID" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_DOCTOR
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_DOCTOR" 
   (	"ID" VARCHAR2(20 BYTE), 
	"NAME" VARCHAR2(20 BYTE), 
	"SALARY" NUMBER(*,0), 
	"PHONE" CHAR(11 BYTE), 
	"WORK_YEARS" NUMBER(*,0), 
	"ACADEMIC_QUALIFICATION" VARCHAR2(50 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_DONATE
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_DONATE" 
   (	"DONATION_ID" VARCHAR2(20 BYTE), 
	"TIME_ID" VARCHAR2(20 BYTE), 
	"DONATION_AMOUNT" NUMBER(*,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_EMPLOYEE
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_EMPLOYEE" 
   (	"ID" VARCHAR2(20 BYTE), 
	"NAME" VARCHAR2(20 BYTE), 
	"SALARY" NUMBER(*,0), 
	"PHONE" CHAR(11 BYTE), 
	"DUTY" VARCHAR2(20 BYTE), 
	"POSITION" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_FINANCE
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_FINANCE" 
   (	"FINANCE_ID" VARCHAR2(20 BYTE), 
	"BALANCE" FLOAT(126), 
	"TIME_ID" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_FOSTER
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_FOSTER" 
   (	"TIME_ID" VARCHAR2(20 BYTE), 
	"DURATION" NUMBER(*,0), 
	"FOSTER_PARENT" VARCHAR2(20 BYTE), 
	"PET_ID" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_NOTICE
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_NOTICE" 
   (	"NOTICE_ID" VARCHAR2(20 BYTE), 
	"NOTICE_TITLE" VARCHAR2(20 BYTE), 
	"NOTICE_CONTENTS" VARCHAR2(200 BYTE), 
	"TIME_ID" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_POST_N
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_POST_N" 
   (	"POST_ID" VARCHAR2(20 BYTE), 
	"POST_CONTENTS" VARCHAR2(200 BYTE), 
	"POST_COMMENT" VARCHAR2(200 BYTE), 
	"LIKES_NUM" NUMBER(*,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_POST_V
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_POST_V" 
   (	"POST_ID" VARCHAR2(20 BYTE), 
	"TIME_ID" VARCHAR2(20 BYTE), 
	"READ_COUNT" NUMBER(*,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_RELEASE
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_RELEASE" 
   (	"TIME_ID" VARCHAR2(20 BYTE), 
	"NOTICE_ID" VARCHAR2(20 BYTE), 
	"ATTACHMENTS" VARCHAR2(20 BYTE), 
	"READ_COUNT" NUMBER(*,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_ROOM
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_ROOM" 
   (	"ROOM_ID" VARCHAR2(20 BYTE), 
	"ROOM_STATUS" NUMBER(1,0), 
	"AREA" NUMBER(*,0), 
	"FLOOR" NUMBER(*,0), 
	"IS_CLEAN" NUMBER(1,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_TIME
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_TIME" 
   (	"TIME_ID" VARCHAR2(20 BYTE), 
	"YEAR" NUMBER(*,0), 
	"MONTH" NUMBER(*,0), 
	"DAY" NUMBER(*,0), 
	"HOUR" NUMBER(*,0), 
	"MINUTE" NUMBER(*,0), 
	"SECOND" NUMBER(*,0)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_TREATMENT
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_TREATMENT" 
   (	"TIME_ID" VARCHAR2(20 BYTE), 
	"TYPE" VARCHAR2(20 BYTE), 
	"PET_ID" VARCHAR2(20 BYTE), 
	"DOCTOR_ID" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table PET_USER
--------------------------------------------------------

  CREATE TABLE "C##PETRESCUE"."PET_USER" 
   (	"USER_ID" VARCHAR2(20 BYTE), 
	"USER_NAME" VARCHAR2(20 BYTE), 
	"PASSWORD" VARCHAR2(2 BYTE), 
	"PHONE" CHAR(11 BYTE), 
	"ACCOUNT_STATUS" NUMBER(*,0), 
	"ADDRESS" VARCHAR2(50 BYTE)
   ) SEGMENT CREATION DEFERRED 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010338
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010338" ON "C##PETRESCUE"."PET" ("PET_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010363
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010363" ON "C##PETRESCUE"."PET_ACCOMMODATE" ("PET_ID", "ROOM_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010370
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010370" ON "C##PETRESCUE"."PET_ADOPT" ("TIME_ID", "ADOPTER_ID", "PET_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010343
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010343" ON "C##PETRESCUE"."PET_DOCTOR" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010353
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010353" ON "C##PETRESCUE"."PET_DONATE" ("DONATION_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010344
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010344" ON "C##PETRESCUE"."PET_EMPLOYEE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010349
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010349" ON "C##PETRESCUE"."PET_FINANCE" ("FINANCE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010359
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010359" ON "C##PETRESCUE"."PET_FOSTER" ("TIME_ID", "FOSTER_PARENT", "PET_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010346
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010346" ON "C##PETRESCUE"."PET_NOTICE" ("NOTICE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010348
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010348" ON "C##PETRESCUE"."PET_POST_N" ("POST_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010351
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010351" ON "C##PETRESCUE"."PET_POST_V" ("POST_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010374
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010374" ON "C##PETRESCUE"."PET_RELEASE" ("TIME_ID", "NOTICE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010342
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010342" ON "C##PETRESCUE"."PET_ROOM" ("ROOM_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010345
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010345" ON "C##PETRESCUE"."PET_TIME" ("TIME_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010366
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010366" ON "C##PETRESCUE"."PET_TREATMENT" ("TIME_ID", "PET_ID", "DOCTOR_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index SYS_C0010341
--------------------------------------------------------

  CREATE UNIQUE INDEX "C##PETRESCUE"."SYS_C0010341" ON "C##PETRESCUE"."PET_USER" ("USER_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table PET
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET" ADD PRIMARY KEY ("PET_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_ACCOMMODATE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_ACCOMMODATE" ADD PRIMARY KEY ("PET_ID", "ROOM_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_ADOPT
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_ADOPT" ADD PRIMARY KEY ("TIME_ID", "ADOPTER_ID", "PET_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_DOCTOR
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_DOCTOR" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_DONATE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_DONATE" ADD PRIMARY KEY ("DONATION_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_EMPLOYEE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_EMPLOYEE" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_FINANCE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_FINANCE" ADD PRIMARY KEY ("FINANCE_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_FOSTER
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_FOSTER" ADD PRIMARY KEY ("TIME_ID", "FOSTER_PARENT", "PET_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_NOTICE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_NOTICE" ADD PRIMARY KEY ("NOTICE_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_POST_N
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_POST_N" ADD PRIMARY KEY ("POST_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_POST_V
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_POST_V" ADD PRIMARY KEY ("POST_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_RELEASE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_RELEASE" ADD PRIMARY KEY ("TIME_ID", "NOTICE_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_ROOM
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_ROOM" ADD PRIMARY KEY ("ROOM_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_TIME
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_TIME" ADD PRIMARY KEY ("TIME_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_TREATMENT
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_TREATMENT" ADD PRIMARY KEY ("TIME_ID", "PET_ID", "DOCTOR_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PET_USER
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_USER" ADD PRIMARY KEY ("USER_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  TABLESPACE "USERS"  ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_ACCOMMODATE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_ACCOMMODATE" ADD FOREIGN KEY ("PET_ID")
	  REFERENCES "C##PETRESCUE"."PET" ("PET_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_ACCOMMODATE" ADD FOREIGN KEY ("ROOM_ID")
	  REFERENCES "C##PETRESCUE"."PET_ROOM" ("ROOM_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_ADOPT
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_ADOPT" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_ADOPT" ADD FOREIGN KEY ("ADOPTER_ID")
	  REFERENCES "C##PETRESCUE"."PET_USER" ("USER_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_ADOPT" ADD FOREIGN KEY ("PET_ID")
	  REFERENCES "C##PETRESCUE"."PET" ("PET_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_DONATE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_DONATE" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_FINANCE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_FINANCE" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_FOSTER
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_FOSTER" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_FOSTER" ADD FOREIGN KEY ("FOSTER_PARENT")
	  REFERENCES "C##PETRESCUE"."PET_USER" ("USER_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_FOSTER" ADD FOREIGN KEY ("PET_ID")
	  REFERENCES "C##PETRESCUE"."PET" ("PET_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_NOTICE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_NOTICE" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_POST_V
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_POST_V" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_RELEASE
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_RELEASE" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_RELEASE" ADD FOREIGN KEY ("NOTICE_ID")
	  REFERENCES "C##PETRESCUE"."PET_NOTICE" ("NOTICE_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PET_TREATMENT
--------------------------------------------------------

  ALTER TABLE "C##PETRESCUE"."PET_TREATMENT" ADD FOREIGN KEY ("TIME_ID")
	  REFERENCES "C##PETRESCUE"."PET_TIME" ("TIME_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_TREATMENT" ADD FOREIGN KEY ("PET_ID")
	  REFERENCES "C##PETRESCUE"."PET" ("PET_ID") ENABLE;
  ALTER TABLE "C##PETRESCUE"."PET_TREATMENT" ADD FOREIGN KEY ("DOCTOR_ID")
	  REFERENCES "C##PETRESCUE"."PET_DOCTOR" ("ID") ENABLE;
