namespace Tweaks.Patches
{
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.Core;

	[HarmonyPatch(typeof(MobileParty), "FillPartyStacks")]
	public class TitanFillPartyStacksPatch
	{
		private static bool HandlePartySizeMultipliers(ref MobileParty __instance, PartyTemplateObject pt, int troopNumberLimit)
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.PartySizeMultipliersEnabled)
			{
				if (__instance.IsBandit || __instance.IsBanditBossParty)
				{
					var num1 = (float)(0.400000005960464 + (0.800000011920929 * Campaign.Current.PlayerProgress));
					var num2 = MBRandom.RandomInt(2);
					var num3 = num2 == 0 ? MBRandom.RandomFloat : (float)(MBRandom.RandomFloat * (double)MBRandom.RandomFloat * MBRandom.RandomFloat * 4.0);
					var num4 = num2 == 0 ? (float)((num3 * 0.800000011920929) + 0.200000002980232) : 1f + num3;
					var randomFloat1 = MBRandom.RandomFloat;
					var randomFloat2 = MBRandom.RandomFloat;
					var randomFloat3 = MBRandom.RandomFloat;
					var f1 = pt.Stacks.Count > 0 ? pt.Stacks[0].MinValue + (num1 * num4 * randomFloat1 * (pt.Stacks[0].MaxValue - pt.Stacks[0].MinValue)) : 0.0f;
					var f2 = pt.Stacks.Count > 1 ? pt.Stacks[1].MinValue + (num1 * num4 * randomFloat2 * (pt.Stacks[1].MaxValue - pt.Stacks[1].MinValue)) : 0.0f;
					var f3 = pt.Stacks.Count > 2 ? pt.Stacks[2].MinValue + (num1 * num4 * randomFloat3 * (pt.Stacks[2].MaxValue - pt.Stacks[2].MinValue)) : 0.0f;
					f1 *= settings.PartySizeBanditMultiplier;
					f2 *= settings.PartySizeBanditMultiplier;
					f3 *= settings.PartySizeBanditMultiplier;
					__instance.AddElementToMemberRoster(pt.Stacks[0].Character, MBRandom.RoundRandomized(f1));
					if (pt.Stacks.Count > 1)
					{
						__instance.AddElementToMemberRoster(pt.Stacks[1].Character, MBRandom.RoundRandomized(f2));
					}

					if (pt.Stacks.Count <= 2)
					{
						return false;
					}

					__instance.AddElementToMemberRoster(pt.Stacks[2].Character, MBRandom.RoundRandomized(f3));
					return false;
				}

				if (__instance.IsVillager)
				{
					for (var index = 0; index < pt.Stacks.Count; ++index)
					{
						__instance.AddElementToMemberRoster(pt.Stacks[0].Character, (int)(troopNumberLimit * settings.PartySizeVillagerMultiplier));
					}

					return false;
				}

				if (__instance.IsMilitia)
				{
					if (troopNumberLimit < 0)
					{
						var gameProcess = Campaign.Current.PlayerProgress;
						for (var index = 0; index < pt.Stacks.Count; ++index)
						{
							var numberToAdd = (int)(gameProcess * (double)(pt.Stacks[index].MaxValue - pt.Stacks[index].MinValue)) + pt.Stacks[index].MinValue;
							__instance.AddElementToMemberRoster(pt.Stacks[index].Character, numberToAdd * (int)settings.PartySizeMilitiaMultiplier);
						}
					}
					else
					{
						for (var index1 = 0; index1 < troopNumberLimit; ++index1)
						{
							var index2 = -1;
							var num5 = 0.0f;
							for (var index3 = 0; index3 < pt.Stacks.Count; ++index3)
							{
								num5 += (float)((!__instance.IsGarrison || !pt.Stacks[index3].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index3].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index3].MaxValue + pt.Stacks[index3].MinValue) / 2.0));
							}

							var num6 = MBRandom.RandomFloat * num5;
							for (var index4 = 0; index4 < pt.Stacks.Count; ++index4)
							{
								num6 -= (float)((!__instance.IsGarrison || !pt.Stacks[index4].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index4].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index4].MaxValue + pt.Stacks[index4].MinValue) / 2.0));
								if (num6 < 0.0)
								{
									index2 = index4;
									break;
								}
							}
							if (index2 < 0)
							{
								index2 = 0;
							}

							__instance.AddElementToMemberRoster(pt.Stacks[index2].Character, 1 * (int)settings.PartySizeMilitiaMultiplier);
						}
					}

					return false;
				}
			}
			return true;
		}

		private static bool HandlePartyCarvanSize(ref MobileParty __instance, PartyTemplateObject pt, int troopNumberLimit)
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled)
			{
				if (__instance.IsCaravan && __instance.Party.Owner != null && __instance.Party.Owner == Hero.MainHero)
				{
					troopNumberLimit = settings.PlayerCaravanPartySize;
					if (troopNumberLimit < 0)
					{
						var gameProcess = Campaign.Current.PlayerProgress;
						for (var index = 0; index < pt.Stacks.Count; ++index)
						{
							var numberToAdd = (int)(gameProcess * (double)(pt.Stacks[index].MaxValue - pt.Stacks[index].MinValue)) + pt.Stacks[index].MinValue;
							__instance.AddElementToMemberRoster(pt.Stacks[index].Character, numberToAdd);
						}
					}
					else
					{
						for (var index1 = 0; index1 < troopNumberLimit; ++index1)
						{
							var index2 = -1;
							var num5 = 0.0f;
							for (var index3 = 0; index3 < pt.Stacks.Count; ++index3)
							{
								num5 += (float)((!__instance.IsGarrison || !pt.Stacks[index3].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index3].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index3].MaxValue + pt.Stacks[index3].MinValue) / 2.0));
							}

							var num6 = MBRandom.RandomFloat * num5;
							for (var index4 = 0; index4 < pt.Stacks.Count; ++index4)
							{
								num6 -= (float)((!__instance.IsGarrison || !pt.Stacks[index4].Character.IsRanged ? (!__instance.IsGarrison || pt.Stacks[index4].Character.IsMounted ? 1.0 : 2.0) : 6.0) * ((pt.Stacks[index4].MaxValue + pt.Stacks[index4].MinValue) / 2.0));
								if (num6 < 0.0)
								{
									index2 = index4;
									break;
								}
							}
							if (index2 < 0)
							{
								index2 = 0;
							}

							__instance.AddElementToMemberRoster(pt.Stacks[index2].Character, 1);
						}
					}

					return false;
				}
			}
			return true;
		}

		private static bool Prefix(ref MobileParty __instance, PartyTemplateObject pt, int troopNumberLimit)
		{
			var result = true;
			result = HandlePartySizeMultipliers(ref __instance, pt, troopNumberLimit);
			result = HandlePartyCarvanSize(ref __instance, pt, troopNumberLimit);
			return result;
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && (settings.PartySizeMultipliersEnabled || settings.PlayerCaravanPartySizeTweakEnabled);
	}
}
