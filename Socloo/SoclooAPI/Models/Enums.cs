namespace SoclooAPI.Models
{
    public enum AnswerEnum
    {
        Mc = 1,
        SA = 2,
        TF = 3
    }
    public enum ChatEnum
    {
        CourseChat = 1,
        GroupChat = 2
    }
    public enum OccurrenceEnum
    {
        Test = 1,
        Assigment = 2,
        SchoolTrip = 3,
        Other = 4
    }

    public enum PostEnum
    {
        Announcement = 1, // circolari di istituto
        Alert = 2, //avvisi non ufficiali
    }
    public enum SchoolAdminEnum
    {
        Owner = 1,
        Standard = 2
    }
    public enum TestEnum
    {
        Assessed = 1,
        NotAssessed = 2
    }
}