﻿using System.Reflection;
using Dapr.Client;
using RecommendCoffee.CustomerManagement.Application.Common;
using RecommendCoffee.CustomerManagement.Domain.Common;

namespace RecommendCoffee.CustomerManagement.Infrastructure.DomainEvents;

public class DaprEventPublisher : IEventPublisher
{
    private readonly DaprClient _daprClient;

    public DaprEventPublisher(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task PublishEventsAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var evt in events)
        {
            var topic = evt.GetType().GetCustomAttribute<TopicAttribute>();
            await _daprClient.PublishEventAsync("pubsub", topic?.Name ?? "customermanagement.deadletter.v0", evt);
        }
    }
}