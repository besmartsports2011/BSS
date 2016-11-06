using System.Web.Http;
using System.Web.Http.Cors;
using BSS_WebAPI.Utils;
using BSS_WebAPI.Models;

namespace BSS_WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("api/v1/email/sendmail/")]
        public IHttpActionResult Post(Email email)
        {
            var mailSent = EmailNotificationUtil.Send(email);
            return Json(mailSent);
        }

    }
}
