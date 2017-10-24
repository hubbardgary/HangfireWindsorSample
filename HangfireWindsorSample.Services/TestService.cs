using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HangfireWindsorSample.Services
{
    public class TestService : ITestService
    {
        private readonly IHelperService _helperService;

        public TestService(IHelperService helperService)
        {
            _helperService = helperService;
        }

        public string BackgroundJob()
        {
            _helperService.DoSomeStuff();
            return "Background job succeeded";
        }

        public string DelayedBackgroundJob()
        {
            return "Delayed job succeeded";
        }

        public string RecurringBackgroundJob()
        {
            return "Recurring job succeeded";
        }

        public string UnreliableMethod()
        {
            var random = new Random();

            // Fail 4 times out of 5
            if(random.Next(1, 5) != 1)
            {
                throw new SystemException("Unreliable method failed");
            }

            return "Unreliable method succeded";
        }

        public async Task<string> SendApiGetRequestAsync(string domain, int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{domain}/api/sampleapi/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new SystemException($"{response.StatusCode}");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> SendApiPostRequestAsync(string domain, int value1, int value2)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{domain}");

                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("Value1", value1.ToString()),
                    new KeyValuePair<string, string>("Value2", value2.ToString())
                });

                var response = await client.PostAsync("/api/sampleapi", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new SystemException($"{response.StatusCode}");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
