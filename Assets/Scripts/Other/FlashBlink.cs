using UnityEngine;
using UnityEngine.PlayerLoop;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _damagableObject;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDuration = 0.2f;

    private float blinkTimer;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        isBlinking = true;
    }

    private void Start()
    {
        if (_damagableObject is Player player)
        {
            player.OnFlashBlink += DamagableObject_OnFlashBlink;
        }
    }

    private void DamagableObject_OnFlashBlink(object sender, System.EventArgs e)
    {
        SetBlinkingMaterial();
    }

    void Update()
    {
        if (isBlinking)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer < 0)
            {
                SetDefaultMaterial();
            }
        }       
    }


    private void SetBlinkingMaterial()
    {
        blinkTimer = blinkDuration;
        spriteRenderer.material = blinkMaterial;
    }

    private void SetDefaultMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    public void StopBlinking()
    {
        SetDefaultMaterial();
        isBlinking = false;
    }

    private void OnDestroy()
    {
        if (_damagableObject is Player player)
        {
            player.OnFlashBlink -= DamagableObject_OnFlashBlink;
        }
    }
}
