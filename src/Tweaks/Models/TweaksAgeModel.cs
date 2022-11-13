namespace Tweaks.Models
{
	using System.Collections.Generic;
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;

	public class TweaksAgeModel : DefaultAgeModel
	{
		public override int BecomeInfantAge => Statics.GetSettingsOrThrow() is {AgeTweaksEnabled: true} settings ? settings.BecomeInfantAge : base.BecomeInfantAge;

		public override int BecomeChildAge => Statics.GetSettingsOrThrow() is {AgeTweaksEnabled: true} settings ? settings.BecomeChildAge : base.BecomeChildAge;

		public override int BecomeTeenagerAge => Statics.GetSettingsOrThrow() is {AgeTweaksEnabled: true} settings ? settings.BecomeTeenagerAge : base.BecomeTeenagerAge;

		public override int HeroComesOfAge => Statics.GetSettingsOrThrow() is {AgeTweaksEnabled: true} settings ? settings.HeroComesOfAge : base.HeroComesOfAge;

		public override int BecomeOldAge => Statics.GetSettingsOrThrow() is {AgeTweaksEnabled: true} settings ? settings.BecomeOldAge : base.BecomeOldAge;

		public override int MaxAge => Statics.GetSettingsOrThrow() is {AgeTweaksEnabled: true} settings ? settings.MaxAge : base.MaxAge;

		public IEnumerable<string> GetConfigErrors()
		{
			if (this.MaxAge <= this.BecomeOldAge)
			{
				yield return "\'Max Age\' must be greater than \'Become Old \'Age\'.";
			}

			if (this.BecomeOldAge <= this.HeroComesOfAge)
			{
				yield return "\'Become Old Age\' must be greater than \'Hero Comes Of Age\'.";
			}

			if (this.HeroComesOfAge <= this.BecomeTeenagerAge)
			{
				yield return "\'Hero Comes Of Age\' must be greater than \'Become Teenager Age\'.";
			}

			if (this.BecomeTeenagerAge <= this.BecomeChildAge)
			{
				yield return "\'Become Teenager Age\' must be greater than \'Become Child Age\'";
			}

			if (this.BecomeChildAge <= this.BecomeInfantAge)
			{
				yield return "\'Become Child Age\' must be greater than \'Become Infant Age\'";
			}
		}
	}
}
