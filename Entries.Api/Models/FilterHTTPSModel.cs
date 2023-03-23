using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Entries.Api.Models
{
    public class FilterHTTPSModel
    {
       
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]    
        public bool HTTPS { get; set; }
    }
}
