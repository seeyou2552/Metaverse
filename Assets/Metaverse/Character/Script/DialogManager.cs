using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public Button choiceBtnPrefab;  // 선택지 버튼 프리팹
    public GameObject buttonParent;  // 버튼을 배치할 부모 객체
    public GameObject dialogueArea;

    public string npcName;
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    Dictionary<int, Dialog> dialogStep;
    int currentStep = 1;
    public float buttonSpacing = -50f;  // 버튼 간 간격
    public float buttonYPos = 10f;     // 버튼 Y 위치
    float tempYPos;
    private Coroutine currentCoroutine; // 대화 코루틴
    public bool isInputBlocked = false;
    // bool isSelect = false;
    private List<Button> choiceButtons = new List<Button>();
    private int selectedButtonIndex = 0;
    Dialog dialog;

    void Start()
    {
        tempYPos = buttonYPos;   
    }

    public void DialogSetting()
    {
        dialogueArea.SetActive(true);
        dialogStep = DialogScript.GetDialog(npcName);
        ShowDialog(1);
    }


    public void ShowDialog(int step)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        
        dialog = dialogStep[step];
        npcNameText.text = npcName;
        dialogueText.text = dialog.text;

        selectedButtonIndex = 0;

        // 기존 선택지 버튼 삭제
        foreach (Transform child in buttonParent.transform)
        {
            buttonParent.SetActive(false);
            Destroy(child.gameObject);
        }
        choiceButtons.Clear();
        

        // 선택지가 있다면 버튼 생성
        if (dialog.choice != null)
        {
            buttonYPos = tempYPos;
            StopAllCoroutines();
            foreach (var c in dialog.choice)
            {
                buttonParent.SetActive(true);
                int nextStep = c.Key;
                string choiceText = c.Value;

                Button btnObj = Instantiate(choiceBtnPrefab, buttonParent.transform);
                btnObj.transform.localPosition = new Vector3(0, buttonYPos -= buttonSpacing, 0);
                btnObj.GetComponentInChildren<TMP_Text>().text = choiceText;

                choiceButtons.Add(btnObj);

                // 선택지에 따라 씬을 이동할 수도 있음
                if (nextStep == 0) // 0번 선택지 선택 시 대화 종료
                {
                    btnObj.onClick.AddListener(() => EndDialog());  // 대화 종료 처리
                }
                else
                {
                    btnObj.onClick.AddListener(() => ShowDialog(nextStep));  // 대화 계속 
                }
            }
            return;
        }

        if (!string.IsNullOrEmpty(dialog.sceneToLoad)) // 씬이 설정되어 있으면 씬 이동
        {
            ChangeScene(dialog.sceneToLoad);
        }
        else if (string.IsNullOrEmpty(dialog.sceneToLoad)) // 아무것도 없다면 다음 대화 진행
        {
            if (dialog.returnStep > 0)
            {
                StartCoroutine(NextZ(dialog.returnStep));
            }
            else if (!isInputBlocked)
            {
                isInputBlocked = true;
                // StartCoroutine(NextZ(step += 1));
                currentCoroutine = StartCoroutine(NextZ(step + 1));
            }
        }
    }

    public void EndDialog()
    {
        isInputBlocked = true;
        foreach (Transform child in buttonParent.transform)
        {
            buttonParent.SetActive(false);
            Destroy(child.gameObject);
        }
        dialogueText.text = dialogStep[0].text;
        Invoke("HideDialogueArea", 1f);  // 대화 UI 비활성화
    }

    private void HideDialogueArea()
    {
        // 대화 UI 비활성화
        dialogueArea.SetActive(false);
        choiceButtons.Clear();

        currentStep = 1;  // 다시 처음부터 시작
        isInputBlocked = false;
    }

    private void ChangeScene(string scene)
    {
        dialogueArea.SetActive(false);
        SceneManager.LoadScene(scene);
    }

    private IEnumerator NextZ(int step)
    {
        // 키가 눌린 상태면, 먼저 뗄 때까지 대기
        yield return new WaitWhile(() => Input.GetKey(KeyCode.Z));
        // 그 다음 눌렀을 때 진행
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        ShowDialog(step);
    }

    void Update()
    {
        if (!dialogueArea.activeSelf || dialog == null || dialog.choice == null) return;
        else if(dialog.choice != null) SelectBtn();
    }

    public void SelectBtn()
    {
        if (choiceButtons == null || choiceButtons.Count == 0)
        return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % choiceButtons.Count;
            EventSystem.current.SetSelectedGameObject(choiceButtons[selectedButtonIndex].gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + choiceButtons.Count) % choiceButtons.Count;
            EventSystem.current.SetSelectedGameObject(choiceButtons[selectedButtonIndex].gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            choiceButtons[selectedButtonIndex].onClick.Invoke();
        }
    }   


    public void StartDialog()
    {
        DialogSetting();
    }
}
