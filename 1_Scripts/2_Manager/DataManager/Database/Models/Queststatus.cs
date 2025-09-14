using System;

// Auto-generated model for table: QuestStatus
public partial class Queststatus : IEquatable<Queststatus>
{
    public int QuestId;
    public int PlayerId;
    public string Status;
    public int? Progress;
    public string UpdatedAt;
    public Queststatus() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public Queststatus(Queststatus other)
    {
        this.QuestId = other.QuestId;
        this.PlayerId = other.PlayerId;
        this.Status = other.Status;
        this.Progress = other.Progress;
        this.UpdatedAt = other.UpdatedAt;
    }
    public bool Equals(Queststatus other)
    {
        if (other == null) return false;
        return
            QuestId == other.QuestId &&
            PlayerId == other.PlayerId &&
            Status == other.Status &&
            Progress == other.Progress &&
            UpdatedAt == other.UpdatedAt;
    }

    public override bool Equals(object obj)
    {
        return obj is Queststatus other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (QuestId, PlayerId, Status, Progress, UpdatedAt).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static Queststatus operator +(Queststatus a, Queststatus b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new Queststatus(a);
        result.QuestId += b.QuestId;
        result.PlayerId += b.PlayerId;
        result.Status += b.Status;
        result.Progress += b.Progress;
        result.UpdatedAt += b.UpdatedAt;
        return result;
    }

}
