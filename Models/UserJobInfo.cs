namespace DotnetAPI
{
    public partial class UserJobInfo
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Departement { get; set; }

        public UserJobInfo()
        {
            if (JobTitle  == null)
            {
                JobTitle  = "";
            }
            if (Departement == null)
            {
                Departement = "";
            }
        }
    }
}