using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float offSet;

    [SerializeField] private float offSetSmooth;
    [SerializeField] private float rotationSmooth;

    private Vector3 playerPosition;
    private Vector3 playerZRotation;
    private void Update()
    {

        if(playerTransform.localScale.x > 0f)
            playerPosition = new Vector3(playerTransform.position.x+ offSet, playerTransform.position.y, transform.position.z);
        else
            playerPosition = new Vector3(playerTransform.position.x - offSet, playerTransform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, offSetSmooth * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, playerTransform.rotation, rotationSmooth * Time.deltaTime);
    }
}
