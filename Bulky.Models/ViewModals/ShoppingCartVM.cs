using Bulky.DataAccess;

namespace Bulky.Models.ViewModals
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

    }
}
