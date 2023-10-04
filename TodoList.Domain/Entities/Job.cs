using TodoList.Domain.Enums;
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class Job
    {
        public Job(int id, string name, string description, DateTime executionDate, JobStatus? jobStatus)
        {

            Id = id;
            Description = description;
            ExecutionDate = executionDate;
           
            JobStatus = jobStatus;


            ValidationAddDomainJob(name, executionDate);


        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string? Description { get; private set; }


        public DateTime ExecutionDate { get; private set; }

        public JobStatus? JobStatus { get; private set; }




        public void ValidationAddDomainJob(string name, DateTime executionDate)
        {

            DomainExceptionValidation.When(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name),
                "O campo nome tem preenchimento obrigatório");

            //DomainExceptionValidation.When(executionDate < DateTime.Now.AddDays(-1), 
            //    "A data para execução da tarefa não pode ser menor que a data de criação");

            Name = name;
            ExecutionDate = executionDate;
            
            
        }

    }
}
