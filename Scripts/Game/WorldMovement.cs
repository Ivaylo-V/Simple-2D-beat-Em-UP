using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private GameObject _level;
    [SerializeField] private GameObject _character;
    [SerializeField] private float _movementSpeed;

    private float delta = 100;
    private bool movingRight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0.07f, 0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(0.93f, 0, 0)).x;

        // Define raycast origins
        Vector2 rayOrigin = _character.transform.position;

        // Define ray directions
        Vector2 rayDirectionVertical = transform.forward;

        // Raycast distances
        float rayDistance = 1.6f;

        RaycastHit2D hitVertical = Physics2D.Raycast(rayOrigin, rayDirectionVertical, rayDistance, LayerMask.GetMask("Wall"));



        if(hitVertical.collider != null)
        {

        }
        else
        {
            if (_character.transform.position.x <= minX && Input.GetKey("a"))
            {
                movingRight = false;
                LevelMovement();
            }
            else if(_character.transform.position.x >= maxX && Input.GetKey("d"))
            {
                movingRight = true;
                LevelMovement();
            }
        }


    }

    private void LevelMovement()
    {

        Vector3 position = _level.transform.position;

        if (movingRight)
        {
            position.x -= _movementSpeed;
        }
        else
        {
            position.x += _movementSpeed;
        }
        

        _level.transform.position = position;
    }
}
