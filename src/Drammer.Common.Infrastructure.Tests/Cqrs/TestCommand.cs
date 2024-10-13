using Drammer.Common.Cqrs;

namespace Drammer.Common.Infrastructure.Tests.Cqrs;

public sealed class TestCommand : ICommand;

public sealed class TestCommandWithResult : ICommand<string>;