using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Discord.Audio;
using System.Linq;
using System.Collections.Generic;

namespace DC
{
	[Group("join")]
	public class Voice : ModuleBase<SocketCommandContext>
	{
		public static async Task OnUserVoiceUpdated(SocketUser user, SocketVoiceState state1, SocketVoiceState state2)
		{
			if (state1.VoiceChannel != null && BoshBot._audioChannel != null)
			{
				if (state1.VoiceChannel.Users.Count <= 1)
					await BoshBot._audioChannel.DisconnectAsync();
			}


			if ((state2.VoiceChannel == BoshBot._audioChannel) && (BoshBot._audioClient != null))
			{
				//TODO : Someone joins channel 
				
				if (BoshBot._channelBotdeneme != null && !user.IsBot)
				{
					await BoshBot._channelBotdeneme.SendMessageAsync(user.Mention + " Allah Kurtarsin Kardeşim...");
					await Task.Delay(300);
					await SendAsync(BoshBot._audioClient, "kurtarsin.mp3");
				}
			}

		}

		[Command(RunMode = RunMode.Async)]
		public async Task Default(IVoiceChannel channel = null)
		{

			if (channel == null)
				BoshBot._audioChannel = (Context.User as IGuildUser).VoiceChannel;
			else
				BoshBot._audioChannel = channel;

			if (BoshBot._audioChannel == null)
			{
				await ReplyAsync("You have to be in a channel or specify a channel id ```Example : join kurtarsin ChannelName```");
				return;
			}

			if(BoshBot._lastChannel != BoshBot._audioChannel)
			{
				BoshBot._audioClient = await BoshBot._audioChannel.ConnectAsync();
				BoshBot._lastChannel = BoshBot._audioChannel;
			}
		}

		public static async Task SendAsync(IAudioClient client, string path)
		{
			// Create FFmpeg using the previous example
			using (var ffmpeg = CreateStream(path))
			using (var output = ffmpeg.StandardOutput.BaseStream)
			using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
			{
				try { await output.CopyToAsync(discord); }
				finally { await discord.FlushAsync(); }
			}
		}

		private static Process CreateStream(string path)
		{
			return Process.Start(new ProcessStartInfo
			{
				FileName = "ffmpeg",
				Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
				UseShellExecute = false,
				RedirectStandardOutput = true,
			});
		}

	}
}
