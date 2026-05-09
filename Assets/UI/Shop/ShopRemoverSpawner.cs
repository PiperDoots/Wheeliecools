using UnityEngine;

public class ShopRemoverSpawner : MonoBehaviour
{
    [SerializeField] private ShopOrderRemover ItemPrefab;
    public void SpawnRemovalButton(string LiquidName)
    {
        ShopOrderRemover Item = Instantiate(ItemPrefab);
        Item.transform.SetParent(gameObject.transform, false);
        Item.liquidName = LiquidName;
    }
    
    public void Reset()
    {
        while(gameObject.transform.childCount > 0)
        {
            Destroy(gameObject.transform.GetChild(0)); //Go to sleep...
        }
    }
}
