using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Demoapplication.Model;

namespace Demoapplication.Migrations
{
    [DbContext(typeof(StudentDataBaseContext))]
    partial class StudentDataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Demoapplication.Model.StudentInformation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactNo");

                    b.Property<string>("Email");

                    b.Property<string>("FatherName");

                    b.Property<decimal>("Marks");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("StudentInformation");
                });
        }
    }
}
