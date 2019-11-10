using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FixedZ : MonoBehaviour
{
    public float Z;
    public bool IsLocal = false;
    private void Awake()
    {
        if (IsLocal)
            transform.localPosition = transform.localPosition.Set(z: Z);
        else
            transform.position = transform.position.Set(z: Z);
    }

    private void LateUpdate()
    {
        if (IsLocal)
            transform.localPosition = transform.localPosition.Set(z: Z);
        else
            transform.position = transform.position.Set(z: Z);
    }

}
