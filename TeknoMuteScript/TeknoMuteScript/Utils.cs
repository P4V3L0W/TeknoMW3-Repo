using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityScript;

namespace TeknoMuteScript
{
	public class Utils : BaseScript
	{
		public static void SayTo(Entity player, string message)
		{
			Utilities.RawSayTo(player, "^;[^5PM^;] ^7: " + message);
		}

		public static void Say(string message)
		{
			Utilities.RawSayAll("^;[^5ClanTag^;] ^7: " + message); //Add server message prefix here
		}

		public static Entity FindEntityByName(string name, List<Entity> players)
		{
			List<Entity> matches = new List<Entity>();

			if (name == "")
			{
				return null;
			}

			foreach (var player in players)
			{
				if (player.Name.ToLower() == name) //exact match
				{
					return player;
				}

				if (player.Name.ToLower().Contains(name.ToLower()))
				{
					matches.Add(player);
				}
			}

			return matches.Count == 1 ? matches[0] : null;
		}
	}
}
