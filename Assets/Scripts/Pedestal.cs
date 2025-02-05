using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private Material _ownerTurnMaterial;
    [SerializeField] private Material _defaultMaterial;

    [SerializeField] private MeshRenderer[] _meshes;

    public void SetOwnerTurn()
    {
        foreach (var mesh in _meshes)
            mesh.material = _ownerTurnMaterial;
    }

    public void UnsetOwnerTurn()
    {
        foreach (var mesh in _meshes)
            mesh.material = _defaultMaterial;
    }
}
