using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Tile : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Vector3 position;
    public bool isPainted;

    void Awake()
    {
        position = transform.position;
        isPainted = false;
    }
}
