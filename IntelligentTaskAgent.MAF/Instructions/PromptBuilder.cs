using IntelligentTaskAgent.MAF.Instructions.Common;

namespace IntelligentTaskAgent.MAF.Instructions;

public sealed class PromptBuilder
{
    private readonly List<string> _sections = new();

    private PromptBuilder()
    {
    }

    public static PromptBuilder Create(string role)
    {
        var builder = new PromptBuilder();

        builder.With(role);

        return builder;
    }

    public PromptBuilder With(string? section)
    {
        if (!string.IsNullOrWhiteSpace(section))
        {
            _sections.Add(section.Trim());
        }

        return this;
    }

    public PromptBuilder WithCommon()
    {
        return this
            .With(DateTimeInstructions.Build())
            .With(GeneralInstructions.Prompt)
            .With(GreetingInstructions.Prompt)
            .With(ToolInstructions.Prompt)
            .With(ResponseInstructions.Prompt)
            .With(SecurityInstructions.Prompt);
    }

    public string Build()
    {
        return string.Join(
            Environment.NewLine + Environment.NewLine,
            _sections);
    }
}