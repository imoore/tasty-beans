﻿namespace TastyBeans.Shipping.Domain.Aggregates.CustomerAggregate;

public record Address(string Street, string HouseNumber, string PostalCode, string City, string CountryCode);
