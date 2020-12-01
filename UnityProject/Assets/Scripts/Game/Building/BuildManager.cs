using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    Camera cam;

    [Header("Setup")]
    public BuildingDatabase buildingDatabase;

    [Header("Raycasting")]
    public LayerMask GroundMask;
    public LayerMask BuildingMask;
    Collider[] colliders;

    [Header("Preview Colors")]
    public Material PreviewMat;
    public Color GoodColor = Color.green;
    public Color BadColor = Color.red;

    [Header("Purchasing")]
    public GameStartAsset startAsset;
    public GameObject BuildButton;
    public int Balance;
    int cost;

    public GameObject PreviewObj { get; private set; }
    public Vector3 PreviewLoc { get; private set; }
    public bool canBuild { get; private set; }
    [HideInInspector] public int lastID = 0;

    void Start()
    {
        Balance = startAsset.Balance;
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, GroundMask))
        {
            PreviewLoc = hit.point;
            PreviewLoc = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
        }
        if (PreviewObj != null)PreviewObj.transform.position = PreviewLoc;

        colliders = Physics.OverlapBox(PreviewLoc, new Vector3(0.49f, 0.49f, 0.49f), Quaternion.identity, BuildingMask);
        cost = buildingDatabase.Profiles[lastID].cost;

        canBuild = (colliders.Length == 0 && Balance >= cost);
        BuildButton.SetActive(PreviewObj != null);

        if (canBuild) PreviewMat.color = GoodColor;
        else PreviewMat.color = BadColor;
    }

    public void MakePreview(int ID)
    {
        if(ID == lastID && PreviewObj != null)
        {
            Destroy(PreviewObj);
            return;
        }

        lastID = ID;
        if (PreviewObj != null) Destroy(PreviewObj);//Delete anything that was the past Preview
        PreviewObj = Instantiate(buildingDatabase.Towers[ID]);// Make the new Preview

        //Destroy or adjust the components of the preview
        foreach (MeshRenderer mesh in PreviewObj.GetComponentsInChildren<MeshRenderer>(true))
        {
            mesh.material = PreviewMat;
        }
        Destroy(PreviewObj.GetComponent<Turret>());
        Destroy(PreviewObj.GetComponent<UnityEngine.AI.NavMeshObstacle>());
        Destroy(PreviewObj.GetComponent<Collider>());
        foreach (Collider collider in PreviewObj.GetComponentsInChildren<Collider>(true))
        {
            Destroy(collider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(PreviewLoc, new Vector3(0.98f, 0.98f, 0.98f));
    }

    public void Build()
    {
        if (PreviewObj == null)
        {
            Debug.Log("Nothing to build");
            return;
        }
        if (!canBuild)
        {
            Debug.Log("Obstructed Build Loc");
            return;
        }
        int cost = buildingDatabase.Profiles[lastID].cost;
        if (Balance < cost)
        {
            Debug.Log("Insufficient funds");
            return;
        }
        Balance -= cost;
        Instantiate(buildingDatabase.Towers[lastID], PreviewLoc, Quaternion.identity);
    }
}
