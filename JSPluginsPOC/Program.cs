// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using Remora.Commands.Groups;
using Remora.Results;
using Tenray.Topaz;

var eng = new TopazEngine();
eng.SetValue("a", (Action<int> a) => a(20));

eng.ExecuteExpression("function owo() { a(\"heck\")}");
eng.InvokeFunction("owo", new CancellationToken(false));

Debugger.Break();