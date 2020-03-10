using System;

using Microsoft.AspNetCore.Http;

namespace FEWebsite.API.DTOs.PhotoDTOs
{
    public class PhotoForUploadDto
    {
        public string Url { get; set; }

        public IFormFile File { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public string PublicId { get; set; }

        public PhotoForUploadDto()
        {
            this.DateAdded = DateTime.Now;
        }
    }
}
