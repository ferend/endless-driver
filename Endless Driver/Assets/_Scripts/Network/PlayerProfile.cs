using Realms;
using Realms.Sync;

public class PlayerProfile : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public string UserId { get; set; }
    
    [MapTo("high_score")]
    public int HighScore { get; set; }
    
    public PlayerProfile () {}

    public PlayerProfile(string userId)
    {
        this.UserId = userId;
        this.HighScore = 0;
    }
}
