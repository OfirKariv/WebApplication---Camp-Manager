using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDoc.Models

{
    public class Comment
    {
        public int ID { get; set; }
        public int campID { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
        public string Writer { get; set; }

    }
}