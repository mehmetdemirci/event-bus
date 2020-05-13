# event-bus

This is a sample implementation of the EventBus that provides a way to implement event publishing and subscriptions, using with [CAP](https://github.com/dotnetcore/CAP). 

### Running

- Clone repository `git clone https://github.com/mehmetdemirci/event-bus.git`
- Open directory **src\WebApi** in command line and execute `dotnet build` then `dotnet run`.

You can also open the Dashboard by visiting the url `http://localhost:xxx/cap.`

```csharp
public static IServiceCollection AddEventSubscription(this IServiceCollection services)
{
    services.SubscribeEventBus<SingleGroup1EventHandler, SingleIntegrationEvent>();
    services.SubscribeEventBus<SingleGroup2EventHandler, SingleIntegrationEvent>();

    services.SubscribeEventBus<MultiGroup1EventHandler, GroupIntegrationEvent>(DispatchMode.Broadcast, "Group1");
    services.SubscribeEventBus<MultiGroup2EventHandler, GroupIntegrationEvent>(DispatchMode.Broadcast, "Group2");

    return services;
}
```

![Sample](https://github.com/mehmetdemirci/event-bus/blob/master/doc/sample.PNG "sample")

### License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
