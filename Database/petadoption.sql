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
  age numeric(2,0),
  avatar varchar2(100),
  health_state varchar2(15),
  vaccine char(1),--Y represents vaccination done while N represents undone.
  read_num int,
  like_num int,
  collect_num int,
  primary key(pet_id),
  CONSTRAINT CHK_PathValue CHECK (REGEXP_LIKE (avatar, '^[A-Z]:\\([a-zA-Z0-9\s_\-]+\\)*([a-zA-Z0-9\s_\-]+\.png|[a-zA-Z0-9\s_\-]+\.jpg|[a-zA-Z0-9\s_\-]+\.jpeg)?$')),
  CONSTRAINT CHK_HealthState CHECK(health_state in('Vibrant','Well','Decent','Unhealthy','Sicky','Critical')),
  CONSTRAINT CHK_Num CHECK(read_num>=0 AND like_num>=0 AND collect_num>=0 AND vaccine in('Y','N'))
);
--create table time2(
--  time_id varchar2(20) not null,
--  year numeric(4,0),
--  month numeric(2,0),
--  day numeric(2,0),
--  hour numeric(2,0),
--  minute numeric(2,0),
--  second numeric(2,0),
--  millisecond numeric(3,0),
--  primary key(time_id),
--  constraint CHK_TIME check((month between 1 and 12) and
--  (day between 1 and 31) and
--  (hour between 0 and 23)and
--  (minute between 0 and 59)and
--  (second between 0 and 59))
--);

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
  published_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(bulletin_id)
);
--potential primary key(user_id,post_contents,post_time)
create table forum_posts(
  post_id varchar2(20) not null,
  user_id varchar2(20) references user2(user_id),
  post_contents varchar2(1000) not null,
  read_count int,
  like_num int,
  comment_num int,
  post_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(post_id),
  constraint CHK_LEGAL2 check(read_count>=0 and like_num>=0 and comment_num>=0
  and read_count>= like_num and like_num>=comment_num)
);
--potential primary key(donor_id,donation_time,donation_amounts)
create table donation(
  donor_id varchar(20) references user2(user_id),
  donation_amount int,
  donation_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  constraints CHK_DONATION check(donation_amount>0),
  primary key(donor_id,donation_amount,donation_time)
);
create table adopt(
  adopter_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  adoption_time varchar2(50) default TO_CHAR(CURRENT_DATE, 'YYYY-MM-DD'),
  primary key(adopter_id,pet_id)
);
create table foster(
  duration smallint,
  fosterer varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  start_year numeric(4,0) default extract(year from CURRENT_DATE),
  start_month numeric(2,0) default extract(month from CURRENT_DATE),
  start_day numeric(2,0) default extract(day from CURRENT_DATE),
  primary key(start_year,start_month,start_day,fosterer,pet_id),
  constraint CHK_Duration check(duration>=0)
);
create table accommodate(
  pet_id varchar2(20) references pet(pet_id),
  storey numeric(2,0) ,
  compartment numeric(2,0) ,
  duration smallint,--must be standalone since in SQL, a column cannot reference another column in a different table. Instead, it references the primary key in the other table.
  start_year numeric(4,0) default extract(year from CURRENT_DATE),
  start_month numeric(2,0) default extract(month from CURRENT_DATE),
  start_day numeric(2,0) default extract(day from CURRENT_DATE),
  primary key(pet_id,storey,compartment,start_year,start_month,start_day),
  foreign key(storey,compartment) references room,
  constraint CHK_Duration2 check(duration>=0)
);
--Because treat is ambiguious and treatment is usually referred in medication/surgery
create table treatment(
  category varchar2(20),
  pet_id varchar2(20) references pet(pet_id),
  vet_id varchar2(20) references vet(vet_id),
  treat_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(treat_time,pet_id,vet_id)
);
create table application(
  pet_id varchar2(20) references pet(pet_id),
  user_id varchar2(20) references user2(user_id),
  category varchar2(20),
  reason varchar2(200) not null,
  apply_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(pet_id,user_id,apply_time)
);
create table like_post(
  user_id varchar2(20) references user2(user_id),
  post_id varchar2(20) references forum_posts(post_id),
  like_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(like_time,user_id,post_id)
);
create table comment_post(
  user_id varchar2(20) references user2(user_id),
  post_id varchar2(20) references forum_posts(post_id),
  comment_contents varchar2(150) not null,
  comment_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(comment_time,user_id,post_id)
);
create table like_pet(
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  like_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(like_time,user_id,pet_id)
);
create table comment_pet(
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  comment_contents varchar2(200) not null,
  comment_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(comment_time,user_id,pet_id)
);
create table collect_pet_info(
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  collect_time varchar2(50) default TO_CHAR(CURRENT_TIMESTAMP),
  primary key(collect_time,user_id,pet_id)
);
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
    v_age numeric(2,0);
    v_avatar varchar2(100);
    v_health_state varchar2(15);
    v_vaccine char(1);
    v_read_num int;
    v_like_num int;
    v_collect_num int;
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
        v_age := ROUND(DBMS_RANDOM.value(0, 20));
        v_avatar := 'C:\RandomPath\' || DBMS_RANDOM.string('A', 10) || '.png'; 
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
        v_like_num := ROUND(DBMS_RANDOM.value(0, v_read_num));
        v_collect_num := ROUND(DBMS_RANDOM.value(0, v_like_num));
        
        INSERT INTO pet(pet_id, pet_name, breed, age, avatar, health_state, vaccine, read_num, like_num, collect_num) 
        VALUES (v_pet_id, v_pet_name, v_breed, v_age, v_avatar, v_health_state, v_vaccine, v_read_num, v_like_num, v_collect_num);
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
    v_like_time varchar2(50);
BEGIN
    FOR i IN 1..20 LOOP
    BEGIN
        -- 根据你的要求，user_id和pet_id的值在1到50之间
        v_user_id := ROUND(DBMS_RANDOM.value(1, 50));
        v_pet_id := ROUND(DBMS_RANDOM.value(1, 50));

        -- 获取当前的时间
        v_like_time := TO_CHAR(CURRENT_TIMESTAMP);

        INSERT INTO like_pet(user_id, pet_id, like_time) 
        VALUES (v_user_id, v_pet_id, v_like_time);
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
    v_like_num int;
    v_comment_num int;
BEGIN
    FOR i IN 1..5 LOOP
    BEGIN
        v_post_id := post_id_seq.NEXTVAL;
        v_user_id := ROUND(DBMS_RANDOM.value(0, 49), 0);
        v_post_contents := 'This is post ' || v_post_id;
        v_read_count := ROUND(DBMS_RANDOM.value(0, 1000), 0);
        v_like_num := ROUND(DBMS_RANDOM.value(0, v_read_count), 0);
        v_comment_num := ROUND(DBMS_RANDOM.value(0, v_like_num), 0);
        INSERT INTO forum_posts(post_id, user_id, post_contents, read_count, like_num, comment_num) 
        VALUES (v_post_id, v_user_id, v_post_contents, v_read_count, v_like_num, v_comment_num);
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
    FOR i IN 1..10 LOOP
    BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_post_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 6)));
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
    FOR i IN 1..5 LOOP
      BEGIN
        v_user_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_post_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 6)));
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
    v_duration smallint;
    v_pet_id varchar2(20);
    v_storey numeric(2,0);
    v_compartment numeric(2,0);
BEGIN
    FOR i IN 1..10 LOOP
      BEGIN
        v_duration := TRUNC(DBMS_RANDOM.value(1, 100));
        v_pet_id := TO_CHAR(TRUNC(DBMS_RANDOM.value(1, 51)));
        v_storey := TRUNC(DBMS_RANDOM.value(1, 11));
        v_compartment:=TRUNC(DBMS_RANDOM.value(1,31));
        INSERT INTO accommodate(duration, pet_id, storey,compartment) 
        VALUES (v_duration, v_pet_id, v_storey,v_compartment);
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
Create or replace view vet_labor as select vet_id,vet_name,salary,ROUND((working_end_hr-working_start_hr)+(working_end_min-working_start_min)/60,2) as working_hours from vet WITH CHECK OPTION;
Create or replace view employee_labor as select employee_id,employee_name,salary,duty,ROUND((working_end_hr-working_start_hr)+(working_end_min-working_start_min)/60,2) as working_hours from employee WITH CHECK OPTION;
DROP materialized　VIEW ownership;
CREATE MATERIALIZED VIEW ownership 
BUILD IMMEDIATE
  REFRESH FORCE
  ON DEMAND
AS SELECT 
    pet.pet_id,
    pet.pet_name,
    CASE
        WHEN accommodate.storey IS NOT NULL and accommodate.compartment IS NOT NULL 
        THEN 'accommodated at room '||accommodate.storey||'-'||accommodate.compartment
        WHEN foster.fosterer IS NOT NULL 
        THEN 'fostered by '||foster.fosterer 
        WHEN adopt.adopter_id IS NOT NULL 
        THEN 'adopted by '||adopt.adopter_id  
        ELSE 'wandered'
    END AS status,
    CASE
        WHEN accommodate.storey IS NOT NULL and accommodate.compartment IS NOT NULL 
        THEN TO_CHAR(TO_DATE(
      accommodate.start_year || '-' || accommodate.start_month || '-' || accommodate.start_day, 'YYYY-MM-DD'),'YYYY-MM-DD')
        WHEN foster.fosterer IS NOT NULL 
        THEN TO_CHAR(TO_DATE(
      foster.start_year || '-' || foster.start_month || '-' || foster.start_day, 'YYYY-MM-DD'),'YYYY-MM-DD')
        WHEN adopt.adopter_id IS NOT NULL 
        THEN adopt.adoption_time
    END AS start_time,
    CASE
        WHEN accommodate.storey IS NOT NULL and accommodate.compartment IS NOT NULL 
        THEN TO_CHAR(TO_DATE(
      accommodate.start_year || '-' || accommodate.start_month || '-' || accommodate.start_day, 
      'YYYY-MM-DD') + NUMTODSINTERVAL(accommodate.duration, 'DAY'), 'YYYY-MM-DD')
        WHEN foster.fosterer IS NOT NULL 
        THEN TO_CHAR(TO_DATE(
      foster.start_year || '-' || foster.start_month || '-' || foster.start_day, 
      'YYYY-MM-DD') + NUMTODSINTERVAL(foster.duration, 'DAY'), 'YYYY-MM-DD')
    END AS end_time
FROM pet
LEFT OUTER JOIN foster ON pet.pet_id = foster.pet_id
LEFT OUTER JOIN accommodate ON accommodate.pet_id = pet.pet_id
LEFT OUTER JOIN adopt ON adopt.pet_id = pet.pet_id
LEFT OUTER JOIN user2 ON user2.user_id = foster.fosterer and adopt.adopter_id= user2.user_id; 
CREATE OR REPLACE VIEW user_profile AS 
SELECT user2.user_id,user2.user_name,
       COUNT(collect_pet_info.collect_time) AS pet_collections,
       COUNT(like_pet.like_time) AS pet_likes,
       COUNT(comment_pet.comment_time) AS pet_comments,
       COUNT(forum_posts.post_time) as total_posts,
       COUNT(like_post.like_time) AS post_likes,
       COUNT(comment_post.comment_time) AS post_comments,
       SUM(donation.donation_amount) AS donation_totol_amounts
FROM user2
LEFT OUTER JOIN collect_pet_info ON user2.user_id = collect_pet_info.user_id
LEFT OUTER JOIN comment_pet ON user2.user_id = comment_pet.user_id
LEFT OUTER JOIN donation ON user2.user_id = donation.donor_id
LEFT OUTER JOIN like_pet ON user2.user_id = like_pet.user_id
LEFT OUTER JOIN forum_posts ON user2.user_id = forum_posts.user_id
LEFT OUTER JOIN comment_post ON forum_posts.post_id = comment_post.post_id
LEFT OUTER JOIN like_post ON forum_posts.post_id = like_post.post_id 
GROUP BY user2.user_id,user2.user_name order by cast(user2.user_id as numeric(5,0)) asc WITH CHECK OPTION; 
CREATE OR REPLACE VIEW pet_profile AS 
SELECT pet.pet_id,pet.pet_name,
       COUNT(collect_pet_info.collect_time) AS collections,
       COUNT(like_pet.like_time) AS likes,
       COUNT(comment_pet.comment_time) AS comments
FROM pet
LEFT OUTER JOIN collect_pet_info ON pet.pet_id = collect_pet_info.pet_id
LEFT OUTER JOIN comment_pet ON pet.pet_id = comment_pet.pet_id
LEFT OUTER JOIN like_pet ON pet.pet_id = like_pet.pet_id
GROUP BY pet.pet_id,pet.pet_name order by cast(pet.pet_id as numeric(5,0)) asc WITH CHECK OPTION;
create or replace view room_avaiable as select room.storey,(count(*))as capacity from room where  not exists(select * from accommodate where room.storey=accommodate.storey and room.compartment=accommodate.compartment ) group by room.storey WITH CHECK OPTION;
