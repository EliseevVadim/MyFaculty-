﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFaculty.Persistence;

namespace MyFaculty.Persistence.Migrations
{
    [DbContext(typeof(MFDbContext))]
    [Migration("20221101110449_AddUsersTableWithCityAndCountryRelationships")]
    partial class AddUsersTableWithCityAndCountryRelationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("MyFaculty.Domain.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AvatarPath")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("TelegramLink")
                        .HasColumnType("longtext");

                    b.Property<string>("VKLink")
                        .HasColumnType("longtext");

                    b.Property<string>("Website")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Auditorium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AuditoriumName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("PositionInfo")
                        .HasColumnType("longtext");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Auditoriums");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CountryName")
                        .IsUnique();

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .HasColumnType("longtext");

                    b.Property<int>("CourseNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisciplineName")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<int?>("StateTransitionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("ExpertSystemAnswers");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Explanation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ExpertSystemStates");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemStateTransition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnswerId")
                        .HasColumnType("int");

                    b.Property<int>("FinalStateId")
                        .HasColumnType("int");

                    b.Property<int>("InitialStateId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLast")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId")
                        .IsUnique();

                    b.HasIndex("FinalStateId");

                    b.HasIndex("InitialStateId");

                    b.ToTable("ExpertSystemStateTransitions");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OfficialWebsite")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bounds")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GroupName")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Pair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AuditoriumId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DayOfWeekId")
                        .HasColumnType("int");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("PairInfoId")
                        .HasColumnType("int");

                    b.Property<string>("PairName")
                        .HasColumnType("longtext");

                    b.Property<int>("PairRepeatingId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuditoriumId");

                    b.HasIndex("DayOfWeekId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("GroupId");

                    b.HasIndex("PairInfoId");

                    b.HasIndex("PairRepeatingId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Pairs");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.PairInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EndTime")
                        .HasColumnType("longtext");

                    b.Property<int>("PairNumber")
                        .HasColumnType("int");

                    b.Property<string>("StartTime")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("PairInfos");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.PairRepeating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RepeatingName")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("PairRepeatings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 11, 1, 14, 4, 48, 39, DateTimeKind.Local).AddTicks(5635),
                            RepeatingName = "Каждую неделю"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 11, 1, 14, 4, 48, 39, DateTimeKind.Local).AddTicks(6329),
                            RepeatingName = "По верхней неделе"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2022, 11, 1, 14, 4, 48, 39, DateTimeKind.Local).AddTicks(6341),
                            RepeatingName = "По нижней неделе"
                        });
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ScienceRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RankName")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ScienceRanks");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.SecondaryObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("ObjectName")
                        .HasColumnType("longtext");

                    b.Property<string>("PositionInfo")
                        .HasColumnType("longtext");

                    b.Property<int>("SecondaryObjectTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("SecondaryObjectTypeId");

                    b.ToTable("SecondaryObjects");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.SecondaryObjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ObjectTypeName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TypePath")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ObjectTypeName")
                        .IsUnique();

                    b.HasIndex("TypePath")
                        .IsUnique();

                    b.ToTable("SecondaryObjectTypes");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FIO")
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("longtext");

                    b.Property<int>("ScienceRankId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ScienceRankId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.TeacherDiscipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherDisciplines");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.WorkDayOfWeek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DaysName")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("DaysOfWeek");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 11, 1, 14, 4, 47, 921, DateTimeKind.Local).AddTicks(6771),
                            DaysName = "Понедельник"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4864),
                            DaysName = "Вторник"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4901),
                            DaysName = "Среда"
                        },
                        new
                        {
                            Id = 4,
                            Created = new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4905),
                            DaysName = "Четверг"
                        },
                        new
                        {
                            Id = 5,
                            Created = new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4906),
                            DaysName = "Пятница"
                        },
                        new
                        {
                            Id = 6,
                            Created = new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4908),
                            DaysName = "Суббота"
                        });
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.AppUser", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Auditorium", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Floor", "Floor")
                        .WithMany("Auditoriums")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Auditoriums")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.City", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Course", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Faculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemAnswer", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.ExpertSystemState", "ExpertSystemState")
                        .WithMany("Answers")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertSystemState");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemStateTransition", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.ExpertSystemAnswer", "Answer")
                        .WithOne("ExpertSystemStateTransition")
                        .HasForeignKey("MyFaculty.Domain.Entities.ExpertSystemStateTransition", "AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.ExpertSystemState", "FinalState")
                        .WithMany("ExpertSystemFinalStateTransitions")
                        .HasForeignKey("FinalStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.ExpertSystemState", "InitialState")
                        .WithMany("ExpertSystemInitialStateTransitions")
                        .HasForeignKey("InitialStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("FinalState");

                    b.Navigation("InitialState");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Floor", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Faculty", "Faculty")
                        .WithMany("Floors")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Group", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Pair", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Auditorium", "Auditorium")
                        .WithMany("Pairs")
                        .HasForeignKey("AuditoriumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.WorkDayOfWeek", "DayOfWeek")
                        .WithMany("Pairs")
                        .HasForeignKey("DayOfWeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.Discipline", "Discipline")
                        .WithMany("Pairs")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.Group", "Group")
                        .WithMany("Pairs")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.PairInfo", "PairInfo")
                        .WithMany("Pairs")
                        .HasForeignKey("PairInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.PairRepeating", "PairRepeating")
                        .WithMany("Pairs")
                        .HasForeignKey("PairRepeatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Pairs")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auditorium");

                    b.Navigation("DayOfWeek");

                    b.Navigation("Discipline");

                    b.Navigation("Group");

                    b.Navigation("PairInfo");

                    b.Navigation("PairRepeating");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.SecondaryObject", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Floor", "Floor")
                        .WithMany("SecondaryObjects")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.SecondaryObjectType", "SecondaryObjectType")
                        .WithMany("SecondaryObjects")
                        .HasForeignKey("SecondaryObjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");

                    b.Navigation("SecondaryObjectType");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.ScienceRank", "ScienceRank")
                        .WithMany("Teachers")
                        .HasForeignKey("ScienceRankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScienceRank");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.TeacherDiscipline", b =>
                {
                    b.HasOne("MyFaculty.Domain.Entities.Discipline", "Discipline")
                        .WithMany("TeacherDisciplines")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFaculty.Domain.Entities.Teacher", "Teacher")
                        .WithMany("TeacherDisciplines")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Auditorium", b =>
                {
                    b.Navigation("Pairs");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Course", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Discipline", b =>
                {
                    b.Navigation("Pairs");

                    b.Navigation("TeacherDisciplines");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemAnswer", b =>
                {
                    b.Navigation("ExpertSystemStateTransition");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ExpertSystemState", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("ExpertSystemFinalStateTransitions");

                    b.Navigation("ExpertSystemInitialStateTransitions");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Faculty", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Floors");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Floor", b =>
                {
                    b.Navigation("Auditoriums");

                    b.Navigation("SecondaryObjects");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Group", b =>
                {
                    b.Navigation("Pairs");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.PairInfo", b =>
                {
                    b.Navigation("Pairs");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.PairRepeating", b =>
                {
                    b.Navigation("Pairs");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.ScienceRank", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.SecondaryObjectType", b =>
                {
                    b.Navigation("SecondaryObjects");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Auditoriums");

                    b.Navigation("Pairs");

                    b.Navigation("TeacherDisciplines");
                });

            modelBuilder.Entity("MyFaculty.Domain.Entities.WorkDayOfWeek", b =>
                {
                    b.Navigation("Pairs");
                });
#pragma warning restore 612, 618
        }
    }
}
