﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public class ViewModel
    {
    }
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public long SerialNo { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorName { get; set; }
        public short CategoryCode { get; set; }
        public string Category => CategoryCode == 1001 ? ".NET" : "杂谈";
        public string ReleaseDate { get; set; }
        public short CommentCounts { get; set; }
        public virtual int Count { get; set; }
        public string SubDate { get; set; }
        public string CreateDate { get; set; }
    }

    public class PostViewModel1
    {
        public Guid Id { get; set; }
        public long SerialNo { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorName { get; set; }
        public short CategoryCode { get; set; }
        public string Category { get; set; } = "杂谈";
        public string ReleaseDate { get; set; }
        public short CommentCounts { get; set; }
        public virtual int Count { get; set; }
        public string SubDate { get; set; }
        public string CreateDate { get; set; }
    }
}
