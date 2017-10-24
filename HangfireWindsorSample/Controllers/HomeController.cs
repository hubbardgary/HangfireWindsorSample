using Hangfire;
using HangfireWindsorSample.Services;
using System.Web.Mvc;

namespace HangfireWindsorSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;

        public HomeController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PerformHangfireAction(string action)
        {
            var domain = $"{Request.Url.Scheme}://{Request.Url.Authority}";

            switch (action)
            {
                case "EnqueueBackgroundJob":
                    BackgroundJob.Enqueue<ITestService>(x => x.BackgroundJob());
                    break;
                case "EnqueueDelayedJob":
                    BackgroundJob.Schedule<ITestService>(x => x.DelayedBackgroundJob(), new System.TimeSpan(0, 1, 0));
                    break;
                case "EnqueueRecurringBackgroundJob":
                    RecurringJob.AddOrUpdate<ITestService>(x => x.RecurringBackgroundJob(), Cron.Minutely);
                    break;
                case "GetApiSuccess":
                    BackgroundJob.Enqueue<ITestService>(x => x.SendApiGetRequestAsync(domain, 1));
                    break;
                case "GetApiFailure":
                    BackgroundJob.Enqueue<ITestService>(x => x.SendApiGetRequestAsync(domain, -1));
                    break;
                case "PostApiSuccess":
                    BackgroundJob.Enqueue<ITestService>(x => x.SendApiPostRequestAsync(domain, 1, 2));
                    break;
                case "PostApiFailure":
                    BackgroundJob.Enqueue<ITestService>(x => x.SendApiPostRequestAsync(domain, -1, -2));
                    break;
                case "UnreliableJob":
                    BackgroundJob.Enqueue<ITestService>(x => x.UnreliableMethod());
                    break;
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}