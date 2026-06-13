using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public interface IExampleModule
    {
        string Name { get; }
        string Id { get; }
        void Render(StringBuilder htmlBuilder);
    }
}
