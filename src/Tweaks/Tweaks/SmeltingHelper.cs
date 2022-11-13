﻿namespace Tweaks.Tweaks
{
	using System.Collections.Generic;
	using System.Linq;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;
	using TaleWorlds.Core;
	using Utils;

	internal class SmeltingHelper
	{
		public static IEnumerable<CraftingPiece> GetNewPartsFromSmelting(ItemObject item)
		{
			if (item == null)
			{
				MessageUtil.ColorRedMessage("Error in Bannerlord Tweaks SmeltingHelper. Did not find" + item!.Name);
			}

			return item.WeaponDesign.UsedPieces.Select(x => x.CraftingPiece).Where(x => x != null && x.IsValid && !Campaign.Current.GetCampaignBehavior<CraftingCampaignBehavior>().IsOpened(x, item.WeaponDesign.Template));
		}
	}
}
