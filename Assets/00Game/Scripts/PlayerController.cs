using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : Singleton<PlayerController>
{
    public  int _lives;
    [SerializeField] bool _isRun = false;
    [SerializeField] bool _isDeath = false;

    [SerializeField] Animator _anim;
    [SerializeField] Volume _volume;
    private ChromaticAberration _chromatic = null;
    private PaniniProjection _panini = null;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _volume.sharedProfile.TryGet<ChromaticAberration>(out _chromatic);
        _volume.sharedProfile.TryGet<PaniniProjection>(out _panini);
        Init();
    }

    public void Init()
    {
        _lives = 3;
        _isDeath = false;
        _isRun = false;
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.2f, 0, true));
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.2f, 0, true));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnim();

        if (GameManager.instance._gameState != GameManager.GAME_STATE.PLAY || _isDeath)
        {
            _isRun = false;
            return;
        }

        if(_lives <= 0)
        {
            _isDeath = true;
            GameManager.instance.ChangeState(GameManager.GAME_STATE.OVER);
        }

        _isRun = true;
        Debug.Log(Input.touchCount);

        if (Input.touchCount >= 2)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(-6, 0.57f, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(-6, -2, 0);
        }
        
    }

    void UpdateAnim()
    {
        _anim.SetBool("Run", _isRun);
        _anim.SetBool("Death", _isDeath);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DamageObj")
        {
            _lives--;
            HitVFX();
        }
        if (collision.gameObject.tag == "heart")
        {
            if(_lives<3)
                _lives++;
            PickVFX();
        }
    }

    void HitVFX()
    {
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(1, 0, true));
        StartCoroutine(ResetChromatic());
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);

    }
    void PickVFX()
    {
        _panini.distance.SetValue(new NoInterpMinFloatParameter(1, 0, true));
        StartCoroutine(ResetPanini());
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[3], 0, false);

    }
    IEnumerator ResetChromatic()
    {

        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.9f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.8f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.7f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.6f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.5f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.4f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.3f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.SetValue(new NoInterpMinFloatParameter(0.2f, 0, true));

    }
    IEnumerator ResetPanini()
    {

        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.9f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.8f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.7f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.6f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.5f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.4f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.3f, 0, true));
        yield return new WaitForSeconds(0.1f);
        _panini.distance.SetValue(new NoInterpMinFloatParameter(0.2f, 0, true));

    }
}
