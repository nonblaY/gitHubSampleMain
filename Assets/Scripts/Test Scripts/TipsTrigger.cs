using UnityEngine;

public class TipsTrigger : MonoBehaviour
{
    [SerializeField] private Sprite tipSprite;
    [SerializeField] private TipsManager tipsManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("����� � ������� ���������");
            tipsManager.ActivateTipsManager();
            TipsManager.disableTipEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("����� �� �������� ���������");
            TipsManager.displayTipEvent?.Invoke(tipSprite);
            tipsManager.DeactivateTipsManager();
        }
    }
}
