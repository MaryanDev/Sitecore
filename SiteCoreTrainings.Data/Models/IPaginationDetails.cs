using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCoreTrainings.Data.Models
{
    public class PaginationDetails
    {
        public int AllPages { get; set; }

        public int CurrentPage { get; set; }

        public string Url { get; set; }
    }
}
