﻿namespace RecommendCoffee.Recommendations.Domain.Aggregates.ProductAggregate.Commands;

public record RegisterProductCommand(Guid Id, string Name);