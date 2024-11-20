using UnityEngine;

public class WithinDistance : MonoBehaviour
{
    private Character character;

    void Awake()
    {
        character = GetComponent<Character>();
    }

    public void WithinDistanceAction()
    {
        character.moving = true;
        character.MoveInDirection(-transform.up);
    }
}
