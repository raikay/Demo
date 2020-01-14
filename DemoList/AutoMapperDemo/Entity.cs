using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public class Entity
    {
    }

    public class PostModel
    {
        public Guid Id { get; set; }
        public long SerialNo { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public short CategoryCode { get; set; }
        public bool IsDraft { get; set; }
        public string Content { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual IList<CommentModel> Comments { get; set; }
    }
    public class CommentModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
