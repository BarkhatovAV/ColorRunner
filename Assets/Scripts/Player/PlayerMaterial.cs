using UnityEngine;
using UnityEngine.Events;

public class PlayerMaterial : MonoBehaviour
{
    [SerializeField] private Materials _materials;
    [SerializeField] private Renderer _renderer;

    private int startColorNumber = 0;
    private int _materialNumber;

    public int MaterialNumber => _materialNumber;

    public event UnityAction<Material> MaterialChanged;

    private void Start()
    {
        ChangeMaterial(startColorNumber);
    }

    public void ChangeMaterial(int materialNumber)
    {
        _renderer.material = _materials.GetMaterial(materialNumber);
        _materialNumber = materialNumber;
        MaterialChanged?.Invoke(_materials.GetMaterial(materialNumber));
    }
}
