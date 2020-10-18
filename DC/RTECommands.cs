using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DC
{
	[Group("RTE")]
	public class RTECommands : ModuleBase<SocketCommandContext>
	{
		string path = "rte/";

		[Command("dolar", RunMode = RunMode.Async)]
		public async Task Biliriz(IVoiceChannel channel = null)
		{
			await Speak(channel, "dolar.mp3");
		}

		private async Task Speak(IVoiceChannel channel, string path)
		{
			Task.Delay(100);
			if (channel == null)
				BoshBot._audioChannel = (Context.User as IGuildUser).VoiceChannel;
			else
				BoshBot._audioChannel = channel;

			if (BoshBot._audioChannel == null)
			{
				await ReplyAsync("You have to be in a channel or specify a channel id ```Example : join kurtarsin ChannelName```");
				return;
			}

			if (BoshBot._audioChannel != BoshBot._lastChannel)
			{
				BoshBot._audioClient = await BoshBot._audioChannel.ConnectAsync();
				BoshBot._lastChannel = BoshBot._audioChannel;
			}

			if (BoshBot._audioClient != null)
				await Voice.SendAsync(BoshBot._audioClient, this.path + path);
		}
	}
}
