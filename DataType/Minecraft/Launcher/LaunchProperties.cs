using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct LaunchProperties
    {
        public struct JVMPropertiesStruct
        {
            public int MaxHeapSize { get; set; }

            public int NewGenHeapSize { get; set; } = 128;

            public GCTypeEnum GCType { get; set; } = GCTypeEnum.G1GC;

            public bool UseAdaptiveSizePolicy = true;

            public bool OmitStackTraceInFastThrow = true;

            public enum GCTypeEnum
            {
                G1GC,
                ZGC,
                SerialGC,
                ParallelGC
            }

            public JVMPropertiesStruct() { }
        }

        public struct GamePropertiesStruct
        {
            public string Username { get; set; }

            public string Version { get; set; }

            public string GameDir { get; set; }

            public string AssetsDir { get; set; }

            public string AssetIndex { get; set; }

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

        public JVMPropertiesStruct JVMProperties = default;

        public GamePropertiesStruct GameProperties = default;

        public LaunchProperties() { }
    }
}
