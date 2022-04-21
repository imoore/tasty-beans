﻿using RecommendCoffee.Shipping.Domain.Common;

namespace RecommendCoffee.Shipping.Domain.Aggregates.ShippingOrderAggregate;

public interface IShippingOrderRepository
{
    Task<ShippingOrder?> FindByIdAsync(Guid id);
    Task<PagedResult<ShippingOrder>> FindAllAsync(int pageIndex, int pageSize);
    Task<int> InsertAsync(ShippingOrder shippingOrder);
    Task<int> UpdateAsync(ShippingOrder shippingOrder);
    Task<int> DeleteAsync(ShippingOrder shippingOrder);
}