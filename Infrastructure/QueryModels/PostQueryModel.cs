using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryModels
{
    public class PostQueryModel
    {
        public string PostTypeName { get; set; }
        public Status PostTypeStatus {  get; set; }
        public string  UserName {  get; set; }
        public string UserEmail {  get; set; }
        public int UserPostsCount { get; set; }
    }
}
