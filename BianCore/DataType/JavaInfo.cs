using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType
{
    public class JavaInfo
    {
        public readonly Version JavaVersion;
        public readonly string JavaPath;

        internal JavaInfo(string version, string javaPath)
        {
            if (!version.Contains(".")) version += ".0";
            Version javaVersion = Version.Parse(version);
            this.JavaVersion = javaVersion;
            this.JavaPath = javaPath;
        }

        public override bool Equals(object obj)
        {
            JavaInfo javaInfo = obj as JavaInfo;
            return this == javaInfo;
        }

        public override int GetHashCode() { return base.GetHashCode(); }

        public static bool operator ==(JavaInfo j1, JavaInfo j2) { return j1?.JavaPath == j2?.JavaPath; }

        public static bool operator !=(JavaInfo j1, JavaInfo j2) { return !(j1 == j2); }
    }
}
