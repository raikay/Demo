数据库自动备份服务，带配置，还算可以吧

周末抽时间，编写了一个这样的工具，可以让，对数据库不了解或不熟悉的人，直接学会使用备份，省时省力，同样，我也将一份，通过脚本进行备份的，也奉献上来，

#  通过sql脚本进行数据库备份

通过脚本备份数据库，同样也支持压缩，但是需要安装winrar来实现，整体来说也还行，在服务器上创建一个 维护计划，就可以实现，也是很方便的，脚本如下：
```sql
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
RECONFIGURE;
declare @prefix         nvarchar(100),
        @datefile       nvarchar(100),
        @bakfile        nvarchar(100),
        @rarfile        nvarchar(100),
        @rarcmd         nvarchar(150),
        @str_date       nvarchar(100),
        @sql            nvarchar(100)
        
--设置备份的目录      
set @prefix='D:/DataBase/' 
set @str_date = replace(replace(replace(convert(varchar(20),getdate(), 120),' ',''),'-',''),':','')
set @datefile = 'xx' +@str_date
set @bakfile = @prefix+@datefile+'.bak'
set @rarfile = @prefix+@datefile+'.rar'
--备份
BACKUP Database mpe_db_Data TO DISK = @bakfile WITH NOFORMAT, NOINIT,  NAME = N'xx-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
--压缩rar
set @rarcmd ='"c:\Program Files\WinRAR\winrar.exe" ' +'a -df ' +@rarfile+' '+@bakfile
exec master..xp_cmdshell @rarcmd,NO_OUTPUT;
```

别问我代码都是干啥的，无非就是打开权限，创建变量、时间戳的文件名、备份脚本、启动备份，哈哈。。都说完了，你也不用问了，
**你是不是要问，那删除文件呢？**
```sql
--删除15天之前的备份
set  @sql='del  d:\DataBase\xx' +rtrim(replace(replace(replace(convert(varchar(20),getdate()-15, 120),' ',''),'-',''),':',''))+'.rar'
```
为啥删除15天的？你想删除多少天，自己写， -15 的15，随你填写。

好了，言归正传，下面是我编写的windows 服务实现，请看：

### 使用方法如下:

#### 1. 使用管理员，打开部署脚本

![img](https://images2018.cnblogs.com/blog/108827/201808/108827-20180806092244726-570547640.png)

#### 2. 根据指示进行配置操作，输入1 是进入配置
 ![img](https://images2018.cnblogs.com/blog/108827/201808/108827-20180806092329273-423098219.png)

#### 3. 配置界面
  ![img](https://images2018.cnblogs.com/blog/108827/201808/108827-20180806092537133-1099426309.png)

#### 4. 安装完成后，启动服务

![img](https://images2018.cnblogs.com/blog/108827/201808/108827-20180806092607253-1199485268.png)

**代码遇到一个小小的bug，当备份数据库巨大时，有的服务器会出现超时现象，我将SqlCommand的 CommandTimeout值设置为3600秒了，问题解决，因为数据大小是32个G，收缩日志之后，所以出现了这个问题，以解决，各位自行修改代码解决即可，代码如下：**

![img](https://images2018.cnblogs.com/blog/108827/201808/108827-20180810154035550-1080053033.png)

实现代码如下：

```
 public static int ExecuteSqlSetTimeOut(string cmdText, int timeOut)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
            cmd.CommandTimeout = timeOut;
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
```

鸣谢：

https://www.cnblogs.com/bq-blog/p/9428610.html