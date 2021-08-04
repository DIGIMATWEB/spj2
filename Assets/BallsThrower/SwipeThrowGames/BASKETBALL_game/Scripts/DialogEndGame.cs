using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// RASKALOF, v1.0, Script handle end game dialog

public class DialogEndGame : MonoBehaviour { 
    [SerializeField] Text title = null; // Link to title
    [SerializeField] Text description = null; // Link to description

    public void ShowDialog(string title, string description, int value) {
        this.title.text = title;
        this.description.text = description;
        this.description.text += value;
    }
}
