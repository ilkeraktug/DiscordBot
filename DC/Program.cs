using System;
using System.IO;
using System.Globalization;
using System.Threading.Channels;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Collections;
using Discord.Audio;
using System.Linq.Expressions;
using System.Reflection;

namespace DC
{
    public class Program
    {
		public static void Main(string[] args)
		{
			new Program().MainAsync().GetAwaiter().GetResult();
		}

		private readonly DateTime today = new DateTime(2020, 10, 15);
		private int startPoint = 107;
		private int offset;
		private bool todayCounted = false;
		private static int alperen;
		private static int alperenMercy;

		private DiscordSocketClient _client;
		private CommandService _commands;
		private IServiceProvider _services;

		private string filePath = "alperen.txt";
		private string filePath1 = "alperenMercy.txt";
		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();
			_commands = new CommandService();

			_client.Log += Log;

			await _client.LoginAsync(TokenType.Bot,
				"Token".Trim());
			await _client.StartAsync();

			alperen = Int32.Parse(File.ReadAllText(filePath));
			alperenMercy = Int32.Parse(File.ReadAllText(filePath1));
			_client.MessageReceived += MessageReceived;
			//_client.UserVoiceStateUpdated += Join;
			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		private async Task MessageReceived(SocketMessage message)
		{

			if (message.Content == "Alperen" || message.Content == "alperen")
			{
				await message.Channel.SendMessageAsync(message.Author.Mention + " Alperen abi beni sev!");
				alperen++;
				await message.Channel.SendMessageAsync("An itibari ile " + alperen + " defa Alperenin sevgisi istendi");
				File.WriteAllText(filePath, alperen.ToString());
			}

			if(message.Content == "Nazmi" || message.Content == "nazmi")
			{
				if (DateTime.Now.Subtract(today).TotalDays >= 0)
				{
					offset = (int)Math.Floor(DateTime.Now.Subtract(today).TotalDays);
				}
				var random = new Random();
				int randomNumber = random.Next(0, 125);
				string nazmi = " nazmi" + (startPoint + offset);
				await message.Channel.SendMessageAsync(message.Author.Mention + nazmi);
				if (randomNumber == 78)
					await message.Channel.SendMessageAsync("Kes Sesini be " + message.Author.Mention + " ...");
			}

			if(message.Author.Id == 351796183666130944 && (message.Content == "Love" || message.Content == "love"))
			{
				await message.Channel.SendMessageAsync("An itibari ile " + alperenMercy + " defa Alperen sevgisini gösterdi");
				alperenMercy++;
				File.WriteAllText(filePath1, alperenMercy.ToString());
			}

			if(message.Content == "!alperenCount")
			{
				await message.Channel.SendMessageAsync("Bu güne kadar " + alperen + " defa Alperenin sevgisi istendi");
			}

			if (message.Content == "!alperenLoveCount")
			{
				await message.Channel.SendMessageAsync("Bu güne kadar " + alperenMercy + " defa Alperen sevgisini gösterdi");
				if (alperenMercy < alperen)
					await message.Channel.SendMessageAsync((alperen - alperenMercy) + " kişi sevgisinin karşılığını bulamadı");
				else
					await message.Channel.SendMessageAsync((alperenMercy - alperen) + " tane sevgi sizleri bekliyor");
			}
		}

		public class Sample : ModuleBase<SocketCommandContext>
		{
			public async Task Join()
			{
				await (Context.User as IGuildUser).VoiceChannel.ConnectAsync();
			}
		}
	}
}
