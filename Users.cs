using System.ComponentModel.DataAnnotations;

namespace WEB_API
{
    //This class holds one user, so User is a clearer name than Users.
    //In C#, public properties are written in PascalCase (UserName, FirstName, LastName, Password, Id).
    //If you rename them, User.txt still has the old names, so read it with
    //new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
    public class Users
    {
        [Required]
        public string userName { get; set; }
        [Required]

        public string firstName { get; set; }
        [Required]

        public string lastName { get; set; }
        [Required]

        public string password { get; set; }
        public int id { get; set; }

    }
    //In C#, each class goes in its own file, named after the class: UserLog.cs.
    //A full name is clearer, for example LoginUser
    public class UserLog
    {
        [Required]

        public string userName { get; set; }
        [Required]

        public string password { get; set; }


    }
}
