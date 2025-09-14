using System;

// Auto-generated model for table: Achievement
public partial class Achievement : IEquatable<Achievement>
{
    public int AchievementId;
    public int PlayerId;
    public int IsUnlocked;
    public string UnlockedTime;
    public Achievement() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public Achievement(Achievement other)
    {
        this.AchievementId = other.AchievementId;
        this.PlayerId = other.PlayerId;
        this.IsUnlocked = other.IsUnlocked;
        this.UnlockedTime = other.UnlockedTime;
    }
    public bool Equals(Achievement other)
    {
        if (other == null) return false;
        return
            AchievementId == other.AchievementId &&
            PlayerId == other.PlayerId &&
            IsUnlocked == other.IsUnlocked &&
            UnlockedTime == other.UnlockedTime;
    }

    public override bool Equals(object obj)
    {
        return obj is Achievement other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (AchievementId, PlayerId, IsUnlocked, UnlockedTime).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static Achievement operator +(Achievement a, Achievement b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new Achievement(a);
        result.AchievementId += b.AchievementId;
        result.PlayerId += b.PlayerId;
        result.IsUnlocked += b.IsUnlocked;
        result.UnlockedTime += b.UnlockedTime;
        return result;
    }

}
