using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Model;
using System;

namespace MarioKartStatistics.Migrations
{
    public partial class initial : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Heat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true)
                    })
                .PrimaryKey("PK_Heat", t => t.Id);
            
            migrationBuilder.CreateTable("HeatScore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        HeatId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_HeatScore", t => t.Id);
            
            migrationBuilder.CreateTable("Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String()
                    })
                .PrimaryKey("PK_Player", t => t.Id);
            
            migrationBuilder.AddForeignKey("HeatScore", "FK_HeatScore_Heat_HeatId", new[] { "HeatId" }, "Heat", new[] { "Id" }, cascadeDelete: false);
            
            migrationBuilder.AddForeignKey("HeatScore", "FK_HeatScore_Player_PlayerId", new[] { "PlayerId" }, "Player", new[] { "Id" }, cascadeDelete: false);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("HeatScore", "FK_HeatScore_Heat_HeatId");
            
            migrationBuilder.DropForeignKey("HeatScore", "FK_HeatScore_Player_PlayerId");
            
            migrationBuilder.DropTable("Heat");
            
            migrationBuilder.DropTable("HeatScore");
            
            migrationBuilder.DropTable("Player");
        }
    }
}