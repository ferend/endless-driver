using Realms;
using Realms.Sync;

public class PlayerProfile : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public string UserId { get; set; }
    
    [MapTo("high_score")]
    public int HighScore { get; set; }
    
    [MapTo("score")]

    public int Score { get; set; }
    
    public PlayerProfile () {}

    public PlayerProfile(string userId)
    {
        this.UserId = userId;
        this.Score = 0;
        this.HighScore = 0;
    }
}
