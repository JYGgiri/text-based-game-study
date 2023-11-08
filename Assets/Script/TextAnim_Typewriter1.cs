using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffectWithButtons : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public GameObject buttonPrefab; // 버튼 프리팹에 대한 참조
    public Transform buttonContainer; // 버튼들을 배치할 컨테이너
    public int numberOfButtons = 3; // 생성할 버튼의 수

    private TMP_Text tmpText;
    private string story;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        story = tmpText.text;
        tmpText.text = "";
    }

    void Start()
    {
        StartCoroutine(TypeText(story));
    }

    IEnumerator TypeText(string textToType)
    {
        foreach (char letter in textToType.ToCharArray())
        {
            tmpText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        // 타이핑 효과가 끝나면 버튼 생성
        yield return new WaitForSeconds(0.5f); // 버튼이 나타나기 전에 약간의 딜레이를 줄 수 있습니다.
        CreateButtons(numberOfButtons);
    }

    void CreateButtons(int buttonCount)
    {
        for (int i = 0; i < buttonCount; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
            buttonObj.transform.localScale = Vector3.one;
            // 여기에서 버튼 텍스트나 클릭 이벤트를 설정할 수 있습니다.
            // 예: buttonObj.GetComponentInChildren<TMP_Text>().text = "Choice " + (i + 1);
            // 예: buttonObj.GetComponent<Button>().onClick.AddListener(() => YourMethodHere());
        }
    }
}