using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class get_npc : MonoBehaviour
{
    public static string userLogin;
    public GameObject PrefabBandit;
    public GameObject PrefabBear;
    public GameObject PrefabFrog;
    public GameObject PrefabDog;
    public GameObject PrefabDino;
    public float checkInterval = 2f; // interval dlya zaprosa r api 

    private Dictionary<int, GameObject> spawnedMobs = new Dictionary<int, GameObject>(); // save info

    [System.Serializable]
    public class User
    {
        public int userID;
        public string userLogin;
        public string userToken;
    }

    private User user;

    [System.Serializable]
    public class Npc
    {
        public int id;
        public int id_type;
        public string type;
        public float x;
        public float y;
    }

    [System.Serializable]
    public class NpcData
    {
        public List<Npc> npc;
    }

    IEnumerator Start()
    {
        user = new User();
        user.userID = PlayerPrefs.GetInt("UserID");
        user.userLogin = PlayerPrefs.GetString("UserLogin");
        user.userToken = PlayerPrefs.GetString("UserToken");

        StartCoroutine(SpawnLoop()); // corutine

        yield return null;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            string url = "http://217.71.129.139:4320/api/get_npc.php?token=" + user.userToken;

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(webRequest.error);
                }
                else
                {
                    string response = webRequest.downloadHandler.text;
                    Debug.Log(response);

                    NpcData nd = JsonUtility.FromJson<NpcData>(response);
                    foreach (var npc in nd.npc)
                    {
                        Debug.Log(npc.type);
                        if (!spawnedMobs.ContainsKey(npc.id)) // проверяем, заспавнен ли моб с таким ID
                        {
                            SpawnMob(npc.x, npc.y, npc.type, npc.id);
                        }
                    }

                    CheckForRemovedMobs(nd); // проверяем мобов
                }
            }
        }
    }
    void CheckForRemovedMobs(NpcData newData)
    {
        List<int> idsToRemove = new List<int>();

        foreach (var mobId in spawnedMobs.Keys) // проверка на спавненных уже мобчиков
        {
            bool found = false; // флаг

            foreach (var npc in newData.npc) // проверка есть ли моб на серваке
            {
                if (npc.id == mobId)
                {
                    found = true;
                    break;
                }
            }
            if (!found) // если нет на сервере удаляем из игры и из словаря
            {
                Destroy(spawnedMobs[mobId]);
                idsToRemove.Add(mobId);
            }
        }
        foreach (var idToRemove in idsToRemove) // а вот удаление заспавненных в игре, если нет на сервере и в словаре
        {
            spawnedMobs.Remove(idToRemove);
        }
    }
    void SpawnMob(float x, float y, string mobType, int mobId)
    {
        x = x / 13.9f;
        y = y - 110;
        if (mobType == "Бандит")
        {
            spawnedMobs.Add(mobId, Instantiate(PrefabBandit, new Vector3(x, y, 0f), Quaternion.identity));
        }
        else if (mobType == "Медведь")
        {
            spawnedMobs.Add(mobId, Instantiate(PrefabBear, new Vector3(x, y, 0f), Quaternion.identity));
        }
        else if(mobType == "Лягушка")
        {
            spawnedMobs.Add(mobId, Instantiate(PrefabFrog, new Vector3(x, y, 0f), Quaternion.identity));
        }
        else if (mobType == "Собака")
        {
            spawnedMobs.Add(mobId, Instantiate(PrefabDog, new Vector3(x, y, 0f), Quaternion.identity));
        }
        else if (mobType == "Динозавр")
        {
            spawnedMobs.Add(mobId, Instantiate(PrefabDino, new Vector3(x, y, 0f), Quaternion.identity));
        }
        else
        {
            Debug.LogError("Неизвестный тип моба: " + mobType);
        }
    }
}
