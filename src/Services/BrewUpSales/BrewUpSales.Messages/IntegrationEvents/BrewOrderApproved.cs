﻿using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpSales.Messages.IntegrationEvents;

public sealed class BrewOrderApproved(BrewOrderId aggregateId, Guid correlationId, BrewOrderBody brewOrderBody)
	: IntegrationEvent(aggregateId,
	correlationId)
{
	public readonly BrewOrderId BrewOrderId = aggregateId;
	public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}