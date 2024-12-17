Imports Microsoft.EntityFrameworkCore

Namespace DevExpress.AI.Samples.Blazor.Data

    Public Class IssuesContext
        Inherits DbContext

        Public Sub New(ByVal options As DbContextOptions(Of IssuesContext))
            MyBase.New(options)
        End Sub

        Public Property Items As DbSet(Of Issue)

        Public Property Projects As DbSet(Of Project)

        Public Property Users As DbSet(Of User)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As ModelBuilder)
            modelBuilder.Entity(Of Issue)(Function(entity)
                entity.HasKey(Function(t) New With {t.ID})
                ' Properties
                entity.Property(Function(t) t.ID)
                entity.Property(Function(t) t.Name).HasMaxLength(50)
                entity.Property(Function(t) t.Description).HasMaxLength(2147483647)
                entity.Property(Function(t) t.Resolution).HasMaxLength(2147483647)
            End Function)
            modelBuilder.Entity(Of Project)(Function(entity)
                entity.HasKey(Function(t) t.ID)
                ' Properties
                entity.Property(Function(t) t.ID)
                entity.Property(Function(t) t.Name).HasMaxLength(100)
            End Function)
            modelBuilder.Entity(Of User)(Function(entity)
                entity.HasKey(Function(t) t.ID)
                ' Properties
                entity.Property(Function(t) t.ID)
                entity.Property(Function(t) t.FirstName).HasMaxLength(25)
                entity.Property(Function(t) t.LastName).HasMaxLength(25)
                entity.Property(Function(t) t.Country).HasMaxLength(15)
                entity.Property(Function(t) t.City).HasMaxLength(15)
                entity.Property(Function(t) t.Address).HasMaxLength(60)
                entity.Property(Function(t) t.Phone).HasMaxLength(24)
                entity.Property(Function(t) t.Email).HasMaxLength(50)
            End Function)
        End Sub
    End Class
End Namespace
