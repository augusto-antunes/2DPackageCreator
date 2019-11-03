using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerSkill
{
    dash, doubleJump
}

public class Player : MonoBehaviour, ISaveObject
{
    public CapsuleCollider2D collx;
    public Transform[] slot;
    bool dashPower;
    public float maxSpeed;
    public Transform[] groundCheck;
    public float jumpForce;
    public float fireRate;
    public int maxHealth;
    public int maxMana;
    public int strength;
    public int defense;
    public int souls;
    public float dashForce;
    public bool dashSkill = true;
    public int manaCost;
    public Rigidbody2D projectile;
    public Transform aim;
    public GameObject flame;
    public PhysicMaterial friction;
    public float speed;
    public Rigidbody2D rb;
    private bool facingRight = true;
    private bool onGround;
    private bool falls;
    private bool jump = false;
    bool nev;
    float fnv;
    float cdnv;
    private bool doubleJump;
    public Animator anim;
    private float nextAttack;
    private int health;
    private int mana;
    private bool canDamage = true;
    private SpriteRenderer sprite;
    private bool isDead = false;
    private bool dash = false;
    public Transform aimi;
    bool paused;
    [SerializeField] int ID;
    [SerializeField] string playerName;
    [SerializeField] string currentScene;
    [SerializeField] string DashV;
    [SerializeField] string Fire;
    Scene m_scene;
    // Use this for initialization
    
    void Awake()
    {
        isDead = false;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_scene = SceneManager.GetActiveScene();
        if (DashV == "DashV")
        {
            dashPower = true;
        }
        currentScene = m_scene.name.ToString();
        Debug.Log(currentScene);

        //Ganon();

    }

    public void Reload()
    {
        paused = false;
        isDead = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(m_scene.name.ToString());
        Time.timeScale = 1;
        GO.SetActive(false);

    }
    public string[] GetCurrentValues()
    {
        return new string[]
        {
            ID.ToString(),
            playerName,
            currentScene,
            DashV,
            Fire
        };
    }
    public string[] GetKeys()
    {
        return new string[]
        {
            "ID",
            "PlayerName",
            "CurrentScene",
            "DashV",
            "Fire"
        };
    }

    public void LoadFromDefault()
    {
        ID = 1;
        playerName = "";
        currentScene = m_scene.name;
        DashV = null;
    }

    public void LoadFromValues(string[] values)
    {
        int.TryParse(values[0], out ID);
        playerName = values[1];
        //this.gameObject.transform.position = slot[ID].position;
        currentScene = values[2];
        DashV = values[3];
    }
    float cool;
    public GameObject a1;
    public GameObject a2;
    // Update is called once per frame
    bool firing;
    void Update()
    {
        
        if (!paused)
        {
            
            //Debug.Log(onGround);
            

            if (!isDead)
            {
                fnv -= Time.deltaTime;
                //Debug.Log(fnv);
                if (Input.GetKeyDown(KeyCode.Z) && cdnv > 5 && !crouch)
                {
                    nev = true;
                    fnv = 3;
                    cdnv = 0;
                }
                else if(fnv <= 0)
                {
                    nev = false;
                    cdnv += 0.1f;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Bola-de-Fogo") || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Machadada"))
                {
                    rb.velocity = Vector2.zero;
                    firing = true;

                }
                else
                {
                    //Debug.Log("oof");
                    firing = false;
                }
                if (Input.GetKeyDown(KeyCode.C) && cool > 3 && onGround && !crouch)
                {
                    
                    cool = 0;
                    Instantiate(flame, aim.transform.position, aim.transform.rotation);
                    anim.SetTrigger("Fire");
                    

                }
                if (Input.GetKeyDown(KeyCode.V) && onGround && !crouch)
                {

                    anim.SetTrigger("Attack");


                }
                if (Input.GetKeyDown(KeyCode.C) && cool > 3 && !onGround && !crouch)
                {

                    cool = 0;
                    Instantiate(flame, aim.transform.position, aim.transform.rotation);
                    anim.SetBool("FireAir", true);
                    if (!IsInvoking("DeFire"))
                    {
                        Invoke("DeFire", 0.7f);
                    }


                }
                if (Input.GetKeyDown(KeyCode.V) && !onGround && !crouch)
                {

                    anim.SetBool("AirAttack", true);
                    if (!IsInvoking("Deat"))
                    {
                        Invoke("Deat", 1.2f);
                    }


                }
                else
                {
                    cool += 0.1f;
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pausing();
                }
                if (Input.GetKey(KeyCode.O))
                {
                    //DELETA A PORRA TODA DE SAVE, PARCIMONIA.
                    PlayerPrefs.DeleteAll();
                }
                if (!Input.anyKey)
                {
                    anim.SetBool("Idle", true);
                }
                else
                {
                    anim.SetBool("Idle", false);
                }
                for (int i = 0; i < groundCheck.Length; i++)
                {

                    onGround = Physics2D.Linecast(transform.position, groundCheck[i].position, 1 << LayerMask.NameToLayer("Ground"));
                    if (onGround && !falls)
                    {
                        
                        anim.SetBool("Floor", true);
                        doubleJump = false;
                        rb.gravityScale = 0;
                        
                    }
                    if (!onGround && !dash)
                    {
                        rb.gravityScale = 10;
                    }


                    if (Input.GetButtonDown("Jump") && (onGround) && !dash && !crouch)
                    {
                        jump = true;
                        anim.SetBool("Floor", false);
                        /*if (!doubleJump && !onGround)
                            doubleJump = true;*/
                    }
                    else if(!falls)
                    {
                        speed = 20;
                    }
                    falls = Physics2D.Linecast(transform.position, groundCheck[i].position, 1 << LayerMask.NameToLayer("Falls"));


                    if (dashPower)
                    {
                        if (onGround && Input.GetKeyDown(KeyCode.X) && !dash && !crouch)
                        {
                            
                            anim.SetBool("Dash", true);
                            dash = true;
                            Invoke("DashFalse", 1f);

                        }
                        else if (!onGround && Input.GetKeyDown(KeyCode.X) && !dash && !doubleJump && !crouch)
                        {
                            rb.velocity = Vector3.zero;
                            rb.gravityScale = 0;
                            doubleJump = true;
                            anim.SetBool("Dash", true);
                            dash = true;
                            Invoke("DashFalse", 1f);

                        }

                    }
                }
            }
        }

    }
    void DeFire()
    {
        anim.SetBool("FireAir", false);
    }
    void Deat()
    {

        anim.SetBool("AirAttack", false);
    }
    bool crouch;
    float h;
    private void FixedUpdate()
    {
        if (!isDead && !paused)
        {
            if (Input.GetKey(KeyCode.DownArrow) && onGround)
            {
                anim.SetBool("crouch", true);
                crouch = true;
            }
            else
            {
                anim.SetBool("crouch", false);
                 crouch = false;
            }
            //Debug.Log(falls);
            rb.velocity.Normalize();
                
            if (falls || firing || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Agachar") || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Agachar_Reverso"))
            {

                speed = 0;
                h = 0;
                if (crouch)
                {
                    //collx.offset = new Vector2(0, 0);
                }
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Agachado") || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Transicao_Andar_Agachado") || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Agachar")
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Andar_Agachado") || anim.GetCurrentAnimatorStateInfo(0).IsName("Raoni_Transicao_Andar_Agachado_Reverso")) {
                collx.size = new Vector2(5, 2);
                collx.offset = new Vector2(0, -3.1807022f);
                speed = 3;
            h = Input.GetAxis("Horizontal");
            }else
            {
                collx.offset = new Vector2(0, -0.1807022f);
                collx.size = new Vector2(5, 10);
                speed = 20;
                h = Input.GetAxisRaw("Horizontal");
            }

            if (canDamage && !dash)
            {
                rb.velocity = new Vector2(h * speed, rb.velocity.y);
            }

            anim.SetFloat("Speed", Mathf.Abs(h));
            anim.SetFloat("VertSpeed", Mathf.Abs(rb.velocity.y));
            if (rb.velocity.y > 1.6f || rb.velocity.y < -1.6f) {
                anim.SetBool("Floor", false);
            }

            if (h > 0 && !facingRight && !dash)
            {
                Flip();
            }
            else if (h < 0 && facingRight && !dash)
            {
                Flip();
            }

            if (jump)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
                jump = false;
            }
            if (dash)
            {
                int hforce = facingRight ? -1 : 1;
                rb.AddForce(Vector2.left * dashForce * hforce);
            }


        }


    }
    public GameObject GO;
    public GameObject GO2;
    //Or else you must DIE. O Codigo faz morrer, blz?
    void Ganon()
    {
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GO.SetActive(true);
        Time.timeScale = 0;
    }

    void Pausing()
    {
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GO2.SetActive(true);
        Time.timeScale = 0;
    }
    public void Unpause()
    {
        Debug.Log("Foi");
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GO2.SetActive(false);
        Time.timeScale = 1;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

   
    

    public int GetHealth()
    {
        return health;
    }

    public int GetMana()
    {
        return mana;
    }

    /*public void TakeDamage(int damage)
    {
        if (canDamage)
        {
            canDamage = false;
            health -= (damage - defense);
            FindObjectOfType<UIManager>().UpdateUI();
            if (health <= 0)
            {
                anim.SetTrigger("Dead");
                Invoke("ReloadScene", 3f);
                isDead = true;
            }
            else
            {
                StartCoroutine(DamageCoroutine());
            }
        }
    }*/


    public void leave()
    {
        Application.Quit();
    }


    public void DashTrue()
    {
        
        dash = true;
    }

    public void DashFalse()
    {
            dash = false;
        rb.gravityScale = 10;
            anim.SetBool("Dash", false);
    }

    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DashPower")
        {
            DashV = "DashV";
            dashPower = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Finish" && isDead == false && !nev)
        {
            
            anim.SetBool("Dead", true);
            isDead = true;
            
            Invoke("Ganon", 2.0f);
        }

    }

}
