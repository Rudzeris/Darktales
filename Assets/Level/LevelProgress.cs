public class LevelProgress
{
    public int Saturation { get; private set; }
    public int CountEnemyDied { get; private set; }
    public int CountGetSaturation { get; private set; }
    public int CountOpenDoors { get; private set; }
    public int CountClosedDoors { get; private set; }
    public int CountUnlockedDoors { get; private set; }

    public void AddSaturation(int saturation)
    {
        Saturation += saturation;
        CountGetSaturation += saturation;
    }
    public void RemoveSaturation(int saturation)
    {
        Saturation -= saturation;
        CountGetSaturation -= saturation;
    }
    public void AddEnemyDied()
    {
        CountEnemyDied++;
    }
    public void AddOpenDoor() => CountOpenDoors++;
    public void AddClosedDoor() => CountClosedDoors++;
    public void AddUnlockedDoor() => CountUnlockedDoors++;

}
