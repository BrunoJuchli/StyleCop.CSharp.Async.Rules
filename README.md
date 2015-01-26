[![Build status](https://ci.appveyor.com/api/projects/status/4k5lqfrunfd51g6y/branch/master?svg=true)](https://ci.appveyor.com/project/BrunoJuchli/stylecop-csharp-async-rules/branch/master)
# StyleCop.CSharp.Async.Rules
Additional StyleCop rules for `async / await` style programming.

## Rules
The following examples will result in StyleCop warnings:
- `async void DoSomethingAsync()` --> method should return awaitable instead of void
- `async Task DoSomething()` --> method should be named  `DoSomethingAsync`
- `Task DoSomethingAsync()` --> method should have `async` modifier

## Installing
#### By Nuget
If you have StyleCop integrated into your build already, just install the nuget package [StyleCop.CSharp.Async.Rules](https://www.nuget.org/packages/StyleCop.CSharp.Async.Rules/) in all projects you the rules active in. If you don't have installed StyleCop yet, i'd recommend installing the [StyleCop.MsBuild](https://www.nuget.org/packages/StyleCop.MSBuild/), too.
#### Manually
The rules-dll (StyleCop.CSharp.Async.Rules.dll) is available on the [build server](https://ci.appveyor.com/project/BrunoJuchli/stylecop-csharp-async-rules)(see **Artifacts**).
You can place it alongside the StyleCop.dll and StyleCop will automatically pick it up.
Alternatively, you can also tell StyleCop where to pick it up by defining an `StyleCopAdditionalAddinPaths` item in the *.csproj file:

  <ItemGroup>
    <StyleCopAdditionalAddinPaths Include="..\StyleCop.CSharp.Async.Rules\">
		<Visible>false</Visible>
	</StyleCopAdditionalAddinPaths>
  </ItemGroup>

## Rule Description
## Info on how to Suppress or Disable Them

### Methods with `async` modifier must end with `Async`
(**AR0001:MethodsWithAsyncModifierMustEndWithAsync**)

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
