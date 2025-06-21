using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ProductDTO(int Id, string ProductName, string ProductDescription, double Price, string ImageUrl, string CategoryName);
}
