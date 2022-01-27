using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_FarmEnemyBomb : MonoBehaviour
{
    //
    [SerializeField] private S_EnemyFarm S_EnemyFarm;
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            S_EnemyFarm.Dead();
        }
    }
}
