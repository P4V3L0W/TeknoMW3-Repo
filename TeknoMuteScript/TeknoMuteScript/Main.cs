using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityScript;

namespace TeknoMuteScript
{
	public class MuteScript : BaseScript
	{
		public MuteScript()
		{
			PlayerConnected += player => { player.SetField("muted", false); };
		}

		public override EventEat OnSay3(Entity player, ChatType type, string name, ref string message)
		{
			if (player.GetField<bool>("muted"))
			{
				Utils.SayTo(player, "You are muted!");
				return EventEat.EatGame;
			}

			/************************************************* Commands ***************************************************/

			if (message.StartsWith("!"))
			{
				var cmdRaw = message.Substring(1);
				var cmdName = cmdRaw.Split(' ')[0].ToLower();
				var cmdParams = cmdRaw.Split(' ').Skip(1).ToArray();

				switch (cmdName)
				{
					case "m":
					case "mute":
					{
						if (cmdParams.Length != 1)
						{
							player.Call("iprintbold", "Usage: !mute <name>");
							return EventEat.EatGame;
						}

						var playerToMute = Utils.FindEntityByName(cmdParams[0], Players);

						if (playerToMute == null)
						{
							Utils.SayTo(player, "Found none or multiple players with that name!");
							break;
						}

						MutePlayer(playerToMute, player);

						break;
					}
					default:
						Utils.SayTo(player, "Command does not exist!");
						break;
				}

				return EventEat.EatGame;
			}

			return base.OnSay3(player, type, name, ref message);
		}

		private static void MutePlayer(Entity player, Entity admin)
		{
			if (player.GetField<bool>("muted"))
			{
				player.SetField("muted", false);
				Utils.Say(player + " has been ^1muted ^7by " + admin);
				Utils.SayTo(player, "You have been ^1muted^7!");
			}
			else
			{
				player.SetField("muted", true);
				Utils.Say(player + " has been ^2unmuted ^7by " + admin);
				Utils.SayTo(player, "You have been ^2unmuted^7!");
			}
		}
	}
}