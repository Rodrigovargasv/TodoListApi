using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;

          
        }



        [AllowAnonymous]
        [HttpGet("/testeApi")]
        public async Task<ActionResult> TesteApi()
        {
            return Ok($"Teste conexão com a Api: {DateTime.Now.ToString("f")}");

        }

        [Authorize(Roles = "commonUser, admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
        {

            try
            {
                var jobs = await _jobService.GetAllJobAsync();

                if (jobs is null)
                    return NotFound();

                return Ok(jobs);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [Authorize(Roles = "commonUser, admin")]
        [HttpGet("{id}", Name = "GetJob")]
        public async Task<ActionResult> GetJob(int id)
        {
            try
            {
                var jobId = await _jobService.GetJobByIdAsync(id);

                if (jobId is null)
                    return NotFound();

                return Ok(jobId);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(Roles = "commonUser, admin")]
        [HttpPost]
        public async Task<ActionResult> CreateJob([FromBody] Job job)
        {

            try
            {
                if (job is null || string.IsNullOrWhiteSpace(job.ToString()))
                    return BadRequest();

                await _jobService.CreateJobAync(job);

                return new CreatedAtRouteResult("GetJob", new { id = job.Id }, job);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateJob(int id, [FromBody] Job job)
        {

            try
            {
                if (id != job.Id)
                    return BadRequest();

                if (job is null)
                    return BadRequest();


                await _jobService.UpdateJobAsync(job);


                return Ok(job);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            try
            {
                var getJob = await _jobService.GetJobByIdAsync(id);

                if (getJob is null)
                    return NotFound();


                await _jobService.DeleteJobAsync(id);

                return Ok(getJob);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
