using Unity.VisualScripting;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float delay = 0f;

    private void Start()
    {
        Destroy(this.gameObject);
    }
}