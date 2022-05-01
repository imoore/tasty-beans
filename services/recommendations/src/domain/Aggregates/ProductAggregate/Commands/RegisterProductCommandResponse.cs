﻿using System.Diagnostics.CodeAnalysis;
using TastyBeans.Shared.Domain;

namespace TastyBeans.Recommendations.Domain.Aggregates.ProductAggregate.Commands;

public record RegisterProductCommandResponse(Product? Product, IEnumerable<ValidationError> Errors)
{
    [MemberNotNullWhen(true, nameof(Product))]
    public bool IsValid => !Errors.Any();
}