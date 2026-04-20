using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneNavagaitor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var exiter = other.transform.GetComponent<ExitArea>();

        if (exiter)
        {
            SceneManager.LoadScene(exiter.GetScene());
        }
    }
}
