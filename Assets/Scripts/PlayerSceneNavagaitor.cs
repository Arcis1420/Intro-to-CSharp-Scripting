using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneNavigator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var exiter = other.GetComponent<ExitArea>();

        if (exiter != null)
        {
            SceneManager.LoadScene(exiter.GetScene());
        }
    }
}