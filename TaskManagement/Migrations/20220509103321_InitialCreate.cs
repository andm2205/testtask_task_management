using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR ALTER FUNCTION GetTree(@id BIGINT)
RETURNS TABLE
RETURN (
	WITH tree (
        [Id]
        ,[Name]
        ,[Description]
        ,[Performers]
        ,[RegistrationDate]
        ,[Status]
        ,[ScheduledExecutionTime]
        ,[ActualExecutionTime]
        ,[CompletionDate]
        ,[ParentId]) 
    AS (
	    SELECT 
            t1.[Id]
            ,t1.[Name]
            ,t1.[Description]
            ,t1.[Performers]
            ,t1.[RegistrationDate]
            ,t1.[Status]
            ,t1.[ScheduledExecutionTime]
            ,t1.[ActualExecutionTime]
            ,t1.[CompletionDate]
            ,t1.[ParentId]
		FROM [TaskManagementDb].[dbo].[Tasks] t1
		WHERE Id = @id
	UNION ALL
		SELECT t2.[Id]
      ,t2.[Name]
      ,t2.[Description]
      ,t2.[Performers]
      ,t2.[RegistrationDate]
      ,t2.[Status]
      ,t2.[ScheduledExecutionTime]
      ,t2.[ActualExecutionTime]
      ,t2.[CompletionDate]
      ,t2.[ParentId]
		FROM [TaskManagementDb].[dbo].[Tasks] t2
		INNER JOIN tree ON t2.ParentId = tree.[Id]
	) SELECT * FROM tree
)");
            migrationBuilder.Sql(@"
CREATE OR ALTER FUNCTION GetParents(@id BIGINT)
RETURNS TABLE
RETURN (
	WITH tree ([Id]
      ,[Name]
      ,[Description]
      ,[Performers]
      ,[RegistrationDate]
      ,[Status]
      ,[ScheduledExecutionTime]
      ,[ActualExecutionTime]
      ,[CompletionDate]
      ,[ParentId]) as (
		SELECT t1.[Id]
      ,t1.[Name]
      ,t1.[Description]
      ,t1.[Performers]
      ,t1.[RegistrationDate]
      ,t1.[Status]
      ,t1.[ScheduledExecutionTime]
      ,t1.[ActualExecutionTime]
      ,t1.[CompletionDate]
      ,t1.[ParentId]
		FROM [TaskManagementDb].[dbo].[Tasks] t1
		WHERE Id = @id
	UNION ALL
		SELECT t2.[Id]
      ,t2.[Name]
      ,t2.[Description]
      ,t2.[Performers]
      ,t2.[RegistrationDate]
      ,t2.[Status]
      ,t2.[ScheduledExecutionTime]
      ,t2.[ActualExecutionTime]
      ,t2.[CompletionDate]
      ,t2.[ParentId]
		FROM [TaskManagementDb].[dbo].[Tasks] t2
		INNER JOIN tree ON t2.Id = tree.ParentId
	) SELECT * FROM tree
);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS GetTree;");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS GetParents;");
            /*migrationBuilder.DropTable(
                name: "Tasks");*/
        }
    }
}
