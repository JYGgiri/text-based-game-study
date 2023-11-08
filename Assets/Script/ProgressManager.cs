using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [System.Serializable]
    public class StoryData
    {
        public Chapter[] chapters;
    }

    [System.Serializable]
    public class Chapter
    {
        public string id;
        public string title;
        public Scene[] scenes;
    }

    [System.Serializable]
    public class Scene
    {
        public string id;
        public string text;
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public string text;
        public string nextScene;
    }

    [SerializeField] private ShowManager showManager; // Inspector를 통해 할당

    private StoryData storyData;

    void Start()
    {
        LoadStoryData(); //json 파일 불러오기

        if (storyData != null) //Json 파일이 잘 불러와졌는지 디버그 로그 찍음
        {
            Debug.Log("Story Loaded");

            ShowInitialScene(); //첫번째 챕터의 첫번째 씬을 ShowManager로 보내서 출력
        }
        else
        {
            Debug.LogError("Failed to load story data.");
        }
    }

    void LoadStoryData()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Story"); // Resources 폴더 내에 Story.json 파일을 불러옴
        storyData = JsonUtility.FromJson<StoryData>(jsonTextFile.text);
    }

    void ShowInitialScene() //처음 한번만 작동하는 첫번째 챕터, 첫번째 씬 출력 용 함수
    {
        Chapter firstchapter = storyData.chapters[0];
        
        Scene initialScene = firstchapter.scenes[0];

        showManager.UpdateStoryText(initialScene.text);
    }

    public void ShowScene(string sceneId) // 유저 선택에 따라 씬을 찾아서 출력
    {
        Scene sceneToLoad = FindSceneById(sceneId);
        if(sceneToLoad != null)
        {
            showManager.UpdateStoryText(sceneToLoad.text);
            // 추가로 여기에 ChoiceManager를 넣어야함 !!!!!!!!!!!!
        }
        else
        {
            Debug.LogError("Scene with id " + sceneId + " not found.");
        }
    }

    private Scene FindSceneById(string sceneId) // ShowScene에서 준 sceneID 값으로 json에서 씬을 가져옴
    {
        foreach(Chapter chapter in storyData.chapters)
        {
            foreach(Scene scene in chapter.scenes)
            {
                if(scene.id == sceneId)
                {
                    return scene;
                }
            }
        }
        return null;
    }

    public void UserMadeChoice(string nextSceneId) // 유저의 선택에 따라 nextSceneID를 받아오는 함수
    {
        ShowScene(nextSceneId);
    }
}
