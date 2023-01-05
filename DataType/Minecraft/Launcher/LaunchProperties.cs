using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct LaunchProperties
    {
        public class JVMPropertiesStruct
        {
            public int MaxHeapSize { get; set; }

            public int NewGenHeapSize { get; set; } = 128;

            public GCTypeEnum GCType { get; set; } = GCTypeEnum.G1GC;

            public bool UseAdaptiveSizePolicy { get; set; } = false;

            public bool OmitStackTraceInFastThrow { get; set; } = false;

            public bool FML_IgnoreInvalidMinecraftCertificates { get; set; } = true;

            public bool FML_IgnorePatchDiscrepancies { get; set; } = true;

            public enum GCTypeEnum
            {
                G1GC,
                ZGC,
                SerialGC,
                ParallelGC
            }

            public JVMPropertiesStruct() { }
        }

        public class GamePropertiesStruct
        {
            public string Username { get; set; }

            public string UUID { get; set; }

            public string AccessToken { get; set; }

            public UserTypeEnum UserType { get; set; }

            public string VersionType { get; set; }

            public int WindowWidth { get; set; } = 854;

            public int WindowHeight { get; set; } = 480;

            public enum UserTypeEnum
            {
                /// <summary>
                /// 正版登录
                /// </summary>
                Mojang,
                /// <summary>
                /// 离线登录
                /// </summary>
                Legacy
            }

            public GamePropertiesStruct() { }
        }

        public JVMPropertiesStruct JVMProperties { get; set; } = new JVMPropertiesStruct();

        public GamePropertiesStruct GameProperties { get; set; } = new GamePropertiesStruct();

        public VersionInfo LaunchVersion { get; set; }

        public LaunchProperties() { }
    }
}
