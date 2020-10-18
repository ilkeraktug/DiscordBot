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
using System.Runtime.CompilerServices;


namespace DC
{
	[Group("Deep")]
	public class DeepCommands : ModuleBase<SocketCommandContext>
	{
		string path = "deep/";

		[Command("kurtarsın", RunMode = RunMode.Async)]
		public async Task kurtarsin(IVoiceChannel channel = null)
		{
			await Speak(channel, "kurtarsin.mp3");
		}

		[Command("bayılmak", RunMode = RunMode.Async)]
		public async Task bayilmak(IVoiceChannel channel = null)
		{
			await Speak(channel, "bayilmak.mp3");
		}

		[Command("uçak", RunMode = RunMode.Async)]
		public async Task ucak(IVoiceChannel channel = null)
		{
			await Speak(channel, "uçak.mp3");
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
