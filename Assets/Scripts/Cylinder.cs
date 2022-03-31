using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private Materials _gameMaterial;
    [SerializeField] private int _materialNumber;
    [SerializeField] private Renderer _renderer;

    public int MaterialNumer => _materialNumber;

    private void Start()
    {
        _renderer.material = _gameMaterial.GetCylinderMaterial(_materialNumber);
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
