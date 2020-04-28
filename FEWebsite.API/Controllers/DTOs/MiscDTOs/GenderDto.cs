using System.ComponentModel.DataAnnotations;

using FEWebsite.API.Controllers.DTOs.BaseDTOs;

namespace FEWebsite.API.Controllers.DTOs.MiscDTOs
{
    public class GenderDto : BaseReferenceDto
    {
        [MaxLength(3)]
        public override string Id { get; set; }
    }
}
