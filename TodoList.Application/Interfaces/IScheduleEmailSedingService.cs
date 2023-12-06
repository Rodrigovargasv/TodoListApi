using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IScheduleEmailSedingService
    {
        Task<string> ScheduleShipping(int jobId, int timeSendEmail, DataEmail emailModel);
    }
}
