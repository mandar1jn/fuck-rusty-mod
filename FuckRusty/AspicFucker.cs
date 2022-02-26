using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FuckRusty
{
    public class AspicFucker : MonoBehaviour
    {
        const int chance = 40;
        static List<string> forbidden = new List<string>()
        {
            "Leak",
            "Low Health Leak",
            "Hit Pt 1",
            "Hit Pt 2",
        };

        public static IEnumerator OnSceneEnter(On.HeroController.orig_EnterScene orig, HeroController self, TransitionPoint enterGate, float delayBeforeEnter)
        {
            List<GameObject> list = FindGameObjectsInLayer(11);
            if(list.Count > 0)
            {
                int result = UnityEngine.Random.Range(0, 101);
                if (chance > result)
                {
                    foreach (GameObject go in list)
                    {
                        if (!forbidden.Contains(go.name))
                        {
                            Vector3 pos = go.transform.position;
                            for (int i = 0; i < 10; i++)
                            {
                                Instantiate(FuckRusty.enemies["Crossroads_08"]["Battle Scene/Wave 1/Spitter"], new Vector3(pos.x, pos.y), Quaternion.identity).SetActive(true);
                            }
                        }
                    }
                }
            }
            return orig(self, enterGate, delayBeforeEnter);
        }

        static List<GameObject> FindGameObjectsInLayer(int layer)
        {
            var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            var goList = new List<GameObject>();
            for (int i = 0; i < goArray.Length; i++)
            {
                if (goArray[i].layer == layer)
                {
                    goList.Add(goArray[i]);
                }
            }
            if (goList.Count == 0)
            {
                return null;
            }
            return goList;
        }

    }
}
