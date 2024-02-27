using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class EventsPanelManager : MonoBehaviour
{
    public GameObject eventsPanel;
    private UnitSpawner unitSpawner;

    public GameObject blueChatBubblePrefab;
    public GameObject redChatBubblePrefab;

    public int blueTeamScore;
    public int redTeamScore;


    void Start()
    {
        CloseEventsPanel();
        unitSpawner = FindObjectOfType<UnitSpawner>();
    }

    public void OpenEventsPanel()
    {
        eventsPanel.SetActive(true);
    }

    public void CloseEventsPanel()
    {
        eventsPanel.SetActive(false);
    }

    public void SpawnBlueBubble(string username, string chatText){

        if(unitSpawner != null)
        {
            DateTime messageTime = DateTime.Now;
            SpawnChatBubble(blueChatBubblePrefab, username, chatText, "1 sec ago");
        }
    }

    public void SpawnRedBubble(string username, string chatText)
    {
        if (unitSpawner != null)
        {
            DateTime messageTime = DateTime.Now;
            SpawnChatBubble(redChatBubblePrefab, username, chatText, "1 sec ago");
        }
    }

    public void UpdateScore(string team, int amount)
    {
        if (team == "Red")
        {
            redTeamScore += amount;
            SpawnRedBubble("Red", "Red score updated by " + amount.ToString());
        }
        else if (team == "Blue")
        {
            blueTeamScore += amount;
            SpawnBlueBubble("Blue", "Blue score updated by " + amount.ToString());
        }
    }

    private void SpawnChatBubble(GameObject chatBubblePrefab,string username, string chatText, string displayTime)
    {
        GameObject contentObject = eventsPanel.transform.Find("Scroll View/Viewport/Content")?.gameObject;
        
        if (contentObject != null)
            {

                GameObject chatBubble = Instantiate(chatBubblePrefab,contentObject.transform);

                Transform usernameTransform = chatBubble.transform.GetChild(1);

                if (usernameTransform != null)
                {
                    TextMeshProUGUI usernameComponent = usernameTransform.GetComponent<TextMeshProUGUI>();
                    if (usernameComponent != null)
                    {
                        usernameComponent.text = username;
                    }
                }

                Transform textTransform = chatBubble.transform.GetChild(2);
                if (textTransform != null)
                {
                    TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
                        if (textComponent != null)
                        {
                            textComponent.text = chatText;
                        }

                } 

                Transform timeTransform = chatBubble.transform.GetChild(3);
                if (timeTransform != null)
                {
                    TextMeshProUGUI timeComponent = timeTransform.GetComponent<TextMeshProUGUI>();
                    if (timeComponent != null)
                    {
                        timeComponent.text = displayTime;
                    }
                }     
            }
        
    }
    
}  
