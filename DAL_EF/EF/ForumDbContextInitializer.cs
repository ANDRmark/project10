using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.EF
{
    class ForumDbContextInitializer: DropCreateDatabaseIfModelChanges<ForumDbContext>
    {
        protected override void Seed(ForumDbContext context)
        {
            context.Themes.AddRange(new Theme[] {
                new Theme(){ Id=1, Title="Theme 1", CreateDate=DateTime.Now, SectionId=1},
                new Theme(){ Id=2, Title="Theme 2", CreateDate=DateTime.Now, SectionId=1},
                new Theme(){ Id=3, Title="Theme 3", CreateDate=DateTime.Now, SectionId=2},
                new Theme(){ Id=4, Title="Theme 4", CreateDate=DateTime.Now, SectionId=2}
            });
            context.Messages.AddRange(new Message[] {
                new Message(){ Id = 1, ThemeId = 1, CreateDate=DateTime.Now, MessageBody="Message 1", UserId=0},
                new Message(){ Id = 2, ThemeId = 1, CreateDate=DateTime.Now, MessageBody="Message 2", UserId=0},
                new Message(){ Id = 3, ThemeId = 1, CreateDate=DateTime.Now, MessageBody="Message 3", UserId=1},
                new Message(){ Id = 4, ThemeId = 1, CreateDate=DateTime.Now, MessageBody="Message 4", UserId=1},

                new Message(){ Id = 5, ThemeId = 2, CreateDate=DateTime.Now, MessageBody="Message 5", UserId=2},
                new Message(){ Id = 6, ThemeId = 2, CreateDate=DateTime.Now, MessageBody="Message 6", UserId=2},
                new Message(){ Id = 7, ThemeId = 2, CreateDate=DateTime.Now, MessageBody="Message 7", UserId=3},
                new Message(){ Id = 8, ThemeId = 2, CreateDate=DateTime.Now, MessageBody="Message 8", UserId=3},

                new Message(){ Id = 9, ThemeId = 3, CreateDate=DateTime.Now, MessageBody="Message 9", UserId=4},
                new Message(){ Id = 10, ThemeId = 3, CreateDate=DateTime.Now, MessageBody="Message 10", UserId=4},
                new Message(){ Id = 11, ThemeId = 3, CreateDate=DateTime.Now, MessageBody="Message 11", UserId=5},
                new Message(){ Id = 12, ThemeId = 3, CreateDate=DateTime.Now, MessageBody="Message 12", UserId=5},

                new Message(){ Id = 13, ThemeId = 4, CreateDate=DateTime.Now, MessageBody="Message 13", UserId=6},
                new Message(){ Id = 14, ThemeId = 4, CreateDate=DateTime.Now, MessageBody="Message 14", UserId=6},
                new Message(){ Id = 15, ThemeId = 4, CreateDate=DateTime.Now, MessageBody="Message 15", UserId=7},
                new Message(){ Id = 16, ThemeId = 4, CreateDate=DateTime.Now, MessageBody="Message 16", UserId=7},
            });
            context.Users.AddRange(new UserInfo[] {
                new UserInfo(){Id=0, UserName="Vasya 1", Email="ololo@olo.ol" },
                new UserInfo(){Id=1, UserName="Vasya 2", Email="ololo@olo.ol" },
                new UserInfo(){Id=2, UserName="Vasya 3", Email="ololo@olo.ol" },
                new UserInfo(){Id=3, UserName="Vasya 4", Email="ololo@olo.ol" },
                new UserInfo(){Id=4, UserName="Vasya 5", Email="ololo@olo.ol" },
                new UserInfo(){Id=5, UserName="Vasya 6", Email="ololo@olo.ol" },
                new UserInfo(){Id=6, UserName="Vasya 7", Email="ololo@olo.ol" },
                new UserInfo(){Id=7, UserName="Vasya 8", Email="ololo@olo.ol" }
            });
            context.Roles.AddRange(new Role[] {
                new Role() { Name="User"},
                new Role() { Name="Moderator"},
                new Role() {Name="Admin"}
            });
            context.Sections.AddRange(new Section[] {
                new Section(){ Id = 1, Title = "Section 1", CreateDate=DateTime.Now},
                new Section(){ Id = 2, Title = "Section 2", CreateDate=DateTime.Now}
            });
            context.Clients.Add(new Client()
            {
                Id = 1,
                Active = true,
                AllowedOrigin = "*",
                ApplicationType = ApplicationTypes.JavaScript,
                Name = "Angular Frontend for Forum",
                RefreshTokenLifeTime = 7200,
                Secret = null
            }
                );
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
