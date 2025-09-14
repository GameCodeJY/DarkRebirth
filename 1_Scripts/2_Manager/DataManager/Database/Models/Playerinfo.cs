using System;

// Auto-generated model for table: PlayerInfo
public partial class Playerinfo : IEquatable<Playerinfo>
{
    public int PlayerId;
    public string Nickname;
    public int Level;
    public int? Exp;
    public string LastLoginTime;
    public Playerinfo() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public Playerinfo(Playerinfo other)
    {
        this.PlayerId = other.PlayerId;
        this.Nickname = other.Nickname;
        this.Level = other.Level;
        this.Exp = other.Exp;
        this.LastLoginTime = other.LastLoginTime;
    }
    public bool Equals(Playerinfo other)
    {
        if (other == null) return false;
        return
            PlayerId == other.PlayerId &&
            Nickname == other.Nickname &&
            Level == other.Level &&
            Exp == other.Exp &&
            LastLoginTime == other.LastLoginTime;
    }

    public override bool Equals(object obj)
    {
        return obj is Playerinfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (PlayerId, Nickname, Level, Exp, LastLoginTime).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static Playerinfo operator +(Playerinfo a, Playerinfo b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new Playerinfo(a);
        result.PlayerId += b.PlayerId;
        result.Nickname += b.Nickname;
        result.Level += b.Level;
        result.Exp += b.Exp;
        result.LastLoginTime += b.LastLoginTime;
        return result;
    }

}
