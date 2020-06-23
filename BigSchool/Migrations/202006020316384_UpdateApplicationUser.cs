namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "LecturerId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Lecturer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Lecturer_Id" });
            DropIndex("dbo.Courses", new[] { "LecturerId_Id" });
            RenameColumn(table: "dbo.Courses", name: "Lecturer_Id", newName: "LecturerId");
            AlterColumn("dbo.Courses", "LecturerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Courses", "LecturerId");
            AddForeignKey("dbo.Courses", "LecturerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Courses", "LecturerId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "LecturerId_Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Courses", "LecturerId", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "LecturerId" });
            AlterColumn("dbo.Courses", "LecturerId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Courses", name: "LecturerId", newName: "Lecturer_Id");
            CreateIndex("dbo.Courses", "LecturerId_Id");
            CreateIndex("dbo.Courses", "Lecturer_Id");
            AddForeignKey("dbo.Courses", "Lecturer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "LecturerId_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
