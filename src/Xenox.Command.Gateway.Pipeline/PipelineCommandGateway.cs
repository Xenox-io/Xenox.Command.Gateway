﻿using System.Threading.Tasks;
using Xenox.Command.Messaging;
using Xenox.Pipeline;

namespace Xenox.Command.Gateway.Pipeline {
	public class PipelineCommandGateway : ICommandGateway {
		private readonly IAuthenticationPipeline _authenticationPipeline;
		private readonly IAuthorizationPipeline _authorizationPipeline;
		private readonly ICommandMessageDispatchingPipeline _commandMessageDispatchingPipeline;

		public PipelineCommandGateway(
			IAuthenticationPipeline authenticationPipeline,
			IAuthorizationPipeline authorizationPipeline,
			ICommandMessageDispatchingPipeline commandMessageDispatchingPipeline
		) {
			_authenticationPipeline = authenticationPipeline;
			_authorizationPipeline = authorizationPipeline;
			_commandMessageDispatchingPipeline = commandMessageDispatchingPipeline;
		}

		public Task Send(CommandMessage commandMessage) {
			return commandMessage
				.SendOn(_authenticationPipeline)
				.ContinueOn(_authorizationPipeline)
				.ContinueOn(_commandMessageDispatchingPipeline)
			;
		}
	}
}
