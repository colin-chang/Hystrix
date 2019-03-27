# HystrixCommand
A light AOP framework that allows you to use `Polly` to handle exceptions with some policies like circuit break or fallback and so on  of your .net core application.It's like Hystrix from Java Platform.

* Based on `Polly` and `AspectCore.Core`.
* Used as Attribute conveniently.
* Class must be `public` and method must be `virtual`. 

Sample:

```csharp
[HystrixCommand(nameof(RetryTestFallBackAsync), MaxRetryTimes = 3, EnableCircuitBreaker = true)]
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
```

We provide 2 samples to show how to use this in Console and Asp.Net Core.
* [Console Sample](https://github.com/colin-chang/Hystrix/tree/master/ColinChang.Hystrix.ConsoleSample)
* [Asp.Net Core Sample](https://github.com/colin-chang/Hystrix/tree/master/ColinChang.Hystrix.WebSample)


> Advanced

The link below explain its theory and how it works.

https://colin-chang.site/netcore/pages/microservice-polly.html
