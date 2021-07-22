
### 添加引用
```
Microsoft.EntityFrameworkCore.Design
Pomelo.EntityFrameworkCore.MySql
```
### 1 添加数据库上下文

```
public class BloggingContext : DbContext
...
...
```
### 修改startup文件

1> 
```
services.AddDbContext<BloggingContext>();
```

2>

```
new DbInitializer().InitializeAsync(context);
```




注意：以下命令的工作目录是项目根目录，非解决方案根目录，在程序包管理器控制台执行以下命令会报错。
# 数据库迁移

## 准备

安装EFCore命令行工具：

```bash
dotnet tool install --global dotnet-ef
```

## 生成迁移文件

```bash
dotnet ef migrations add <NAME>
```

<NAME>为生成的迁移文件名称，根据实际情况命名。

## 撤销迁移

```bash
dotnet ef migrations remove
```

## 生成迁移Sql

```bash
dotnet ef migrations script <FROM> <TO>
```

<FROM>：生成SQL范围的起始迁移文件，默认为'0'。
<TO>：生成SQL范围的终止迁移文件，默认为最近的迁移文件。

## 将迁移同步到数据库

```bash
dotnet ef database update <MIGRATION>
```

<MIGRATION>: 要更新的迁移文件，默认为最近的迁移文件。
