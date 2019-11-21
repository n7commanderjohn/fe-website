using System;
using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models
{
    public class Photo : DirectlyRelatedToUser
    {
        public string Url { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }
    }
}
