using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class MissileHealth : MonoBehaviour
{
    public int defaultHealth = 3;
    [Header("read only")]
    public int _currentHealth = 3;
    // Start is called before the first frame update
    void Start()
    {

        GameManager.changeDayEvent.AddListener(ResetHealth);
        _currentHealth = defaultHealth;
    }
    public void ResetHealth() {
        _currentHealth = defaultHealth;
    }


    public void AddDamage(int damage = 1) {
        _currentHealth -= damage;
        if (_currentHealth <= 0) {
            GameManager.MissileDestruct();
            ResetHealth();
        }
    }
}
