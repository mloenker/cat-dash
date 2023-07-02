using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformDefaultPrefab;
    public GameObject platformIcePrefab;
    public GameObject platformBouncyPrefab;
    public GameObject platformDashPointPrefab;
    public float spawnWidth = 7f; // Max distance in both x directions
    public float spawnNegativeZone = 3f; // No-spawn zone around last platform
    public Vector3 spawnPosition = new Vector3(0, 0, -1);
    public Transform player;


    void Start()
    {
        spawnNewBatch();
    }

    // Spawn new platforms if player is close to end
    void Update()
    {
        if(spawnPosition.y-player.position.y<10.0f){
            spawnNewBatch();
        }
    }

    // Spawn 20 new platforms
    void spawnNewBatch(){
         for (int i = 0; i<20; i++)
        {
            // Random y in jump range
            spawnPosition.y+=Random.Range(2f, 4f);

            // New X should not be to close to old X
            float leftX = Random.Range(-spawnWidth, spawnPosition.x-spawnNegativeZone);
            float rightX = Random.Range(spawnPosition.x+spawnNegativeZone, spawnWidth);
            
            if (Random.value<=0.5 && -spawnWidth<spawnPosition.x-spawnNegativeZone)
            {
                spawnPosition.x= leftX;

            }else{
                if(spawnPosition.x+spawnNegativeZone < spawnWidth){
                    spawnPosition.x= rightX;
                }else{
                    spawnPosition.x= leftX;
                }
            }

            // Different platform types
            int randomNumber = Random.Range(0,100); // weighting
            if(randomNumber <=10){
                Instantiate(platformIcePrefab, spawnPosition, Quaternion.identity);
            }else if(randomNumber<=20) {
                Instantiate(platformBouncyPrefab, spawnPosition, Quaternion.identity);
            }else if(randomNumber<=30) {
                Instantiate(platformDashPointPrefab, spawnPosition, Quaternion.identity);
            }else{
                Instantiate(platformDefaultPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
