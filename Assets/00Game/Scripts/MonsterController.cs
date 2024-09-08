using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance._gameState != GameManager.GAME_STATE.PLAY)
            return;

        this.transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);
        if (this.transform.position.x < -25)
        {
            this.gameObject.SetActive(false);
        }
    }
}
