using System;

// Auto-generated model for table: player_equipped_skill_gems
public partial class PlayerEquippedSkillGems : IEquatable<PlayerEquippedSkillGems>
{
    public string PlayerUid;
    public int SlotIndex;
    public int GemIndex;
    public string GemId;
    public PlayerEquippedSkillGems() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public PlayerEquippedSkillGems(PlayerEquippedSkillGems other)
    {
        this.PlayerUid = other.PlayerUid;
        this.SlotIndex = other.SlotIndex;
        this.GemIndex = other.GemIndex;
        this.GemId = other.GemId;
    }
    public bool Equals(PlayerEquippedSkillGems other)
    {
        if (other == null) return false;
        return
            PlayerUid == other.PlayerUid &&
            SlotIndex == other.SlotIndex &&
            GemIndex == other.GemIndex &&
            GemId == other.GemId;
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerEquippedSkillGems other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (PlayerUid, SlotIndex, GemIndex, GemId).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static PlayerEquippedSkillGems operator +(PlayerEquippedSkillGems a, PlayerEquippedSkillGems b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new PlayerEquippedSkillGems(a);
        result.PlayerUid += b.PlayerUid;
        result.SlotIndex += b.SlotIndex;
        result.GemIndex += b.GemIndex;
        result.GemId += b.GemId;
        return result;
    }

}
