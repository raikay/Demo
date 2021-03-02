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
