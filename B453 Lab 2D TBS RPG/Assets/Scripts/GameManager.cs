using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Fields
    // Array to store instances of Characters
    public Character[] characterArray = new Character[10];
    // The sprite to use for the class of this character.
    public Sprite characterSprite;

    public Vector3 characterPosition;

    // Makes a public static (doesn't need to be instantiated) reference to an instance of this class.
    public static GameManager Instance;

    // Awake is called before Start()
    private void Awake()
    {
        // Check to see if the Instance field is null.
        if (Instance == null)
        {
            // If it's null, it means no instance of GameManager has been assigned to it yet, so we assign this instance as Instance.
            Instance = this;
            // This makes it so this game object will NOT be destroyed when loading new scenes and will transfer between them.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If Instance wasn't null (it's already been assigned), destroy this instance of the game object.
            Destroy(gameObject);
        }
    }

    // NOTE: The public static GameManager Instance combined with the code in Awake is how to implement a singleton.

    // Starts a new game.
    public void StartNewGame()
    {
        // Load the scene from the list of scenes to build that is at index 1. In this case, that's the character select screen.
        // NOTE: You can also type in the name of the scene if you don't wanna add scenes to the build list.
        // You can add scenes to the build list with File -> Build Settings -> Add Open Scenes. Then rearrange them as needed.
        SceneManager.LoadScene(1);
    }

    // Quits to the desktop.
    public void QuitGameToDesktop()
    {
        // This exits the game completely, shuts it down, and returns the user to their desktop.
        Application.Quit();
    }

    // Takes the player to the overworld scene and grabs the new character created and the sprite to use for them.
    public void EnterOverworld(Character newCharacter, Sprite characterSprite)
    {
        // Load the passed character into the characterArray.
        characterArray[0] = newCharacter;
        // Set the sprite to be used for this character to be the passed Sprite.
        this.characterSprite = characterSprite;
        // Loads the third scene in our build which will be the overworld scene.
        SceneManager.LoadScene(2);
    }

    public void EnterFight(string sceneName, Transform currentTransform)
    {
        characterPosition = currentTransform.position;
        SceneManager.LoadScene(sceneName);
        Invoke("SetHp", 1.0f);
    }

    public void ReturnOverworld(int xp)
    {
        characterArray[0].gainXP(xp);
        characterArray[0].curHp = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatCharacter>().curHp;
        SceneManager.LoadScene(2);
        Invoke("SetPosition", 1.0f);
    }

    public void SetPosition()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = characterPosition;
    }

    public void SetHp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CombatCharacter>().curHp = characterArray[0].curHp;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CombatCharacter>().maxHp = characterArray[0].maxHp;
    }
}