using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;

        }

        [AllowAnonymous]
        [HttpGet("testeApi")]
        public ActionResult TesteApi()
            => Ok($"Teste conexão com a Api: {DateTime.Now.ToString("f")}");


        [Authorize(Roles = "commonUser, admin")]
        [HttpGet("GetAllJobs")]
        public async Task<IEnumerable<Job>> GetAllJobs()
            => await _jobService.GetAllJobAsync();


        [Authorize(Roles = "commonUser, admin")]
        [HttpGet("GetJob/{id}")]
        public async Task<Job> GetJob(int id)
           => await _jobService.GetJobByIdAsync(id);

            
        [HttpPost("CreateJob")]
        [Authorize(Roles = "commonUser, admin")]
        public async Task<int> CreateJob([FromBody] Job job)
               => await _jobService.CreateJobAync(job);


        [Authorize(Roles = "admin")]
        [HttpPut("UpdateJob/{id:int}")]
        public async Task<string> UpdateJob(int id, [FromBody] Job job)
               => await _jobService.UpdateJobAsync(job, id);

            
        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteJob/{id:int}")]
        public async Task<string> DeleteJob(int id)
                => await _jobService.DeleteJobAsync(id);   
        
    }
}
