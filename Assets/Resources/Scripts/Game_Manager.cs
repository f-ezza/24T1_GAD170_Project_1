using DungeonEscape;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    #region - Variables -
    public enum Difficulty { Easy, Medium, Hard };
    [SerializeField] private Difficulty difficulty;
    private Player_Controller player;


    [Header("Areas")]
    [SerializeField] private int numOfAreas;
    [SerializeField] private List<AreaData> areaDataList;
    [SerializeField] private List<Area> areas;

    [Header("Combat Manager")]
    public bool isPlayersTurn;
    public bool newRound;
    public bool enemyDead;
    #endregion

    #region - Initialization -
    /*
     * Awake does the following:
     *  - Create a new list and assign it to areas
     *  - Get the Player_Controller script from the player (Done so via GameObject.Find(string name)
     *  - A switch statment that will alternate the numOfAreas to generate dependent on the difficulty selected
     *  - Set areaDataList to contain all the ScriptableObjects inside of the folder AreaData
     *  - Call to Method GenerateAreas
     */
    private void Awake()
    {
        areas = new List<Area>();
        player = GameObject.Find("Player").GetComponent<Player_Controller>();

        switch (difficulty)
        {
            case Difficulty.Easy:
                numOfAreas = 4;
                break;

            case Difficulty.Medium:
                numOfAreas = 6;
                break;

            case Difficulty.Hard:
                numOfAreas = 8;
                break;

            default:
                numOfAreas = 4; // Default to easy if the difficulty level is not recognized
                break;
        }
        areaDataList = new List<AreaData>(Resources.LoadAll<AreaData>("Prefabs/Scriptable_Objects/AreaData"));

        GenerateAreas();
    }
    #endregion

    #region - Methods -
    private void StartRound()
    {
        isPlayersTurn = true;
        newRound = false;
    }



    /*
     * Create Area is a function that does the following:
     *  - It takes a parent GameObject and the current index as arguments 1 & 2
     *  - It creates a RandomIndex int that firstly checks if it is the first area. If it isnt it generates a random area. If it is it will generate area_0
     *  - It then creates a GameObject and parents it to the passed parent object
     *  - It then adds a script component to the created GameObject of type 'Area'
     *  - It then calls a method in that script called SetAreaData, feeding the index of the generated area
     *  - It will then add the area to the list embedded in the Game_Manager for convience
     *  - It then returns the generated script 'areaScript' in case further adjustments are needed in this iteration - Also nescessary for the first iteration.
     */
    private Area CreateArea(GameObject parent, int areaIndex, bool isFirst)
    {
        int randomIndex = !isFirst ? Random.Range(1, areaDataList.Count) : 0;
        GameObject areaObject = new GameObject();
        areaObject.transform.parent = parent.transform;
        Area areaScript = areaObject.AddComponent<Area>();
        areaScript.SetAreaData(areaDataList[randomIndex]);
        areas.Add(areaScript);
        return areaScript;
    }

    /*
     * Generate Area is a function that does the following:
     *  - It clears the areas list as a precaution for not causing double ups
     *  - It creates a new game object with a script of type 'Area' using the CreateArea Method
     *  - It then adds a script component to the created GameObject of type 'Area'
     *  - It adds a the new object of type 'Area' to the areas list
     *  - It then initiates a for loop, looping through the amount of areas (dependent on the difficulty) creating new areas.
     *  - Sends the list to the player
     *  - Loads the first Area
     */
    private void GenerateAreas()
    {
        areas.Clear(); // Ensure the list is cleared before adding new areas.

        // Ensure that index 0 is always the first area created
        Area firstAreaScript = CreateArea(gameObject, 0, true);
        areas.Add(firstAreaScript);

        // Generate additional areas
        for (int i = 1; i < numOfAreas; i++)
        {
            CreateArea(gameObject, i, false);
        }

        player.GetAreaList(areas);
        LoadArea(0);
    }

    /*
     * Load Area is a function that does the following:
     *  - Takes an argument of type int which represents the index of the area to load
     *  - Inform the player to load the area.
     */
    private void LoadArea(int index)
    {
        player.LoadArea(index);
        if (player.enemy != null)
        {
            StartRound();
        }
    }

    public bool PlayerTurn()
    {
        return isPlayersTurn;
    }

    #endregion
}
