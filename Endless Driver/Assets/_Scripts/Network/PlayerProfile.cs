using Realms;

public class PlayerProfile : RealmObject
{
    [PrimaryKey]
    public string UserId { get; set; }
    public int HighScore { get; set; }
    
    public PlayerProfile () {}

    public PlayerProfile(string userId)
    {
        this.UserId = userId;
        this.HighScore = 0;
    }
}
