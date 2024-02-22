using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject blueUnitPrefab;
    public GameObject redUnitPrefab;

    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 spawnPosition = GetMousePos();
            SpawnUnit(blueUnitPrefab, spawnPosition, "BlueUnit");
            
        }

        
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector3 spawnPosition = GetMousePos();
            SpawnUnit(redUnitPrefab, spawnPosition, "RedUnit");
            
        }
    }

    private void SpawnUnit(GameObject unitPrefab, Vector3 spawnPosition, string unitTag)
    {
        if (unitPrefab != null)
        {
            // Raycast downwards to find the ground position
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                spawnPosition = hit.point;
            }

            // Ensure units are not spawned below the ground plane
            if (spawnPosition.y < 0)
            {
                spawnPosition.y = 0; // Set y position to 0 if below ground
            }

            GameObject unit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);

            // Rotate the unit to face towards the camera

            Vector3 directionToCamera = Camera.main.transform.position - unit.transform.position;
            directionToCamera.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(directionToCamera);
            unit.transform.rotation = rotation;

            unit.tag = unitTag;

        }
        else
        {
            Debug.LogWarning("Unit prefab is not assigned!");
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

}