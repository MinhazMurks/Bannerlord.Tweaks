namespace Tweaks.Patches
{
	using HarmonyLib;
	using Objects.Experience;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.Core;

	internal class HeroPatcher
	{
		[HarmonyPatch(typeof(Hero))]//, "AddSkillXp"
		public class Patches
		{
			[HarmonyPrefix]
			[HarmonyPatch("AddSkillXp")]
			public static void Prefix(Hero __instance, SkillObject skill, ref float xpAmount)
			{
				if (__instance != null && skill != null && __instance.HeroDeveloper != null && skill.GetName() != null && Hero.MainHero != null)
				{
					var kaosesSkillXp = new TweaksAddSkillXp(__instance, skill, xpAmount);
					if (kaosesSkillXp.HasModifiedXP())
					{
						xpAmount = kaosesSkillXp.GetNewSkillXp();
					}
				}
			}
		}
	}
}
