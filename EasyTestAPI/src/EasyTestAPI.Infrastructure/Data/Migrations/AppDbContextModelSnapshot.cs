﻿// <auto-generated />
using System;
using EasyTestAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EasyTestAPI.Core.Entities.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EasyTestAPI.Core.Entities.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Answer", b =>
                {
                    b.Property<string>("AnswerId")
                        .HasColumnType("text");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.AnsweredTest", b =>
                {
                    b.Property<string>("AnsweredTestId")
                        .HasColumnType("text");

                    b.Property<string>("TestId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("AnsweredTestId");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("AnsweredTests");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Question", b =>
                {
                    b.Property<string>("QuestionId")
                        .HasColumnType("text");

                    b.Property<string>("QuestionTypeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TestId")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionTypeId");

                    b.HasIndex("TestId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.QuestionType", b =>
                {
                    b.Property<string>("QuestionTypeId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("QuestionTypeId");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Test", b =>
                {
                    b.Property<string>("TestId")
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TestId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.TestAnswer", b =>
                {
                    b.Property<string>("TestAnswerId")
                        .HasColumnType("text");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AnsweredTestId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TestAnswerId");

                    b.HasIndex("AnsweredTestId");

                    b.HasIndex("QuestionId");

                    b.ToTable("TestAnswers");
                });

            modelBuilder.Entity("EasyTestAPI.Core.Entities.User", b =>
                {
                    b.HasOne("EasyTestAPI.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Answer", b =>
                {
                    b.HasOne("EasyTestAPI.Core.TestAggregate.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.AnsweredTest", b =>
                {
                    b.HasOne("EasyTestAPI.Core.TestAggregate.Test", "Test")
                        .WithMany("AnsweredTests")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTestAPI.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Question", b =>
                {
                    b.HasOne("EasyTestAPI.Core.TestAggregate.QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTestAPI.Core.TestAggregate.Test", null)
                        .WithMany("Questions")
                        .HasForeignKey("TestId");

                    b.Navigation("QuestionType");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Test", b =>
                {
                    b.HasOne("EasyTestAPI.Core.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.TestAnswer", b =>
                {
                    b.HasOne("EasyTestAPI.Core.TestAggregate.AnsweredTest", "AnsweredTest")
                        .WithMany("TestAnswers")
                        .HasForeignKey("AnsweredTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTestAPI.Core.TestAggregate.Question", "Question")
                        .WithMany("TestAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnsweredTest");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.AnsweredTest", b =>
                {
                    b.Navigation("TestAnswers");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("TestAnswers");
                });

            modelBuilder.Entity("EasyTestAPI.Core.TestAggregate.Test", b =>
                {
                    b.Navigation("AnsweredTests");

                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
