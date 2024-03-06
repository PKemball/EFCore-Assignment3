using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.QueryModels {
    public class QueryResultModel
    {
        public string BlogURL { get; set; }
        public bool BlogIsPublic { get; set; }
        public string BlogTypeName { get; set; }
        public Status BlogTypeStatus { get; set; }
        public List<PostQueryModel> Posts { get; set; }
        public QueryResultModel()
        {
            Posts = new List<PostQueryModel>();
        }
    }
}
