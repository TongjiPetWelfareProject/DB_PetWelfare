# 关于数据库创建的一些问题
## 配置
  首先，按照学院服务器上的方法创建一个用户名为sys的连接，并且以DBA的身份登录。
  接着，你需要找出你的可插入数据库(PDB)的名称。你可以使用以下的SQL查询来查看所有的PDB：
```sql
SELECT name FROM v$pdbs;
```
在连接PDB之前，我们需要检查PDB的状态
```sql
ALTER PLUGGABLE DATABASE pdborcl OPEN;
```
然后，你可以使用以下的SQL命令连接到你的PDB）,

注意我们只有在CDB下才能够创建用户（PDB下是不能的）
```sql
ALTER SESSION SET CONTAINER = cdb$root;
CREATE USER c##pet IDENTIFIED BY campus;
```
***在Oracle数据库中，用户名以"C##"开头的用户是被称为"Containerized"用户的特殊类型。这种用户类型是为了支持多租户架构（Multitenant Architecture）而引入的。***

多租户架构是Oracle 12c及更高版本中引入的一种架构，它允许在单个数据库实例中创建多个独立的数据库（被称为可插入数据库或PDB）。每个PDB都是一个独立的数据库，具有自己的表空间、用户和对象。

"C##"开头的用户名被用于创建全局共享的用户。这些用户在所有的PDB中都是可见的，并且可以跨PDB访问和管理数据库对象。这种类型的用户可以被用于在多个PDB之间共享数据和共享数据库管理任务。

***需要注意的是，创建和管理"C##"开头的用户需要DBA（数据库管理员）权限。***
创建完用户之后，需要授予用户权限：
```sql
-- 给新用户授权
GRANT CONNECT, RESOURCE TO c##pet;
GRANT CREATE TABLE TO c##pet;
GRANT CREATE SESSION TO c##pet;
GRANT CREATE VIEW TO c##pet;
GRANT CREATE TRIGGER TO c##pet;
GRANT UNLIMITED TABLESPACE TO c##pet;-- 为用户在所有表空间上赋予无限制的配额
GRANT CREATE SEQUENCE TO c##pet;
GRANT CREATE MATERIALIZED VIEW TO c##pet;
CREATE OR REPLACE DIRECTORY my_dir AS 'D:/Desktop';--将D:/Desktop转化为存储图片的路径
GRANT READ, WRITE ON DIRECTORY my_dir TO c##pet;
GRANT CREATE PROCEDURE TO c##pet;
GRANT CREATE FUNCTION TO c##pet;
--以下四行解决了调度程序无法运行的错误
GRANT CREATE JOB TO c##pet;
GRANT CREATE EXTERNAL JOB TO C##pet;
GRANT EXECUTE ON DBMS_SCHEDULER TO C##pet;
GRANT MANAGE SCHEDULER TO C##pet;
```
接下来我们创建几个表空间：
- PETS--默认存储
- SOCIAL--社交媒体信息存储（由于需要高I/O，因此存到更高性能的硬盘上）
- ARCHIVE--存档数据(存到性能较差的磁盘上)
下面是创建表空间的一般语句，地址需要自己指定
```sql
CREATE TABLESPACE mytablespace
    DATAFILE 'path_to_your_directory/mytablespace.dbf' SIZE 50M 
    AUTOEXTEND ON 
    NEXT 250K 
    MAXSIZE UNLIMITED;
```
**注意，上述代码只是一个模板！！！**
具体来说就是你需要先在F盘或者任意一个盘下新建一个Tablespace文件夹即可，里面的dbf会自动生成。
```sql
CREATE TABLESPACE pets
    DATAFILE 'F:/Tablespace/pets.dbf' SIZE 50M 
    AUTOEXTEND ON 
    NEXT 250K 
    MAXSIZE UNLIMITED;
CREATE TABLESPACE social
    DATAFILE 'F:/Tablespace/social.dbf' SIZE 50M 
    AUTOEXTEND ON 
    NEXT 250K 
    MAXSIZE UNLIMITED;
CREATE TABLESPACE archive
    DATAFILE 'F:/Tablespace/archive.dbf' SIZE 50M 
    AUTOEXTEND ON 
    NEXT 250K 
    MAXSIZE UNLIMITED;
```
修改默认表空间为pet
```sql
ALTER DATABASE DEFAULT TABLESPACE pet;
```
-- 连接到新用户
CONNECT c##pet/************;
在连接成功后会立即断开，这是因为在SQL Developer中是无法切换用户的，只能在SQL Plus中切换，因此我们选择再次创建一个数据库连接，用户名为c##pet
在左侧视窗中的其他用户右键，按模式C##pet过滤用户，此时只可以看到当前用户的数据，系统用户的数据被过滤了。
然后我们导入SQL文档petrescue.sql，创建表格。
如果手动创建，那么需要切换模式到C##pet，但是由于默认模式即与用户名重名，因此忽略以下步骤：
```sql
ALTER SESSION SET CURRENT_SCHEMA = C##pet;
```
使用以下的查询可以查看当前模式下的表格：
```
SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER='C##pet';
```
注意到有一些特殊字段是不能作为表格名的，例如user,time，因此将user后加2.

以上方式为本地配置，远程配置需要配置服务器的listener.ora，设置监听字段。
