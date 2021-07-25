namespace QuanLyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Minh_Model : DbContext
    {
        public Minh_Model()
            : base("name=Minh_Model")
        {
        }
        //DbSet<AssignTask> assigntask { get; set; }
        //DbSet<Project> project { get; set; }
        //DbSet<Employee> employee { get; set; }
        //DbSet<Client> client { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<QuanLyProject.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<QuanLyProject.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<QuanLyProject.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<QuanLyProject.Models.AssignTask> AssignTasks { get; set; }
    }
}
