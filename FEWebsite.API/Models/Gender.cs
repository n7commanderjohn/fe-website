using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models
{
    public class Gender : BaseReferenceModel
    {
        [MaxLength(3)]
        public override string Id { get; set; }
    }
}
