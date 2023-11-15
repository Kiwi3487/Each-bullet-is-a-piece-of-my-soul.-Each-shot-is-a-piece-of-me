using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunsText : MonoBehaviour
{
    TextMeshProUGUI textField;
    Player player; // Reference to the Player class
    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("GunText").GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>(); // Find the Player component in the scene
    }
    // Update is called once per frame
    void Update()
    {
        textField.text = "Current Gun: " + player.GetCurrentGun();
    }
}