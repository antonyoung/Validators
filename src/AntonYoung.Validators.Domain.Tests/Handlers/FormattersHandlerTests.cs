using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AntonYoung.Validators.Domain.Tests.Handlers
{
    public class FormattersHandlerTests
    {
        private readonly IFormattersHandler _handler;

        private readonly IEnumerable<FormatterResponse> _formatters = new List<FormatterResponse>
        {
            new()
            {
                Formatter = Formatters.None,
                Description = "None: keep as original with hyphen(s) and or whitespace(s)"
            },
            new()
            {
                Formatter = Formatters.Hyphens,
                Description = "Replace: hyphens \"-\" with given replace ( default = string.Empty )"
            },
            new()
            {
                Formatter = Formatters.HyphensAndWhiteSpaces,
                Description = "Replace: hyphens \"-\" and whitespaces \" \" with given replace ( default = string.Empty )"
            },
            new()
            {
                Formatter = Formatters.WhiteSpaces,
                Description = "Replace: whitespaces \" \" with given replace ( default = string.Empty )"
            }
        };

        public FormattersHandlerTests()
            => _handler = new FormattersHandler();

        [Fact]
        public async Task All()
        {
            var result = await _handler
                .HandleAsync(CancellationToken.None);

            result
                .Should()
                .HaveCount(4);

            result
                .Should()
                .BeEquivalentTo(_formatters);
        }
    }
}