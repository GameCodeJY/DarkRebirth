using System;

// Auto-generated model for table: player_save_data
public partial class PlayerSaveData : IEquatable<PlayerSaveData>
{
    public string PlayerUid;
    public string LastLoginAt;
    public string Gender;
    public int PlayerLevel;
    public int PlayerExp;
    public int TutorialRunCount;
    public string LastTutorialStage;
    public int StrStat;
    public int DexStat;
    public int IntStat;
    public int ConStat;
    public int LuckStat;
    public int StatPoint;
    public int TraitPoint;
    public int Gold;
    public int Blitz;
    public int Ruin;
    public int Stella;
    public int ChaosMarkCount;
    public string EquippedPhaeling;
    public string EquippedPartnerBoss;
    public int ReachedSavePoint;
    public int StageRepeatCount;
    public string MaxStage;
    public string CreatedAt;
    public string UpdatedAt;
    public PlayerSaveData() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public PlayerSaveData(PlayerSaveData other)
    {
        this.PlayerUid = other.PlayerUid;
        this.LastLoginAt = other.LastLoginAt;
        this.Gender = other.Gender;
        this.PlayerLevel = other.PlayerLevel;
        this.PlayerExp = other.PlayerExp;
        this.TutorialRunCount = other.TutorialRunCount;
        this.LastTutorialStage = other.LastTutorialStage;
        this.StrStat = other.StrStat;
        this.DexStat = other.DexStat;
        this.IntStat = other.IntStat;
        this.ConStat = other.ConStat;
        this.LuckStat = other.LuckStat;
        this.StatPoint = other.StatPoint;
        this.TraitPoint = other.TraitPoint;
        this.Gold = other.Gold;
        this.Blitz = other.Blitz;
        this.Ruin = other.Ruin;
        this.Stella = other.Stella;
        this.ChaosMarkCount = other.ChaosMarkCount;
        this.EquippedPhaeling = other.EquippedPhaeling;
        this.EquippedPartnerBoss = other.EquippedPartnerBoss;
        this.ReachedSavePoint = other.ReachedSavePoint;
        this.StageRepeatCount = other.StageRepeatCount;
        this.MaxStage = other.MaxStage;
        this.CreatedAt = other.CreatedAt;
        this.UpdatedAt = other.UpdatedAt;
    }
    public bool Equals(PlayerSaveData other)
    {
        if (other == null) return false;
        return
            PlayerUid == other.PlayerUid &&
            LastLoginAt == other.LastLoginAt &&
            Gender == other.Gender &&
            PlayerLevel == other.PlayerLevel &&
            PlayerExp == other.PlayerExp &&
            TutorialRunCount == other.TutorialRunCount &&
            LastTutorialStage == other.LastTutorialStage &&
            StrStat == other.StrStat &&
            DexStat == other.DexStat &&
            IntStat == other.IntStat &&
            ConStat == other.ConStat &&
            LuckStat == other.LuckStat &&
            StatPoint == other.StatPoint &&
            TraitPoint == other.TraitPoint &&
            Gold == other.Gold &&
            Blitz == other.Blitz &&
            Ruin == other.Ruin &&
            Stella == other.Stella &&
            ChaosMarkCount == other.ChaosMarkCount &&
            EquippedPhaeling == other.EquippedPhaeling &&
            EquippedPartnerBoss == other.EquippedPartnerBoss &&
            ReachedSavePoint == other.ReachedSavePoint &&
            StageRepeatCount == other.StageRepeatCount &&
            MaxStage == other.MaxStage &&
            CreatedAt == other.CreatedAt &&
            UpdatedAt == other.UpdatedAt;
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerSaveData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (PlayerUid, LastLoginAt, Gender, PlayerLevel, PlayerExp, TutorialRunCount, LastTutorialStage, StrStat, DexStat, IntStat, ConStat, LuckStat, StatPoint, TraitPoint, Gold, Blitz, Ruin, Stella, ChaosMarkCount, EquippedPhaeling, EquippedPartnerBoss, ReachedSavePoint, StageRepeatCount, MaxStage, CreatedAt, UpdatedAt).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static PlayerSaveData operator +(PlayerSaveData a, PlayerSaveData b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new PlayerSaveData(a);
        result.PlayerUid += b.PlayerUid;
        result.LastLoginAt += b.LastLoginAt;
        result.Gender += b.Gender;
        result.PlayerLevel += b.PlayerLevel;
        result.PlayerExp += b.PlayerExp;
        result.TutorialRunCount += b.TutorialRunCount;
        result.LastTutorialStage += b.LastTutorialStage;
        result.StrStat += b.StrStat;
        result.DexStat += b.DexStat;
        result.IntStat += b.IntStat;
        result.ConStat += b.ConStat;
        result.LuckStat += b.LuckStat;
        result.StatPoint += b.StatPoint;
        result.TraitPoint += b.TraitPoint;
        result.Gold += b.Gold;
        result.Blitz += b.Blitz;
        result.Ruin += b.Ruin;
        result.Stella += b.Stella;
        result.ChaosMarkCount += b.ChaosMarkCount;
        result.EquippedPhaeling += b.EquippedPhaeling;
        result.EquippedPartnerBoss += b.EquippedPartnerBoss;
        result.ReachedSavePoint += b.ReachedSavePoint;
        result.StageRepeatCount += b.StageRepeatCount;
        result.MaxStage += b.MaxStage;
        result.CreatedAt += b.CreatedAt;
        result.UpdatedAt += b.UpdatedAt;
        return result;
    }

}
