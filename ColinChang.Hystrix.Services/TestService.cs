using System;
using System.Threading.Tasks;

namespace ColinChang.Hystrix.Services
{
    //需要public类
    public class TestService : ITestService
    {
        [Hystrix(nameof(RetryTestFallBackAsync), MaxRetryTimes = 3, EnableCircuitBreaker = true)]
        public virtual async Task<string> RetryTestAsync(string str)//It must be virtual method
        {
            Console.WriteLine(nameof(RetryTestAsync));
            str.ToString();

            return await Task.FromResult("ok" + str);
        }

        public async Task<string> RetryTestFallBackAsync(string name)
        {
            Console.WriteLine(nameof(RetryTestFallBackAsync));

            return "RetryTestFallBackAsync";
        }


        [Hystrix(nameof(FallbackTestFallback), EnableCircuitBreaker = true)]
        public virtual int FallbackTest(int i, int j)
        {
            string s = null;
            s.ToString();
            return i + j;
        }

        public int FallbackTestFallback(int i, int j)
        {
            Console.WriteLine($"降级执行{nameof(FallbackTestFallback)}");
            return 0;
        }

        [Hystrix(nameof(TimeoutTestFallbackAsync), TimeOut = 1000)]
        public virtual async Task TimeoutTestAsync(int i)
        {
            Console.WriteLine("Test" + i);
            await Task.Delay(2000);
        }

        public async Task TimeoutTestFallbackAsync(int i)
        {
            Console.WriteLine("超时降级");
            await Task.CompletedTask;
        }
    }
}