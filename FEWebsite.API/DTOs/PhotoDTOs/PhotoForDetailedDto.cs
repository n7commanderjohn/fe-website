using System;
using FEWebsite.API.DTOs.BaseDTOs;

namespace FEWebsite.API.DTOs.PhotoDTOs
{
    public class PhotoForDetailedDto : BaseDto
    {
        public string Url { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }
    }
}
