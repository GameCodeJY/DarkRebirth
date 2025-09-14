using System;

// Auto-generated model for table: player_phaeling_data
public partial class PlayerPhaelingData : IEquatable<PlayerPhaelingData>
{
    public string MonsterId;
    public int? ResidueCount;
    public string MonsterGrade;
    public string Ability1;
    public string Ability2;
    public string Ability3;
    public string Ability4;
    public int? Isowned;
    public PlayerPhaelingData() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public PlayerPhaelingData(PlayerPhaelingData other)
    {
        this.MonsterId = other.MonsterId;
        this.ResidueCount = other.ResidueCount;
        this.MonsterGrade = other.MonsterGrade;
        this.Ability1 = other.Ability1;
        this.Ability2 = other.Ability2;
        this.Ability3 = other.Ability3;
        this.Ability4 = other.Ability4;
        this.Isowned = other.Isowned;
    }
    public bool Equals(PlayerPhaelingData other)
    {
        if (other == null) return false;
        return
            MonsterId == other.MonsterId &&
            ResidueCount == other.ResidueCount &&
            MonsterGrade == other.MonsterGrade &&
            Ability1 == other.Ability1 &&
            Ability2 == other.Ability2 &&
            Ability3 == other.Ability3 &&
            Ability4 == other.Ability4 &&
            Isowned == other.Isowned;
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerPhaelingData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (MonsterId, ResidueCount, MonsterGrade, Ability1, Ability2, Ability3, Ability4, Isowned).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static PlayerPhaelingData operator +(PlayerPhaelingData a, PlayerPhaelingData b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new PlayerPhaelingData(a);
        result.MonsterId += b.MonsterId;
        result.ResidueCount += b.ResidueCount;
        result.MonsterGrade += b.MonsterGrade;
        result.Ability1 += b.Ability1;
        result.Ability2 += b.Ability2;
        result.Ability3 += b.Ability3;
        result.Ability4 += b.Ability4;
        result.Isowned += b.Isowned;
        return result;
    }

}
