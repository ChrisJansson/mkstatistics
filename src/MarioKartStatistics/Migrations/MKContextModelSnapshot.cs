using MarioKartStatistics;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace MarioKartStatistics.Migrations
{
    [ContextType(typeof(MKContext))]
    public class MKContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("MarioKartStatistics.Heat", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Key("Id");
                    });
                
                builder.Entity("MarioKartStatistics.HeatScore", b =>
                    {
                        b.Property<int>("HeatId");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<int>("PlayerId");
                        b.Property<int>("Score");
                        b.Key("Id");
                    });
                
                builder.Entity("MarioKartStatistics.Player", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<string>("Name");
                        b.Key("Id");
                    });
                
                builder.Entity("MarioKartStatistics.HeatScore", b =>
                    {
                        b.ForeignKey("MarioKartStatistics.Heat", "HeatId");
                        b.ForeignKey("MarioKartStatistics.Player", "PlayerId");
                    });
                
                return builder.Model;
            }
        }
    }
}