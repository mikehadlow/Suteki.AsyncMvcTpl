using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Suteki.AsyncMvcTpl.Models;
using Suteki.AsyncMvcTpl.Services;

namespace Suteki.AsyncMvcTpl.Controllers
{
    public class HomeController : AsyncController
    {
        readonly UserService userService = new UserService();

        [HttpGet]
        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            userService.GetCurrentUser().ContinueWith(t1 =>
            {
                var user = t1.Result;
                userService.SendUserAMessage(user, "Hi From the MVC TPL experiment").ContinueWith(_ =>
                {
                    AsyncManager.Parameters["user"] = user;
                    AsyncManager.OutstandingOperations.Decrement();
                });
            });
            
        }

        public ViewResult IndexCompleted(User user)
        {
            return View(user);
        }

        // this would be very nice if it actually worked :)
        [HttpGet]
        public Task<ViewResult> IndexLinq()
        {
            return from user in userService.GetCurrentUser()
                   from _ in userService.SendUserAMessage(user, "Hi From the MVC TPL experiment")
                   select View(user);
        }

    }
}