using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
      UnitSelectionManager.Instance.allUnitList.Add(gameObject);   
    }

    void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitList.Remove(gameObject);
    }
}
