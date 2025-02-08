// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Must compile under net framework and netstandard2.0", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0090:'new' expression can be simplified", Justification = "Must compile under net framework and netstandard2.0", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0300:Collection initialization can be simplified", Justification = "Must compile under net framework and netstandard2.0", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0305:Collection initialization can be simplified", Justification = "Must compile under net framework and netstandard2.0", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0305:Collection initialization can be simplified", Justification = "Must compile under net framework and netstandard2.0", Scope = "module")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "This warning is just rediculous", Scope = "type", Target = "~T:Benchmarks.MetaFileDataBenchmarks")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "This warning is just rediculous", Scope = "type", Target = "~T:Benchmarks.TokenizerBenchmarks")]
