using Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FuckRusty
{
    public class FuckRusty : Mod
    {
		public static List<AudioClip> hitClips = new List<AudioClip>();
		static Stream stream;
		static Assembly assembly = Assembly.GetExecutingAssembly();
		AudioSource audioPlayer;
		System.Random random;

		public static void LoadHitAudioFromStream(string path)
		{
			
			stream = assembly.GetManifestResourceStream(path);
			byte[] buffer = new byte[stream.Length];
			stream.Read(buffer, 0, buffer.Length);
			hitClips.Add(WavUtility.ToAudioClip(buffer));
		}

		public static void LoadHitAudio()
		{
			LoadHitAudioFromStream("FuckRusty.assets.damnit.wav");
			LoadHitAudioFromStream("FuckRusty.assets.eat my ass.wav");
			LoadHitAudioFromStream("FuckRusty.assets.eat my shit.wav");
			LoadHitAudioFromStream("FuckRusty.assets.fucker.wav");
			LoadHitAudioFromStream("FuckRusty.assets.I bet you eat soup with a fork.wav");
			LoadHitAudioFromStream("FuckRusty.assets.oh cockass.wav");
			LoadHitAudioFromStream("FuckRusty.assets.okay that was bullshit.wav");
			LoadHitAudioFromStream("FuckRusty.assets.shit.wav");
			LoadHitAudioFromStream("FuckRusty.assets.what the fuck.wav");
			LoadHitAudioFromStream("FuckRusty.assets.aaaah.wav");
			LoadHitAudioFromStream("FuckRusty.assets.aaah.wav");
			LoadHitAudioFromStream("FuckRusty.assets.aah.wav");
			LoadHitAudioFromStream("FuckRusty.assets.fuck out of here.wav");
			LoadHitAudioFromStream("FuckRusty.assets.fuck this bullshit.wav");
			LoadHitAudioFromStream("FuckRusty.assets.fucking asshole.wav");
			LoadHitAudioFromStream("FuckRusty.assets.get the fuck out of my way.wav");
			LoadHitAudioFromStream("FuckRusty.assets.go fuck yourself are you kidding me.wav");
			LoadHitAudioFromStream("FuckRusty.assets.holy fuck 1.wav");
			LoadHitAudioFromStream("FuckRusty.assets.holy fuck 2.wav");
			LoadHitAudioFromStream("FuckRusty.assets.holy fuck.wav");
			LoadHitAudioFromStream("FuckRusty.assets.holy shit.wav");
			LoadHitAudioFromStream("FuckRusty.assets.jezus fucking hell.wav");
			LoadHitAudioFromStream("FuckRusty.assets.shit 1.wav");
			LoadHitAudioFromStream("FuckRusty.assets.spaghetti sauce.wav");
			LoadHitAudioFromStream("FuckRusty.assets.titsville.wav");
			LoadHitAudioFromStream("FuckRusty.assets.you are just fucked.wav");
			LoadHitAudioFromStream("FuckRusty.assets.you ass.wav");
			stream.Dispose();
		}

		public override void Initialize()
		{
			LoadHitAudio();
			GameObject audioHolder = UnityEngine.Object.Instantiate(new GameObject());
			audioHolder.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(audioHolder);
			audioPlayer = audioHolder.GetComponent<AudioSource>();

			random = new System.Random();

			ModHooks.AfterTakeDamageHook += PlaySound;
			
			On.PlayMakerFSM.Awake += FSMAwake;
		}

		private int PlaySound(int hazardType, int damageAmount)
		{
			audioPlayer.clip = hitClips[random.Next(0, hitClips.Count)];
			audioPlayer.Play();
			return damageAmount;
		}
		
		private void FSMAwake(On.PlayMakerFSM.orig_Awake orig, PlayMakerFSM self)
		{
			orig(self);
			
			if (self.FsmName == "Conversation Control")
			{
			    if (self.gameObject.name.StartsWith("Relic Dealer")) {
				self.GetAction<PlayerDataBoolTest>("Convo Choice", 2).isFalse = FsmEvent.GetFsmEvent("DUNG");
			    }
			}
			else if (self.FsmName == "Shop Region")
			{
			    self.GetAction<PlayerDataBoolTest>("Check Relics", 0).isFalse = FsmEvent.GetFsmEvent("DUNG");
			}
		}
	}
}
