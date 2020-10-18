using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DC
{
	[Group("sönmez")]
	public class SonmezCommands : ModuleBase<SocketCommandContext>
	{
		string path = "sonmez/";

		[Command("kamera", RunMode = RunMode.Async)]
		public async Task kamera(IVoiceChannel channel = null)
		{
			await Speak(channel, "kamera.mp3");
		}

		[Command("alagavat", RunMode = RunMode.Async)]
		public async Task alagavat(IVoiceChannel channel = null)
		{
			await Speak(channel, "alagavat.mp3");
		}

		[Command("eruhlu", RunMode = RunMode.Async)]
		public async Task eruhlu(IVoiceChannel channel = null)
		{
			await Speak(channel, "eruhlu.mp3");
		}

		[Command("saat2", RunMode = RunMode.Async)]
		public async Task saat2(IVoiceChannel channel = null)
		{
			await Speak(channel, "saat2.mp3");
		}

		[Command("istila", RunMode = RunMode.Async)]
		public async Task istila(IVoiceChannel channel = null)
		{
			await Speak(channel, "istila.mp3");
		}

		[Command("akuva", RunMode = RunMode.Async)]
		public async Task akuva(IVoiceChannel channel = null)
		{
			await Speak(channel, "akuva.mp3");
		}

		[Command("dino", RunMode = RunMode.Async)]
		public async Task dino(IVoiceChannel channel = null)
		{
			await Speak(channel, "dino.mp3");
		}

		[Command("ibne", RunMode = RunMode.Async)]
		public async Task ibne(IVoiceChannel channel = null)
		{
			await Speak(channel, "ibne.mp3");
		}

		[Command("fako", RunMode = RunMode.Async)]
		public async Task fako(IVoiceChannel channel = null)
		{
			await Speak(channel, "fako.mp3");
		}

		[Command("at", RunMode = RunMode.Async)]
		public async Task at(IVoiceChannel channel = null)
		{
			await Speak(channel, "at.mp3");
		}

		[Command("yazi", RunMode = RunMode.Async)]
		public async Task yazi(IVoiceChannel channel = null)
		{
			await Speak(channel, "yazi.mp3");
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

			if(BoshBot._audioChannel != BoshBot._lastChannel)
			{
				BoshBot._audioClient = await BoshBot._audioChannel.ConnectAsync();
				BoshBot._lastChannel = BoshBot._audioChannel;
			}

			if (BoshBot._audioClient != null)
				await Voice.SendAsync(BoshBot._audioClient, this.path + path);
		}
	}
}
