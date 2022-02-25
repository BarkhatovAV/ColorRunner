using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    public Material GetMaterial(int numberMaterial)
    {
        
        return _materials[numberMaterial];
    }
}
