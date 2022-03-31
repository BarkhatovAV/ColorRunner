using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<Material> _cylinderMaterials;

    public Material GetMaterial(int numberMaterial)
    {
        return _materials[numberMaterial];
    }

    public Material GetCylinderMaterial(int numberMaterial)
    {
        return _cylinderMaterials[numberMaterial];
    }
}
