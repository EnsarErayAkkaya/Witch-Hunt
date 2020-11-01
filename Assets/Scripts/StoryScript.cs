using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryScript : MonoBehaviour
{
    public WitchAttack attacker;
    public Player player;
    public RectTransform storyPanel;
    public Sprite boy, witch, admin;
    public Image talkerImage;
    public TextWriterUI textWriterUI;
    public Vector3 showPos, hidePos;
    public List<StoryHelper> stories;
    public EnemySpawner currentSpawner;
    public bool isFirstText;
    private void Start() 
    {
        player = FindObjectOfType<Player>();    
        attacker = FindObjectOfType<WitchAttack>();
        AudioManager.instance.Mute("Theme1");
        ShowRelatedStory(0);   
    }
    public void SetEnemySpawner(EnemySpawner e)
    {
        currentSpawner = e;
    }
    public void ShowRelatedStory(int storyIndex)
    {
        player.movement.stop = true;

        attacker.willAttack = false;
        ShowPanel();
        StartCoroutine( StoryRoutine(storyIndex) );
    }
    IEnumerator StoryRoutine(int storyIndex)
    {
        int textIndex = 0;

        while( textIndex < stories[storyIndex].texts.Count  )
        {
            if(stories[storyIndex].texts[textIndex].isAdmin)
            {
                talkerImage.sprite = admin;
            }
            else
            {
                if(stories[storyIndex].texts[textIndex].isWitch)
                    talkerImage.sprite = witch;
                else
                    talkerImage.sprite = boy;
            }
             
            
            textWriterUI.story = stories[storyIndex].texts[textIndex].text;
            textWriterUI.CallPlayText();
            
            textIndex += 1;
            while (!Input.GetKeyDown(KeyCode.Return))
                yield return null;
            if( textWriterUI.writing )
            {
                textWriterUI.cancelWriting = true;
            }
            yield return null;
        }
       
        CloseStory();
    }
    
    void CloseStory()
    {
        if(!isFirstText)
        {
            isFirstText = true;
            AudioManager.instance.Unmute("Theme1");
        }
        HidePanel();
        player.movement.stop = false;
        attacker.willAttack = true;
        if(currentSpawner != null)
        {
            foreach (var item in currentSpawner.liveEnemies)
            {
                item.GetComponent<BaseZombie>().ContinueZombie();
            }
            currentSpawner = null;
        }
        
    }
    public void ShowPanel()
    {
        storyPanel.gameObject.SetActive(true);
    }
    public void HidePanel()
    {
        storyPanel.gameObject.SetActive(false);       
    }
}
[System.Serializable]
public class TextHelper
{
    public string text;
    public bool isWitch;
    public bool isAdmin;
}
[System.Serializable]
public class StoryHelper
{
    public List<TextHelper> texts;
}