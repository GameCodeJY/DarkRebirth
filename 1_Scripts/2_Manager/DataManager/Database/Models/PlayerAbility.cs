using System;

// Auto-generated model for table: player_ability
public partial class PlayerAbility : IEquatable<PlayerAbility>
{
    public string PlayerUid;
    public float AttackPower;
    public float AttackSpeed;
    public float CriticalChance;
    public float CriticalDamage;
    public int MaxHp;
    public int CurrentHp;
    public float Defense;
    public float MagicResist;
    public float Stamina;
    public float StaminaRecovery;
    public float ItemDropBonus;
    public PlayerAbility() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public PlayerAbility(PlayerAbility other)
    {
        this.PlayerUid = other.PlayerUid;
        this.AttackPower = other.AttackPower;
        this.AttackSpeed = other.AttackSpeed;
        this.CriticalChance = other.CriticalChance;
        this.CriticalDamage = other.CriticalDamage;
        this.MaxHp = other.MaxHp;
        this.CurrentHp = other.CurrentHp;
        this.Defense = other.Defense;
        this.MagicResist = other.MagicResist;
        this.Stamina = other.Stamina;
        this.StaminaRecovery = other.StaminaRecovery;
        this.ItemDropBonus = other.ItemDropBonus;
    }
    public bool Equals(PlayerAbility other)
    {
        if (other == null) return false;
        return
            PlayerUid == other.PlayerUid &&
            AttackPower == other.AttackPower &&
            AttackSpeed == other.AttackSpeed &&
            CriticalChance == other.CriticalChance &&
            CriticalDamage == other.CriticalDamage &&
            MaxHp == other.MaxHp &&
            CurrentHp == other.CurrentHp &&
            Defense == other.Defense &&
            MagicResist == other.MagicResist &&
            Stamina == other.Stamina &&
            StaminaRecovery == other.StaminaRecovery &&
            ItemDropBonus == other.ItemDropBonus;
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerAbility other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (PlayerUid, AttackPower, AttackSpeed, CriticalChance, CriticalDamage, MaxHp, CurrentHp, Defense, MagicResist, Stamina, StaminaRecovery, ItemDropBonus).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static PlayerAbility operator +(PlayerAbility a, PlayerAbility b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new PlayerAbility(a);
        result.PlayerUid += b.PlayerUid;
        result.AttackPower += b.AttackPower;
        result.AttackSpeed += b.AttackSpeed;
        result.CriticalChance += b.CriticalChance;
        result.CriticalDamage += b.CriticalDamage;
        result.MaxHp += b.MaxHp;
        result.CurrentHp += b.CurrentHp;
        result.Defense += b.Defense;
        result.MagicResist += b.MagicResist;
        result.Stamina += b.Stamina;
        result.StaminaRecovery += b.StaminaRecovery;
        result.ItemDropBonus += b.ItemDropBonus;
        return result;
    }

}
