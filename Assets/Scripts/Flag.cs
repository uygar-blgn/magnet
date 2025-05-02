using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject youWon;
    public GameObject quit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            youWon.SetActive(true);
            quit.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
