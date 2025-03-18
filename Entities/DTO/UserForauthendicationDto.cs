using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record UserForauthendicationDto
    {
        [Required(ErrorMessage ="Bu alan zorunludur.")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string? Password { get; init; }
    }
}
