using Microsoft.Data.Entity;
using System;
using Microsoft.Data.Entity.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarioKartStatistics
{
    public class MKContext : DbContext
    {
        public DbSet<Heat> Heats { get; set; }
        public DbSet<HeatScore> HeatScores { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Heat>()
                .Key(x => x.Id);

            modelBuilder.Entity<Heat>()
                .OneToMany(x => x.Scores)
                .ForeignKey(x => x.HeatId)
                .Required();

            modelBuilder.Entity<HeatScore>()
                .Key(x => x.Id);

            modelBuilder.Entity<HeatScore>()
                .Property(x => x.Id);

            modelBuilder.Entity<HeatScore>()
                .ManyToOne(x => x.Player)
                .ForeignKey(x => x.PlayerId)
                .Required();
        }
    }

    public class Heat
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<HeatScore> Scores { get; set; }
    }

    public class HeatScore
    {
        public int Id { get; set; }
        public int HeatId { get; set; }
        public virtual Player Player { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}