using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCoreTrainings.Data.Models
{
    public interface IMenuItem : IContentBase
    {
        string Title { get; set; }

        IEnumerable<IMenuItem> Children { get; set; }
    }
}
