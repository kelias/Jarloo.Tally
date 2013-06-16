using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Caliburn.Micro;
using Jarloo.Tally.Models;

namespace Jarloo.Tally
{
    internal interface IProcessor
    {
        void Process(State state, CancellationTokenSource cs);
    }

    internal class Processor : IProcessor
    {
        private State state;

        private CancellationTokenSource cancelSource;

        public void Process(State s, CancellationTokenSource cs)
        {
            state = s;
            cancelSource = cs;

            ProcessFolder(state.TargetFolder);
        }

        private void ProcessFolder(string path)
        {
            Execute.BeginOnUIThread(()=>state.TotalFolderCount++);

            IEnumerable<string> proj = Directory.EnumerateFiles(path);

            foreach (string s in proj)
            {
                if (cancelSource.IsCancellationRequested) break;

                string ext = Path.GetExtension(s).ToUpper();

                //only support c# and vb atm.
                if (ext != ".CSPROJ" && ext != ".VBPROJ") continue;

                ProcessProject(s);
            }

            IEnumerable<string> directories = Directory.EnumerateDirectories(path);

            Parallel.ForEach(directories, ProcessFolder);
        }

        private void ProcessProject(string projectPath)
        {
            Execute.BeginOnUIThread(()=>state.TotalProjectCount++);

            string basePath = Path.GetDirectoryName(projectPath);

            Project project = new Project {Path = projectPath};
            Execute.BeginOnUIThread(()=>state.Projects.Add(project));

            string data = File.ReadAllText(projectPath);

            //yes this works in Visual Studio 2010, they have an old year in the namespace
            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            XDocument doc = XDocument.Parse(data);

            XElement ele = doc.Root.Element(ns + "PropertyGroup").Element(ns + "AssemblyName");

            if (ele != null) Execute.BeginOnUIThread(()=>project.Name = ele.Value);

            var pages = doc.Root.Elements(ns + "ItemGroup").Elements(ns + "Page");

            Parallel.ForEach(pages, (p) =>
                {
                    if (cancelSource.IsCancellationRequested) return;
                    string fullPath = Path.Combine(basePath, p.Attribute("Include").Value);
                    ProcessFile(fullPath, project);
                });

            var files = doc.Root.Elements(ns + "ItemGroup").Elements(ns + "Compile");

            foreach (var f in files)
            {
                if (cancelSource.IsCancellationRequested) break;
                string fullPath = Path.Combine(basePath, f.Attribute("Include").Value);
                ProcessFile(fullPath, project);
            }
        }

        private void ProcessFile(string path, Project project)
        {
            if (!File.Exists(path)) return;

            ProjectFile f = new ProjectFile {Path = path};
            f.Name = Path.GetFileNameWithoutExtension(path);
            f.Extension = Path.GetExtension(path).ToUpper();

            project.Files.Add(f);

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (cancelSource.IsCancellationRequested) break;

                string l = line.Replace("\t", "").Replace("\n", "").Replace("\r", "").Trim();

                if (String.IsNullOrEmpty(l)) continue;

                f.LineCount++;

                //Ignore brackets for signifigant count
                if (l == "{" || l == "}") continue;
                f.SignifigantLineCount++;
            }

            Execute.BeginOnUIThread(() =>
                {
                    state.TotalLineCount += f.LineCount;
                    state.TotalFileCount++;
                    project.TotalLineCount += f.LineCount;
                });
        }
    }
}