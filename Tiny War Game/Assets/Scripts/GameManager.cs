using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    void Start()
    {
        StartCoroutine(DelayGameManager(10f));
    }

    IEnumerator DelayGameManager(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        while (!gameOver)
        {
            int blueUnitCount = GameObject.FindGameObjectsWithTag("BlueUnit").Length;
            int redUnitCount = GameObject.FindGameObjectsWithTag("RedUnit").Length;

            if (blueUnitCount == 0 || redUnitCount == 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                Time.timeScale = 0f;
            }
            yield return null;
        }
    }
}
