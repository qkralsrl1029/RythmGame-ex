using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab;
    public int count;
    public Transform tfPoolParent;
}
public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfos = null;

    public static ObjectPool instance;      //공유 자원

    public Queue<GameObject> noteQueue = new Queue<GameObject>();       //pool
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        noteQueue = InsertQueue(objectInfos[0]);
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_object)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for (int i = 0; i < p_object.count; i++)
        {
            GameObject t_clone = Instantiate(p_object.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if (p_object.tfPoolParent != null)
                t_clone.transform.SetParent(p_object.tfPoolParent);
            else
                t_clone.transform.SetParent(this.transform);

            t_queue.Enqueue(t_clone);
        }

        return t_queue;
    }
}
