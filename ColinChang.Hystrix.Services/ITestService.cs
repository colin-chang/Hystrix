using System.Threading.Tasks;

namespace ColinChang.Hystrix.Services
{
    public interface ITestService
    {
        Task<string> RetryTestAsync(string str);

        int FallbackTest(int i, int j);

        Task TimeoutTestAsync(int i);
    }
}