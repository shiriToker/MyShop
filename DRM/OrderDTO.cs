using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(DateOnly OrserDate, decimal OrderSum,string? UserUserName);
    public record OrderCreatDTO(DateOnly OrserDate, decimal OrderSum,int UserId, List<CreatOrdrItemDTO> OrderItems);

    public record CreatOrdrItemDTO(int ProductId, int Quantity);


}
