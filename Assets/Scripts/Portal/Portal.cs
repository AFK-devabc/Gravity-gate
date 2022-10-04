using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal ConnectedPortal;
    private bool IsTeleportable = true;

    [SerializeField] private Transform cameraTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if(collision.tag == "Player"  )   
        {
            if (IsTeleportable == true)
            {
                ConnectedPortal.SetTeleportable();


                collision.GetComponent<Transform>().position = ConnectedPortal.transform.position;
                //cameraTransform.rotation = ConnectedPortal.transform.rotation;
                collision.GetComponent<Transform>().rotation = ConnectedPortal.transform.rotation;
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

            }
        }
    }

    public void SetTeleportable()
    {
        IsTeleportable = false;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            IsTeleportable = true;
    }
}
