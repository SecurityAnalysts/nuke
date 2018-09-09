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
    /// Interface according to the <a href="https://wiki.jenkins.io/display/JENKINS/Building+a+software+project">official website</a>.
    /// </summary>
    [PublicAPI]
    [BuildServer]
    [ExcludeFromCodeCoverage]
    public class Jenkins : Host
    {
        internal Jenkins()
        {
        }

        protected internal override bool IsRunning => Environment.GetEnvironmentVariable("JENKINS_HOME") != null;
        protected internal override IOutputSink OutputSink => new ConsoleOutputSink();

        /// <summary>
        /// The current build display name, such as "#14".
        /// </summary>
        public string BuilDisplayName => EnvironmentInfo.Variable("BUILD_DISPLAY_NAME");

        /// <summary>
        /// The current build number, such as "14".
        /// </summary>
        public int BuildNumber => EnvironmentInfo.Variable<int>("BUILD_NUMBER");

        /// <summary>
        /// The current build tag, such as "jenkins-nuke-14".
        /// </summary>
        public string BuildTag => EnvironmentInfo.Variable("BUILD_TAG");

        /// <summary>
        /// The number of the executor this build is running on, Equals '0' for first executor.
        /// </summary>
        public int ExecutorNumber => EnvironmentInfo.Variable<int>("EXECUTOR_NUMBER");

        /// <summary>
        ///  For Git-based projects, this variable contains the Git branch that was checked out for the build (normally origin/master) ﻿(all the Git* properties require git plugin).
        /// </summary>
        public string GitBranch => EnvironmentInfo.Variable("GIT_BRANCH");

        /// <summary>
        ///  For Git-based projects, this variable contains the Git hash of the commit checked out for the build (like ce9a3c1404e8c91be604088670e93434c4253f03) ﻿(all the Git* properties require git plugin).
        /// </summary>
        [CanBeNull] public string GitCommit => EnvironmentInfo.Variable("GIT_COMMIT");

        /// <summary>
        ///  For Git-based projects, this variable contains the Git hash of the previous build commit (like ce9a3c1404e8c91be604088670e93434c4253f03) ﻿(all the Git* properties require git plugin).
        /// </summary>
        [CanBeNull] public string GitPreviousCommit => EnvironmentInfo.Variable("GIT_PREVIOUS_COMMIT");

        /// <summary>
        ///  For Git-based projects, this variable contains the Git hash of the last successful build (like ce9a3c1404e8c91be604088670e93434c4253f03) ﻿(all the Git* properties require git plugin).
        /// </summary>
        [CanBeNull] public string GitPreviousSuccessfulCommit => EnvironmentInfo.Variable("GIT_PREVIOUS_SUCCESSFUL_COMMIT");

        /// <summary>
        ///  For Git-based projects, this variable contains the Git url (like git@github.com:user/repo.git or [https://github.com/user/repo.git])  (all the Git* properties require git plugin).
        /// </summary>
        [CanBeNull] public string GitUrl => EnvironmentInfo.Variable("GIT_URL");

        /// <summary>
        /// The path to the jenkins home directory.
        /// </summary>
        public string JenkinsHome => EnvironmentInfo.Variable("JENKINS_HOME");

        /// <summary>
        /// The jenkins server cookie.
        /// </summary>
        public string JenkinsServerCookie => EnvironmentInfo.Variable("JENKINS_SERVER_COOKIE");

        /// <summary>
        /// The base name of the current job, such as "Nuke".
        /// </summary>
        public string JobBaseName => EnvironmentInfo.Variable("JOB_BASE_NAME");

        /// <summary>
        /// The url to the currents job overview.
        /// </summary>
        public string JobDisplayUrl => EnvironmentInfo.Variable("JOB_DISPLAY_URL");

        /// <summary>
        /// The  name of the current job, such as "Nuke".
        /// </summary>
        public string JobName => EnvironmentInfo.Variable("JOB_NAME");

        /// <summary>
        /// The  labels of the node this build is running on, such as "win64 msbuild".
        /// </summary>
        public string NodeLabels => EnvironmentInfo.Variable("NODE_LABELS");

        /// <summary>
        /// The name of the node this build is running on, such as "master".
        /// </summary>
        public string NodeName => EnvironmentInfo.Variable("NODE_NAME");

        /// <summary>
        /// The url to the currents run changes page.
        /// </summary>
        public string RunChangesDisplayUrl => EnvironmentInfo.Variable("RUN_CHANGES_DISPLAY_URL");

        /// <summary>
        /// The url to the currents run overview page.
        /// </summary>
        public string RunDisplayUrl => EnvironmentInfo.Variable("RUN_DISPLAY_URL");

        /// <summary>
        /// The path to the folder this job is running in.
        /// </summary>
        public string Workspace => EnvironmentInfo.Variable("WORKSPACE");
    }
}
