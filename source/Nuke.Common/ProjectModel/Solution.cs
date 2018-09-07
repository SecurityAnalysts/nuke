// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Nuke.Common.ProjectModel
{
    [PublicAPI]
    public class Solution
    {
        private static readonly Guid s_solutionFolderGuid = Guid.Parse("2150E333-8FDC-42A3-9474-1A3956D46DE8");
        
        private readonly IReadOnlyCollection<string> _header;
        private readonly List<Project> _projects;
        private readonly Dictionary<Project, Project> _parents;

        internal Solution(string path, IEnumerable<string> header)
        {
            Path = path;
            _header = header.ToList().AsReadOnly();
            _projects = new List<Project>();
        }

        public string Path { get; }
        public string[] Header { get; }
        public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

        public static implicit operator string(Solution solution)
        {
            return solution.Path;
        }

        public Project AddProject(string name, Guid typeId, string path = null, Guid? projectId = null, Project parent = null)
        {
            projectId = projectId ?? Guid.NewGuid();
            ControlFlow.Assert(_projects.All(x => x.ProjectId != projectId), $"Id {projectId} is already taken.");
            
            var project = new Project(this, projectId.Value, name, path ?? name, typeId);
            _projects.Add(project);
            
            ControlFlow.Assert(parent == null || parent.ProjectId.Equals(s_solutionFolderGuid), "Parent project can only be a solution folder.");
            _parents.Add(project, parent);

            return project;
        }

        /// <summary>
        /// Removes a project from a solution.
        /// </summary>
        /// <returns>The list of child projects which have bee moved upwards due to removal of the project.</returns>
        public IReadOnlyCollection<Project> RemoveProject(Project project)
        {
            // _projects.Remove(project);
            // foreach (var pair in _parents)
            // {
            //     if (pair.Key)
            // }

            return null;
        }

        internal Project GetParentProject(Project project)
        {
            return _parents[project];
        }
    }

    public static class SolutionExtensions
    {
        
        [CanBeNull]
        public static Project GetProject(this Solution solution, string wildcardPattern)
        {
            var projects = solution.GetProjects(wildcardPattern).ToList();
            ControlFlow.Assert(projects.Count <= 1, "projects.Count <= 1");
            return projects.SingleOrDefault();
        }

        public static IEnumerable<Project> GetProjects(this Solution solution, string wildcardPattern)
        {
            wildcardPattern = $"^{wildcardPattern}$";
            var regex = new Regex(wildcardPattern
                .Replace(".", "\\.")
                .Replace("*", ".*"));
            return solution.Projects.Where(x => regex.IsMatch(x.Name));
        }
    }
}
