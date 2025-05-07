using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DialogScript
{
    public static Dictionary<int, Dialog> GetDialog(string npcName)
    {
        var dialogStep = new Dictionary<int, Dialog>();

        if (npcName == "Elf")
        {
            dialogStep.Add(1, new Dialog {
                text = "무슨 게임을 하고 싶어?",
                choice = new Dictionary<int, string> {
                    { 2, "Plane" },
                    { 3, "Survivor" },
                    { 0, "아무것도 아니야" }
                },
                sceneToLoad = "",
            });

            dialogStep.Add(2, new Dialog {
                text = "",
                sceneToLoad = "Plane"
            });

            dialogStep.Add(3, new Dialog {
                text = "",
                sceneToLoad = "Survivor"
            });

            dialogStep.Add(0, new Dialog {
                text = "그래, 잘가.",
                sceneToLoad = "",
            });
        }

        if (npcName == "Dwarf")
        {
            dialogStep.Add(1, new Dialog {
                text = "나는 이 던전을 안내하고 있다네",
                returnStep = 0,
            });

            dialogStep.Add(2, new Dialog {
                text = "무엇이 알고싶은가?",
                choice = new Dictionary<int, string> {
                    { 3, "Plane" },
                    { 4, "Survivor" },
                    { 0, "아무것도 아니야" }
                },
                returnStep = 0,
            });

            dialogStep.Add(3, new Dialog {
                text = "Plane은 장애물을 피하는 게임으로 SpaceBar를 통해 위로 조금씩 튀어오를 수 있다네, 직접 한번 해보면 이해가 쉬울거야.",
                returnStep = 2
            });

            dialogStep.Add(4, new Dialog {
                text = "뱀서라이크 게임으로 마우스 방향으로 자동으로 적을 공격하여, 물리치면된다네, 직접 한번 해보면 이해가 쉬울거야.",
                returnStep = 2
            });

            dialogStep.Add(0, new Dialog {
                text = "잘가게.",
                returnStep = 0,
            });
        }

        return dialogStep;
    }
}
