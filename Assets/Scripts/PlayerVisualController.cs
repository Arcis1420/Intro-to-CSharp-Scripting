using UnityEngine;
using UnityEngine.U2D;

public class PlayerVisualController : MonoBehaviour
{
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GameManager.OnAfterStateChanged += HandleStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnAfterStateChanged -= HandleStateChange;
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                _sprite.color = new Color(1f, 104f / 255f, 0f);
                break;

            case GameState.Paused:
                _sprite.color = Color.gray;
                break;

            case GameState.FailScreen:
                _sprite.color = Color.gray;
                break;
        }
    }
}