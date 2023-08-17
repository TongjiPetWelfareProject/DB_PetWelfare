drop table collect_pet_info ;
drop table comment_pet;
drop table like_pet;
drop table comment_post;
drop table like_post;
drop table post_images;
drop table appointment;
drop table treatment;
drop table accommodate;
drop table foster;
drop table adopt;
drop table adopt_apply;
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
  password varchar2(64) default '1d707811988069ca760826861d6d63a10e8c3b7f171c4441a6472ea58c11711b',--adopt SHA256 Alg,the default password is'Password1!' abide by the same regex
  phone_number varchar2(20) unique,--it always comes true that a phone number will contain some special character dash or space ,so its length is variable
  account_status varchar2(20),
  address varchar2(100),
  role varchar2(10) default 'User',
  CONSTRAINT CHK_Role CHECK(role in('Admin','User','Unknown','Other')),
  CONSTRAINT CHK_PhoneNumber CHECK (REGEXP_LIKE (phone_number, '^\d{3}-\d{4}-\d{4}$') OR REGEXP_LIKE (phone_number, '^\d{11}$') OR REGEXP_LIKE (phone_number, '^\d{3} \d{4} \d{4}$')),--check if the phone_number is legal
    primary key(user_id),
    check(account_status in('Compliant','In Good Standing','Under Review','Warning Issued','Suspended','Probation','Banned','Appealing'))
);
--table pet
create table pet(
  pet_id varchar2(20) not null,
  pet_name varchar2(20),
  species varchar2(20) default 'dog' check(species in('bird','dog','snake','cat','parrot','bear','goldfish')),
  sex varchar2(1) default 'M' check( sex in('M','F')),
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
  heading varchar2(50),
  post_contents varchar2(1000) not null,
  read_count int default 0,
  like_num int default 0,
  comment_num int default 0,
  censored CHAR(1) default 'N' CHECK(censored in('Y','N')),
  post_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(post_id),
  constraint CHK_LEGAL2 check(read_count>=0 and like_num>=0 and comment_num>=0
  and read_count>= like_num and read_count>=comment_num)
)partition by range(post_time)interval(interval '1' year)
(partition start_post values less than(TIMESTAMP '2023-09-01 00:00:00'));
--potential primary key(donor_id,donation_time,donation_amounts)
create table donation(
  donor_id varchar(20) references user2(user_id),
  donation_amount decimal(10,2),
  donation_time TIMESTAMP default CURRENT_TIMESTAMP,
  censor_state varchar2(20) default 'to be censored' check( censor_state in('to be censored','aborted','legitimate','outdated','invalid')),
  constraints CHK_DONATION check(donation_amount>0),
  primary key(donor_id,donation_amount,donation_time)
)partition by range(donation_time)interval(interval '1' year)
(partition start_donate values less than(TIMESTAMP '2023-09-01 00:00:00'));
CREATE TABLE adopt_apply (
    adopter_id VARCHAR2(20)  REFERENCES user2(user_id),
    adopter_gender VARCHAR2(1) CHECK (adopter_gender IN ('M', 'F')),
    apply_date date default CURRENT_DATE, -- 领养时间（年月日）
    pet_id varchar2(20) references pet(pet_id),
    pet_experience VARCHAR2(1) CHECK (pet_experience IN ('Y', 'N')), 
    long_term_care VARCHAR2(1) CHECK (long_term_care IN ('Y', 'N')), 
    willing_to_treat VARCHAR2(1) CHECK (willing_to_treat IN ('Y', 'N')), 
    daily_care_hours NUMERIC(2,0) CHECK (daily_care_hours between 0 and 24), 
    primary_caregiver VARCHAR2(50),
    family_population NUMERIC(2,0), 
    has_children VARCHAR2(1) CHECK (has_children IN ('Y', 'N')), 
    accept_visits VARCHAR2(1) CHECK (accept_visits IN ('Y', 'N')),
    censor_state varchar2(20) default 'to be censored' check( censor_state in('to be censored','aborted','legitimate','outdated','invalid')),
    PRIMARY KEY(adopter_id)
);
create table adopt (
  adopter_id varchar2(20),
  pet_id varchar2(20),
  adoption_time date default current_date,
  primary key (adopter_id, pet_id),
  foreign key (adopter_id, pet_id) references adopt_apply(adopter_id, pet_id)
)
partition by range (adoption_time) interval (interval '1' year) (
  partition start_donate values less than (timestamp '2023-09-01 00:00:00')
);
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
create table appointment(
  pet_id varchar2(20) references pet(pet_id),
  user_id varchar2(20) references user2(user_id),
  species varchar2(20),
  reason varchar2(200) not null,
  custom_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(pet_id,user_id,apply_time)
)partition by range(apply_time)interval(interval '1' year)
(partition start_apply values less than(TIMESTAMP '2023-09-01 00:00:00'));
CREATE TABLE post_images (
  image_id INT,
  post_id varchar2(50),
  image_data BLOB,
  FOREIGN KEY (post_id) REFERENCES forum_posts(post_id)
);
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
--建立图片image_id的序列
DROP SEQUENCE img_id_seq;
CREATE SEQUENCE img_id_seq START WITH 1 INCREMENT BY 1;
DROP SEQUENCE employee_id_seq;
CREATE SEQUENCE employee_id_seq
  START WITH     1
  INCREMENT BY   1
  NOCACHE
  NOCYCLE;
DROP SEQUENCE post_id_seq;
CREATE SEQUENCE post_id_seq
  START WITH     1
  INCREMENT BY   1
  NOCACHE
  NOCYCLE;
/
Create or replace view vet_labor 
as select vet_id,vet_name,salary,ROUND((working_end_hr-working_start_hr)+(working_end_min-working_start_min)/60,2) as working_hours 
from vet WITH CHECK OPTION;
Create or replace view employee_labor 
as select employee_id,employee_name,salary,duty,ROUND((working_end_hr-working_start_hr)+(working_end_min-working_start_min)/60,2) as working_hours 
from employee WITH CHECK OPTION;
create or replace view foster_window 
as select user2.user_id,user2.user_name as owner,accommodate.pet_id,pet_name,species,pet.psize,duration,
to_char(foster.start_year)||'-'||to_char(foster.start_month)||'-'||to_char(foster.start_day) as foster_start_date,
TO_CHAR(
        TO_DATE(foster.start_year || '-' || foster.start_month || '-' || foster.start_day, 'YYYY-MM-DD') + duration,
        'YYYY-MM-DD'
    ) AS foster_end_date,
to_char(accommodate.storey)||'-'||to_char(accommodate.compartment) as room_id,room_size,censor_state
from foster 
join  pet on pet.pet_id=foster.pet_id join  user2 on fosterer=user_id join accommodate on accommodate.owner_id=foster.fosterer and accommodate.pet_id=foster.pet_id 
join room on room.storey=accommodate.storey and
room.compartment=accommodate.compartment;
create or replace view verbosepost as 
select forum_posts.post_id,forum_posts.user_id as poster,heading,read_count,comment_num,like_num,comment_post.user_id 
as commenter,comment_post.comment_contents,forum_posts.post_contents,post_images.image_data,comment_post.comment_time 
from forum_posts
join post_images on post_images.post_id=forum_posts.post_id join comment_post on forum_posts.post_id = comment_post.post_id;
create or replace view user_profile as select user2.user_id,coalesce(sum(forum_posts.like_num),0) as total_like,coalesce(sum(forum_posts.comment_num),0) as total_comment, 
coalesce(sum(forum_posts.read_count),0) as clicks
from user2 left outer join forum_posts on forum_posts.user_id=user2.user_id 
group by user2.user_id order by total_like*10+total_comment*20+clicks desc;
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
  -- Drop the job
  DBMS_SCHEDULER.DROP_JOB(
    job_name        => 'CHECK_FOSTER_DURATION_JOB',
    force           => FALSE,
    commit_semantics => 'TRANSACTIONAL'
  );
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
CREATE OR REPLACE FUNCTION comment_num_func(pid IN VARCHAR2) RETURN NUMBER IS
    comment_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO comment_count
    FROM comment_pet
    WHERE pet_id = pid;

    RETURN comment_count;
END;
/
create or replace view  pet_profile as SELECT
   p.read_num * 1 + p.like_num * 2 + comment_num_func(p.pet_id) * 5 + p.collect_num as popularity, p.*,
    comment_num_func(p.pet_id) as comment_num,comment_pet.comment_contents,comment_pet.comment_time
    
FROM
    pet p left outer join comment_pet  on comment_pet.pet_id=p.pet_id order by popularity desc;
create or replace view adopt_view as SELECT 
    apply_date,pet_id,
    adopter_id,
     '该' || adopter_gender || '性用户想要' || 
     '领养一只宠物'|| '-养宠经验:' || pet_experience ||
    '-长期照顾:' || long_term_care || '-愿意治疗:' || willing_to_treat || '-每日照顾小时:' || 
    TO_CHAR(daily_care_hours) || '-主要照顾者:' || primary_caregiver || '-家庭人口:' || 
    TO_CHAR(family_population) || '-有孩子:' || has_children || '-接受访问:' || accept_visits  
     AS reason
FROM 
    adopt_apply where censor_state='to be censored';
