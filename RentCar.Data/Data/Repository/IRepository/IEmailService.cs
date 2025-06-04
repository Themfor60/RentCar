using RentCar.Models;

namespace SendEmail.Services
{


    public interface IEmailService
    {

        void SendEmail(RentaFormularioViewModel request);
    }
}