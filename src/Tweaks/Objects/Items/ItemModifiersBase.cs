namespace Tweaks.Objects
{
	using Settings;
	using TaleWorlds.Core;
	using Utils;

	public class ItemModifiersBase
	{

		public ItemObject _item;
		public TweaksMCMSettings _settings;


		public ItemModifiersBase(ItemObject itemObject)
		{
			this._item = itemObject;
			this._settings = Statics._settings;
		}

		protected void SetItemsValue(int multiplePriceValue, float multiplier = 0.0f)
		{
			this.DebugValue(this._item, multiplePriceValue, multiplier);
			typeof(ItemObject).GetProperty("Value").SetValue(this._item, multiplePriceValue);
		}

		protected void SetItemsWeight(float multipleWeightValue, float multiplier = 0.0f)
		{
			this.DebugWeight(this._item, multipleWeightValue, multiplier);
			typeof(ItemObject).GetProperty("Weight").SetValue(this._item, multipleWeightValue);

		}
		protected void SetItemsStack(float multiplier = 0.0f)
		{
			var weaponData = this._item.PrimaryWeapon;
			var tmpMax = weaponData.MaxDataValue * multiplier;
			var newMax = (short)tmpMax;
			this.DebugStack(this._item, newMax, multiplier);
			typeof(WeaponComponentData).GetProperty("MaxDataValue").SetValue(weaponData, newMax);
		}


		protected void DebugValue(ItemObject item, float newValue, float multiplier)
		{
			if (this._settings.ItemDebugMode)
			{
				MessageUtil.MessageDebug(item.Name.ToString() + " Old Price: " + item.Value.ToString() + "  New Price: " + newValue.ToString() + " using multiplier: " + multiplier);
			}
		}

		protected void DebugWeight(ItemObject item, float newValue, float multiplier)
		{
			if (this._settings.ItemDebugMode)
			{
				MessageUtil.MessageDebug(item.Name.ToString() + " Old Weight: " + item.Weight.ToString() + "  New Weight: " + newValue.ToString() + " using multiplier: " + multiplier);
			}
		}

		protected void DebugStack(ItemObject item, float newValue, float multiplier)
		{
			if (this._settings.ItemDebugMode)
			{
				var weaponData = this._item.PrimaryWeapon;
				var tmpMax = weaponData.MaxDataValue * multiplier;
				var newMax = (short)tmpMax;
				MessageUtil.MessageDebug(item.Name.ToString() + " Old Stack: " + weaponData.MaxDataValue.ToString() + "  New Stack: " + newValue.ToString() + " using multiplier: " + multiplier);
			}
		}

	}


}
