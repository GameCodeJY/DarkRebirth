using System;

// Auto-generated model for table: GameSettings
public partial class Gamesettings : IEquatable<Gamesettings>
{
    public int PlayerId;
    public string SettingKey;
    public string SettingValue;
    public Gamesettings() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public Gamesettings(Gamesettings other)
    {
        this.PlayerId = other.PlayerId;
        this.SettingKey = other.SettingKey;
        this.SettingValue = other.SettingValue;
    }
    public bool Equals(Gamesettings other)
    {
        if (other == null) return false;
        return
            PlayerId == other.PlayerId &&
            SettingKey == other.SettingKey &&
            SettingValue == other.SettingValue;
    }

    public override bool Equals(object obj)
    {
        return obj is Gamesettings other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (PlayerId, SettingKey, SettingValue).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static Gamesettings operator +(Gamesettings a, Gamesettings b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new Gamesettings(a);
        result.PlayerId += b.PlayerId;
        result.SettingKey += b.SettingKey;
        result.SettingValue += b.SettingValue;
        return result;
    }

}
