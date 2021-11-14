using FluentValidation;
using GTSLogGeneratorApi.Infrastructure.Extensions;

namespace GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest
{
    public class RunLogsGenerationJobRequestValidator : AbstractValidator<RunLogsGenerationJobRequest>
    {
        public RunLogsGenerationJobRequestValidator()
        {
            RuleFor(x => x.Path)
                .DirectoryExists();
        }
    }
}
