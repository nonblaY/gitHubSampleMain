using UnityEngine;

public class TipsTrigger : MonoBehaviour
{
    [SerializeField] private Sprite tipSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Вошел в триггер подсказки");
            TipsManager.disableTipEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Вышел из триггера подсказки");
            TipsManager.displayTipEvent?.Invoke(tipSprite);
        }
    }
}
