using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    [SerializeField] private Sword sword;
    [SerializeField] private Animator animator; // Перетащи сюда Animator из PlayerVisual


    private const string ATTACK = "Attack";

    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void OnDestroy()
    {
        sword.OnSwordSwing -= Sword_OnSwordSwing;
    }
}