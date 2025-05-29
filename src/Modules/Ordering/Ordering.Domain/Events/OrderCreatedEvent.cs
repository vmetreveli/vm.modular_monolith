﻿using Framework.Abstractions.Events;
using Ordering.Domain.Entities;

namespace Ordering.Domain.Events;
public record OrderCreatedEvent(Order Order)
    : IDomainEvent;
