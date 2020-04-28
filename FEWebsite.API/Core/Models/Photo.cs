using System;

using FEWebsite.API.Core.Models.AbstractModels;

namespace FEWebsite.API.Core.Models
{
    public class Photo : DirectlyRelatedToUser
    {
        public string Url { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }

        public string PublicId { get; set; }
    }
}
