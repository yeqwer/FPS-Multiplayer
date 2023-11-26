using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _armHolder;
    [SerializeField] private GameObject _bodyCollider;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private int _hp = 100;
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _groundDrag = 5f;
    [SerializeField] private float _sensitivityX = 1f;
    [SerializeField] private float _sensitivityY = 1f;
    private Rigidbody _rb;
    private MainInputSystem _playerInput; 
    private SettingsCursor _settingsCursor;
    private float _xRotation;
    private float _yRotation;   
    public float playerHeight 
    { 
        get 
        {   
            return _playerHeight;
        }
        set 
        {   
            if (value <= 1) { _playerHeight = 1; } 
            else if (value >= 2) { _playerHeight = 2; }
            else { _playerHeight = value; }
        }
    }

    private void Awake()
    {
        _playerInput = new MainInputSystem();

        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        Time.timeScale = 1;
        
        _settingsCursor = new SettingsCursor();
        _settingsCursor.CursorOff();
    }
    
    private void Start() 
    {
        SelectGunActions.OnSelectM4A4(); //TEST
        PlayerActions.StartGame();


    }

    private void Update()
    {
        SetHeight();
        LookPlayer();
        CheckGunInput();
        CheckPause();

        if (CheckGround()) 
        {           
            if (_playerInput.PlayerController.Jump.triggered)
            {
                JumpPlayer();
            }
            
            MovePlayer();
            _rb.drag = _groundDrag;
        } 
        else 
        {
            _rb.drag = 0;
        }


    }

    private void CheckPause() 
    {
        if (_playerInput.UI.Esc.triggered)
        {
            PlayerActions.PauseGame();
        }
    }

    public void CheckPlayerHP()
    {
        _sliderHP.value = _hp;
        if (_hp <= 0)
        {
            Death();
        }
    }

    public void SetDamage(int damage)
    {
        _hp -= damage;
        CheckPlayerHP();
    }

    private void Death()
    {
        PlayerActions.GameOver();
    }

    private void CheckGunInput()
    {
        if (_playerInput.PlayerController.MouseLeftClick.inProgress)
        {
            GunActions.OnGunFire();
        }

        if (_playerInput.PlayerController.Reload.triggered)
        {
            GunActions.OnGunReload();
        }
        
        if (_playerInput.PlayerController.MouseScroll.triggered)
        {
            float changeInput = _playerInput.PlayerController.MouseScroll.ReadValue<float>();
            if (changeInput > 0) { SelectGunActions.OnSelectM4A4(); }
            else if (changeInput < 0) { SelectGunActions.OnSelectPistol(); }
        }

        if (_playerInput.ChooseGun._1.triggered) 
        { 
            SelectGunActions.OnSelectM4A4(); 
        }

        if (_playerInput.ChooseGun._2.triggered) 
        {
            SelectGunActions.OnSelectPistol();
        }
    }

    private void SetHeight() 
    {
        _bodyCollider.GetComponent<CapsuleCollider>().height = _playerHeight;
    }

    private bool CheckGround()
    {
        return Physics.Raycast(_bodyCollider.transform.position, Vector3.down, _playerHeight/2 + 0.2f, _whatIsGround);
    }

    private void MovePlayer()
    {
        _rb.AddForce(CalculateMoveDirection().normalized * _playerSpeed, ForceMode.Force);
    }
    
    private void JumpPlayer()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(gameObject.transform.up * _jumpForce, ForceMode.Impulse); 
    }

    private void LookPlayer() 
    {
        _head.transform.rotation = Quaternion.Euler(CalculateLookDirection());
        DelayHandHolder();
        SetCurrentlyLookForward();
    }

    private void DelayHandHolder() 
    {
        _armHolder.transform.rotation = Quaternion.Slerp(_armHolder.transform.rotation, _head.transform.rotation, Time.deltaTime * 50f);
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector2 playerVelocity = _playerInput.PlayerController.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = _bodyCollider.transform.forward * playerVelocity.y + _bodyCollider.transform.right * playerVelocity.x;

        return moveDirection;
    }

    private Vector3 CalculateLookDirection()
    {
        Vector2 mouseLook = _playerInput.PlayerController.MouseLook.ReadValue<Vector2>();

        _yRotation += mouseLook.x * _sensitivityY;
        _xRotation -= mouseLook.y * _sensitivityX;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
 
        return new Vector3(_xRotation, _yRotation, 0);
    }

    private void SetCurrentlyLookForward() 
    {
        _bodyCollider.transform.eulerAngles = new Vector3(0,_head.transform.eulerAngles.y,0);
    }

    private void OnEnable() 
    {
        _playerInput.Enable();
    }

    private void OnDisable() 
    {
        _playerInput.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_bodyCollider.transform.position, new Vector3(0, -_playerHeight/2 - 0.2f, 0));
    }
}