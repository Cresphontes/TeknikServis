using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Enums;

namespace Web.BLL.Services
{
    public interface IMessageService
    {
        MessageStates MessageStates { get; }
        Task SendAsync(IdentityMessage message, params string[] contacts);
        void Send(IdentityMessage message, params string[] contacts);
    }
}
