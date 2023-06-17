using Microsoft.AspNetCore.Http.HttpResults;

namespace AutoSchedule.Models.Enum
{
    public enum StatusCode
    {
        UserAlreadyExists = 1,
        TeacherNoFound = 2,
        OK = 200,
        InternalServerError = 500
    }
}
