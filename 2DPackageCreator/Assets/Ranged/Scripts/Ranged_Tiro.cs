using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Tiro : MonoBehaviour
{
    public Rigidbody2D projetil;

    public Transform emissor;
    public Transform braco;

    public Transform player;

    private void Start()
    {
        braco = transform.GetChild(0);
    }

    private void Update()
    {
        Vector2 p_Pos = new Vector3(player.transform.position.x, player.transform.position.y);
        braco.transform.right = p_Pos * -1;

    }

    public void Atira ()
    {
        Rigidbody2D mao = Instantiate(projetil, emissor.position, emissor.rotation);
        mao.AddForce(Vector2.left * 1000);
        Object.Destroy(mao, 2.0f);
    }

}
