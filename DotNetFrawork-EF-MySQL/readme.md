

# .Net FramWork环境下使用EF连接Mysql

### Nuget包

```
MySql.Data.Entity
#引用这个时会自动引入依赖项 MySql.Data、EF
```



### 数据库实体

```
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class Livro
    {
        [Required]
        public int ID { get; set; }
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{4}")]
        public int AnoPublicacao { get; set; }
    }


    /// <summary>
    /// EF生成数据库的实体类，表名sys_publictypeinfo
    /// </summary>
    [System.Serializable]
    [Table("User")]
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
    }
}

```

### 数据库上下文

```
using ClassLibrary1.Models;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlContext : DbContext
    {
        public MySqlContext() : base("MySqlConnection") { }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}

```

### 使用

```
        public ActionResult Index()
        {
            using (var _db=new MySqlContext())
            {

                var user1 = new User
                {
                    Id = 3,
                    Name = "张三3号"
                };

                var s1 = _db.Users.Add(user1);
                var s2 = _db.SaveChanges();
                var ss = _db.Users.Find(1);
            }
            return View();
        }
```

