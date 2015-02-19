using LibrameSample.Mvc.Models;
using Microsoft.AspNet.Mvc;

namespace LibrameSample.Mvc.Apis
{
    [Route("api/[controller]")]
    public class BlogsController : Librame.Context.Mvc.ControllerApi<SampleDbContext, Blog>
    {
    }
}