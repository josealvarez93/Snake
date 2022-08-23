using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
 private void OnMouseDown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
