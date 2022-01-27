using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E_Pit : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void PitAnim()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
