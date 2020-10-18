using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Discord.Audio;

namespace DC
{
	class BoshBot
	{
		string token = File.ReadAllText("token.txt");

		DiscordSocketClient _client;
		CommandService _commands;
		IServiceProvider _services;

		public static IAudioChannel _audioChannel;
		public static IAudioClient _audioClient;
		public static IAudioChannel _lastChannel;

		public static IMessageChannel _channelBotdeneme;

		public static int alperen = int.Parse(File.ReadAllText("alperen.txt"));
		public static int alperenMercy = int.Parse(File.ReadAllText("alperenMercy.txt"));

		public static string help = File.ReadAllText("help.txt");

		public static readonly DateTime today = new DateTime(2020, 10, 15);
		public static int startPoint = 107;
		public static int offset;

		public async Task Run()
		{
			_services = ConfigureServices();
			_client = _services.GetRequiredService<DiscordSocketClient>();
			_commands = new CommandService();

			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();

			await _services.GetRequiredService<CommandHandler>().Init();

			_client.Log += Log;

			await Task.Delay(-1);
		}

		private Task Log(LogMessage log)
		{
			Console.WriteLine(log.ToString());
			return Task.CompletedTask;
		}

		private ServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
			.AddSingleton<DiscordSocketClient>()
			.AddSingleton<CommandService>()
			.AddSingleton<CommandHandler>()
			.AddSingleton<HttpClient>()
			.BuildServiceProvider();
		}
	}
}
