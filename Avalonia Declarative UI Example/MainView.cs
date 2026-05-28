namespace Avalonia_Declarative_UI_Example
{
    public class MainView() : ViewBase
    {
        protected override object Build()
            => ViewFactory.Create<CounterComponent>()
                .Name("MainView", Scope);
    }
}