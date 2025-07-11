using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    int actualZone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualZone = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(actualZone + 1);
        }
    }
}
