using System.Text;

namespace Jasmine.Format.Demo.Modules
{
    public abstract class ExampleModuleBase : IExampleModule
    {
        public abstract string Name { get; }
        public abstract string Id { get; }

        public abstract void Render(StringBuilder htmlBuilder);

        protected void AddCard(StringBuilder htmlBuilder, string title, string code, string output)
        {
            htmlBuilder.Append($@"
    <div class='card'>
        <h4>{title}</h4>
        <div class='code-block'>{code}</div>
        <div class='output-box'>{output}</div>
    </div>");
        }

        protected void AddSectionStart(StringBuilder htmlBuilder, string title)
        {
            htmlBuilder.Append($@"
<div class='demo-section'>
    <h3>{title}</h3>");
        }

        protected void AddSectionEnd(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append(@"
</div>");
        }
    }
}
