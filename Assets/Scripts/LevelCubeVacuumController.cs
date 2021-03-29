using DG.Tweening;
using UnityEngine;
public class LevelCubeVacuumController : MonoBehaviour
{
    #region Variable Declarations
#pragma warning disable 0649
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float risingSpeed;
    [SerializeField] private float risingRotationSpeed;
    [SerializeField] private float vacuumSpeed;
#pragma warning restore 0649
    private CubeStates _currentState;
    private Rigidbody _cubeRB;

    private enum CubeStates
    {
        HOLD,
        VACUUM,
        RISE
    }
    #endregion
    private void Start ()
    {
        _cubeRB = GetComponent<Rigidbody> ();
    }

    private void Update ()
    {
        HandleStates ();
    }

    private void HandleStates ()
    {
        switch (_currentState)
        {
            case CubeStates.HOLD:
                break;
            case CubeStates.VACUUM:
                HandleInVacuum ();
                break;
            case CubeStates.RISE:
                HandleRising ();
                break;
            default:
                break;
        }
    }

    void HandleInVacuum ()
    {
        transform.Rotate (new Vector3 (0f, (risingRotationSpeed / 2f) * Time.deltaTime, (risingRotationSpeed / 2f) * Time.deltaTime), Space.Self);

    }
    void HandleRising ()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y + (risingSpeed * Time.deltaTime), transform.position.z);
        transform.Rotate (new Vector3 (0f, risingRotationSpeed * Time.deltaTime, risingRotationSpeed * Time.deltaTime), Space.Self);

    }

    private void TriggerShrink ()
    {
        transform.DOScale (0.03f, shrinkSpeed).SetDelay (0.1f).OnComplete (() =>
        {
            GameManager.instance.HandleObjectCount ();
            Destroy (gameObject);
        });
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("TornadoInnerTrigger"))
        {
            if (_currentState != CubeStates.RISE)
            {
                _currentState = CubeStates.RISE;
                transform.parent = other.transform;
                _cubeRB.useGravity = false;
                TriggerShrink ();

            }
        }
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag ("TornadoVacuumCollider"))
        {
            if (_currentState != CubeStates.RISE)
            {
                _currentState = CubeStates.VACUUM;

                Vector3 dir = (other.GetComponent<TornadoVacuumCollController> ().tornadoCenter.position - transform.position).normalized;
                _cubeRB.AddForce (dir * vacuumSpeed * Time.deltaTime);
            }
        }

    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag ("TornadoVacuumCollider"))
        {
            if (_currentState != CubeStates.RISE)
            {
                _currentState = CubeStates.HOLD;
            }
        }

    }

}