﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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



        [HttpGet("/testeApi")]
        public async Task<ActionResult> TesteApi()
        {
            return Ok($"Teste conexão com a Api: {DateTime.Now.ToString("f")}");

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
        {

            var jobs = await _jobService.GetAllJobAsync();

            if (jobs is null)
                return NotFound();

            return Ok(jobs);

        }

        [HttpGet("{id}", Name = "GetJob")]
        public async Task<ActionResult> GetJob(int id)
        {
            var jobId = await _jobService.GetJobByIdAsync(id);

            if (jobId is null)
                return NotFound();

            return Ok(jobId);
        }

        [HttpPost]
        public async Task<ActionResult> CreateJob([FromBody] Job job)
        {

            if (job is null || string.IsNullOrWhiteSpace(job.ToString()))
                return BadRequest();

            await _jobService.CreateJobAync(job);

            return new CreatedAtRouteResult("GetJob", new { id = job.Id }, job);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            if (id != job.Id)
                return BadRequest();

            if (job is null)
                BadRequest();

            await _jobService.UpdateJobAsync(job);

            return Ok(job);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            var getJob = await _jobService.GetJobByIdAsync(id);

            if (getJob is null)
                return NotFound();


            await _jobService.DeleteJobAsync(id);

            return Ok(getJob);
        }
    }
}