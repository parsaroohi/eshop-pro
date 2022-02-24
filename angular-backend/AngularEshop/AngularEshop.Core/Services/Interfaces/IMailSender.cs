namespace AngularEshop.Core.Services.Interfaces
{
    public interface IMailSender
    {
        void Send(string to, string subject, string body);
    }
}