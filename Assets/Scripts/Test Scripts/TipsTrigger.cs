using UnityEngine;

public class TipsTrigger : MonoBehaviour
{
    [SerializeField] private Sprite tipSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("����� � ������� ���������");
            TipsManager.disableTipEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("����� �� �������� ���������");
            TipsManager.displayTipEvent?.Invoke(tipSprite);
        }
    }
}
