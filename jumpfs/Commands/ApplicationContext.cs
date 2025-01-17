﻿using System;
using System.IO;
using jumpfs.Bookmarking;

namespace jumpfs.Commands
{
    public class ApplicationContext
    {
        private readonly PathConverter _pathConverter;
        public readonly string[] Args = Array.Empty<string>();
        public readonly TextWriter ErrorStream;
        public readonly TextWriter OutputStream;
        public readonly BookmarkRepository Repo;

        public ApplicationContext(
            BookmarkRepository repo,
            TextWriter outputStream,
            TextWriter errorStream)
        {
            Repo = repo;

            OutputStream = outputStream;
            ErrorStream = errorStream;
            _pathConverter = new PathConverter(repo.Environment.GetEnvironmentVariable(EnvVariables.WslRootVar));
        }

        public void WriteLine(string str) => OutputStream.WriteLine(str);
        public string ToNative(string path) => _pathConverter.ToShell(Repo.Environment.ShellType, path);

        public string ToAbsolutePath(string path)
        {
            var pm = new FullPathCalculator(Repo.Environment);
            var abs = pm.ToAbsolute(path);
            return _pathConverter.ToShell(ShellType.PowerShell, abs);
        }
    }
}