namespace Tweaks.Patches
{
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;

	[HarmonyPatch(typeof(NotablesCampaignBehavior), "SpawnNotablesAtGameStart")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public static class SpawnNotablesAtGameStartPatch
	{

		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			var list = new List<CodeInstruction>(instructions);
			if (list.Count == 165)
			{
				list.RemoveRange(147, 3);
			}
			return list.AsEnumerable();
		}

		public static void Postfix()
		{
			/*
						if ((MCMSettings.Instance is { } settings && settings.ProductionTweakEnabled))
						{
							if (Statics.GetSettingsOrThrow().SettlementsDebug)
							{

								IM.MessageDebug("DailyProductionAmount: original : " + __result.ToString() + "\r\n"
									+ " OtherTweakAmount " + settings.ProductionOtherTweakAmount.ToString() + "\r\n"
									+ " final " + (__result * settings.ProductionOtherTweakAmount).ToString() + "\r\n"
									);
							}
							__result *= settings.ProductionOtherTweakAmount;
						}

						if  (Campaign.Current.AliveHeroes != null && Statics.GetSettingsOrThrow().WandererLocationDebug)
						{
							//Dictionary<Hero, string> wList = new Dictionary<Hero, string>();
							Dictionary<string, string> wList = new Dictionary<string, string>();
							foreach (Hero hero in Campaign.Current.AliveHeroes)
							{
								if (hero != null)
								{
									if (hero.CharacterObject.Occupation == Occupation.Wanderer && hero != null)
									{
										if (hero.CurrentSettlement != null)
										{
											if (!wList.ContainsKey(hero.Name.ToString()))
											{
												wList.Add(hero.Name.ToString(), hero.CurrentSettlement.Name.ToString());
											}
											//IM.MessageDebug("Wanderer Name: " + hero.Name.ToString() + "   CurrentSettlement: " +hero.CurrentSettlement.Name.ToString());
										}
									}
								}
							}

							foreach (KeyValuePair<string, string> entry in wList)
							{
								IM.MessageDebug("Wanderer Name: " + entry.Key.ToString() + "   CurrentSettlement: " + entry.Value.ToString());
							}
						}*/
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {UnlimitedWanderersPatch: true};
	}
}
