using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// RASKALOF, v1.0, Script used for displaying end game menu
public class DialogHandler : MonoBehaviour {
    [SerializeField] Text title = null;
    [SerializeField] Text description = null;
    [SerializeField] Button next_button = null;

    public void ShowDialog(string title, string description, bool enable_next, byte value1, byte value2) {
        this.title.text = title;
        this.description.text = description;
        next_button.interactable = enable_next;
        this.description.text += value1 + "/" + value2;
    }
}
