
namespace RescueMe.Domain.Commands
{
    internal interface ICommand
    {
        string Name { get; }
        string Execute(IncomingSmsMessage message);
    }
}
