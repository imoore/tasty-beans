﻿namespace TastyBeans.Subscriptions.Domain.Services.Recommendations;

public interface IRecommendations
{
    public Task<Guid> GetRecommendProductAsync(Guid customerId);
}