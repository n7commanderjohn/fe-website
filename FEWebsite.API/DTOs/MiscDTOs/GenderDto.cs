using System.ComponentModel.DataAnnotations;
using FEWebsite.API.DTOs.BaseDTOs;

namespace FEWebsite.API.DTOs.MiscDTOs
{
    public class GenderDto : BaseReferenceDto
    {
        [MaxLength(3)]
        public override string Id { get; set; }
    }
}
