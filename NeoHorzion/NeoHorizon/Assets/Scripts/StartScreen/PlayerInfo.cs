[System.Serializable]
public class PlayerInfo
{
    public string Name = "AAA";

    public int shootScore = 0;
    public int ballScore = 0;

    public int GetTotal() {
        return shootScore + ballScore;
    }
}
