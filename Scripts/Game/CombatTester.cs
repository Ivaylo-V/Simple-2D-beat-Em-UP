// Author: Nick Hwang
// For: Beat Em Up Style Tutorials
// youtube.com/c/nickhwang

using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CombatTester : MonoBehaviour
{
    [SerializeField] private bool canAttack = true;
    
    [SerializeField] private Collider2D inLineCollider;
    
    [SerializeField] private LayerMask enemyLayer;
    
    PlayerInput input;
    Controls controls = new Controls();
    private ContactFilter2D contactFilter2D;
    private GameObject target;
    public List<Collider2D> cols = new List<Collider2D>();
    
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        contactFilter2D.SetLayerMask(enemyLayer);
    }

    // Update is called once per frame
    void Update()
    {
        controls = input.GetInput();
        if (controls.AttackState)
        {
            
            inLineCollider.Overlap(contactFilter2D, cols);
            if (cols.Count > 0)
            {
                foreach (var col in cols)
                {
                    print(col.transform.name);
                    if (col.TryGetComponent(out SpriteRenderer sr))
                    {
                        target = sr.gameObject;
                        StartCoroutine("FlashDMG");
                    }
                }
            }
        }
    }


  
        private IEnumerator FlashDMG()
        {
            while (true)
            {
                for(int i = 0; i < 2; i++)
                {
                    Debug.Log($"{i}");
                    target.GetComponent<SpriteRenderer>().color = Color.red;
                    yield return new WaitForSeconds(0.2f);
                    target.GetComponent<SpriteRenderer>().color = Color.white;
                    yield return new WaitForSeconds(0.2f);
                   
                }

                yield return new WaitForSeconds(0.2f);

                target.SetActive(false);
                StopCoroutine("FlashDMG");
                
            }
        }
    
}
