using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f; // 한 글자씩 표시하는데 걸리는 시간
    private TMP_Text tmpText;
    private string story;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        story = tmpText.text; // 원본 스토리 텍스트
        tmpText.text = ""; // 초기 텍스트는 비워둠
    }

    void Start()
    {
        StartCoroutine(TypeText(story));
    }

    IEnumerator TypeText(string textToType)
    {
        foreach (char letter in textToType.ToCharArray())
        {
            tmpText.text += letter; // 한 글자씩 텍스트에 추가
            yield return new WaitForSeconds(typingSpeed); // 설정한 딜레이 만큼 기다림
        }
    }

    // 스토리 텍스트를 업데이트하고 애니메이션을 재시작하고 싶을 때 사용하는 메서드
    public void UpdateText(string newText)
    {
        StopAllCoroutines(); // 현재 진행 중인 타이핑을 멈춤
        story = newText; // 새 스토리로 업데이트
        tmpText.text = ""; // 텍스트를 비우고
        StartCoroutine(TypeText(story)); // 새로운 스토리로 타이핑 시작
    }
}
