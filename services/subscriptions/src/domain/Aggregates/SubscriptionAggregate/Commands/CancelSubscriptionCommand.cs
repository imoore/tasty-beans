﻿namespace TastyBeans.Subscriptions.Domain.Aggregates.SubscriptionAggregate.Commands;

public record CancelSubscriptionCommand(Guid CustomerId);