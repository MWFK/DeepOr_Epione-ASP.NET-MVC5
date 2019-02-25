namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v0 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" }, "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" });
            DropPrimaryKey("dbo.Chats");
            AlterColumn("dbo.IdentityUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Cursus", "dateCursus", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Appointments", "date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.User", "birthday", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.User", "createdDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.User", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Chats", "dateChat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Messages", "dateMessage", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Messages", "Chat_dateChat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Calendars", "dateCal", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddPrimaryKey("dbo.Chats", new[] { "PatientId", "DoctorId", "dateChat" });
            CreateIndex("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" });
            AddForeignKey("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" }, "dbo.Chats", new[] { "PatientId", "DoctorId", "dateChat" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" }, "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" });
            DropPrimaryKey("dbo.Chats");
            AlterColumn("dbo.Calendars", "dateCal", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "Chat_dateChat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "dateMessage", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Chats", "dateChat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.User", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.User", "createdDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.User", "birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cursus", "dateCursus", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IdentityUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddPrimaryKey("dbo.Chats", new[] { "PatientId", "DoctorId", "dateChat" });
            CreateIndex("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" });
            AddForeignKey("dbo.Messages", new[] { "Chat_PatientId", "Chat_DoctorId", "Chat_dateChat" }, "dbo.Chats", new[] { "PatientId", "DoctorId", "dateChat" }, cascadeDelete: true);
        }
    }
}
