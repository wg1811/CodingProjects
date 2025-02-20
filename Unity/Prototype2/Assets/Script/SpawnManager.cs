using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    //  Need to make the spawner spawn in random places.  Use Random stuff (see ex. 2.3)
    private float spawnRangeX = 20;
    private float spawnPosZ = 20;
    public int animalIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(
                animalPrefabs[animalIndex],
                new Vector3(0, 0, 20),
                animalPrefabs[animalIndex].transform.rotation
            );
        }
    }
}
