using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace OurMemory.Service.Model
{
    public class SearchRequestModelBase
    {
        public int Size { get; set; }
        public int Page { get; set; } = 1;
        public SortDirection Direction { get; set; }
    }
}
