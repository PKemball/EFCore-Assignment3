using Domain.Entities;

namespace Domain.DataContainer
{
    public class DataContainer
    {
        public DataContainer()
        {
            Users = new List<User>();
            PostTypes = new List<PostType>();
            BlogTypes = new List<BlogType>();
            Blogs = new List<Blog>();
            Posts = new List<Post>();
        }

        public List<User> Users { get; set; }
        public List<PostType> PostTypes { get; set; }
        public List<BlogType> BlogTypes { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Post> Posts { get; set; }
    }
}
