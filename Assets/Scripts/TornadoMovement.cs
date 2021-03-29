using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    #region Variable Declarations
#pragma warning disable 0649
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform leftBoundry;
    [SerializeField] private Transform rightBoundry;
    [SerializeField] private Transform topBoundry;
    [SerializeField] private Transform downBoundry;
#pragma warning restore 0649
    private float _minX;
    private float _maxX;
    private float _minZ;
    private float _maxZ;
    private Vector3 _worldPosition;
    private Camera _mainCam;
    #endregion
    void Start ()
    {
        _mainCam = Camera.main;
        AssignBoundryValues ();
    }

    void Update ()
    {
        HandleMovement ();
    }

    private void HandleMovement ()
    {
        var plane = new Plane (Vector3.up, 0);
        var distance = 100f;

        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (plane.Raycast (ray, out distance))
        {
            _worldPosition = ray.GetPoint (distance);
        }
        transform.position = _worldPosition * sensitivity;
        transform.position = new Vector3 (
            Mathf.Clamp (transform.position.x, _minX, _maxX),
            transform.position.y,
            Mathf.Clamp (transform.position.z, _minZ, _maxZ)
        );
    }

    private void AssignBoundryValues ()
    {
        _minX = leftBoundry.position.x;
        _maxX = rightBoundry.position.x;
        _minZ = downBoundry.position.z;
        _maxZ = topBoundry.position.z;
    }

}