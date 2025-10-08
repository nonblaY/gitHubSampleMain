using UnityEngine;

public class DestructibleObjectVisual : MonoBehaviour
{
    [SerializeField] private DestructibleObject _destructibleObject;
    [SerializeField] private GameObject _rockDeathVFXPrefab;


    private void Start()
    {
        _destructibleObject.OnDestructibleTakeDamage += DestructibleObject_OmDestructibleTakeDamage;
    }
    private void DestructibleObject_OmDestructibleTakeDamage(object sender, System.EventArgs e)
    {
        showDeathVFX();
    }


    private void showDeathVFX()
    {
        Instantiate(_rockDeathVFXPrefab, _destructibleObject.transform.position, Quaternion.identity);
    }


    private void OnDestroy()
    {
        _destructibleObject.OnDestructibleTakeDamage -= DestructibleObject_OmDestructibleTakeDamage;
    }
}
