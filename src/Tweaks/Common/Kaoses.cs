namespace Tweaks.Common
{
	using System.Linq;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.Engine;
	using Utils;

	internal class Kaoses
	{
		public static bool IsPlayerClan(PartyBase party)
		{
			var isSame = false;
			var hero = party.LeaderHero;
			if (hero != null)
			{
				var clan = hero.Clan;
				var playerClan = Clan.PlayerClan;
				if (clan == playerClan)
				{
					isSame = true;
				}
			}
			return isSame;
		}

		public static bool IsPlayerClan(Hero hero)
		{
			var isSame = false;
			if (hero != null)
			{
				var clan = hero.Clan;
				var playerClan = Clan.PlayerClan;
				if (clan == playerClan)
				{
					isSame = true;
				}
			}
			return isSame;
		}

		public static bool IsPlayerClan(MobileParty mobileParty)
		{
			var isPlayerClan = false;
			Clan clan;
			Clan playerClan;
			if (mobileParty.IsCaravan)
			{
				var hero = mobileParty.LeaderHero;
				if (hero != null)
				{
					clan = hero.Clan;
					playerClan = Clan.PlayerClan;
					if (clan == playerClan)
					{
						isPlayerClan = true;
					}
				}
			}
			else if (mobileParty.IsGarrison)
			{
				var settlement = mobileParty.CurrentSettlement;
				clan = settlement.OwnerClan;
				playerClan = Clan.PlayerClan;
				if (clan == playerClan)
				{
					isPlayerClan = true;
				}
			}
			return isPlayerClan;
		}

		public static bool IsPlayer(Hero hero)
		{
			var isPlayer = false;

			if (hero != null)
			{
				if (Hero.MainHero == hero)
				{
					isPlayer = true;
				}
			}
			return isPlayer;
		}

		/// <summary>
		/// Checks if the hero is a Lord/Lady or Wanderer and is not the player
		/// </summary>
		/// <param name="hero"></param>
		/// <returns></returns>
		public static bool IsLord(Hero hero) =>
			/*
	IM.MessageDebug("IsLord: "
	+ "Name: " + hero.CharacterObject.Name.ToString() +"\r\n"
	+ "Occupation: " + hero.CharacterObject.Occupation.ToString() +"\r\n"
	+ "IsHero: " + hero.CharacterObject.IsHero.ToString() +"\r\n"
	//+ "IsBasicTroop: " + hero.CharacterObject.IsBasicTroop.ToString() +"\r\n"
	+ "result" + ((hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.Lady || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter).ToString() +"\r\n"
	);
	*/
			(hero.CharacterObject.Occupation == Occupation.Mercenary || hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.GangLeader || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter;/*
                Kaoses Tweaks : IsLordName: Nadea the Wanderer
                Occupation: Wanderer
                IsHero: True
                IsBasicTroop: False

                Kaoses Tweaks : IsLordName: Ira
                Occupation: Lord
                IsHero: True
                IsBasicTroop: False
             */

		public static bool IsPlayerLord(Hero hero) =>
			//hero.CharacterObject.IsHero
			/*
						IM.MessageDebug("IsLord: "
							+ "Name: " + hero.CharacterObject.Name.ToString() + "\r\n"
							+ "Occupation: " + hero.CharacterObject.Occupation.ToString() + "\r\n"
							+ "IsHero: " + hero.CharacterObject.IsHero.ToString() + "\r\n"
							+ "IsPlayerClan: " + IsPlayerClan(hero).ToString() + "\r\n"
							//+ "IsBasicTroop: " + hero.CharacterObject.IsBasicTroop.ToString() +"\r\n"
							+ "result" + ((hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.Lady || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter && IsPlayerClan(hero)).ToString() + "\r\n"
							);*/
			(hero.CharacterObject.Occupation == Occupation.Mercenary || hero.CharacterObject.Occupation == Occupation.Lord || hero.CharacterObject.Occupation == Occupation.GangLeader || hero.CharacterObject.Occupation == Occupation.Wanderer) && !hero.IsHumanPlayerCharacter;
	}
}
