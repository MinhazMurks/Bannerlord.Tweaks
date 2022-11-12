namespace Tweaks.Utils
{
	using System;
	using System.IO;

	internal class Logging
	{
		public static string PrePrend = "";

		public static void Lm(string message)
		{
			try
			{
				using var sw = File.AppendText(Statics.LogPath);
				sw.WriteLine(PrePrend + " : " + DateTime.Now.ToString() + " : " + message + "\r\n");
			}
			catch
			{

			}
		}

	}
}
