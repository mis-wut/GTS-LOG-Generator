using FluentValidation;
using GTSLogGeneratorApi.Infrastructure.Extensions;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public class UpdateLogsGenerationJobRequestValidator : AbstractValidator<UpdateLogsGenerationJobRequest>
    {
        public UpdateLogsGenerationJobRequestValidator()
        {
            RuleFor(x => x.Path)
                .DirectoryExists();
        }
    }
}