using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Co_P_Library.Models;

public partial class CoPNewContext : DbContext
{
    public CoPNewContext()
    {
    }

    public CoPNewContext(DbContextOptions<CoPNewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicYear> AcademicYears { get; set; }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<ActualActivity> ActualActivities { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<DailyAttendance> DailyAttendances { get; set; }

    public virtual DbSet<DaySummary> DaySummaries { get; set; }

    public virtual DbSet<DiagnosedWith> DiagnosedWiths { get; set; }

    public virtual DbSet<Duty> Duties { get; set; }

    public virtual DbSet<HealthProblem> HealthProblems { get; set; }

    public virtual DbSet<Interest> Interests { get; set; }

    public virtual DbSet<Kindergarten> Kindergartens { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<RegisterdTo> RegisterdTos { get; set; }

    public virtual DbSet<StaffMember> StaffMembers { get; set; }

    public virtual DbSet<SufferingFrom> SufferingFroms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInKindergarten> UserInKindergartens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= Yasmin;Database=Co-p new;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.HasKey(e => e.CurrentAcademicYear).HasName("PK__Academic__7E471AD338F7F444");

            entity.ToTable("Academic Year");

            entity.Property(e => e.CurrentAcademicYear).ValueGeneratedNever();

            entity.HasMany(d => d.KindergartenNumbers).WithMany(p => p.CurrentAcademicYears)
                .UsingEntity<Dictionary<string, object>>(
                    "KindergartenYear",
                    r => r.HasOne<Kindergarten>().WithMany()
                        .HasForeignKey("KindergartenNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Kindergar__Kinde__214BF109"),
                    l => l.HasOne<AcademicYear>().WithMany()
                        .HasForeignKey("CurrentAcademicYear")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Kindergar__Curre__2057CCD0"),
                    j =>
                    {
                        j.HasKey("CurrentAcademicYear", "KindergartenNumber").HasName("PK__Kinderga__8779E3CA15D9BEDB");
                        j.ToTable("Kindergarten Year");
                    });
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.ActivityNumber).HasName("PK__Activity__CA8A5612993C9DE1");

            entity.ToTable("Activity Type");

            entity.Property(e => e.ActivityName).HasMaxLength(20);
        });

        modelBuilder.Entity<ActualActivity>(entity =>
        {
            entity.HasKey(e => e.ActuaActivityNumber).HasName("PK__Actual A__0A3025C9DFC401EE");

            entity.ToTable("Actual Activity");

            entity.Property(e => e.Equipment).HasMaxLength(250);

            entity.HasOne(d => d.ActivityNumberNavigation).WithMany(p => p.ActualActivities)
                .HasForeignKey(d => d.ActivityNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Actual Ac__Activ__719CDDE7");

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.ActualActivities)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Actual Ac__Kinde__73852659");

            entity.HasOne(d => d.MealNumberNavigation).WithMany(p => p.ActualActivities)
                .HasForeignKey(d => d.MealNumber)
                .HasConstraintName("FK__Actual Ac__MealN__72910220");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceCode).HasName("PK__Attendan__013780A326D3DD24");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceCode).ValueGeneratedNever();
            entity.Property(e => e.AttendanceCodeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__Child__BEFA0736BE5FE93A");

            entity.ToTable("Child");

            entity.Property(e => e.ChildId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChildID");
            entity.Property(e => e.ChildBirthDate).HasColumnType("datetime");
            entity.Property(e => e.ChildFirstName).HasMaxLength(10);
            entity.Property(e => e.ChildGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ChildSurname).HasMaxLength(10);
            entity.Property(e => e.Parent1)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Parent_1");
            entity.Property(e => e.Parent2)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Parent_2");

            entity.HasOne(d => d.Parent1Navigation).WithMany(p => p.ChildParent1Navigations)
                .HasForeignKey(d => d.Parent1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Child__Parent_1__0697FACD");

            entity.HasOne(d => d.Parent2Navigation).WithMany(p => p.ChildParent2Navigations)
                .HasForeignKey(d => d.Parent2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Child__Parent_2__078C1F06");
        });

        modelBuilder.Entity<DailyAttendance>(entity =>
        {
            entity.HasKey(e => e.DailyAttendanceId).HasName("PK__DailyAtt__70B4ADABC28D2EA9");

            entity.ToTable("DailyAttendance");

            entity.Property(e => e.DailyAttendanceId).HasColumnName("DailyAttendanceID");
            entity.Property(e => e.ChildId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChildID");

            entity.HasOne(d => d.AfternoonPresenceNavigation).WithMany(p => p.DailyAttendanceAfternoonPresenceNavigations)
                .HasForeignKey(d => d.AfternoonPresence)
                .HasConstraintName("FK__DailyAtte__After__0D44F85C");

            entity.HasOne(d => d.Child).WithMany(p => p.DailyAttendances)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyAtte__Child__0B5CAFEA");

            entity.HasOne(d => d.MorningPresenceNavigation).WithMany(p => p.DailyAttendanceMorningPresenceNavigations)
                .HasForeignKey(d => d.MorningPresence)
                .HasConstraintName("FK__DailyAtte__Morni__0C50D423");
        });

        modelBuilder.Entity<DaySummary>(entity =>
        {
            entity.HasKey(e => e.DaySummaryDate).HasName("PK__Day Summ__2F5D71788747C820");

            entity.ToTable("Day Summary");

            entity.Property(e => e.DaySummaryDate).HasColumnType("datetime");
            entity.Property(e => e.DaySummaryHour).HasColumnType("datetime");
            entity.Property(e => e.SummaryDetails).HasMaxLength(500);

            entity.HasOne(d => d.CurrentAcademicYearNavigation).WithMany(p => p.DaySummaries)
                .HasForeignKey(d => d.CurrentAcademicYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Day Summa__Curre__18B6AB08");

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.DaySummaries)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Day Summa__Kinde__19AACF41");
        });

        modelBuilder.Entity<DiagnosedWith>(entity =>
        {
            entity.HasKey(e => new { e.ChildId, e.HealthProblemsNumber }).HasName("PK__Diagnose__BB2FE8CBDFE80A21");

            entity.ToTable("Diagnosed With");

            entity.Property(e => e.ChildId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChildID");
            entity.Property(e => e.Care)
                .HasMaxLength(500)
                .HasColumnName("care");

            entity.HasOne(d => d.Child).WithMany(p => p.DiagnosedWiths)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diagnosed__Child__318258D2");

            entity.HasOne(d => d.HealthProblemsNumberNavigation).WithMany(p => p.DiagnosedWiths)
                .HasForeignKey(d => d.HealthProblemsNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diagnosed__Healt__32767D0B");
        });

        modelBuilder.Entity<Duty>(entity =>
        {
            entity.HasKey(e => e.DutyDate).HasName("PK__Duty__34617F93632EABFA");

            entity.ToTable("Duty");

            entity.Property(e => e.DutyDate).HasColumnType("datetime");
            entity.Property(e => e.Child1)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Child_1");
            entity.Property(e => e.Child2)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Child_2");

            entity.HasOne(d => d.Child1Navigation).WithMany(p => p.DutyChild1Navigations)
                .HasForeignKey(d => d.Child1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duty__Child_1__10216507");

            entity.HasOne(d => d.Child2Navigation).WithMany(p => p.DutyChild2Navigations)
                .HasForeignKey(d => d.Child2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duty__Child_2__11158940");

            entity.HasOne(d => d.CurrentAcademicYearNavigation).WithMany(p => p.Duties)
                .HasForeignKey(d => d.CurrentAcademicYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duty__CurrentAca__1209AD79");

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.Duties)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duty__Kindergart__12FDD1B2");
        });

        modelBuilder.Entity<HealthProblem>(entity =>
        {
            entity.HasKey(e => e.HealthProblemsNumber).HasName("PK__Health P__5D5EFFD1F48AF89E");

            entity.ToTable("Health Problems");

            entity.Property(e => e.HealthProblemName).HasMaxLength(20);
        });

        modelBuilder.Entity<Interest>(entity =>
        {
            entity.HasKey(e => e.InterestsNumber).HasName("PK__Interest__00A5C7480F5B6EA5");

            entity.Property(e => e.InterestsName)
                .HasMaxLength(20)
                .HasColumnName("[InterestsName");
        });

        modelBuilder.Entity<Kindergarten>(entity =>
        {
            entity.HasKey(e => e.KindergartenNumber).HasName("PK__Kinderga__93EF919E8CD4A82A");

            entity.ToTable("Kindergarten");

            entity.Property(e => e.KindergartenNumber).ValueGeneratedNever();
            entity.Property(e => e.KindergartenAddress).HasMaxLength(30);
            entity.Property(e => e.KindergartenName).HasMaxLength(20);

            entity.HasMany(d => d.MealNumbers).WithMany(p => p.KindergartenNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "ServedIn",
                    r => r.HasOne<Meal>().WithMany()
                        .HasForeignKey("MealNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Served In__MealN__2EA5EC27"),
                    l => l.HasOne<Kindergarten>().WithMany()
                        .HasForeignKey("KindergartenNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Served In__Kinde__2DB1C7EE"),
                    j =>
                    {
                        j.HasKey("KindergartenNumber", "MealNumber").HasName("PK__Served I__80CBF1D45B5F4066");
                        j.ToTable("Served In");
                    });
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.MealNumber).HasName("PK__Meal__324604A0FA02207A");

            entity.ToTable("Meal");

            entity.Property(e => e.MealDetails).HasMaxLength(100);
            entity.Property(e => e.MealType).HasMaxLength(20);
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Parent__1788CCAC961265E5");

            entity.ToTable("Parent");

            entity.Property(e => e.UserId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.Parent)
                .HasForeignKey<Parent>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Parent__UserID__7FEAFD3E");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoCode).HasName("PK__Photo__D954591EA2E2B98F");

            entity.ToTable("Photo");

            entity.Property(e => e.PhotoDate).HasColumnType("datetime");
            entity.Property(e => e.PhotoHour).HasColumnType("datetime");

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Photo__Kindergar__15DA3E5D");
        });

        modelBuilder.Entity<RegisterdTo>(entity =>
        {
            entity.HasKey(e => new { e.CurrentAcademicYear, e.ChildId }).HasName("PK__Register__05A8BAA066EA92E4");

            entity.ToTable("Registerd To");

            entity.Property(e => e.ChildId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ChildID");

            entity.HasOne(d => d.Child).WithMany(p => p.RegisterdTos)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registerd__Child__29E1370A");

            entity.HasOne(d => d.CurrentAcademicYearNavigation).WithMany(p => p.RegisterdTos)
                .HasForeignKey(d => d.CurrentAcademicYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registerd__Curre__28ED12D1");

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.RegisterdTos)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registerd__Kinde__2AD55B43");
        });

        modelBuilder.Entity<StaffMember>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__staff me__1788CCACDFB2052C");

            entity.ToTable("staff member");

            entity.Property(e => e.UserId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.KindergartenNumber).ValueGeneratedOnAdd();

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.StaffMembers)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staff mem__Kinde__03BB8E22");

            entity.HasOne(d => d.User).WithOne(p => p.StaffMember)
                .HasForeignKey<StaffMember>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staff mem__UserI__02C769E9");
        });

        modelBuilder.Entity<SufferingFrom>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.HealthProblemsNumber }).HasName("PK__Sufferin__125D2351E3112990");

            entity.ToTable("Suffering From");

            entity.Property(e => e.UserId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.Care)
                .HasMaxLength(500)
                .HasColumnName("care");

            entity.HasOne(d => d.HealthProblemsNumberNavigation).WithMany(p => p.SufferingFroms)
                .HasForeignKey(d => d.HealthProblemsNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Suffering__Healt__36470DEF");

            entity.HasOne(d => d.User).WithMany(p => p.SufferingFroms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Suffering__UserI__3552E9B6");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC36EA283C");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserBirthDate).HasColumnType("datetime");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(40)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserGender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserPhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserPrivetName).HasMaxLength(10);
            entity.Property(e => e.UserSurname).HasMaxLength(10);
            entity.Property(e => e.UserpPassword).HasMaxLength(20);

            entity.HasMany(d => d.InterestsNumbers).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "InterestsOfStaffMember",
                    r => r.HasOne<Interest>().WithMany()
                        .HasForeignKey("InterestsNumber")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Interests__Inter__1D7B6025"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Interests__UserI__1C873BEC"),
                    j =>
                    {
                        j.HasKey("UserId", "InterestsNumber").HasName("PK__Interest__878290D812DD65B1");
                        j.ToTable("Interests of staff member");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(9)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("UserID");
                    });
        });

        modelBuilder.Entity<UserInKindergarten>(entity =>
        {
            entity.HasKey(e => new { e.CurrentAcademicYear, e.KindergartenNumber, e.UserId }).HasName("PK__User in __2A6E6B069ECB03C2");

            entity.ToTable("User in Kindergarten ");

            entity.Property(e => e.UserId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.CurrentAcademicYearNavigation).WithMany(p => p.UserInKindergartens)
                .HasForeignKey(d => d.CurrentAcademicYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User in K__Curre__24285DB4");

            entity.HasOne(d => d.KindergartenNumberNavigation).WithMany(p => p.UserInKindergartens)
                .HasForeignKey(d => d.KindergartenNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User in K__Kinde__251C81ED");

            entity.HasOne(d => d.User).WithMany(p => p.UserInKindergartens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User in K__UserI__2610A626");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
