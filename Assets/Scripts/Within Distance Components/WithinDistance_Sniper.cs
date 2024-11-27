public class WithinDistance_Sniper : WithinDistance, IWithinDistance
{
    private Character character;

    public void Awake()
    {
        character = GetComponent<Character>();
    }

    protected override void WithinDistanceAction()
    {
        character.move.moving = true;
        character.MoveInDirection(-transform.up);
    }
}
