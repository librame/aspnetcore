using LibrameSample.Mvc.Models;

namespace LibrameSample.Mvc.Controllers
{
    public class BlogController : Librame.Context.Mvc.ControllerRepository<SampleDbContext, Blog>
    {
    }
}