using System;

// Auto-generated model for table: player_equipped_skills
public partial class PlayerEquippedSkills : IEquatable<PlayerEquippedSkills>
{
    public string PlayerUid;
    public int SlotIndex;
    public string SkillId;
    public PlayerEquippedSkills() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public PlayerEquippedSkills(PlayerEquippedSkills other)
    {
        this.PlayerUid = other.PlayerUid;
        this.SlotIndex = other.SlotIndex;
        this.SkillId = other.SkillId;
    }
    public bool Equals(PlayerEquippedSkills other)
    {
        if (other == null) return false;
        return
            PlayerUid == other.PlayerUid &&
            SlotIndex == other.SlotIndex &&
            SkillId == other.SkillId;
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerEquippedSkills other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (PlayerUid, SlotIndex, SkillId).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static PlayerEquippedSkills operator +(PlayerEquippedSkills a, PlayerEquippedSkills b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new PlayerEquippedSkills(a);
        result.PlayerUid += b.PlayerUid;
        result.SlotIndex += b.SlotIndex;
        result.SkillId += b.SkillId;
        return result;
    }

}
