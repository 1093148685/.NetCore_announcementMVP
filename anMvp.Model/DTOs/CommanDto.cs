using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anMvp.Model.DTOs
{
    public class PageDto<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }
}
