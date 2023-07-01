using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{

    public int selectedSkin = 0;
    public float horizontal;
    public GameObject[] skinList;
    public GameObject earthPlatform;
    Animator animator_orange;

    // Start is called before the first frame update
    void Start()
    {
        animator_orange = skinList[0].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // Start Game
        if(Input.GetButtonDown("Jump")) {
            SceneManager.LoadScene("MainScene");
        }

        // Select Skin
        if(Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow)) {
            selectedSkin = MathMod(selectedSkin-1, 3);
            newSkin(selectedSkin);
        }

        if(Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)) {
            selectedSkin = MathMod(selectedSkin+1, 3);
            newSkin(selectedSkin);
        }

        // Animate Orange
        if(Random.Range(0f,1f)<0.0004f){
            animator_orange.SetTrigger("licking");
        }

        if(Random.Range(0f,1f)>0.9992f){
            animator_orange.SetTrigger("itching");
        }

    }

    // Select Skin
    void newSkin(int selectedSkin){
        earthPlatform.transform.position = new Vector3(skinList[selectedSkin].transform.position.x, earthPlatform.transform.position.y, 0);
        for(int i = 0; i<=2; i++){
            skinList[i].transform.position = new Vector3(skinList[i].transform.position.x, -4.1f, 0);
        }
        skinList[selectedSkin].transform.position = new Vector3(skinList[selectedSkin].transform.position.x, -3.24f, 0);
        // Save Skin Selection
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        PlayerPrefs.Save();
    }

    // True Modulo
    static int MathMod(int a, int b) {
        return (Mathf.Abs(a * b) + a) % b;
    }

}
