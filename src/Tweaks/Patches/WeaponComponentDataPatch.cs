namespace Tweaks.Patches
{
	using HarmonyLib;
	using TaleWorlds.Core;
	using Tweaks.Settings;

	[HarmonyPatch(typeof(WeaponComponentData), "CanHitMultipleTargets", MethodType.Getter)]
	internal class WeaponComponentDataPatch
	{
		private static void Postfix(ref bool __result, WeaponComponentData __instance)
		{
			if (TweaksMCMSettings.Instance is { } settings)
			{
				var twoHanded = (settings.TwoHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.TwoHandedAxe) ||
					__instance.WeaponClass == WeaponClass.TwoHandedMace || __instance.WeaponClass == WeaponClass.TwoHandedPolearm ||
					__instance.WeaponClass == WeaponClass.TwoHandedSword;

				var oneHanded = (settings.SingleHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.OneHandedSword) ||
					__instance.WeaponClass == WeaponClass.OneHandedPolearm || __instance.WeaponClass == WeaponClass.OneHandedAxe;

				var all = settings.AllWeaponsSliceThroughEnabled;

				__result = twoHanded || oneHanded || all;
			}
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.SliceThroughEnabled;
	}
}
