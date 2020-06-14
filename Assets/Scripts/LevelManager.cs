using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Objects")]
    public GameObject player;
    public GameObject objective;
    public GameObject enemySpawner;
    public GameObject mainCamera;

    [Header("Level Data JSON")]
    public TextAsset jsonLevelData;

    private int amountOfLevels;
    private int currentLevel = 0;
    private LevelData loadedLevelData;
    private PlayerControl playerControl;
    private EnemySpawner enemySpawnerScript;
    private CameraFollow cameraFollow;

    void Awake() {
        amountOfLevels = transform.childCount; // Set number of levels to amount of children (tilemap levels)
        loadedLevelData = JsonUtility.FromJson<LevelData>(jsonLevelData.text);
        playerControl = player.GetComponent<PlayerControl>();
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
        cameraFollow = mainCamera.GetComponent<CameraFollow>();
        changeLevel(currentLevel); // Start game with level 0 (currentLevel)
    }

    void Update() {
        if(playerControl.hasDied) {
            playerControl.hasDied = false;
            changeLevel(currentLevel);
        } else if(playerControl.hasReachedObjective) {
            playerControl.hasReachedObjective = false;
            changeLevel(Mathf.Min(currentLevel + 1, amountOfLevels - 1));
        } else if(Input.GetKeyDown(KeyCode.Space)) {
            changeLevel(currentLevel);
            cameraFollow.CameraShake();
        }
    }

    private Vector3 getPlayerPosition() {
        return new Vector3(
            loadedLevelData.levels[currentLevel].player_position[0], 
            loadedLevelData.levels[currentLevel].player_position[1],
            player.transform.position.z
        );
    }

    private Vector3 getObjectivePosition() {
        return new Vector3(
            loadedLevelData.levels[currentLevel].objective_position[0], 
            loadedLevelData.levels[currentLevel].objective_position[1],
            objective.transform.position.z
        );
    }
    
    private float[,] getEnemyPositions() {
        float[,] enemyPositions = new float[loadedLevelData.levels[currentLevel].enemy_positions.Length, 2];

        for(int i = 0; i < loadedLevelData.levels[currentLevel].enemy_positions.Length; i++) {
            enemyPositions[i, 0] = loadedLevelData.levels[currentLevel].enemy_positions[i].x;
            enemyPositions[i, 1]= loadedLevelData.levels[currentLevel].enemy_positions[i].y; 
        }

        return enemyPositions;
    }

    public void changeLevel(int levelID) {
        int i = 0; // Current child (level) index
        foreach(Transform child in transform) {
            child.gameObject.SetActive((i == levelID) ? true : false); // If child (level) is equal to given levelID, make it active
            i++;
        }

        currentLevel = levelID; // Change the current level to the given levelID

        player.transform.position = getPlayerPosition(); // Set player position based on JSON data of changed level
        objective.transform.position = getObjectivePosition(); // Set objective position based on JSON data of changed level

        enemySpawnerScript.enemyPositions = getEnemyPositions();
        enemySpawnerScript.spawnEnemies();
    }

    [System.Serializable]
    public class LevelData {
        public Level[] levels;
    }

    [System.Serializable]
    public class Level {
        public int id;
        public Position[] enemy_positions;
        public float[] player_position;
        public float[] objective_position;
    }

    [System.Serializable]
    public class Position {
        public float x;
        public float y;
    } 
}