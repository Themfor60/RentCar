using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Data.Data.Repository.IRepository
{
    public interface IEmailService
    {
        Task SendRentalConfirmationEmailAsync(string toEmail, string userName, string vehicleModel, int rentalDays, decimal totalPrice, string userAddress);
    }

}
