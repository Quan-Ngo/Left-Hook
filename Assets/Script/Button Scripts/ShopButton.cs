using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour
{
    public GameObject fishingButton; 

    // Start is called before the first frame update
    void Start()
    {
        Button button = this.GetComponent<Button>(); 
        button.onClick.AddListener(OpenShop);
    }

    void OpenShop()
    {
        
        SceneManager.LoadScene("Shop");

        // Make the ShopButton and FishingButton invisible
        this.gameObject.SetActive(false);
        fishingButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
