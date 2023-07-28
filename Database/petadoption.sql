drop table collect_pet_info ;
drop table comment_pet;
drop table like_pet;
drop table comment_post;
drop table like_post;
drop table application;
drop table treatment;
drop table accommodate;
drop table foster;
drop table adopt;
drop table donation;
drop table forum_posts;
drop table bulletin;
drop table employee;
drop table vet;
drop table room;
drop table pet;
drop table user2;
create table user2(--since table name 'user' is not valid in that it'a a internal name of system
  user_id varchar2(20) not null,
  user_name varchar2(20),
  password varchar2(16),
  phone_number varchar2(20),--it always comes true that a phone number will contain some special character dash or space ,so its length is variable
  account_status varchar2(20),
  address varchar2(100),
  role varchar2(10) default 'User',
  CONSTRAINT CHK_Role CHECK(role in('Admin','User','Unknown','Other')),
  CONSTRAINT CHK_PhoneNumber CHECK (REGEXP_LIKE (phone_number, '^\d{3}-\d{4}-\d{4}$') OR REGEXP_LIKE (phone_number, '^\d{11}$') OR REGEXP_LIKE (phone_number, '^\d{3} \d{4} \d{4}$')),--check if the phone_number is legal
  CONSTRAINT CHK_Password CHECK(LENGTH(password) >= 10 AND
    REGEXP_LIKE(password, '[0-9]') AND
    REGEXP_LIKE(password, '[a-z]') AND
    REGEXP_LIKE(password, '[A-Z]') AND
    REGEXP_LIKE(password, '[!@#$%^&*()]')),
    primary key(user_id),
    check(account_status in('Compliant','In Good Standing','Under Review','Warning Issued','Suspended','Probation','Banned','Appealing'))
);
--table pet
create table pet(
  pet_id varchar2(20) not null,
  pet_name varchar2(20),
  breed varchar2(20),
  psize varchar2(20) default 'small' check (psize in('small','large','medium')),
  birthdate DATE,
  avatar BLOB,
  health_state varchar2(15),
  vaccine char(1),--Y represents vaccination done while N represents undone.
  read_num int default 0,
  like_num int default 0,
  collect_num int default 0,
  primary key(pet_id),
  CONSTRAINT CHK_HealthState CHECK(health_state in('Vibrant','Well','Decent','Unhealthy','Sicky','Critical')),
  CONSTRAINT CHK_Num CHECK(read_num>=0 AND like_num>=0 AND collect_num>=0 AND vaccine in('Y','N'))
)LOB(avatar) STORE AS SECUREFILE;

--table room
create table room(
  room_status char(1),--'Y'/'N'
  room_size numeric(5,2),--since area may lead to ambiguity
  storey numeric(2,0),--since floor may lead to ambiguity
  compartment numeric(2,0),
  cleaning_time  varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(storey,compartment),
  check (storey between 1 and 10 and compartment between 1 and 30),
  CONSTRAINT CHK_RoomStatus CHECK(room_status in('Y','N')),
  CONSTRAINT CHK_Legal CHECK(storey>0 AND room_size>=0)
);

--table vet(since doctor will cause ambiguity and vet is more appropriate in the context)
create table vet(
  vet_id varchar2(20) not null,
  vet_name varchar(20),
  salary numeric(9,2),
  phone_number varchar2(20),
  --the following attributes as an integrity represents the interval of working hours
  working_start_hr numeric(2,0),
  working_end_hr numeric(2,0),
  working_start_min numeric(2,0),
  working_end_min numeric(2,0),
  primary key(vet_id),
  CONSTRAINT CHK_WorkingHours CHECK(
  working_start_hr between 0 and 23 and working_end_hr between working_start_hr and 23 
  and working_end_hr*60+working_end_min>=working_start_hr*60+working_start_min
  and working_start_min between 0 and 59 and working_end_min between 0 and 59 ),
  CONSTRAINT CHK_PhoneNumber2 CHECK (REGEXP_LIKE (phone_number, '^\d{3}-\d{4}-\d{4}$') OR REGEXP_LIKE (phone_number, '^\d{11}$') OR REGEXP_LIKE (phone_number, '^\d{3} \d{4} \d{4}$'))
);
create table employee(
  employee_id varchar2(20) not null,
  employee_name varchar(20),
  salary numeric(9,2),
  phone_number varchar2(20),
  duty varchar2(50),
  --the following attributes as an integrity represents the interval of working hours
  working_start_hr numeric(2,0),
  working_start_min numeric(2,0),
  working_end_hr numeric(2,0),
  working_end_min numeric(2,0),
  primary key(employee_id),
  CONSTRAINT CHK_WorkingHours2 CHECK(
  working_start_hr between 0 and 23 and working_end_hr between working_start_hr and 23 
  and working_end_hr*60+working_end_min>=working_start_hr*60+working_start_min
  and working_start_min between 0 and 59 and working_end_min between 0 and 59 ),
  CONSTRAINT CHK_PhoneNumber3 CHECK (REGEXP_LIKE (phone_number, '^\d{3}-\d{4}-\d{4}$') OR REGEXP_LIKE (phone_number, '^\d{11}$') OR REGEXP_LIKE (phone_number, '^\d{3} \d{4} \d{4}$')),
  CONSTRAINT CHK_DUTIES CHECK(duty in('Animal Care Specialist','Adoption Counselor','Veterinary Technician',
  'Animal Behaviorist','Volunteer Coordinator','Foster Coordinator',
  'Facility Manager','Fundraising and Outreach Coordinator','Rescue Transporter'))
);
--potential primary key(heading,bulletin_contents)
create table bulletin(
  bulletin_id varchar2(20),
  employee_id varchar2(20) references employee(employee_id) ,
  heading varchar2(20),
  bulletin_contents varchar2(2000) not null,
  read_count int,
  published_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(bulletin_id)
)partition by range(published_time)interval(interval '1' year)
(partition start_bulletion values less than(TIMESTAMP '2023-09-01 00:00:00'));
--potential primary key(user_id,post_contents,post_time)
create table forum_posts(
  post_id varchar2(20) not null,
  user_id varchar2(20) references user2(user_id),
  post_contents varchar2(1000) not null,
  read_count int default 0,
  like_num int default 0,
  comment_num int default 0,
  censored CHAR(1) CHECK(censored in('Y','N')),
  post_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(post_id),
  constraint CHK_LEGAL2 check(read_count>=0 and like_num>=0 and comment_num>=0
  and read_count>= like_num and read_count>=comment_num)
)partition by range(post_time)interval(interval '1' year)
(partition start_post values less than(TIMESTAMP '2023-09-01 00:00:00'));
--potential primary key(donor_id,donation_time,donation_amounts)
create table donation(
  donor_id varchar(20) references user2(user_id),
  donation_amount int,
  donation_time TIMESTAMP default CURRENT_TIMESTAMP,
  censor_state varchar2(20) default 'to be censored' check( censor_state in('to be censored','aborted','legitimate','outdated','invalid')),
  constraints CHK_DONATION check(donation_amount>0),
  primary key(donor_id,donation_amount,donation_time)
)partition by range(donation_time)interval(interval '1' year)
(partition start_donate values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table adopt(
  adopter_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  adoption_time DATE default CURRENT_DATE,
  primary key(adopter_id,pet_id)
)partition by range(adoption_time)interval(interval '1' year)
(partition start_donate values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table foster(
  duration smallint,
  fosterer varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  start_year numeric(4,0) default extract(year from CURRENT_DATE),
  start_month numeric(2,0) default extract(month from CURRENT_DATE),
  start_day numeric(2,0) default extract(day from CURRENT_DATE),
  censor_state varchar2(20) default 'to be censored' check( censor_state in('to be censored','aborted','legitimate','outdated','invalid')),
  remark varchar2(100) default 'May the censorship passed!',
  primary key(start_year,start_month,start_day,fosterer,pet_id),
  constraint CHK_Duration check(duration>=0)
)partition by range(start_year)
(partition start_foster values less than('2024'));
create table accommodate(
  owner_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  storey numeric(2,0) ,
  compartment numeric(2,0) ,
  primary key(pet_id,storey,compartment),
  foreign key(storey,compartment) references room
);
--Because treat is ambiguious and treatment is usually referred in medication/surgery
create table treatment(
  category varchar2(20),
  pet_id varchar2(20) references pet(pet_id),
  vet_id varchar2(20) references vet(vet_id),
  treat_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(treat_time,pet_id,vet_id)
)partition by range(treat_time)interval(interval '1' year)
(partition start_treatment values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table application(
  pet_id varchar2(20) references pet(pet_id),
  user_id varchar2(20) references user2(user_id),
  category varchar2(20),
  reason varchar2(200) not null,
  apply_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(pet_id,user_id,apply_time)
)partition by range(apply_time)interval(interval '1' year)
(partition start_apply values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table like_post(
  user_id varchar2(20) references user2(user_id),
  post_id varchar2(20) references forum_posts(post_id),
  like_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(like_time,user_id,post_id)
)partition by range(like_time)interval(interval '1' year)
(partition start_like_post values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table comment_post(
  user_id varchar2(20) references user2(user_id),
  post_id varchar2(20) references forum_posts(post_id),
  comment_contents varchar2(150) not null,
  comment_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(comment_time,user_id,post_id)
)partition by range(comment_time)interval(interval '1' year)
(partition start_comment_post values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table like_pet(
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  like_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(like_time,user_id,pet_id)
)partition by range(like_time)interval(interval '1' year)
(partition start_like_pet values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table comment_pet(
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  comment_contents varchar2(200) not null,
  comment_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(comment_time,user_id,pet_id)
)partition by range(comment_time)interval(interval '1' year)
(partition start_comment_pet values less than(TIMESTAMP '2023-09-01 00:00:00'));
create table collect_pet_info(
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  collect_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(collect_time,user_id,pet_id)
)partition by range(collect_time)interval(interval '1' year)
(partition start_collect_pet values less than(TIMESTAMP '2023-09-01 00:00:00'));
CREATE OR REPLACE TRIGGER increase_like_count
AFTER INSERT ON like_post
FOR EACH ROW
BEGIN
   UPDATE forum_posts
   SET like_num = like_num + 1
   WHERE post_id = :NEW.post_id;
END;
/

CREATE OR REPLACE TRIGGER decrease_like_count
AFTER DELETE ON like_post
FOR EACH ROW
BEGIN
   UPDATE forum_posts
   SET like_num = like_num - 1
   WHERE post_id = :OLD.post_id;
END;
/
CREATE OR REPLACE TRIGGER increase_comment_count
AFTER INSERT ON comment_post
FOR EACH ROW
BEGIN
   UPDATE forum_posts
   SET comment_num = comment_num + 1
   WHERE post_id = :NEW.post_id;
END;
/
CREATE OR REPLACE TRIGGER decrease_comment_count
AFTER DELETE ON comment_post
FOR EACH ROW
BEGIN
   UPDATE forum_posts
   SET comment_num = comment_num - 1
   WHERE post_id = :NEW.post_id;
END;
/

CREATE OR REPLACE TRIGGER increase_pet_like_count
AFTER INSERT ON like_pet
FOR EACH ROW
BEGIN
   UPDATE pet
   SET like_num = like_num + 1
   WHERE pet_id = :NEW.pet_id;
END;
/
CREATE OR REPLACE TRIGGER decrease_pet_like_count
AFTER DELETE ON like_pet
FOR EACH ROW
BEGIN
   UPDATE pet
   SET like_num = like_num - 1
   WHERE pet_id = :NEW.pet_id;
END;
/
CREATE OR REPLACE TRIGGER increase_pet_collect_count
AFTER INSERT ON collect_pet_info
FOR EACH ROW
BEGIN
   UPDATE pet
   SET collect_num = collect_num + 1
   WHERE pet_id = :NEW.pet_id;
END;
/
CREATE OR REPLACE TRIGGER decrease_pet_collect_count
AFTER DELETE ON collect_pet_info
FOR EACH ROW
BEGIN
   UPDATE pet
   SET collect_num = collect_num - 1
   WHERE pet_id = :NEW.pet_id;
END;
/
--建立用户UID的序列用于生成ID
DROP SEQUENCE user_id_seq;
CREATE SEQUENCE user_id_seq START WITH 1 INCREMENT BY 1;
--生成用户的随机数据
BEGIN
    FOR i IN 1..50 LOOP
    BEGIN
        INSERT INTO user2 (user_id, user_name, password, phone_number, account_status, address) 
        VALUES (
            user_id_seq.NEXTVAL, -- 随机20位的字符串
            DBMS_RANDOM.string('A', 5), -- 随机20位的字符串
            'Password1!', -- 静态密码，因为生成随机满足复杂性要求的密码在PL/SQL中会有些复杂
            CASE 
    WHEN MOD(i, 3) = 0 THEN TO_CHAR(TRUNC(DBMS_RANDOM.value(100, 999))) || '-' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999))) || '-' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999)))
    WHEN MOD(i, 3) = 1 THEN TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 9))) || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000000000, 9999999999)))
    ELSE TO_CHAR(TRUNC(DBMS_RANDOM.value(100, 999))) || ' ' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999))) || ' ' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999)))
            END,
            CASE 
                WHEN MOD(i, 7) = 0 THEN 'Compliant'
                WHEN MOD(i, 7) = 1 THEN 'In Good Standing'
                WHEN MOD(i, 7) = 2 THEN 'Under Review'
                WHEN MOD(i, 7) = 3 THEN 'Warning Issued'
                WHEN MOD(i, 7) = 4 THEN 'Suspended'
                WHEN MOD(i, 7) = 5 THEN 'Probation'
                ELSE 'Banned'
            END, -- 轮流分配账户状态
            'Random Address' -- 随机地址
        );
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
--生成随机的宠物
-- 创建一个序列
DROP SEQUENCE pet_id_seq;
CREATE SEQUENCE pet_id_seq
START WITH     1
INCREMENT BY   1;
-- 使用PL/SQL块生成随机的宠物条目
DECLARE 
    v_pet_id varchar2(20);
    v_pet_name varchar2(20);
    v_breed varchar2(20);
    v_birthdate date;
    v_avatar BLOB;
    v_health_state varchar2(15);
    v_vaccine char(1);
    v_read_num int;
    src_bfile BFILE;
BEGIN
    FOR i IN 1..50 LOOP
    BEGIN
        v_pet_id := pet_id_seq.NEXTVAL; -- 使用序列生成pet_id
        v_pet_name := DBMS_RANDOM.string('A', 10);
        v_breed := CASE
                      WHEN MOD(i,10)=0 THEN 'German Shepherd'
                      WHEN MOD(i,10)=1 THEN 'Labrador Retriever'
                      WHEN MOD(i,10)=2 THEN 'Golden Retriever'
                      WHEN MOD(i,10)=3 THEN 'Bulldog'
                      WHEN MOD(i,10)=4 THEN 'Beagle'
                      WHEN MOD(i,10)=5 THEN 'Poodle'
                      WHEN MOD(i,10)=6 THEN 'Rottweiler'
                      WHEN MOD(i,10)=7 THEN 'Yorkshire Terrier'
                      WHEN MOD(i,10)=8 THEN 'Boxer'
                      ELSE 'Dachshund'
                      END;
        v_birthdate := SYSDATE-(ROUND(DBMS_RANDOM.value(0, 20))*INTERVAL '1' YEAR);
        -- Assuming the file is stored on the server's file system
        src_bfile := BFILENAME('MY_DIR', 'picture' || TO_CHAR(ROUND(DBMS_RANDOM.value(1, 3), 0)) || '.png'); -- modify 'MY_DIR' and 'my_file.jpg' accordingly
   
        -- Load the BFILE data into the BLOB
        DBMS_LOB.createtemporary(v_avatar, TRUE);
        DBMS_LOB.fileopen(src_bfile, DBMS_LOB.file_readonly);
        DBMS_LOB.loadfromfile(v_avatar, src_bfile, DBMS_LOB.getlength(src_bfile));
        DBMS_LOB.fileclose(src_bfile);

        v_health_state := CASE 
                              WHEN MOD(i, 6) = 0 THEN 'Vibrant'
                              WHEN MOD(i, 6) = 1 THEN 'Well'
                              WHEN MOD(i, 6) = 2 THEN 'Decent'
                              WHEN MOD(i, 6) = 3 THEN 'Unhealthy'
                              WHEN MOD(i, 6) = 4 THEN 'Sicky'
                              ELSE 'Critical'
                          END; 
        v_vaccine := CASE 
                         WHEN MOD(i, 2) = 0 THEN 'Y'
                         ELSE 'N'
                     END;
        v_read_num := ROUND(DBMS_RANDOM.value(0, 100));
        
        INSERT INTO pet(pet_id, pet_name, breed, birthdate, avatar, health_state, vaccine, read_num) 
        VALUES (v_pet_id, v_pet_name, v_breed, v_birthdate, v_avatar, v_health_state, v_vaccine, v_read_num);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_user_id varchar2(20);
    v_pet_id varchar2(20);
BEGIN
    FOR i IN 1..20 LOOP
    BEGIN
        -- 根据你的要求，user_id和pet_id的值在1到50之间
        v_user_id := ROUND(DBMS_RANDOM.value(1, 50));
        v_pet_id := ROUND(DBMS_RANDOM.value(1, 50));

        INSERT INTO like_pet(user_id, pet_id) 
        VALUES (v_user_id, v_pet_id);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DROP SEQUENCE employee_id_seq;
CREATE SEQUENCE employee_id_seq
  START WITH     1
  INCREMENT BY   1
  NOCACHE
  NOCYCLE;
  
DECLARE 
    v_employee_id varchar2(20);
    v_employee_name varchar(20);
    v_salary numeric(9,2);
    v_phone_number varchar2(20);
    v_duty varchar2(50);
    v_working_start_hr numeric(2,0);
    v_working_end_hr numeric(2,0);
    v_working_start_min numeric(2,0);
    v_working_end_min numeric(2,0);
BEGIN
    FOR i IN 1..15 LOOP
    BEGIN
        v_employee_id := employee_id_seq.NEXTVAL;
        v_employee_name := 'Employee ' || v_employee_id;
        v_salary := ROUND(DBMS_RANDOM.value(10000, 99999), 2);
        v_phone_number := CASE 
    WHEN MOD(i, 3) = 0 THEN TO_CHAR(TRUNC(DBMS_RANDOM.value(100, 999))) || '-' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999))) || '-' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999)))
    WHEN MOD(i, 3) = 1 THEN TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 9))) || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000000000, 9999999999)))
    ELSE TO_CHAR(TRUNC(DBMS_RANDOM.value(100, 999))) || ' ' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999))) || ' ' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999)))
            END;
        v_duty := CASE 
                    WHEN MOD(i,9) = 0 THEN 'Animal Care Specialist'
                    WHEN MOD(i,9) = 1 THEN 'Adoption Counselor'
                    WHEN MOD(i,9) = 2 THEN 'Veterinary Technician'
                    WHEN MOD(i,9) = 3 THEN 'Animal Behaviorist'
                    WHEN MOD(i,9) = 4 THEN 'Volunteer Coordinator'
                    WHEN MOD(i,9) = 5 THEN 'Foster Coordinator'
                    WHEN MOD(i,9) = 6 THEN 'Facility Manager'
                    WHEN MOD(i,9) = 7 THEN 'Fundraising and Outreach Coordinator'
                    ELSE 'Rescue Transporter'
                    END;
        v_working_start_hr := ROUND(DBMS_RANDOM.value(0, 12), 0);
        v_working_start_min := ROUND(DBMS_RANDOM.value(0, 59), 0);
        v_working_end_hr := ROUND(DBMS_RANDOM.value(12, 23), 0);
        v_working_end_min :=ROUND(DBMS_RANDOM.value(0, 59), 0);
        INSERT INTO employee(employee_id, employee_name, salary, phone_number, duty, working_start_hr,working_start_min,working_end_hr, working_end_min) 
        VALUES (v_employee_id, v_employee_name, v_salary, v_phone_number, v_duty, v_working_start_hr, v_working_start_min,v_working_end_hr, v_working_end_min);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
  v_bulletin_id bulletin.bulletin_id%TYPE;
  v_employee_id bulletin.employee_id%TYPE;
  v_heading bulletin.heading%TYPE;
  v_bulletin_contents bulletin.bulletin_contents%TYPE;
  v_read_count bulletin.read_count%TYPE;
  v_published_time bulletin.published_time%TYPE;
BEGIN
  FOR i IN 1..50 LOOP
    v_bulletin_id := 'B' || TO_CHAR(i);
    v_employee_id := TO_CHAR(TRUNC(dbms_random.value(1, 16))); -- Random employee_id from E01 to E15
    v_heading := 'Heading' || TO_CHAR(i);
    v_bulletin_contents := 'Bulletin Content ' || TO_CHAR(i);
    v_read_count := TRUNC(dbms_random.value(0, 1001)); -- Random read count from 0 to 1000
    
    -- Random timestamp generation
    v_published_time := CURRENT_TIMESTAMP + NUMTODSINTERVAL(FLOOR(dbms_random.value(0, (365.25*3))), 'DAY') + NUMTODSINTERVAL(FLOOR(dbms_random.value(0, 24*60*60)), 'SECOND');
    
    INSERT INTO bulletin(bulletin_id, employee_id, heading, bulletin_contents, read_count, published_time)
    VALUES(v_bulletin_id, v_employee_id, v_heading, v_bulletin_contents, v_read_count, v_published_time);
  END LOOP;
  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
END;
/
DROP SEQUENCE post_id_seq;
CREATE SEQUENCE post_id_seq
  START WITH     1
  INCREMENT BY   1
  NOCACHE
  NOCYCLE;
/
DECLARE 
    v_post_id varchar2(20);
    v_user_id varchar2(20);
    v_post_contents varchar2(1000);
    v_read_count int;
BEGIN
    FOR i IN 1..20 LOOP
    BEGIN
        v_post_id := post_id_seq.NEXTVAL;
        v_user_id := ROUND(DBMS_RANDOM.value(0, 49), 0);
        v_post_contents := 'This is post ' || v_post_id;
        v_read_count := ROUND(DBMS_RANDOM.value(0, 1000), 0);
        INSERT INTO forum_posts(post_id, user_id, post_contents, read_count) 
        VALUES (v_post_id, v_user_id, v_post_contents, v_read_count);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_compartment numeric(2,0);
    v_room_status char(1);
    v_room_size numeric(5,2);
    v_storey numeric(2,0);
    room_count_by_storey numeric(2,0);
BEGIN
    FOR i IN 1..10 LOOP
        FOR j IN 1..30 LOOP
        BEGIN
            v_compartment :=j;
            v_room_status := CASE MOD(j,2) WHEN 1 THEN 'Y' ELSE 'N' END;
            v_room_size := ROUND(DBMS_RANDOM.value(10, 100), 2);
            v_storey := i;
            SELECT COUNT(*) INTO room_count_by_storey FROM room WHERE storey = v_storey;
            IF room_count_by_storey < 30 THEN
                INSERT INTO room(compartment, room_status, room_size, storey) 
                VALUES (v_compartment, v_room_status, v_room_size, v_storey);
            END IF;
            EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
            END; 
        END LOOP;
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DROP SEQUENCE vet_id_seq;
CREATE SEQUENCE vet_id_seq
  START WITH     1
  INCREMENT BY   1
  NOCACHE
  NOCYCLE;

DECLARE 
    v_vet_id varchar2(20);
    v_vet_name varchar(20);
    v_salary numeric(9,2);
    v_phone_number varchar2(20);
    v_working_start_hr numeric(2,0);
    v_working_end_hr numeric(2,0);
    v_working_start_min numeric(2,0);
    v_working_end_min numeric(2,0);
BEGIN
    FOR i IN 1..10 LOOP
    BEGIN
        v_vet_id := vet_id_seq.NEXTVAL;
        v_vet_name := 'Vet ' || v_vet_id;
        v_salary := ROUND(DBMS_RANDOM.value(50000, 100000), 2);
        v_phone_number := CASE 
                            WHEN MOD(i, 3) = 0 THEN TO_CHAR(TRUNC(DBMS_RANDOM.value(100, 999))) || '-' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999))) || '-' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999)))
                            WHEN MOD(i, 3) = 1 THEN TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 9))) || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000000000, 9999999999)))
                            ELSE TO_CHAR(TRUNC(DBMS_RANDOM.value(100, 999))) || ' ' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999))) || ' ' || TO_CHAR(TRUNC(DBMS_RANDOM.value(1000, 9999)))
                          END;
        v_working_start_hr := ROUND(DBMS_RANDOM.value(0, 12), 0);
        v_working_start_min := ROUND(DBMS_RANDOM.value(0, 59), 0);
        v_working_end_hr := ROUND(DBMS_RANDOM.value(12, 23), 0);
        v_working_end_min :=ROUND(DBMS_RANDOM.value(0, 59), 0);
        INSERT INTO vet(vet_id, vet_name, salary, phone_number, working_start_hr,working_start_min,working_end_hr, working_end_min) 
        VALUES (v_vet_id, v_vet_name, v_salary, v_phone_number, v_working_start_hr, v_working_start_min,v_working_end_hr, v_working_end_min);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_user_id varchar2(20);
    v_post_id varchar2(20);
BEGIN
    FOR i IN 1..50 LOOP
    BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_post_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 21)));
        INSERT INTO like_post(user_id, post_id) 
        VALUES (v_user_id, v_post_id);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_user_id varchar2(20);
    v_post_id varchar2(20);
    v_comment_contents varchar2(100);
    sample_comments varchar2(150) := 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.';
BEGIN
    FOR i IN 1..20 LOOP
      BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_post_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 21)));
        v_comment_contents := SUBSTR(sample_comments, DBMS_RANDOM.value(1, 50), DBMS_RANDOM.value(1, 50));
        INSERT INTO comment_post(user_id, post_id, comment_contents) 
        VALUES (v_user_id, v_post_id, v_comment_contents);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_user_id varchar2(20);
    v_pet_id varchar2(20);
    v_comment_contents varchar2(200);
    sample_comments varchar2(200) := 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.';
BEGIN
    FOR i IN 1..30 LOOP
      BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_comment_contents := SUBSTR(sample_comments, DBMS_RANDOM.value(1, 50), DBMS_RANDOM.value(1, 50));
        INSERT INTO comment_pet(user_id, pet_id, comment_contents) 
        VALUES (v_user_id, v_pet_id, v_comment_contents);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_user_id varchar2(20);
    v_pet_id varchar2(20);
BEGIN
    FOR i IN 1..30 LOOP
      BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        INSERT INTO collect_pet_info(user_id, pet_id) 
        VALUES (v_user_id, v_pet_id);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_donor_id varchar2(20);
    v_donation_amount int;
BEGIN
    FOR i IN 1..10 LOOP
      BEGIN
        v_donor_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_donation_amount := TRUNC(DBMS_RANDOM.value(1, 5000));
        INSERT INTO donation(donor_id, donation_amount) 
        VALUES (v_donor_id, v_donation_amount);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_pet_id varchar2(20);
    v_user_id varchar2(20);
    v_category varchar2(20);
    v_reason varchar2(200);
BEGIN
    FOR i IN 1..10 LOOP
    BEGIN
        v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_category := CASE 
                        WHEN MOD(i,7) = 0 THEN 'Lost Pet'
                        WHEN MOD(i,7) = 1 THEN 'Treatment'
                        WHEN MOD(i,7) = 2 THEN 'Vaccination'
                        WHEN MOD(i,7) = 3 THEN 'Breeding'
                        WHEN MOD(i,7) = 4 THEN 'Adoption'
                        WHEN MOD(i,7) = 5 THEN 'Donation'
                        ELSE 'Complaint'
                      END;
        v_reason := 'Reason ' || v_pet_id;
        INSERT INTO application(pet_id, user_id, category, reason) 
        VALUES (v_pet_id, v_user_id, v_category, v_reason);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/

DECLARE 
    v_duration smallint;
    v_fosterer varchar2(20);
    v_pet_id varchar2(20);
BEGIN
    FOR i IN 1..10 LOOP
        BEGIN
            v_duration := TRUNC(DBMS_RANDOM.value(1, 100));
            v_fosterer := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
            v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
            INSERT INTO foster(duration, fosterer, pet_id) 
            VALUES (v_duration, v_fosterer, v_pet_id);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
        END;
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/

DECLARE 
    v_user_id varchar2(20);
    v_pet_id varchar2(20);
    v_storey numeric(2,0);
    v_compartment numeric(2,0);
BEGIN
    FOR i IN 1..10 LOOP
      BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_storey := TRUNC(DBMS_RANDOM.value(1, 11));
        v_compartment:=TRUNC(DBMS_RANDOM.value(1,31));
        INSERT INTO accommodate(owner_id,pet_id, storey,compartment) 
        VALUES (v_user_id,v_pet_id, v_storey,v_compartment);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
DECLARE 
    v_adopter_id varchar2(20);
    v_pet_id varchar2(20);
BEGIN
    FOR i IN 1..10 LOOP
    BEGIN
        v_adopter_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        INSERT INTO adopt(adopter_id, pet_id) 
        VALUES (v_adopter_id, v_pet_id);
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                NULL; -- 当唯一性约束违反时，忽略并继续
      END; 
    END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
/
Create or replace view vet_labor 
as select vet_id,vet_name,salary,ROUND((working_end_hr-working_start_hr)+(working_end_min-working_start_min)/60,2) as working_hours 
from vet WITH CHECK OPTION;
Create or replace view employee_labor 
as select employee_id,employee_name,salary,duty,ROUND((working_end_hr-working_start_hr)+(working_end_min-working_start_min)/60,2) as working_hours 
from employee WITH CHECK OPTION;
create or replace view foster_window 
as select user2.user_id,user2.user_name as owner,accommodate.pet_id,pet_name,start_year,duration,
to_char(foster.start_year)||'-'||to_char(foster.start_month)||'-'||to_char(foster.start_day) as foster_start_date,breed,pet.psize,
to_char(accommodate.storey)||'-'||to_char(accommodate.compartment) as room_id,room_size,censor_state
from foster 
join  pet on pet.pet_id=foster.pet_id join  user2 on fosterer=user_id join accommodate on accommodate.owner_id=foster.fosterer and accommodate.pet_id=foster.pet_id 
join room on room.storey=accommodate.storey and
room.compartment=accommodate.compartment;
create or replace 
TRIGGER trg_check_foster_censor_state
AFTER UPDATE ON foster
FOR EACH ROW
BEGIN
    -- 当censor_state由'legitimate'变为'outdated'
    IF :NEW.censor_state = 'outdated' OR :NEW.censor_state = 'aborted' THEN
        -- 删除对应的accommodate表中宠物与用户ID相同的条目
        DELETE FROM accommodate
        WHERE pet_id = :NEW.pet_id AND owner_id = :NEW.fosterer;
    END IF;
END;
/
create or replace 
TRIGGER trg_before_delete_accommodate
BEFORE DELETE ON accommodate
FOR EACH ROW
DECLARE
    v_room_status room.room_status%TYPE;
BEGIN
    -- 在删除accommodate记录之前，获取对应room的room_status
    SELECT room_status
    INTO v_room_status
    FROM room
    WHERE storey = :OLD.storey AND compartment = :OLD.compartment;

    -- 如果对应room的room_status不为'N'，则更新为'N'
    IF v_room_status = 'Y' THEN
        UPDATE room
        SET room_status = 'N'
        WHERE storey = :OLD.storey AND compartment = :OLD.compartment;
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        -- 处理找不到对应room记录的情况，可以根据需要进行处理，比如抛出异常或记录日志。
        NULL;
END;
/
CREATE OR REPLACE PROCEDURE check_foster_duration_proc AS
    foster_start_date DATE;
    foster_end_date DATE;
BEGIN
    FOR foster_rec IN (SELECT * FROM foster WHERE censor_state = 'legitimate') LOOP
        -- 计算寄养开始日期和结束日期
        foster_start_date := TO_DATE(foster_rec.start_year || '-' || foster_rec.start_month || '-' || foster_rec.start_day, 'YYYY-MM-DD');
        foster_end_date := foster_start_date + foster_rec.duration;

        IF foster_end_date < SYSDATE THEN
            -- 当寄养期限超限时
            UPDATE foster SET censor_state = 'outdated';
        END IF;
    END LOOP;
END;
/
BEGIN
  DBMS_SCHEDULER.CREATE_JOB (
    job_name        => 'CHECK_FOSTER_DURATION_JOB',
    job_type        => 'PLSQL_BLOCK',
    job_action      => 'BEGIN check_foster_duration_proc; END;',
    start_date      => TRUNC(SYSDATE) + INTERVAL '1' DAY,
    repeat_interval => 'FREQ=DAILY; BYHOUR=0; BYMINUTE=0; BYSECOND=0;',
    enabled         => TRUE,
    comments        => 'Daily job to check and update foster durations'
  );
END;
/
