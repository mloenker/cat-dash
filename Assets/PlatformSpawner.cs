using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformDefaultPrefab;
    public GameObject platformIcePrefab;
    public GameObject platformBouncyPrefab;
    public float spawnWidth = 7f;
    public float spawnNegativeZone = 3f;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        // Platform Random Spawn Code
        //Vector3 spawnPosition = new Vector3(0, 0, 0);
        spawnNewBatch();

    }

    // Update is called once per frame
    void Update()
    {
        if(spawnPosition.y-player.position.y<10.0f){
            spawnNewBatch();
        }
    }

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
            int randomNumber = Random.Range(0,100);
            if(randomNumber <=10){
                Instantiate(platformIcePrefab, spawnPosition, Quaternion.identity);
            }else if(randomNumber<=40) {
                Instantiate(platformBouncyPrefab, spawnPosition, Quaternion.identity);
            }else{
                Instantiate(platformDefaultPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
