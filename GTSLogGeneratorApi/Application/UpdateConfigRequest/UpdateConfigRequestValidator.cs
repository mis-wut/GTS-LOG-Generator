using System.Data;
using FluentValidation;
using GTSLogGeneratorApi.Infrastructure.Extensions;

namespace GTSLogGeneratorApi.Application.UpdateConfigRequest
{
    public class UpdateConfigRequestValidator : AbstractValidator<UpdateConfigRequest>
    {
        public UpdateConfigRequestValidator()
        {
            RuleFor(x => x.ConfigFilePath)
                .FileExists();

            RuleFor(x => x.LogsInputFolder)
                .DirectoryExists();

            RuleFor(x => x.ErrorLogsFolder)
                .DirectoryExists()
                .Unless(x => string.IsNullOrEmpty(x.ErrorLogsFolder));
        }
    }
}