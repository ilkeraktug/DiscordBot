using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace DC
{
	public class ComamndModule : ModuleBase<SocketCommandContext>
	{

		[Command("..help")]
		public async Task help()
		{
			await ReplyAsync(BoshBot.help);
		}

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
			int randomNumber = random.Next(0, 25);
			string nazmi = " nazmi" + (BoshBot.startPoint + BoshBot.offset);
			await ReplyAsync(Context.User.Mention + nazmi);
			if (randomNumber == 10)
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
				var dispoe = Context.Channel.EnterTypingState();
				await Task.Delay(500);
				await ReplyAsync("Sadece Alperen sevebilir!!");
				dispoe.Dispose();
			}
		}

		[Command("leave")]
		public async Task Leave()
		{
			var user = Context.User as IGuildUser;

			if (user.VoiceChannel == BoshBot._audioChannel && user.VoiceChannel != null)
			{
				await user.VoiceChannel.DisconnectAsync();
				BoshBot._audioChannel = null;
			}

		}

		[Command("leave")]
		public async Task Leave(IVoiceChannel channel = null)
		{
			var user = Context.User as IGuildUser;
			if (channel != null)
				await channel.DisconnectAsync();

			if (user.VoiceChannel == BoshBot._audioChannel)
			{
				await user.VoiceChannel.DisconnectAsync();
				BoshBot._audioChannel = null;
			}

		}

		/*[Command("stop")]
		*public async Task stop(IVoiceChannel channel = null)
		*{
		*	var user = Context.User as IGuildUser;
		*	if (channel != null)
		*		await channel.DisconnectAsync();
		*
		*	if (user.VoiceChannel == BoshBot._audioChannel && user.VoiceChannel != null)
		*	{
		*		Context.Client.St
		*	}
		}*/

		[Command("Demokrasi")]
		public async Task Demokrasi(IGuildUser user)
		{
				await user.ModifyAsync(x => { 
					x.ChannelId = 743042276464263250;
				});
		}
	}


}
