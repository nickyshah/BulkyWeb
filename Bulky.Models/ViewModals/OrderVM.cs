using Bulky.DataAccess;

namespace Bulky.Models.ViewModals
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }

        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
