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
            =>  Ok($"Teste conexão com a Api: {DateTime.Now.ToString("f")}");


        [Authorize(Roles = "commonUser, admin")]
        [HttpGet("GetAllJobs")]
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

      
        [HttpPost("CreateJob")]
        [Authorize(Roles = "commonUser, admin")]
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
        [HttpPut("UpdateJob/{id:int}")]
        public async Task<ActionResult> UpdateJob(int id, [FromBody] Job job)
        {

            try
            {
                
                if (job is null || id != job.Id)
                    return BadRequest();

                await _jobService.UpdateJobAsync(job);

                return Ok(job);
            }
            catch { return NotFound("Tarefa não encontrada."); }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteJob/{id:int}")]
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
