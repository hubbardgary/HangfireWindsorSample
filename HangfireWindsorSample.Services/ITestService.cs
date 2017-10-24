using System.Threading.Tasks;

namespace HangfireWindsorSample.Services
{
    public interface ITestService
    {
        string BackgroundJob();
        string DelayedBackgroundJob();
        string RecurringBackgroundJob();
        string UnreliableMethod();
        Task<string> SendApiGetRequestAsync(string domain, int id);
        Task<string> SendApiPostRequestAsync(string domain, int value1, int value2);
    }
}
