﻿using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace BrewUpLogistics.Domain.CommandHandlers;

public abstract class CommandHandlerBaseAsync<TCommand> : CommandHandlerAsync<TCommand> where TCommand : class, ICommand
{
	protected CommandHandlerBaseAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(TCommand command, CancellationToken cancellationToken = new())
	{
		try
		{
			Logger.LogInformation(
				"Processing command: {Type} - Aggregate: {CommandAggregateId} - CommandId : {CommandMessageId}",
				command.GetType(), command.AggregateId, command.MessageId);
			await ProcessCommand(command, cancellationToken);
		}
		catch (Exception ex)
		{
			// Better to raise an event to notify the error
			Logger.LogError(
				"Error processing command: {Type} - Aggregate: {CommandAggregateId} - CommandId : {CommandMessageId} - Messagge: {ExMessage} - Stack Trace {ExStackTrace}",
				command.GetType(), command.AggregateId, command.MessageId, ex.Message, ex.StackTrace);
		}
	}

	public abstract Task ProcessCommand(TCommand command, CancellationToken cancellationToken = default);
}