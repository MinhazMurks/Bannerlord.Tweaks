namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class ItemModifiersBase
	{
		protected ItemObject Item { get; }
		protected float MultiplierPrice { get; set; } = 1.0f;
		protected float MultiplierWeight { get; set; } = 1.0f;
		protected float MultiplierStack { get; set; } = 1.0f;

		protected ItemModifiersBase(ItemObject itemObject) => this.Item = itemObject;

		protected void SetItemsValue(int multiplePriceValue, float multiplier = 0.0f)
		{
			this.DebugValue(this.Item, multiplePriceValue, multiplier);
			typeof(ItemObject).GetProperty("Value")?.SetValue(this.Item, multiplePriceValue);
		}

		protected void SetItemsWeight(float multipleWeightValue, float multiplier = 0.0f)
		{
			this.DebugWeight(this.Item, multipleWeightValue, multiplier);
			typeof(ItemObject).GetProperty("Weight")?.SetValue(this.Item, multipleWeightValue);

		}
		protected void SetItemsStack(float multiplier = 0.0f)
		{
			var weaponData = this.Item.PrimaryWeapon;
			var tmpMax = weaponData.MaxDataValue * multiplier;
			var newMax = (short)tmpMax;
			this.DebugStack(this.Item, newMax, multiplier);
			typeof(WeaponComponentData).GetProperty("MaxDataValue")?.SetValue(weaponData, newMax);
		}


		private void DebugValue(ItemObject item, float newValue, float multiplier)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug(item.Name + " Old Price: " + item.Value + "  New Price: " + newValue + " using multiplier: " + multiplier);
			}
		}

		private void DebugWeight(ItemObject item, float newValue, float multiplier)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug(item.Name + " Old Weight: " + item.Weight + "  New Weight: " + newValue + " using multiplier: " + multiplier);
			}
		}

		private void DebugStack(ItemObject item, float newValue, float multiplier)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				var weaponData = this.Item.PrimaryWeapon;
				var tmpMax = weaponData.MaxDataValue * multiplier;
				var newMax = (short)tmpMax;
				MessageUtil.MessageDebug(item.Name + " Old Stack: " + weaponData.MaxDataValue + "  New Stack: " + newValue + " using multiplier: " + multiplier);
			}
		}

	}


}
