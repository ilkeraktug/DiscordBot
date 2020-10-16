using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;

namespace DC
{
	public class ComamndModule : ModuleBase<SocketCommandContext>
	{

		[Command("Alperen")]
		public async Task Alperen()
		{
			await ReplyAsync(Context.User.Mention + " Alperen abi beni sev!");
			BoshBot.alperen++;
			await ReplyAsync("An itibari ile " + BoshBot.alperen + " defa Alperenin sevgisi istendi");
			File.WriteAllText("alperen.txt", BoshBot.alperen.ToString());
		}

		[Command("Nazmi", true, RunMode = RunMode.Async)]
		public async Task Nazmi()
		{
			if (DateTime.Now.Subtract(BoshBot.today).TotalDays >= 0)
			{
				BoshBot.offset = (int)Math.Floor(DateTime.Now.Subtract(BoshBot.today).TotalDays);
			}
			var random = new Random();
			int randomNumber = random.Next(0, 125);
			string nazmi = " nazmi" + (BoshBot.startPoint + BoshBot.offset);
			await ReplyAsync(Context.User.Mention + nazmi);
			if (randomNumber == 78)
				await ReplyAsync("Kes Sesini be " + Context.User.Mention + " ...");
		}

		[Command("Love")]
		public async Task Love()
		{
			if (Context.User.Id == 351796183666130944)
			{
				await ReplyAsync("An itibari ile " + BoshBot.alperenMercy + " defa Alperen sevgisini gösterdi");
				BoshBot.alperenMercy++;
				File.WriteAllText("alperenMercy.txt", BoshBot.alperenMercy.ToString());
			}
			else
			{
				await ReplyAsync("Sadece Alperen sevebilir!!");
			}

		}

		[Command("leave")]
		public async Task Leave()
		{
			var user = Context.User as IGuildUser;
			if (user.VoiceChannel == BoshBot._audioChannel && user.VoiceChannel != null)
				await user.VoiceChannel.DisconnectAsync();

		}
	}
}
