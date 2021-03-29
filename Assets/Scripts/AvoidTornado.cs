using UnityEngine;

public class AvoidTornado : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Transform tornado;
    [SerializeField] private float moveSpeed;

#pragma warning restore 0649
    void Update ()
    {
        var dir = transform.position - tornado.position;

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * 40f);
        transform.Translate (-dir * moveSpeed * Time.deltaTime);
    }
}