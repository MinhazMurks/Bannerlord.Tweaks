namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Objects.Experience;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.Core;


	[HarmonyPatch(typeof(Hero))]//, "AddSkillXp"
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class HeroPatch
	{
		[HarmonyPrefix]
		[HarmonyPatch("AddSkillXp")]
		public static void Prefix(Hero? __instance, SkillObject? skill, ref float xpAmount)
		{
			if (__instance != null && skill != null && __instance.HeroDeveloper != null && skill.GetName() != null && Hero.MainHero != null)
			{
				var kaosesSkillXp = new TweaksAddSkillXp(__instance, skill, xpAmount);
				if (kaosesSkillXp.HasModifiedXp())
				{
					xpAmount = kaosesSkillXp.GetNewSkillXp();
				}
			}
		}
	}
}
