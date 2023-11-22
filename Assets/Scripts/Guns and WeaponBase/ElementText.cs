using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementText : MonoBehaviour
{
    TextMeshProUGUI textField;
    Player player;

    void Start()
    {
        textField = GameObject.Find("ElementText").GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>(); 
    }
    // Update is called once per frame
    void Update()
    {
        textField.text = "Imbued Element: " + player.GetCurrentElement();
    }
}