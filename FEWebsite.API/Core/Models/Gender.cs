using System.ComponentModel.DataAnnotations;

using FEWebsite.API.Core.Models.AbstractModels;

namespace FEWebsite.API.Core.Models
{
    public class Gender : BaseReferenceModel
    {
        [MaxLength(3)]
        public override string Id { get; set; }
    }
}
