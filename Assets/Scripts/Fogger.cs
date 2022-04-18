using UnityEngine;
//using https://www.youtube.com/watch?v=iGAdaZ1ICaI&t=249s

public class Fogger : MonoBehaviour
{
    PlayerScripts.Player player;
    LayerMask fogLayer;
    float radius = 5;
    float radiusSqur { get { return radius * radius; } }
    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = Managers.GameManager.Instance.Player;
        fogLayer = LayerMask.GetMask("Fog");
        mainCamera = Camera.main;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, player.transform.position - mainCamera.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = vertices[i];
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < radiusSqur)
                {
                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqur);
                    colors[i].a = alpha;
                }
                else
                {
                    colors[i].a = 1;
                }
                UpdateColor();
            }
        }
    }

    void Initialize()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = transform.TransformPoint(vertices[i]);
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        mesh.colors = colors;
    }
}
