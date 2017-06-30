using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration.Fluent;

namespace SiteCoreTrainings.Data.Models
{
    public interface IGlassBase
    {
        string Name { get; set; }

        string TemplateName { get; set; }
    }
}
