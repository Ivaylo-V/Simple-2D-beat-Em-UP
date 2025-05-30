using UnityEngine;
using UnityEngine.Sprites;
using System.Collections;

public class Animations : MonoBehaviour
{
    [SerializeField] private GameObject _character;

    [SerializeField] private Sprite attack1;
    [SerializeField] private Sprite attack2;
    [SerializeField] private Sprite attack3;
    [SerializeField] private Sprite attack4;
    [SerializeField] private Sprite attack5;
    [SerializeField] private Sprite attack6;

    [SerializeField] private Sprite Neutral;

    [SerializeField] private Sprite walk1;
    [SerializeField] private Sprite walk2;

    private bool walking = false;
    private bool alreadyWalking = false;

    private bool attacking = false;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Tab) && !attacking)
        {
            StartCoroutine(attackAnimation());
            attacking = true;
        }

        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            walking = true;
        }
        else
        {

            walking = false;
        }

        if (walking && !alreadyWalking)
        {
            StartCoroutine(walkingAnimation());
            alreadyWalking = true;
        }
        else if (!walking)
        {
            alreadyWalking = false;
        }

    }

    IEnumerator attackAnimation()
    {
        _character.GetComponent<SpriteRenderer>().sprite = attack1;
        yield return new WaitForSeconds(0.1f);
        _character.GetComponent<SpriteRenderer>().sprite = attack2;
        yield return new WaitForSeconds(0.1f);
        _character.GetComponent<SpriteRenderer>().sprite = attack3;
        yield return new WaitForSeconds(0.2f);
        _character.GetComponent<SpriteRenderer>().sprite = attack4;
        yield return new WaitForSeconds(0.1f);
        _character.GetComponent<SpriteRenderer>().sprite = attack5;
        yield return new WaitForSeconds(0.2f);
        _character.GetComponent<SpriteRenderer>().sprite = attack6;
        yield return new WaitForSeconds(0.1f);
        _character.GetComponent<SpriteRenderer>().sprite = Neutral;
        attacking = false;
        yield return null;
    }

    IEnumerator walkingAnimation()
    {

        if (walking)
        {
            _character.GetComponent<SpriteRenderer>().sprite = walk1;
            yield return new WaitForSeconds(0.2f);
            _character.GetComponent<SpriteRenderer>().sprite = walk2;
            yield return new WaitForSeconds(0.2f);
            yield return walkingAnimation();
        }
        else
        {
            yield return new WaitForSeconds(0.16f);
            _character.GetComponent<SpriteRenderer>().sprite = Neutral;
            yield return null;
        }

    }

    IEnumerator neutralAnimation()
    {
        _character.GetComponent<SpriteRenderer>().sprite = Neutral;
        yield return new WaitForSeconds(1);
        _character.GetComponent<SpriteRenderer>().sprite = Neutral;
        yield return new WaitForSeconds(1);
    }

}
