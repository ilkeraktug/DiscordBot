using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DC
{
	class CommandHandler
	{
		private readonly DiscordSocketClient _client;
		private readonly CommandService _commands;
		private readonly IServiceProvider _services;
		private IMessageChannel _channel;

		public CommandHandler(IServiceProvider services)
		{
			_client = services.GetRequiredService<DiscordSocketClient>();
			_commands = services.GetRequiredService<CommandService>();
			_services = services;

			//TODO : Attach Evenets

			_commands.CommandExecuted += ExecuteCommands;
			_client.MessageReceived += MessageReceived;
			_client.UserVoiceStateUpdated += Voice.OnUserVoiceUpdated;
		}

		public async Task Init()
		{
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
		}

		public async Task MessageReceived(SocketMessage message)
		{
			if (!(message is SocketUserMessage msg)) return;
			if (message.Author.IsBot) return;

			//int argPos = 0;

			//if (!(msg.HasCharPrefix('.', ref argPos))) return;

			var context = new SocketCommandContext(_client, msg);

			await _commands.ExecuteAsync(context, 0, _services);

			if (message.Content == "!alperenCount")
			{
				await message.Channel.SendMessageAsync("Bu güne kadar " + BoshBot.alperen + " defa Alperenin sevgisi istendi");
			}

			if (message.Content == "!alperenLoveCount")
			{
				await message.Channel.SendMessageAsync("Bu güne kadar " + BoshBot.alperenMercy + " defa Alperen sevgisini gösterdi");
				if (BoshBot.alperenMercy < BoshBot.alperen)
					await message.Channel.SendMessageAsync((BoshBot.alperen - BoshBot.alperenMercy) + " kişi sevgisinin karşılığını bulamadı");
				else
					await message.Channel.SendMessageAsync((BoshBot.alperenMercy - BoshBot.alperen) + " tane sevgi sizleri bekliyor");
			}
		}

		private async Task ExecuteCommands(Optional<CommandInfo> command, ICommandContext context, IResult result)
		{
			_channel = _client.GetChannel(766718633442803763) as IMessageChannel;

			if (!command.IsSpecified) return;

			if (!result.IsSuccess)
			{
				await _channel.SendMessageAsync(StringMaker.SingleLine(DateTime.Now.ToString()) + StringMaker.Red("Error : " + result.Error) + " due to " + StringMaker.Blue(result.ErrorReason));
				return;
			}

			if (result.IsSuccess)
				Console.WriteLine("Succes : " + result.ToString());
		}
	}
}
