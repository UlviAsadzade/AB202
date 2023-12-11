using PustokAB202.Models;
using PustokAB202.Utilities.Enums;

namespace PustokAB202.ViewModels
{
    public class OrderVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

        public List<CheckoutItemVM> CheckoutItemVMs = new List<CheckoutItemVM>();


    }
}
