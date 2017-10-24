using System.Diagnostics;

namespace HangfireWindsorSample.Services
{
    public class HelperService : IHelperService
    {
        public void DoSomeStuff()
        {
            Debug.WriteLine("Helper service");
        }
    }
}
