
using UnityEngine;

[CreateAssetMenu]
public class Mesh2D : ScriptableObject
{
    public Vector2[] vertices;
    public int[] lineIndices;
    public Vector2[] uvs;
    public int VertexCount => vertices.Length;
    public int LineCount => lineIndices.Length;
}

public struct OrientedPoint
{

    public Vector3 pos;
    public Quaternion rot;

    public OrientedPoint(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }

    public OrientedPoint(Vector3 pos, Vector3 forward)
    {
        this.pos = pos;
        this.rot = Quaternion.LookRotation(forward);
    }

    public Vector3 LocalToWorldPos(Vector3 localSpacePos)
    {
        return pos + rot * localSpacePos;
    }

    public Vector3 LocalToWorldVec(Vector3 localSpacePos)
    {
        return rot * localSpacePos;
    }


}
