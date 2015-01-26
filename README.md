[![Build status](https://ci.appveyor.com/api/projects/status/4k5lqfrunfd51g6y/branch/master?svg=true)](https://ci.appveyor.com/project/BrunoJuchli/stylecop-csharp-async-rules/branch/master)
# StyleCop.CSharp.Async.Rules
Additional StyleCop rules for `async / await` style programming.

## Rules

### Methods with `async` modifier must end with `Async` (**AR0001:MethodsWithAsyncModifierMustEndWithAsync**)

Violated when an `async` method is named `Foo()` instead of `FooAsync()`.
##### Suppress specific warning / occurrence in code

    [SuppressMessage(
      "StyleCop.CSharp.AsyncRules",
      "AR0001:MethodsWithAsyncModifierMustEndWithAsync",
      Justification = "Yipii-ai-ei-oh")]
    async Task DoSomething() { }

##### Disable in `Settings.StyleCop` file

    <Analyzer AnalyzerId="StyleCop.CSharp.AsyncRules">
      <Rules>
        <Rule Name="MethodsWithAsyncModifierMustEndWithAsync">
            <RuleSettings>
              <BooleanProperty Name="Enabled">False</BooleanProperty>
            </RuleSettings>
        </Rule>
      </Rules>
    </Analyzer>
    
### Methods ending with `Async` must have `async` modifier
(**AR0002:MethodEndingWithAsyncMustHaveAsyncModifier**)

Violated when a method named `FooAsync` does not have the `async` modifier.

##### Suppress specific warning / occurrence in code

    [SuppressMessage(
      "StyleCop.CSharp.AsyncRules",
      "AR0002:MethodEndingWithAsyncMustHaveAsyncModifier",
      Justification = "doesn't need the async modifier")]
    Task FooAsync() { }

##### Disable in `Settings.StyleCop` file

    <Analyzer AnalyzerId="StyleCop.CSharp.AsyncRules">
      <Rules>
        <Rule Name="MethodEndingWithAsyncMustHaveAsyncModifier">
            <RuleSettings>
              <BooleanProperty Name="Enabled">False</BooleanProperty>
            </RuleSettings>
        </Rule>
      </Rules>
    </Analyzer>
    
### Methods with `async` modifier must return awaitable
(**AR1001:MethodsWithAsyncModifierShouldReturnAwaitable**)

Violated when a method  `async void FooAsync()` returns void instead of `Task` or another awaitable type.

##### Suppress specific warning / occurrence in code

    [SuppressMessage(
      "StyleCop.CSharp.AsyncRules",
      "AR1001:MethodsWithAsyncModifierShouldReturnAwaitable",
      Justification = "event handler :))]
    async void HandleEventAsync() { }

##### Disable in `Settings.StyleCop` file

    <Analyzer AnalyzerId="StyleCop.CSharp.AsyncRules">
      <Rules>
        <Rule Name="MethodsWithAsyncModifierShouldReturnAwaitable">
            <RuleSettings>
              <BooleanProperty Name="Enabled">False</BooleanProperty>
            </RuleSettings>
        </Rule>
      </Rules>
    </Analyzer>
