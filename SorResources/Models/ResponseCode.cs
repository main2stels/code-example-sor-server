namespace SorResources.Models
{
    public enum ResponseCode
    {
        Success = 200,

        Failed = 400,

        NonAuthorizedError = 401,

        RequestNotFound = 404,

        UnknownError = 500,

        NoMoney = 601,

        ObjectNotFound = 602,

        ChangingSomeoneObject = 603,

        OldVersion = 701,
    }
}
