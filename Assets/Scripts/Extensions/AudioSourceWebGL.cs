using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceWebGL {

	/*
	 * 
	 * To Remove audio clicking noise
	 * 
	 */

	public static void StopWebGL(this AudioSource audioSource) {
		
		audioSource.volume = 0f;
		audioSource.Stop();
	}
	public static void PlayWebGL(this AudioSource audioSource, AudioClip newAudioClip = null, float newVolume = 1f) {

        /*
		if (newVolume == -1f) {

			newVolume = audioSource.volume;
		}
		*/
#if UNITY_EDITOR
        if (audioSource.gameObject.activeSelf == false)
        {
            Debug.LogWarning("PlayWebGL is set in an inactive object. name: " + audioSource.name);
        }
#endif
        audioSource.volume = 0f;
		audioSource.Stop();


		if (newAudioClip != null) {
			audioSource.clip = newAudioClip;
		}

		audioSource.volume = newVolume;
		audioSource.Play();
	}
}
