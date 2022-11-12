namespace Tweaks.Patches
{
	using System.Linq;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;
	using TaleWorlds.Core;
	using Tweaks.Settings;

	[HarmonyPatch(typeof(RecruitmentCampaignBehavior), "OnSettlementEntered")]
	internal class RecruitmentCampaignBehaviorPatch
	{
		private static void Postfix()
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.BalancingUpgradeTroopsTweaksEnabled)
			{
				foreach (var settlement in from settlement in Campaign.Current.Settlements
										   where settlement.OwnerClan != null
		&& settlement.OwnerClan.Kingdom != null && ((settlement.IsTown && !settlement.Town.InRebelliousState)
		|| (settlement.IsVillage && !settlement.Village.Bound.Town.InRebelliousState))
										   select settlement)
				{
					var num = 0f;
					if (settings.KingdomBalanceStrengthVanEnabled)
					{
						num = settlement.OwnerClan.Kingdom.StringId switch
						{
							"vlandia" => settings.VlandiaBoost,
							"battania" => settings.BattaniaBoost,
							"empire" => settings.Empire_N_Boost,
							"empire_s" => settings.Empire_S_Boost,
							"empire_w" => settings.Empire_W_Boost,
							"sturgia" => settings.SturgiaBoost,
							"khuzait" => settings.KhuzaitBoost,
							"aserai" => settings.AseraiBoost,
							_ => 0f
						};
					}
					if (settings.KingdomBalanceStrengthCEKEnabled)
					{
						num = settlement.OwnerClan.Kingdom.StringId switch
						{
							"nordlings" => settings.NordlingsBoost,
							"vagir" => settings.VagirBoost,
							"royalist_vlandia" => settings.RoyalistVlandiaBoost,
							"apolssaly" => settings.ApolssalyBoost,
							"lyrion" => settings.LyrionBoost,
							"rebel_khuzait" => settings.RebelKhuzaitBoost,
							"paleician" => settings.PaleicianBoost,
							"ariorum" => settings.AriorumBoost,
							"vlandia" => settings.Vlandia_CEK_Boost,
							"battania" => settings.Battania_CEK_Boost,
							"empire" => settings.Empire_CEK_Boost,
							"empire_s" => settings.Empire_S_CEK_Boost,
							"empire_w" => settings.Empire_W_CEK_Boost,
							"sturgia" => settings.Sturgia_CEK_Boost,
							"khuzait" => settings.Khuzait_CEK_Boost,
							"aserai" => settings.Aserai_CEK_Boost,
							_ => 0f
						};
					}
					if (num == 0f && settlement.OwnerClan.Kingdom.Leader == Hero.MainHero)
					{
						num = settings.KingdomBalanceStrengthCEKEnabled ? settings.Player_CEK_Boost : settings.PlayerBoost;
					}

					foreach (var hero in settlement.Notables)
					{
						if (hero.CanHaveRecruits)
						{
							for (var i = 0; i < 6; i++)
							{
								if (hero.VolunteerTypes[i] != null && MBRandom.RandomFloat < (num * 0.5) && hero.VolunteerTypes[i].UpgradeTargets != null
									&& hero.VolunteerTypes[i].Level < 20)
								{
									var cultureObject = (hero.CurrentSettlement != null) ? hero.CurrentSettlement.Culture : hero.Clan.Culture;
									var basicTroop = cultureObject.BasicTroop;
									var basicVolunteer = Campaign.Current.Models.VolunteerModel.GetBasicVolunteer(hero);
									var flag2 = basicVolunteer != basicTroop;

									/*if (hero.VolunteerTypes[i] == basicTroop && HeroShouldGiveEliteTroop(hero))
									{
										hero.VolunteerTypes[i] = cultureObject.EliteBasicTroop;
									}*/
									if (hero.VolunteerTypes[i] == basicTroop && flag2)
									{
										hero.VolunteerTypes[i] = basicVolunteer;
									}
									else
									{
										hero.VolunteerTypes[i] = hero.VolunteerTypes[i].UpgradeTargets[MBRandom.RandomInt(hero.VolunteerTypes[i].UpgradeTargets.Length)];
									}
								}
							}

						}
					}
				}
			}
		}


		// Token: 0x0600003C RID: 60 RVA: 0x000046A4 File Offset: 0x000028A4
		public static bool HeroShouldGiveEliteTroop(Hero sellerHero) => (sellerHero.IsRuralNotable || sellerHero.IsHeadman) && sellerHero.Power >= 200f;

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.KingdomBalanceStrengthEnabled;
	}
}
