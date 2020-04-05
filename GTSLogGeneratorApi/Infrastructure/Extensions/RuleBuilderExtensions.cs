using System.IO;
using FluentValidation;

namespace GTSLogGeneratorApi.Infrastructure.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> DirectoryExists<T>(this IRuleBuilder<T, string> ruleBuilder) {
            return ruleBuilder.Must(Directory.Exists)
                .WithMessage("There is not existing directory at {PropertyValue} path.");
        }
        
        public static IRuleBuilderOptions<T, string> FileExists<T>(this IRuleBuilder<T, string> ruleBuilder) {
            return ruleBuilder.Must(File.Exists)
                .WithMessage("There is not existing file at {PropertyValue} path.");
        }
    }
}