namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.Core;

	[HarmonyPatch(typeof(WeaponComponentData), "CanHitMultipleTargets", MethodType.Getter)]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class WeaponComponentDataPatch
	{
		private static void Postfix(ref bool __result, WeaponComponentData __instance)
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				var twoHanded = (settings.TwoHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.TwoHandedAxe) ||
				                __instance.WeaponClass is WeaponClass.TwoHandedMace or WeaponClass.TwoHandedPolearm or WeaponClass.TwoHandedSword;

				var oneHanded = (settings.SingleHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.OneHandedSword) ||
				                __instance.WeaponClass is WeaponClass.OneHandedPolearm or WeaponClass.OneHandedAxe;

				var all = settings.AllWeaponsSliceThroughEnabled;

				__result = twoHanded || oneHanded || all;
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {SliceThroughEnabled: true};
	}
}
