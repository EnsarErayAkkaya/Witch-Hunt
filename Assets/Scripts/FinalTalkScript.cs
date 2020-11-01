using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTalkScript : MonoBehaviour
{
    public StoryScript storyScript;
    public Player player;
    public int storyIndex;
    private void Start() {
        storyScript = FindObjectOfType<StoryScript>();
    }
    private void Update() {
        if(player.canTalkWithWitch && Input.GetKeyDown(KeyCode.F))
        {
            player.canTalkWithWitch = false;
            if(storyIndex != -1)
                storyScript.ShowRelatedStory(storyIndex);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(player.canTalkWithWitch && other.CompareTag("Player"))
        {
            player.playerUI.ActivatePickUpButton();
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(player.canTalkWithWitch && other.CompareTag("Player"))
        {
            player.canTalkWithWitch = true;
            player.playerUI.DeactivateMockCooldownPanel();
        }
    }
}
