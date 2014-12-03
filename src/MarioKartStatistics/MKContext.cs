using Microsoft.Data.Entity;
using System;

namespace  MarioKartStatistics
{
    public class MKContext : DbContext
    {
        public DbSet<Heat> Heats { get; set; }
    }

    public class Heat
    {
        public int A { get; set; }
    }
}