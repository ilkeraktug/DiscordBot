using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace DC
{
	[Group(".play")]
	public class UnorderedCommands : ModuleBase<SocketCommandContext>
	{
		string path = "unsorted/";

		[Command("Mithat", RunMode = RunMode.Async)]
		public async Task mithat(IVoiceChannel channel = null)
		{
			await Speak(channel, "mithat.mp3");
		}

		[Command("155", RunMode = RunMode.Async)]
		public async Task ararim(IVoiceChannel channel = null)
		{
			await Speak(channel, "155.mp3");
		}

		[Command("adam", RunMode = RunMode.Async)]
		public async Task adam(IVoiceChannel channel = null)
		{
			await Speak(channel, "adam.mp3");
		}

		[Command("azerbaycan", RunMode = RunMode.Async)]
		public async Task azerbaycan(IVoiceChannel channel = null)
		{
			await Speak(channel, "azerbaycan.mp3");
		}

		[Command("ecel", RunMode = RunMode.Async)]
		public async Task ecel(IVoiceChannel channel = null)
		{
			await Speak(channel, "ecel.mp3");
		}

		[Command("ekremabi", RunMode = RunMode.Async)]
		public async Task ekremabi(IVoiceChannel channel = null)
		{
			await Speak(channel, "ekremabi.mp3");
		}

		[Command("kiki", RunMode = RunMode.Async)]
		public async Task kiki(IVoiceChannel channel = null)
		{
			await Speak(channel, "kiki.mp3");
		}

		[Command("kör", RunMode = RunMode.Async)]
		public async Task kor(IVoiceChannel channel = null)
		{
			await Speak(channel, "kör.mp3");
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
