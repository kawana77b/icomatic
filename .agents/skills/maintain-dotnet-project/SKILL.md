---
name: maintain-dotnet-project
description: Maintain icomatic by upgrading to the latest .NET LTS and updating its SDK, runtime, NuGet dependencies, and release CI. Use for periodic platform maintenance, not standalone features or routine bug fixes.
---

# Maintain icomatic's .NET environment

Upgrade to the latest stable LTS and compatible dependencies, then verify that the application can still be distributed. Research versions at execution time instead of reusing a previous maintenance task's version numbers as "latest." Write project skills and their supporting metadata in English.

## Scope and preparation

- Read applicable `AGENTS.md` instructions and inspect the working tree, branch, and remotes. Preserve the user's uncommitted changes.
- For a new maintenance task, fetch main and create and check out a `codex/` branch from the latest fetched `origin/main` commit. Record the starting SHA and verify that it matches. Continue on the existing task branch for follow-up changes to the same task.
- Read these files and search for additional configuration, projects, and workflows:
  - `global.json`: SDK baseline, rollForward, and prerelease policy.
  - `Icomatic/Icomatic.csproj`: TargetFramework, RuntimeFrameworkVersion, PackageReference, and publishing settings.
  - `.github/workflows/release.yml`: SDK, every uses entry, and commands from restore through release.
  - `README.md`, `licenses.txt`, and `Makefile`: development instructions and bundled licenses.
- This image-generation CLI ships self-contained, trimmed, single-file builds for Windows x64 and Linux x64. Verify these distribution requirements after upgrading.

## Select update versions

- Consult the [official support policy](https://dotnet.microsoft.com/platform/support/policy) and [.NET downloads](https://dotnet.microsoft.com/download/dotnet) for the latest stable LTS and its SDK and runtime patches. Distinguish LTS from newer STS and preview releases.
- Evaluate SDK and runtime versions separately. Honor an explicitly requested SDK version. `rollForward: latestFeature` allows a newer installed SDK within the same major/minor version; it does not strictly pin execution to the specified version.
- Use the official NuGet API, such as `https://api.nuget.org/v3-flatcontainer/{lowercase-package-id}/index.json`, to identify the latest stable direct dependencies. Only adopt previews for an explicit reason. Keep the Magick.NET implementation and Core package versions aligned.
- Read official release notes and breaking changes for major upgrades and migrations from beta versions. Focus on API names, CLI parsing and exit codes, native dependencies, and trimming.
- Distinguish outdated transitive dependencies from direct dependencies. Do not override an upstream-selected transitive dependency with a direct reference solely to obtain its latest version. Investigate a resolution when vulnerabilities or compatibility problems warrant it.
- Check official GitHub Releases or API responses for the latest stable version of every Action in the workflows, including the release Action as well as checkout and setup-dotnet. For major upgrades, review runner and Node requirements, inputs, permissions, and draft behavior. Base selections on concrete versions returned by the registry or API.

## Update configuration and compatibility

- Align TargetFramework, any required RuntimeFrameworkVersion, SDK selection, the CI SDK, and direct dependencies. Update development instructions and the license inventory's versions and metadata.
- If publishing tools cannot run with the user's requested SDK, do not silently raise that version. Report the cause and the SDK actually used for validation. If a separate validation SDK is needed, isolate it in a Git-ignored directory such as `artifacts/` and verify the official distribution's checksum.
- Determine whether application code changes are required and limit fixes to the necessary scope. Check Magick.NET image and alpha processing, System.CommandLine's public CLI behavior, and Sharprompt's interactive input.
- Investigate trimming warnings. For reflection over enum Display attributes, preserve the required metadata using `typeof(T)` and appropriate `DynamicallyAccessedMembers` annotations or another suitable solution. Do not resolve warnings by suppressing them indiscriminately.

## Review release.yml

Treat a successful application build and a correct release workflow as separate verification requirements.

- Specify the PowerShell execution shell and explicit project paths for restore, build, and publish. When using `--no-restore`, verify in a clean copy that the preceding restore provides assets for both RIDs.
- Quote paths that may contain spaces. Pass tags through environment variables and remove only the leading v. For example, retain the v inside `v1.2.3-preview.1`. Reject malformed tags.
- Check publishing for both RIDs, copying both license files, ZIP directory structure and names, and matching attachment paths in the release Action.
- Allow sufficient timeout for SDK installation, trimming both RIDs, and compression. Fail on missing attachments and verify draft settings and contents permissions.
- Use `actionlint` to check syntax and expressions when available. If downloading it, use official distributions and verify checksums.

## Validate the result

Adapt these checks to the current project configuration:

```powershell
dotnet restore Icomatic/Icomatic.csproj
dotnet build Icomatic/Icomatic.csproj --no-restore -c Release
dotnet publish Icomatic/Icomatic.csproj --no-restore -r win-x64 -c Release -o artifacts/publish/win-x64
dotnet publish Icomatic/Icomatic.csproj --no-restore -r linux-x64 -c Release -o artifacts/publish/linux-x64
dotnet list Icomatic/Icomatic.csproj package --outdated --include-transitive
dotnet list Icomatic/Icomatic.csproj package --vulnerable --include-transitive
```

- When changing CI, execute the actual commands from its YAML in a clean working copy, from restore through ZIP creation. Running only similar commands from the examples above does not constitute verification of the workflow.
- Check that each ZIP contains the target executable, `LICENSE.txt`, and `licenses.txt`. Verify that `--version` from the extracted Windows EXE matches the version derived from the tag.
- Check the distributed EXE's `--help`, `list`, `info pwa`, and exit codes for invalid templates and missing arguments. Confirm that template names and descriptions survive trimming.
- When changing image processing, check that rounded and circular masks have transparent corners and opaque centers, and verify dimensions and PNG/ICO/JPEG/BMP output. Account for format aliases returned by decoders such as JPEG.
- Report local validation, execution on Linux, execution in GitHub Actions, and GitHub Release creation separately. Do not infer success for unexecuted checks from a successful local run.
- This skill does not authorize pushing, creating tags, or creating GitHub Releases. External publication is unnecessary when only validating the maintenance changes.

## Commit and report

- Use English Conventional Commits. Normally group runtime, dependency, and related configuration updates into one commit and put required compatibility code fixes in a separate commit. Additional CI fixes may use a separate `ci:` commit when appropriate.
- Examples: `chore(deps): upgrade to the latest .NET LTS and stable libraries`, `fix: adapt image processing to updated dependencies`, and `ci: update release actions and packaging commands`.
- When asked to incorporate a change into a specific existing commit, fold it into that commit while preserving subsequent commits' contents. Otherwise, do not automatically rewrite earlier commits.
- Briefly report the starting SHA, task branch, updates, reasons for code changes, validation results and unverified items, and commit IDs. Report new IDs after rewriting history.
