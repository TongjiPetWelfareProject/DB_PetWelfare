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
  address varchar2(50),
  CONSTRAINT CHK_PhoneNumber CHECK (REGEXP_LIKE(phone_number, '^\+\d{1,3}-\d{1,3}-\d{1,4}$')),--check if the phone_number is legal
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
  CONSTRAINT CHK_PathValue CHECK (REGEXP_LIKE(avatar, '^\/[a-zA-Z0-9_\-\/]*$')),
  CONSTRAINT CHK_HealthState CHECK(health_state in('Vibrant','Well','Decent','Unhealthy','Sicky','Critical')),
  CONSTRAINT CHK_Num CHECK(read_num>=0 AND like_num>=0 AND collect_num>=0 AND vaccine in('Y','N'))
);

--table room
create table room(
  room_id varchar2(5) not null,
  room_status char(1),--'Y'/'N'
  room_size numeric(5,2),--since area may lead to ambiguity
  storey numeric(2,0),--since floor may lead to ambiguity
  cleaning_time  TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(room_id),
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
  working_start_hr between 0 and 23 and working_end_hr between 0 and 23 
  and working_end_hr>=working_start_hr and working_start_min>working_end_min
  and working_start_min between 0 and 59 and working_end_min between 0 and 59 ),
  CONSTRAINT CHK_PhoneNumber2 CHECK (REGEXP_LIKE(phone_number, '^\+\d{1,3}-\d{1,3}-\d{1,4}$'))
);
create table employee(
  employee_id varchar2(20) not null,
  employee_name varchar(20),
  salary numeric(9,2),
  phone_number varchar2(20),
  duty varchar2(50),
  --the following attributes as an integrity represents the interval of working hours
  working_start_hr numeric(2,0),
  working_end_hr numeric(2,0),
  working_start_min numeric(2,0),
  working_end_min numeric(2,0),
  primary key(employee_id),
  CONSTRAINT CHK_WorkingHours2 CHECK(
  working_start_hr between 0 and 23 and working_end_hr between 0 and 23 
  and working_end_hr>=working_start_hr and working_start_min>working_end_min
  and working_start_min between 0 and 59 and working_end_min between 0 and 59 ),
  CONSTRAINT CHK_PhoneNumber3 CHECK (REGEXP_LIKE(phone_number, '^\+\d{1,3}-\d{1,3}-\d{1,4}$')),
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
  published_time TIMESTAMP default CURRENT_TIMESTAMP,
  read_count int,
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
  post_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(post_id),
  constraint CHK_LEGAL2 check(read_count>=0 and like_num>=0 and comment_num>=0
  and read_count>= like_num and like_num>=comment_num)
);
--potential primary key(donor_id,donation_time,donation_amounts)
create table donation(
  donation_time TIMESTAMP default CURRENT_TIMESTAMP,
  donor_id varchar(20) references user2(user_id),
  donation_amount int,
  constraints CHK_DONATION check(donation_amount>0),
  primary key(donor_id,donation_amount,donation_time)
);
create table adopt(
  adoption_time TIMESTAMP default CURRENT_TIMESTAMP,
  adopter_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  primary key(adopter_id,pet_id)
);
create table foster(
  foster_time TIMESTAMP default CURRENT_TIMESTAMP,
  duration smallint,
  fosterer varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  primary key(foster_time,fosterer,pet_id),
  constraint CHK_Duration check(duration>=0)
);
create table accommodate(
  pet_id varchar2(20) references pet(pet_id),
  room_id varchar2(5) references room(room_id),
  accommodate_time TIMESTAMP default CURRENT_TIMESTAMP,
  duration smallint,--must be standalone since in SQL, a column cannot reference another column in a different table. Instead, it references the primary key in the other table.
  primary key(pet_id,room_id,accommodate_time),
  constraint CHK_Duration2 check(duration>=0)
);
--Because treat is ambiguious and treatment is usually referred in medication/surgery
create table treatment(
  treat_time TIMESTAMP default CURRENT_TIMESTAMP,
  category varchar2(20),
  pet_id varchar2(20) references pet(pet_id),
  vet_id varchar2(20) references vet(vet_id),
  primary key(treat_time,pet_id,vet_id)
);
create table application(
  pet_id varchar2(20) references pet(pet_id),
  user_id varchar2(20) references user2(user_id),
  category varchar2(20),
  reason varchar2(200) not null,
  apply_time TIMESTAMP default CURRENT_TIMESTAMP,
  primary key(pet_id,user_id,apply_time)
);
create table like_post(
  like_time TIMESTAMP default CURRENT_TIMESTAMP,
  user_id varchar2(20) references user2(user_id),
  post_id varchar2(20) references forum_posts(post_id),
  primary key(like_time,user_id,post_id)
);
create table comment_post(
  comment_time TIMESTAMP default CURRENT_TIMESTAMP,
  user_id varchar2(20) references user2(user_id),
  post_id varchar2(20) references forum_posts(post_id),
  comment_contents varchar2(100) not null,
  primary key(comment_time,user_id,post_id)
);
create table like_pet(
  like_time TIMESTAMP default CURRENT_TIMESTAMP,
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  primary key(like_time,user_id,pet_id)
);
create table comment_pet(
  comment_time TIMESTAMP default CURRENT_TIMESTAMP,
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  comment_contents varchar2(200) not null,
  primary key(comment_time,user_id,pet_id)
);
create table collect_pet_info(
  collect_time TIMESTAMP default CURRENT_TIMESTAMP,
  user_id varchar2(20) references user2(user_id),
  pet_id varchar2(20) references pet(pet_id),
  primary key(collect_time,user_id,pet_id)
);
