using Streetunes.Models;
using Streetunes.ViewModels;

namespace Streetunes.ViewModel

{
    public class HomeViewModel
    {
       

            public IEnumerable<Event>? Events { get; set; }

            public string City { get; set; }

            public string Plz { get; set; }

             public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();

    }
}
