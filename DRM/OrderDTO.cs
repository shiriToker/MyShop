using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(int OrderId,DateOnly OrserDate, decimal OrderSum,string? UserUserName);
    public record OrderCreatDTO(decimal OrderSum,int UserId, List<CreatOrdrItemDTO> OrderItems);

    public record CreatOrdrItemDTO(int ProductId, int Quantity);

}
