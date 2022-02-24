namespace AngularEshop.Core.Security
{
    public interface IPasswordHelper
    {
        string EncodePasswordMd5(string password);
    }
}
