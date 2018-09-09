﻿// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.Tooling
{
    public class Process2 : IProcess
    {
        private readonly Process _process;
        private readonly int? _timeout;

        public Process2(Process process, int? timeout, IReadOnlyCollection<Output> output)
        {
            _process = process;
            _timeout = timeout;
            Output = output;
        }

        public string FileName => _process.StartInfo.FileName;

        public string Arguments => _process.StartInfo.Arguments;

        public string WorkingDirectory => _process.StartInfo.WorkingDirectory;

        public IReadOnlyCollection<Output> Output { get; private set; }

        public int ExitCode => _process.ExitCode;

        public void Dispose()
        {
            _process.Dispose();
        }

        public void Kill()
        {
            _process.Kill();
        }

        public bool WaitForExit()
        {
            // TODO: we are assuming that this method is called directly after process creation
            // use _process.StartTime
            var hasExited = _process.WaitForExit(_timeout ?? -1);
            if (!hasExited)
                _process.Kill();
            return hasExited;
        }
    }
}
