using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string text;
    public Dictionary<int, string> choice; // 선택지: 선택 번호와 내용
    public int returnStep = 0;
    public string sceneToLoad;
}
