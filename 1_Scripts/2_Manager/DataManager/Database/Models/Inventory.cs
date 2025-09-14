using System;

// Auto-generated model for table: Inventory
public partial class Inventory : IEquatable<Inventory>
{
    public int ItemId;
    public string ItemType;
    public int Count;
    public string AcquiredTime;
    public int PlayerId;
    public Inventory() { }

    /// <summary>
    /// Copy ctor for deep clone of all fields
    /// </summary>
    public Inventory(Inventory other)
    {
        this.ItemId = other.ItemId;
        this.ItemType = other.ItemType;
        this.Count = other.Count;
        this.AcquiredTime = other.AcquiredTime;
        this.PlayerId = other.PlayerId;
    }
    public bool Equals(Inventory other)
    {
        if (other == null) return false;
        return
            ItemId == other.ItemId &&
            ItemType == other.ItemType &&
            Count == other.Count &&
            AcquiredTime == other.AcquiredTime &&
            PlayerId == other.PlayerId;
    }

    public override bool Equals(object obj)
    {
        return obj is Inventory other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (ItemId, ItemType, Count, AcquiredTime, PlayerId).GetHashCode();
    }

    // Operator+ : combine all fields by addition
    public static Inventory operator +(Inventory a, Inventory b)
    {
        if (a == null) return b;
        if (b == null) return a;
        var result = new Inventory(a);
        result.ItemId += b.ItemId;
        result.ItemType += b.ItemType;
        result.Count += b.Count;
        result.AcquiredTime += b.AcquiredTime;
        result.PlayerId += b.PlayerId;
        return result;
    }

}
