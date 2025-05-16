using System.Collections.Generic;
using UnityEngine;

public class SpawPlayers : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private int playerCount;

    private List<GameObject> playerInstances = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject playerInstance = Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Count)], Vector3.zero, Quaternion.identity);
            playerInstances.Add(playerInstance);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
