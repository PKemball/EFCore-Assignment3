using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog : Entity
    {
        public Blog()
        {
            Posts = new List<Post>();
        }

        public string Url { get; set; }
        public bool IsPublic { get; set; }
        public int BlogTypeId { get; set; }
        public BlogType BlogType { get; set; }
        public List<Post> Posts { get; set; }
    }
}
