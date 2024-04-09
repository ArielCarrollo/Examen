using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float x_Movement;
    [SerializeField] private float y_Movement;
    [SerializeField] public int Victory = 3;
    [SerializeField] private float vida = 100;
    [SerializeField] private Material[] mat;
    [SerializeField] public int Jumps;
    [SerializeField] public int fuerza;
    [SerializeField] public LayerMask layermask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("Right Click");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
        }
        if (Victory <= 0)
        {
            SceneManager.LoadScene("ganaste");
        }
        else if (vida <= 0)
        {
            SceneManager.LoadScene("perdiste");
        }
    }
    void FixedUpdate()
    {
        myRB.velocity = new Vector2(x_Movement * velocityModifier, myRB.velocity.y);
        Chequearpiso();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        x_Movement = context.ReadValue<Vector2>().x;
        y_Movement = context.ReadValue<Vector2>().y;
    }
    public void SwitchPL2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            this.GetComponent<Renderer>().material = mat[1];
            vida = 100;
        }
    }
    public void SwitchPL1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            this.GetComponent<Renderer>().material = mat[0];
            vida = 100;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumP();
        }
    }
    void JumP()
    {
        if (Jumps > 0)
        {
            myRB.AddForce(Vector2.up * fuerza, ForceMode.Impulse);
            Jumps--;
        }
    }
    void Chequearpiso()
    {
        RaycastHit raycast; 
        if (Physics.Raycast(transform.position, Vector3.down, out raycast, rayDistance, layermask))
        {
            if (raycast.collider.CompareTag("Piso"))
            {
                Jumps = 1;
            }
        }
    }
}
