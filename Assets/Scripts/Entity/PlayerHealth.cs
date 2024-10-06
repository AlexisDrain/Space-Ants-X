using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public AudioClip clipPlayerHurt;
    public SpriteRenderer mySprite;
    // public List<AudioClip> clipDeath;
    public int defaultHealth = 3;

    [Header("read only")]
    public int _currentHealth = 3;

    void Awake()
    {

        _currentHealth = defaultHealth;
    }
    public void ResetHealth() {
        mySprite.enabled = true;
        _currentHealth = defaultHealth;
    }
    public void AddDamage(int damage=1) {
        if (_currentHealth <= 0) { // why we check this variable twice? player hitbox is still active at death.
            return;
        }
        _currentHealth -= damage;
        GameManager.SpawnLoudAudio(clipPlayerHurt);
        if(_currentHealth <= 0) {
            GameManager.KillPlayer();
            mySprite.enabled = false;
            return;
        }
    }
}
