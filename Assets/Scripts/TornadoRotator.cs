using UnityEngine;

public class TornadoRotator : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Transform centerToRotate;
    [SerializeField] private float rotationSpeed;
#pragma warning restore 0649

    private void Update ()
    {
        centerToRotate.Rotate (new Vector3 (0f, rotationSpeed * Time.deltaTime, 0f));
    }
}