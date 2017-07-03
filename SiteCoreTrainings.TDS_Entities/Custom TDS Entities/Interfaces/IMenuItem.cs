using System.Collections.Generic;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public interface IMenuItem : IGlassBase
    {
        string Title { get; set; }

        IEnumerable<IMenuItem> Children { get; set; }
    }
}
