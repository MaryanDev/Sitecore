using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;

namespace SiteCoreTrainings.Data.Models
{
    [SitecoreType]
    public interface IAuthorDetails : IContentBase
    {
        [SitecoreField("Full Name")]
        string FullName { get; set; }

        [SitecoreField("Bio")]
        string Bio { get; set; }

        [SitecoreField("Date of Birth")]
        DateTime DateOfBirth { get; set; }
        
        [SitecoreField("Profile Image")]
        Image ProfileImage { get; set; }

        //[SitecoreField("Articles")]
        List<IArticleDetails> Articles { get; set; }

        [SitecoreChildren]
        IEnumerable<Comment> Comments { get; set; }
    }
}
