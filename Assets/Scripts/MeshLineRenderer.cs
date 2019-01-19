using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshLineRenderer : MonoBehaviour
{

    public Material lmat;

    private Mesh ml;

    private Vector3 s;

    private float lineSize = .1f;

    private bool firstQuad = true;

    public List<Vector3> LinePoints = new List<Vector3>();

    public Transform normalObj = null;

    public Color brushColor=Color.white;

    private Vector3 lastl, lastl1, lastl2;
    void Start()
    {
        ml = GetComponent<MeshFilter>().mesh;
        GetComponent<MeshRenderer>().material =  lmat;
        GetComponent<MeshRenderer>().receiveShadows = false;
    }

    private void Update()
    {
        //Vector3[] nv = new Vector3[ml.vertices.Length];
        //for(int i = 0; i < ml.vertices.Length; i++)
        //{
        //    nv[i] = ml.vertices[i] - new Vector3(0, Time.deltaTime, 0);
        //}
        //ml.vertices = nv;
        //ml.RecalculateBounds();
        //ml.RecalculateNormals();
    }

    public void SetWidth(float width)
    {
        lineSize = width;
    }
    public void InitBrush(BrushInfo info, Transform normalLookObj=null,float width = 0.1f,Color c=new Color())
    {
        lineSize = width;
        lmat =new Material(info.brushMat);
        
        normalObj = normalLookObj;
        brushColor = c;
        lmat.color = brushColor;
        //lmat.SetColor("_Color", brushColor);
    }
    public void AddPoint(Vector3 point)
    {
        LinePoints.Add(( point));
        if (s != Vector3.zero)
        {
            AddLine(ml, MakeQuad(s, point, lineSize, firstQuad));
            firstQuad = false;

        }
        s = point;
        
    }

    Vector3[] MakeQuad(Vector3 s, Vector3 e, float w, bool all)
    {
        w = w / 2;

        Vector3[] q;
        if (all)
        {
            q = new Vector3[4];
        }
        else
        {
            q = new Vector3[2];
        }
        Vector3 n, l;
        if (normalObj != null)
        {
            n = normalObj.transform.position - (s + e) / 2;
            l = Vector3.Cross(n, e - s);
            if (l == Vector3.zero&&lastl!=Vector3.zero)
            {
                l = lastl;
                if (l == Vector3.zero)
                {
                    l = Vector3.right;
                }
            }

        }
        else
        {
            n = Vector3.Cross(s, e);
            l = Vector3.Cross(n, e - s);
        }
        l.Normalize();
        lastl = l;
        if (all)
        {
            q[0] = transform.InverseTransformPoint(s + l * w);
            q[1] = transform.InverseTransformPoint(s + l * -w);
            q[2] = transform.InverseTransformPoint(e + l * w);
            q[3] = transform.InverseTransformPoint(e + l * -w);
        }
        else
        {
            q[0] = transform.InverseTransformPoint(e + l * w);
            q[1] = transform.InverseTransformPoint(e + l * -w);
        }
        return q;
    }

    void AddLine(Mesh m, Vector3[] quad)
    {
        int vl = m.vertices.Length;

        Vector3[] vs = m.vertices;
        vs = resizeVertices(vs, 2 * quad.Length);

        for (int i = 0; i < 2 * quad.Length; i += 2)
        {
            vs[vl + i] = quad[i / 2];
            vs[vl + i + 1] = quad[i / 2];
        }
        //调整vertices
        vs = AdjustVertices(vs, vl);
        vs = AdjustVertices(vs, vl - 4);


        Vector2[] uvs = m.uv;
        uvs = resizeUVs(uvs, 2 * quad.Length);

        if (quad.Length == 4)
        {
            //uvs[vl] = Vector2.zero;
            //uvs[vl + 1] = Vector2.zero;
            //uvs[vl + 2] = Vector2.right;
            //uvs[vl + 3] = Vector2.right;
            //uvs[vl + 4] = Vector2.up;
            //uvs[vl + 5] = Vector2.up;
            //uvs[vl + 6] = Vector2.one;
            //uvs[vl + 7] = Vector2.one;

            uvs[vl] = Vector2.zero;
            uvs[vl + 1] = Vector2.zero;
            uvs[vl + 2] = Vector2.up;
            uvs[vl + 3] = Vector2.up;
            uvs[vl + 4] = Vector2.right;
            uvs[vl + 5] = Vector2.right;
            uvs[vl + 6] = Vector2.one;
            uvs[vl + 7] = Vector2.one;



        }
        else
        {
            if (vl % 8 == 0)
            {
                //uvs[vl] = Vector2.zero;
                //uvs[vl + 1] = Vector2.zero;
                //uvs[vl + 2] = Vector2.right;
                //uvs[vl + 3] = Vector2.right;

                uvs[vl] = Vector2.zero;
                uvs[vl + 1] = Vector2.zero;
                uvs[vl + 2] = Vector2.up;
                uvs[vl + 3] = Vector2.up;

            }
            else
            {
                //uvs[vl] = Vector2.up;
                //uvs[vl + 1] = Vector2.up;
                //uvs[vl + 2] = Vector2.one;
                //uvs[vl + 3] = Vector2.one;

                uvs[vl] = Vector2.right;
                uvs[vl + 1] = Vector2.right;
                uvs[vl + 2] = Vector2.one;
                uvs[vl + 3] = Vector2.one;
            }
        }
        //调整uv
        uvs= AdjustUvs(uvs, vl);


        int tl = m.triangles.Length;

        int[] ts = m.triangles;
        ts = resizeTriangles(ts, 12);

        if (quad.Length == 2)
        {
            vl -= 4;
        }

        // front-facing quad
        ts[tl] = vl;
        ts[tl + 1] = vl + 2;
        ts[tl + 2] = vl + 4;

        ts[tl + 3] = vl + 2;
        ts[tl + 4] = vl + 6;
        ts[tl + 5] = vl + 4;

        // back-facing quad
        ts[tl + 6] = vl + 5;
        ts[tl + 7] = vl + 3;
        ts[tl + 8] = vl + 1;

        ts[tl + 9] = vl + 5;
        ts[tl + 10] = vl + 7;
        ts[tl + 11] = vl + 3;

        m.vertices = vs;
        m.uv = uvs;
        m.triangles = ts;
        m.RecalculateBounds();
        m.RecalculateNormals();
    }

    Vector3[] resizeVertices(Vector3[] ovs, int ns)
    {
        Vector3[] nvs = new Vector3[ovs.Length + ns];
        for (int i = 0; i < ovs.Length; i++)
        {
            nvs[i] = ovs[i];
        }

        return nvs;
    }

    Vector2[] resizeUVs(Vector2[] uvs, int ns)
    {
        Vector2[] nvs = new Vector2[uvs.Length + ns];
        for (int i = 0; i < uvs.Length; i++)
        {
            nvs[i] = uvs[i];
        }

        return nvs;
    }

    int[] resizeTriangles(int[] ovs, int ns)
    {
        int oldVerticesLength = ovs.Length;
        int[] nvs = new int[oldVerticesLength + ns];
        for (int i = 0; i < oldVerticesLength; i++)
        {
            nvs[i] = ovs[i];
        }

        return nvs;
    }

    Vector3[] AdjustVertices(Vector3[] nv,int vertexNum)
    {
        int pointsCount = vertexNum/4+1;
        if (vertexNum < 16) return nv;
        Vector3 p0, p1, p2, p3;
        p0 = LinePoints[pointsCount - 4];
        p1 = LinePoints[pointsCount - 3];
        p2 = LinePoints[pointsCount - 2];
        p3 = LinePoints[pointsCount - 1];

        Vector3 tempp1 = Bezier.GetPoint4(p0, p1, p2, p3, 0.333f);
        Vector3 tempp2 = Bezier.GetPoint4(p0, p1, p2, p3, 0.666f);
        p1 = tempp1;
        p2 = tempp2;

        Vector3 l1 ;
        Vector3 l2 ;
        Vector3 n;
        if (normalObj != null)
        {
            n =  (p2 + p0)/2-normalObj.position;
            l1 = Vector3.Cross(n, p2 - p0);
            
            if (l1 == Vector3.zero&&lastl1!=Vector3.zero)
            {
                l1 = lastl1;
                if (l1 == Vector3.zero)
                {
                    l1 = Vector3.right;
                }
            }

            n =  (p3 + p1)/2-normalObj.position;
            l2 = Vector3.Cross(n, p3 - p1);
            
            if (l2 == Vector3.zero && lastl2 != Vector3.zero)
            {
                l2 = lastl2;
                if (l2 == Vector3.zero)
                {
                    l2 = Vector3.right;
                }
            }

        }
        else
        {
            l1 = Vector3.right;
            l2 = Vector3.right;
        }
        l1.Normalize();
        l2.Normalize();
        lastl1 = l1;
        lastl2 = l2;
        Vector3[] q1 = new Vector3[2];
        q1[0]= transform.InverseTransformPoint(p1 - l1 * lineSize/2);
        q1[1]= transform.InverseTransformPoint(p1 + l1 * lineSize/2);

        Vector3[] q2 = new Vector3[2];
        q2[0] = transform.InverseTransformPoint(p2 - l2 * lineSize / 2);
        q2[1] = transform.InverseTransformPoint(p2 + l2 * lineSize / 2);

        for (int i = 0; i < 2 * q1.Length; i += 2)
        {
            nv[vertexNum-8 + i] = q1[i / 2];
            nv[vertexNum-8 + i + 1] = q1[i / 2];
        }
        for (int i = 0; i < 2 * q2.Length; i += 2)
        {
            nv[vertexNum-4 + i] = q2[i / 2];
            nv[vertexNum-4 + i + 1] = q2[i / 2];
        }


        return nv;

    }

    Vector2[] AdjustUvs(Vector2[] nv, int vertexNum)
    {
        if (nv.Length < 8) return nv;
        for(int i = vertexNum; i > 0; i-=4)
        {
            nv[i] = new Vector2((float)i / (float)vertexNum, 0);
            nv[i+1] = new Vector2((float)i / (float)vertexNum, 0);
            nv[i+2] = new Vector2((float)i / (float)vertexNum, 1);
            nv[i+3] = new Vector2((float)i / (float)vertexNum, 1);
        }
        return nv;
    }

}