// Some copyright should be here...

using System;
using System.Diagnostics;
using System.IO;
using UnrealBuildTool;
using Console = Internal.Console;

public class Libbar : ModuleRules
{
	private void BuildCMakeProjectWindows()
	{
		BuildCMake("cmake.exe", ",,");
		BuildCMake("cmake.exe", "--build", ".");
	}

	private void BuildCMakeProjectLinux()
	{
		BuildCMake("cmake", "..");
		BuildCMake("cmake", "--build", ".");
	}

	private void BuildCMakeProjectMac()
	{
		// see: CMakeLists.txt; we're doing a cross compile for this.
		BuildCMake("cmake", "..");
		BuildCMake("cmake", "--build", ".");
	}

	private void BuildCMake(string command, params string[] args)
	{
		Process p = new Process();
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.RedirectStandardOutput = true;
		p.StartInfo.WorkingDirectory = Path.Combine(ModuleDirectory, "..", "..", "library", "build");
		Console.WriteLine($"{p.StartInfo.WorkingDirectory}");
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
			BuildCMakeProjectWindows();
			return;
		}

		if (Target.Platform == UnrealTargetPlatform.Mac)
		{
			BuildCMakeProjectMac();
			return;
		}

		if (Target.Platform == UnrealTargetPlatform.Linux)
		{
			BuildCMakeProjectLinux();
			return;
		}

		throw new Exception("Unsupported build target for project");
	}

	public Libbar(ReadOnlyTargetRules Target) : base(Target)
	{
		BuildSourceLibrary();

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "..", "..", "library", "build", "bar.lib"));
		}

		if (Target.Platform == UnrealTargetPlatform.Mac)
		{
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "..", "..", "library", "build", "libbar.a"));
		}

		// Add includes to path
		PublicIncludePaths.AddRange(new string[] { Path.Combine(ModuleDirectory, "..", "..", "library", "build", "include") });

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