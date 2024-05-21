using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class VoxelDataLoader : MonoBehaviour
{
    public TextAsset jsonFile; // Assign the TextAsset containing your JSON data in the Unity Inspector
    string MaterialPath => "Assets/Prefabs/" + Name + "/Mat_";
    string Path => "Assets/Prefabs/" + Name + "/" + Name + ".prefab";
    public string Name = "";
    [SerializeField] VoxelData voxelData;

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
    public float colorThreshold = 10;
    [ContextMenu("Create From Data")]
    private void CreateFromData()
    {
        Dictionary<Color, Material> mats = new Dictionary<Color, Material>();
        if (voxelData != null && voxelData.voxels != null)
        {
            GameObject parent = new GameObject(Name);
            parent.transform.SetParent(transform);
            foreach (Voxel voxel in voxelData.voxels)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = voxel.position;
                // obj.GetComponent<Renderer>().material.color = voxel.color;
                // Material.color = voxel.color;
                Material mat = null;
                foreach (var kvp in mats)
                {
                    if (AreColorsSimilar(kvp.Key, voxel.color, colorThreshold))
                    {
                        mat = kvp.Value;
                        break;
                    }
                }

                if (mat == null)
                {
                    mat = new Material(Shader.Find("Standard"))
                    {
                        color = voxel.color
                    };
                    mats.Add(voxel.color, mat);
                    SaveMaterialAsAsset(mat, MaterialPath + ColorUtility.ToHtmlStringRGBA(voxel.color) + ".mat");
                }
                // Material mat = new Material(obj.GetComponent<Renderer>().sharedMaterial);
                // mat.color = voxel.color;
                obj.GetComponent<Renderer>().sharedMaterial = mat;
                obj.transform.SetParent(parent.transform);
                // // Now you can use 'voxel' objects as needed
                // Debug.Log("Voxel ID: " + voxel.id);
                // Debug.Log("Position: " + voxel.position);
                // Debug.Log("Color: " + voxel.color);
            }
            // SaveMaterialAsAsset(parent, Path);
            // PrefabUtility.ConvertToPrefabInstance(parent,parent);
        }
        else
        {
            Debug.LogError("No JSON file assigned.");
        }
    }
#if UNITY_EDITOR
    void SaveMaterialAsAsset(Object material, string path)
    {
        // Create a new asset in the specified path
        AssetDatabase.CreateAsset(material, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Material saved to " + path);
    }
    private bool AreColorsSimilar(Color color1, Color color2, float threshold)
    {
        float rDiff = Mathf.Abs(color1.r - color2.r);
        float gDiff = Mathf.Abs(color1.g - color2.g);
        float bDiff = Mathf.Abs(color1.b - color2.b);
        // float aDiff = Mathf.Abs(color1.a - color2.a);

        return (rDiff + gDiff + bDiff) < threshold;
    }
#endif
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