using UnityEngine;

public class ARWorld : MonoBehaviour
{
    [SerializeField] GameObject walls;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("isAR", 0) != 0)
        {
            walls.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
