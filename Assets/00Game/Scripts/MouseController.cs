using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonsterController
{
    bool _teleported;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init()
    {
        _teleported = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance._gameState != GameManager.GAME_STATE.PLAY)
            return;

        this.transform.Translate(Vector3.left * base._movementSpeed * Time.deltaTime);
        if (this.transform.position.x < -25)
        {
            this.gameObject.SetActive(false);
        }

        if (this.transform.position.x <= 0f && !_teleported)
        {
            if(this.transform.position.y < 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.85f, 0);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, -1.75f, 0);
            }
            _teleported = true;
        }
    }
}
