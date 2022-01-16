using Modding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FuckRusty
{
	class LanguagePatcher
	{
		static Dictionary<string, string> translations = new Dictionary<string, string>();
		public static void Initialize()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			Stream s = a.GetManifestResourceStream("FuckRusty.assets.language.json");
			byte[] buffer = new byte[s.Length];
			s.Read(buffer, 0, buffer.Length);
			translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(Encoding.UTF8.GetString(buffer));
			ModHooks.LanguageGetHook += PatchLanguage;
		}

		public static string PatchLanguage(string key, string sheetTitle, string orig)
		{
			if (translations.ContainsKey(key))
			{
				return translations[key];
			}
			return orig;
		}
	}
}
