using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DTO
{
    [Serializable]
    public record BookDto(int Id,String? Name,decimal Price);
}
