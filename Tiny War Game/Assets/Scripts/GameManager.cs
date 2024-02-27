using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    public EventsPanelManager eventsPanelManager;
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

                int redTeamScore = eventsPanelManager.redTeamScore; 
                int blueTeamScore = eventsPanelManager.blueTeamScore;

                if (eventsPanelManager != null)
                {
                    eventsPanelManager.SpawnRedBubble("Red", "Game Over! Red team Score: " + redTeamScore);
                    eventsPanelManager.SpawnBlueBubble("Blue", "Game Over! Blue team Score: " + blueTeamScore);
                }
            }
            yield return null;
        }
    }
}
