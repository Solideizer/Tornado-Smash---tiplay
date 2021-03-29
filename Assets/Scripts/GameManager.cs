using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
#pragma warning disable 0649
    [SerializeField] private Slider slider;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI levelCompleteText;
    [SerializeField] private Image finishPanel;
#pragma warning restore 0649
    private GameObject[] _objects;
    private int _totalObjectCount;
    private int _destroyedObjectCount = 0;

    void Start ()
    {
        GetObjectCount ();
        CreateSingletonInstance ();
    }
    private void Update ()
    {
        UpdateProgressBar ();
    }
    private void GetObjectCount ()
    {
        _objects = GameObject.FindGameObjectsWithTag ("Collectibles");
        _totalObjectCount = _objects.Length;
        slider.maxValue = _totalObjectCount;
    }

    private void CreateSingletonInstance ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy (gameObject);
        }
    }

    public void HandleObjectCount ()
    {
        _destroyedObjectCount++;
        CheckIfLevelComplete ();
    }
    private void CheckIfLevelComplete ()
    {
        if (_destroyedObjectCount == _totalObjectCount)
        {
            ActivateUI ();
        }
    }

    private void UpdateProgressBar ()
    {
        slider.value = _destroyedObjectCount;
    }
    public void Restart ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
    private void ActivateUI ()
    {
        finishPanel.gameObject.SetActive (true);
        restartButton.gameObject.SetActive (true);
        levelCompleteText.gameObject.SetActive (true);
    }
}