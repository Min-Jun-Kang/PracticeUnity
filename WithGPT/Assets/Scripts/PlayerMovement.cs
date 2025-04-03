using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A, D 또는 왼쪽/오른쪽 화살표
        float moveZ = Input.GetAxis("Vertical");   // W, S 또는 위/아래 화살표

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
