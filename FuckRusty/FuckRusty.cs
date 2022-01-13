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
			stream.Dispose();
		}

		public override void Initialize()
		{
			LoadHitAudio();
			GameObject audioHolder = UnityEngine.Object.Instantiate(new GameObject());
			audioHolder.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(audioHolder);
			audioPlayer = audioHolder.GetComponent<AudioSource>();

			ModHooks.AfterTakeDamageHook += PlaySound;
		}

		private int PlaySound(int hazardType, int damageAmount)
		{
			audioPlayer.clip = hitClips[UnityEngine.Random.Range(0, hitClips.Count - 1)];
			audioPlayer.Play();
			return damageAmount;
		}
	}
}
