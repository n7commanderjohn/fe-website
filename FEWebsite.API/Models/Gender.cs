using System.ComponentModel.DataAnnotations;

using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models
{
    public class Gender : BaseReferenceModel
    {
        [MaxLength(3)]
        public override string Id { get; set; }
    }
}
