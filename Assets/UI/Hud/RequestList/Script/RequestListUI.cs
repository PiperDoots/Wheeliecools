using UnityEngine;

public class RequestListUI : MonoBehaviour
{
    void Start()
    {
        BuildRequestList();
    }

    public void BuildRequestList()
    {
        if (RequestManager.Instance == null)
        {
            Debug.LogError("RequestListUI: no RequestManager found in scene");
            return;
        }
    }

    public void UpdateRequestList()
    {
        foreach (Request request in RequestManager.Instance.Requests)
        {

        }
    }
}
