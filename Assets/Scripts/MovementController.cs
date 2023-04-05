using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]float speed = 5f;

    [SerializeField]private float lookSensitivity = 3f; //Inspectorで調整可    
    [SerializeField]GameObject fpsCamera; //Inspectorで紐づけ

    private Vector3 velocity = Vector3.zero;

    private Vector3 rotation = Vector3.zero; //初期値ゼロ
    private float CameraUpAndDownRotation = 0f; //視点上下用の変数
    private float CurrentCameraUpAndDownRotation = 0f; //視点上下用の変数

    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;
        velocity = _movementVelocity;
        
        float _yRotation = Input.GetAxis("Horizontal2"); //マウスカーソルの水平の動きを入力値として紐づけ
        Vector3 _rotationVector = new Vector3(0, _yRotation,0) * lookSensitivity; //y値のみ変動するVector3変数を宣言
        rotation = _rotationVector; //rotationに値を代入

        float _cameraUpDownRotation = -1 * Input.GetAxis("Vertical2") * lookSensitivity; //マウスカーソルの垂直の動きを入力値として紐づけ
        CameraUpAndDownRotation = _cameraUpDownRotation;
        
    }

    private void FixedUpdate()
    {
        if(velocity!= Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation)); //MoveRotationメソッドを呼び、回転させる値を渡す
        if(fpsCamera!= null) //Cameraがあるならば
            {
                CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation; //正の値を負に（カーソルのほうを向く回転値へ）
                CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation,-80,80); //値の最小最大を設定
                fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation,0,0); //x軸にのみカメラを回転させる
            }
        
        
    }
}