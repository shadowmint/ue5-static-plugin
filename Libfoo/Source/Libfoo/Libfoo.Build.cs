// Some copyright should be here...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnrealBuildTool;
using Console = Internal.Console;

public class Libfoo : ModuleRules
{
	private void BuildRustProjectWindows()
	{
		BuildRust("cargo.exe", "build", "--release");
	}

	private void BuildRustProjectLinux()
	{
		BuildRust("cargo", "build", "--release");
	}

	private void BuildRustProjectMac()
	{
		Console.WriteLine("warning: cross compile for x86_64-apple-darwin; if this fails run: rustup target add x86_64-apple-darwin");
		BuildRust("cargo", "build", "--release", "--target", "x86_64-apple-darwin");
	}
	
	private void BuildRust(string command, params string[] args)
	{
		Process p = new Process();
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.RedirectStandardOutput = true;
		p.StartInfo.WorkingDirectory = Path.Combine(ModuleDirectory, "source");

		p.StartInfo.FileName = command;
		p.StartInfo.Arguments = string.Join(" ", args);
		Console.WriteLine($"running: {command} {p.StartInfo.Arguments}");
		p.Start();
		
		p.StandardOutput.ReadToEnd();
		p.WaitForExit();
	}
	
	public void BuildSourceLibrary() 
	{
		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			BuildRustProjectWindows();
			return;
		}

		if (Target.Platform == UnrealTargetPlatform.Mac)
		{
			BuildRustProjectMac();
			return;
		}

		if (Target.Platform == UnrealTargetPlatform.Linux)
		{
			BuildRustProjectLinux();
			return;
		}

		throw new Exception("Unsupported build target for project");
	}
	
	public Libfoo(ReadOnlyTargetRules Target) : base(Target)
	{
		BuildSourceLibrary();
		
		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "source", "target", "release", "foo.lib"));
		}

		if (Target.Platform == UnrealTargetPlatform.Mac)
		{
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "source", "target", "x86_64-apple-darwin", "release", "libfoo.a"));
		}

		// Add includes to path
		PublicIncludePaths.AddRange(new string[] { Path.Combine(ModuleDirectory, "source", "target", "include") });

		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicIncludePaths.AddRange(
			new string[]
			{
				// ... add public include paths required here ...
			}
		);
		
		PrivateIncludePaths.AddRange(
			new string[]
			{
				// ... add other private include paths required here ...
			}
		);
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
				// ... add other public dependencies that you statically link with here ...
			}
		);
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
				// ... add private dependencies that you statically link with here ...	
			}
		);

		DynamicallyLoadedModuleNames.AddRange(
			new string[]
			{
				// ... add any modules that your module loads dynamically here ...
			}
		);
	}
}