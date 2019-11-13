[![Build status](https://ci.appveyor.com/api/projects/status/4k5lqfrunfd51g6y/branch/master?svg=true)](https://ci.appveyor.com/project/BrunoJuchli/stylecop-csharp-async-rules/branch/master)
# StyleCop.CSharp.Async.Rules
Additional StyleCop rules for `async / await` style programming.

The following examples will result in StyleCop warnings:
- **`async void DoSomethingAsync()` --> method should return awaitable instead of void**
 - see [Stephen Cleary](https://msdn.microsoft.com/en-us/magazine/jj991977.aspx), [Phil Haack](http://haacked.com/archive/2014/11/11/async-void-methods/),...
- **`async Task DoSomething()` --> method should be named  `DoSomethingAsync`**
 - see [Task-based Asynchronous Pattern (TAP)](https://msdn.microsoft.com/en-us/library/hh873175%28v=vs.110%29.aspx)
- **`void DoSomethingAsync()` --> method should have `async` modifier or return a Task/Task<T>**
 - see [Task-based Asynchronous Pattern (TAP)](https://msdn.microsoft.com/en-us/library/hh873175%28v=vs.110%29.aspx)

## 1. Installation
#### By Nuget
If you have StyleCop integrated into your build already, just install the nuget package [StyleCop.CSharp.Async.Rules](https://www.nuget.org/packages/StyleCop.CSharp.Async.Rules/) in all projects you want to be verified.

If you don't have installed StyleCop yet, I'd recommend installing the nuget package [StyleCop.MsBuild](https://www.nuget.org/packages/StyleCop.MSBuild/).
#### Manually
The rules-dll (StyleCop.CSharp.Async.Rules.dll) is available on the [build server](https://ci.appveyor.com/project/BrunoJuchli/stylecop-csharp-async-rules)(see **Artifacts**).
You can place it alongside the StyleCop.dll and StyleCop will automatically pick it up.
Alternatively, you can also tell StyleCop where to pick it up by defining an `StyleCopAdditionalAddinPaths` item in the *.csproj file:

    <ItemGroup>
      <StyleCopAdditionalAddinPaths Include="..\StyleCop.CSharp.Async.Rules\">
        <Visible>false</Visible>
      </StyleCopAdditionalAddinPaths>
    </ItemGroup>

## 2. Rules
Description, supressing a violation and disabling a rule entirely
#### Methods with `async` modifier must end with `Async`
ID: AR0001:MethodsWithAsyncModifierMustEndWithAsync

Violated when an `async` method is named `Foo()` instead of `FooAsync()`.
###### Suppress specific warning / occurrence in code

    [SuppressMessage(
      "StyleCop.CSharp.AsyncRules",
      "AR0001:MethodsWithAsyncModifierMustEndWithAsync",
      Justification = "Yipii-ai-ei-oh")]
    async Task DoSomething() { }

###### Disable in `Settings.StyleCop` file

    <Analyzer AnalyzerId="StyleCop.CSharp.AsyncRules">
      <Rules>
        <Rule Name="MethodsWithAsyncModifierMustEndWithAsync">
            <RuleSettings>
              <BooleanProperty Name="Enabled">False</BooleanProperty>
            </RuleSettings>
        </Rule>
      </Rules>
    </Analyzer>
    
#### Methods ending with `Async` must have `async` modifier or return a `Task`
ID: AR0002:MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTask

> Hint: This was recently adapted and renamed from MethodEndingWithAsyncMustHaveAsyncModifier to MethodEndingWithAsyncMustHaveAsyncModifier*OrReturnTask*

Violated when a method named `FooAsync` does not have the `async` modifier and does not return a `Task` / `Task<T>`.

###### Suppress specific warning / occurrence in code

    [SuppressMessage(
      "StyleCop.CSharp.AsyncRules",
      "AR0002:MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTask",
      Justification = "I'm cheating")]
    int FooAsync() { }

###### Disable in `Settings.StyleCop` file

    <Analyzer AnalyzerId="StyleCop.CSharp.AsyncRules">
      <Rules>
        <Rule Name="MethodEndingWithAsyncMustHaveAsyncModifierOrReturnTask">
            <RuleSettings>
              <BooleanProperty Name="Enabled">False</BooleanProperty>
            </RuleSettings>
        </Rule>
      </Rules>
    </Analyzer>
    
#### Methods with `async` modifier must return awaitable
ID: AR1001:MethodsWithAsyncModifierShouldReturnAwaitable

Violated when a method  `async void FooAsync()` returns void instead of `Task` or another awaitable type.

###### Suppress specific warning / occurrence in code

    [SuppressMessage(
      "StyleCop.CSharp.AsyncRules",
      "AR1001:MethodsWithAsyncModifierShouldReturnAwaitable",
      Justification = "no need to wait for task to end")]
    async void HandleEventAsync() { }

###### Disable in `Settings.StyleCop` file

    <Analyzer AnalyzerId="StyleCop.CSharp.AsyncRules">
      <Rules>
        <Rule Name="MethodsWithAsyncModifierShouldReturnAwaitable">
            <RuleSettings>
              <BooleanProperty Name="Enabled">False</BooleanProperty>
            </RuleSettings>
        </Rule>
      </Rules>
    </Analyzer>
