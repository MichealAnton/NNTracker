using Microsoft.AspNetCore.Mvc;

namespace NNTracker.Views.Shared.SearchBar
{
    public class SearchBarView: ViewComponent
    {
        public SearchBarView() 
        {
        
        }
        public IViewComponentResult Invoke (Spager SearchPager)
        {
            return View("Default", SearchPager);
        }
    }
}
