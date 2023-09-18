using System;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public bool Equals(GridPosition other)
    {
        return this==other;
    }

    //Bool fonksiyonlarýnýn her seferinde dogru calismasi icin alinan iki hazir onlem
    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    

    //Bool fonksiyonlarýnýn her seferinde dogru calismasi icin alinan iki hazir onlem
    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public override string ToString()
    {
        return $"x: {x}, z: {z}";
    }

    public static bool operator ==(GridPosition left, GridPosition right) 
    {
        return left.x==right.x && left.z==right.z ;
    }

    public static bool operator !=(GridPosition left, GridPosition right)
    {
        return !(left==right);
    }
}