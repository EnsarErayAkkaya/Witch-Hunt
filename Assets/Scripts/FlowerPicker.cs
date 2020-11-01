using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPicker : MonoBehaviour
{
    public StoryScript storyScript;
    public FlowerType flowerType;
    public Player player;
    public bool pickable, isItFirstTime;
    public int storyIndex;
    private void Start() {
        player = FindObjectOfType<Player>();
        storyScript = FindObjectOfType<StoryScript>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            if(!player.witchAttack.playerInFight && !isItFirstTime)
            {
                isItFirstTime = true;
                if(storyIndex != -1)
                    storyScript.ShowRelatedStory(storyIndex);
            }
            pickable = true;
            player.canPickUpFlower = true;
             player.playerUI.ActivatePickUpButton();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            pickable = false;
            player.canPickUpFlower = false;
             player.playerUI.DeactivatePickUpButton();
        }
    }
    public void Collect()
    {
        AudioManager.instance.Play("flowerPickUp");
        player.canPickUpFlower = false;
        pickable = false;
        player.AddFlower(this.flowerType);
        player.flowers.Remove(this);
         player.playerUI.DeactivatePickUpButton();
        Destroy(gameObject);
    }
}
public enum FlowerType
{
    fl1, fl2, fl3
}