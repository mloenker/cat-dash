using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformDefaultPrefab;
    public GameObject platformIcePrefab;
    public GameObject platformBouncyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        for (int i = 0; i<20; i++)
        {
            spawnPosition.y+=Random.Range(2f, 4f);
            spawnPosition.x=Random.Range(-7f, 7f);
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

    // Update is called once per frame
    void Update()
    {

    }
}
