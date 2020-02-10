using AspnetCoreStudy.Models;
using Microsoft.EntityFrameworkCore;


namespace AspnetCoreStudy.DataContext
{
    public class AspnetNoteDBContext : DbContext  //EntityFramework 에서 DbCOntext로 상속
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //DB에 있는 추상 클래스 사용하기 위해 override 키워드
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=AspnetNoteDb;User Id=sa;Password=gkeoehd103;");
        }
    }
}