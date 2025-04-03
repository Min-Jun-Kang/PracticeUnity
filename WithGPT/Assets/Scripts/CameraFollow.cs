using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    public float rotationSpeed = 5f; // 회전 속도
    public float zoomSpeed = 5f; // 줌 속도
    public float minZoom = 5f, maxZoom = 15f; // 줌 범위

    private float targetRotationX = 0f; // 목표 X축 회전값
    private float targetRotationY = 0f; // 목표 Y축 회전값
    private float currentRotationX = 0f; // 현재 X축 회전값
    private float currentRotationY = 0f; // 현재 Y축 회전값
    private float currentZoom = 10f; // 현재 줌 값
    private Vector3 offset; // 카메라 위치 오프셋

    void Start()
    {
        offset = new Vector3(0, 3, -currentZoom); // 기본 오프셋 설정
    }

    void Update()
    {
        bool click = Input.GetMouseButton(1); // 마우스 오른쪽 버튼 확인

        if (click)
        {
            // 마우스 입력 받기
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // 목표 회전값 변경
            targetRotationY += mouseX;
            targetRotationX -= mouseY;

            // 위아래 회전 각도 제한
            targetRotationX = Mathf.Clamp(targetRotationX, -30f, 60f);

            // 🎯 부드러운 회전 적용 (현재 회전 → 목표 회전으로 Lerp 이동)
            currentRotationX = Mathf.Lerp(currentRotationX, targetRotationX, Time.deltaTime * 5f);
            currentRotationY = Mathf.Lerp(currentRotationY, targetRotationY, Time.deltaTime * 5f);
        }
        else
        {
            currentRotationX = Mathf.Lerp(currentRotationX, 0, Time.deltaTime * 5f);
            currentRotationY = Mathf.Lerp(currentRotationY, 0, Time.deltaTime * 5f);
        }
 

        // 🎯 마우스 휠 줌 기능
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); // 줌 거리 제한

        // 🎯 현재 줌 값 반영
        offset = new Vector3(0, 3, -currentZoom);

        // 🎯 새로운 위치와 회전 적용
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position);
    }
}
