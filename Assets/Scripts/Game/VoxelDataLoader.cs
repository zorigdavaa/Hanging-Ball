using UnityEngine;



public class VoxelDataLoader : MonoBehaviour
{
    public TextAsset jsonFile; // Assign the TextAsset containing your JSON data in the Unity Inspector
    [SerializeField] VoxelData voxelData;
    [SerializeField] Material Material;

    [ContextMenu("Populate Data")]
    private void PopulateData()
    {
        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            voxelData = JsonUtility.FromJson<VoxelData>(jsonData);

            if (voxelData != null && voxelData.voxels != null)
            {
                Debug.LogError("Parsed voxel data.");
                // foreach (Voxel voxel in voxelData.voxels)
                // {
                //     // Now you can use 'voxel' objects as needed
                //     Debug.Log("Voxel ID: " + voxel.id);
                //     Debug.Log("Position: " + voxel.position);
                //     Debug.Log("Color: " + voxel.color);
                // }
            }
            else
            {
                Debug.LogError("Failed to parse voxel data.");
            }
        }
        else
        {
            Debug.LogError("No JSON file assigned.");
        }
    }
    [ContextMenu("Create From Data")]
    private void CreateFromData()
    {
        if (voxelData != null && voxelData.voxels != null)
        {
            GameObject parent = new GameObject("Parent");
            parent.transform.SetParent(transform);
            foreach (Voxel voxel in voxelData.voxels)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = voxel.position;
                // obj.GetComponent<Renderer>().material.color = voxel.color;
                Material.color = voxel.color;
                Material mat = new Material(obj.GetComponent<Renderer>().sharedMaterial);
                mat.color = voxel.color;
                obj.GetComponent<Renderer>().sharedMaterial = mat;
                obj.transform.SetParent(parent.transform);
                // // Now you can use 'voxel' objects as needed
                // Debug.Log("Voxel ID: " + voxel.id);
                // Debug.Log("Position: " + voxel.position);
                // Debug.Log("Color: " + voxel.color);
            }
        }
        else
        {
            Debug.LogError("No JSON file assigned.");
        }
    }
}

[System.Serializable]
public class VoxelData
{
    public Voxel[] voxels;
}

[System.Serializable]
public class Voxel
{
    public string id;
    public float x;
    public float y;
    public float z;
    public float red;
    public float green;
    public float blue;
    // public Voxel(string id, float x, float y, float z, float r, float g, float b)
    // {
    //     id = id
    //     position = new Vector3(x, y, z);
    //     color = new Color(r / 255, g / 255, b / 255);
    // }
    public Vector3 position => new Vector3(x, y, z);
    public Color color => new Color(red / 255, green / 255, blue / 255);
    // public Vector3 position;
    // public Color color;
}