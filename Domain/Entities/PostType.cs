using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostType : BaseType
    {
        public PostType()
        {
            Posts = new List<Post>();
        }

        public List<Post> Posts { get; set; }
    }
}
