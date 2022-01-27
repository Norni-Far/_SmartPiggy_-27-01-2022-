using UnityEngine;

public class S_Bomb : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPlace_Prefab;

    public void Explosion()   // запуск анимацией, событие 
    {
        gameObject.tag = "Bomb";
        Instantiate(ExplosionPlace_Prefab, transform.position, transform.rotation);
    }

    public void Destroy_Object()
    {
        GameObject.Destroy(gameObject);
    }



}
