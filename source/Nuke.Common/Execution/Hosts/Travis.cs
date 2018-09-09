﻿// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities.Output;

namespace Nuke.Common.Execution.Hosts
{
    /// <summary>
    /// Interface according to the <a href="https://docs.travis-ci.com/user/environment-variables/">official website</a>.
    /// </summary>
    [PublicAPI]
    [BuildServer]
    [ExcludeFromCodeCoverage]
    public class Travis : Host
    {
        internal Travis()
        {
        }

        protected internal override bool IsRunning => Environment.GetEnvironmentVariable("TRAVIS") != null;
        protected internal override IOutputSink OutputSink => new ConsoleOutputSink();

        public bool Ci => EnvironmentInfo.Variable<bool>("CI");
        public bool ContinousIntegration => EnvironmentInfo.Variable<bool>("CONTINUOUS_INTEGRATION");

        /// <summary>
        /// Whether you have defined any encrypted variables, including variables defined in the Repository Settings.
        /// </summary>
        public bool SecureEnvVars => EnvironmentInfo.Variable<bool>("TRAVIS_SECURE_ENV_VARS");

        /// <summary>
        /// Set to <c>true</c> if the job is allowed to fail otherwhise <c>false</c>.
        /// </summary>
        public bool AllowFailure => EnvironmentInfo.Variable<bool>("TRAVIS_ALLOW_FAILURE");

        /// <summary>
        /// For push builds, or builds not triggered by a pull request, this is the name of the branch.
        /// For builds triggered by a pull request this is the name of the branch targeted by the pull request.
        /// For builds triggered by a tag, this is the same as the name of the tag (<c>TRAVIS_TAG</c>).
        /// </summary>
        public string Branch => EnvironmentInfo.Variable("TRAVIS_BRANCH");

        /// <summary>
        /// The absolute path to the directory where the repository being built has been copied on the worker.
        /// </summary>
        public string BuildDir => EnvironmentInfo.Variable("TRAVIS_BUILD_DIR");

        /// <summary>
        ///  The id of the current build that Travis CI uses internally.
        /// </summary>
        public long BuildId => EnvironmentInfo.Variable<long>("TRAVIS_BUILD_ID");

        /// <summary>
        /// The number of the current build (for example, “4”).
        /// </summary>
        public long BuildNumber => EnvironmentInfo.Variable<long>("TRAVIS_BUILD_NUMBER");

        /// <summary>
        /// The commit that the current build is testing.
        /// </summary>
        public string Commit => EnvironmentInfo.Variable("TRAVIS_COMMIT");

        /// <summary>
        /// The commit subject and body, unwrapped.
        /// </summary>
        public string CommitMessage => EnvironmentInfo.Variable("TRAVIS_COMMIT_MESSAGE");

        /// <summary>
        /// The range of commits that were included in the push or pull request. (Note that this is empty for builds triggered by the initial commit of a new branch.)
        /// </summary>
        public string CommitRange => EnvironmentInfo.Variable("TRAVIS_COMMIT_RANGE");

        /// <summary>
        /// Indicates how the build was triggered. 
        /// </summary>
        public TravisEventType EventType => EnvironmentInfo.Variable<TravisEventType>("TRAVIS_EVENT_TYPE");

        /// <summary>
        /// The id of the current job that Travis CI uses internally.
        /// </summary>
        public long JobId => EnvironmentInfo.Variable<long>("TRAVIS_JOB_ID");

        /// <summary>
        /// The number of the current job (for example, “4.1”).
        /// </summary>
        [NoConvert] public string JobNumber => EnvironmentInfo.Variable("TRAVIS_JOB_NUMBER");

        /// <summary>
        /// On multi-OS builds, this value indicates the platform the job is running on. Values are <c>linux</c> and <c>osx</c> currently, to be extended in the future.
        /// </summary>
        public string OsName => EnvironmentInfo.Variable("TRAVIS_OS_NAME");

        /// <summary>
        /// <c>TRAVIS_PULL_REQUEST</c> is set to the pull request number if the current job is a pull request build, or <c>false</c> if it’s not.
        /// </summary>
        [NoConvert] public string PullRequest => EnvironmentInfo.Variable("TRAVIS_PULL_REQUEST");

        /// <summary>
        /// If the current job is a pull request, the name of the branch from which the PR originated.
        /// If the current job is a push build, this variable is empty(<c>""</c>).
        /// </summary>
        public string PullRequestBranch => EnvironmentInfo.Variable("TRAVIS_PULL_REQUEST_BRANCH");

        /// <summary>
        /// If the current job is a pull request, the commit SHA of the HEAD commit of the PR.
        /// If the current job is a push build, this variable is empty(<c>""</c>).
        /// </summary>
        public string PullRequestSha => EnvironmentInfo.Variable("TRAVIS_PULL_REQUEST_SHA");

        /// <summary>
        /// If the current job is a pull request, the slug (in the form <c>owner_name/repo_name</c>) of the repository from which the PR originated.
        /// If the current job is a push build, this variable is empty(<c>""</c>).
        /// </summary>
        public string PullRequestSlug => EnvironmentInfo.Variable("TRAVIS_PULL_REQUEST_SLUG");

        /// <summary>
        /// The slug (in form: <c>owner_name/repo_name</c>) of the repository currently being built.
        /// </summary>
        public string RepoSlug => EnvironmentInfo.Variable("TRAVIS_REPO_SLUG");

        /// <summary>
        /// <c>true</c> or <c>false</c> based on whether sudo is enabled.
        /// </summary>
        public bool Sudo => EnvironmentInfo.Variable<bool>("TRAVIS_SUDO");

        /// <summary>
        /// Is set to <em>0</em> if the build is successful and <em>1</em> if the build is broken.
        /// </summary>
        [CanBeNull] public string TestResult => EnvironmentInfo.Variable("TRAVIS_TEST_RESULT");

        /// <summary>
        /// If the current build is for a git tag, this variable is set to the tag’s name.
        /// </summary>
        public string Tag => EnvironmentInfo.Variable("TRAVIS_TAG");

        [CanBeNull] public string DartVersion => EnvironmentInfo.Variable("TRAVIS_DARTVersion");
        [CanBeNull] public string GoVersion => EnvironmentInfo.Variable("TRAVIS_GOVersion");
        [CanBeNull] public string HaxeVersion => EnvironmentInfo.Variable("TRAVIS_HAXEVersion");
        [CanBeNull] public string JdkVersion => EnvironmentInfo.Variable("TRAVIS_JDKVersion");
        [CanBeNull] public string JuliaVersion => EnvironmentInfo.Variable("TRAVIS_JULIAVersion");
        [CanBeNull] public string NodeVersion => EnvironmentInfo.Variable("TRAVIS_NODEVersion");
        [CanBeNull] public string OtpRelease => EnvironmentInfo.Variable("TRAVIS_OTP_RELEASE");
        [CanBeNull] public string PerlVersion => EnvironmentInfo.Variable("TRAVIS_PERLVersion");
        [CanBeNull] public string PhpVersion => EnvironmentInfo.Variable("TRAVIS_PHPVersion");
        [CanBeNull] public string PythonVersion => EnvironmentInfo.Variable("TRAVIS_PYTHONVersion");
        [CanBeNull] public string RVersion => EnvironmentInfo.Variable("TRAVIS_RVersion");
        [CanBeNull] public string RubyVersion => EnvironmentInfo.Variable("TRAVIS_RUBYVersion");
        [CanBeNull] public string RustVersion => EnvironmentInfo.Variable("TRAVIS_RUSTVersion");
        [CanBeNull] public string ScalaVersion => EnvironmentInfo.Variable("TRAVIS_SCALAVersion");
        [CanBeNull] public string XCodeSdk => EnvironmentInfo.Variable("TRAVIS_XCODE_SDK");
        [CanBeNull] public string XCodeScheme => EnvironmentInfo.Variable("TRAVIS_XCODE_SCHEME");
        [CanBeNull] public string XCodeProject => EnvironmentInfo.Variable("TRAVIS_XCODE_PROJECT");
        [CanBeNull] public string XCodeWorkspace => EnvironmentInfo.Variable("TRAVIS_XCODE_WORKSPACE");
    }
}
