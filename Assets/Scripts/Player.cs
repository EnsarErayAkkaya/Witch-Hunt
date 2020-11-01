using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public StoryScript storyScript;
    public Movement movement;
    public WitchAttack witchAttack;
    public Health health;
    public PlayerUI playerUI;
    public float mockCooldown;
    public float mockedAttackInterval;
    public bool canMock, canPickUpFlower;
    public List<FlowerPicker> flowers;
    public List<FlowerType> ownedFlowers;
    public bool canTalkWithWitch;
    public int storyIndex;
    public GameObject jokeText;
    
    private void Start() {
        storyScript = FindObjectOfType<StoryScript>();
        flowers = FindObjectsOfType<FlowerPicker>().ToList();
        ownedFlowers = new List<FlowerType>();
        canMock = true;
        witchAttack = FindObjectOfType<WitchAttack>();
        health = GetComponent<Health>();
        playerUI = FindObjectOfType<PlayerUI>();
    }
    private void Update() 
    {
        if(canPickUpFlower && Input.GetKeyDown(KeyCode.F))
        {
            flowers.First(s => s.pickable).Collect();
        }
        Mock();
        if(Input.GetKeyDown(KeyCode.P))
        {
            witchAttack.willAttack = !witchAttack.willAttack;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Mock()
    {
        if( Input.GetKeyDown(KeyCode.E) && canMock )
        {
            jokeText.SetActive(true);
            canMock = false;
            StartCoroutine( StartMock() );
            
            playerUI.ActivateMockCooldownPanel();
            StartCoroutine( MockCooldownCounter() );
        }
    }
    IEnumerator StartMock()
    {
        witchAttack.AttackCall();
        yield return new WaitForSeconds(mockedAttackInterval);
        witchAttack.AttackCall();
        yield return new WaitForSeconds(mockedAttackInterval);
        witchAttack.AttackCall();
        jokeText.SetActive(false);
    }
    IEnumerator MockCooldownCounter()
    {
        float t= 0;
        while (t < mockCooldown)
        {
            t += Time.deltaTime;
            playerUI.UpdateMockCooldownText(mockCooldown - t);
            yield return null;
        }
        playerUI.DeactivateMockCooldownPanel();
        canMock = true;
    }
    public void CallHealthUIUpdate(float h)
    {
        playerUI.UpdateHealthImage(h);
    }
    public void AddFlower(FlowerType f)
    {
        ownedFlowers.Add(f);
        if(ownedFlowers.Count == 3)
        {
            canTalkWithWitch = true;
            if(storyIndex != -1)
                storyScript.ShowRelatedStory(storyIndex);
        }
    }
    public void Die()
    {
        playerUI.restartText.gameObject.SetActive(true);
        AudioManager.instance.Play("Die");
        movement.canMove = false;
        movement.canRoll = false;
        witchAttack.willAttack = false;
    }
}
