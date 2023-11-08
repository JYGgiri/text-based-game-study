using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowManager : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;

    private void Awake() 
    {
        storyText = GetComponentInChildren<TMP_Text>();
    }

    public void UpdateStoryText(string text)
    {
        Debug.Log("UpdateStoryText called");
        if(storyText != null) {
            storyText.text = text;
        }
    }
}
