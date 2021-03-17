using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SawController : MonoBehaviour
{
    public GameObject[] sawController;
    int current = 0;
    float rotSpeed;
    public float speed;
    float SawRadius = 1;

    void Update()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * 200);
        if (Vector3.Distance(sawController[current].transform.position, transform.position) < SawRadius)
        {
            current++;
            if (current >= sawController.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, sawController[current].transform.position, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision col)
    {
        Run runningObject = col.gameObject.GetComponent<Run>();

        //if (runningObject != null)
        //{
            
        //    Debug.Log("Çarpışma gerçekleşti");
        //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        if(col.gameObject.tag == "DrawedMesh")
        {
            Debug.Log("Çarpışma ");
            speed = 0;
        }

    }
}
