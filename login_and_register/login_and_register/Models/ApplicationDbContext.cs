
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace login_and_register.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {           
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotificationss { get; set; }
        public DbSet<CourseNotification> CourseNotificationss { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionAssignment> SubmissionAssignments { get; set; }
        public DbSet<QuestionsSubs> QuestionsSubs { get; set; }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        public DbSet<ChatGroup> ChatGroups { get; set; }

        public DbSet<ChatGroupMember> ChatGroupMembers { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    //var config = new ConfigurationBuilder().AddJsonFile("appsetting").Build();
        //    //var connectionstring = config.GetSection("constr").Value;
        //    optionsBuilder.UseSqlServer("Server = DESKTOP-MN6ULTF\\SSEXP; Database= AcademixDb; TrustServerCertificate = True; Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
