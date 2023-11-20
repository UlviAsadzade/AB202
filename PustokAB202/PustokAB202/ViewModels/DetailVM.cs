using PustokAB202.Models;

namespace PustokAB202.ViewModels
{
    public class DetailVM
    {
        public Book Book { get; set; }

        public List<Book> RelatedBooks { get; set; }
    }
}
