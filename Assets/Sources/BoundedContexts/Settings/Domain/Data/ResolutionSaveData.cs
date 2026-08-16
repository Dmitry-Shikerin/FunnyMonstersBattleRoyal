using System;

namespace Sources.BoundedContexts.Settings.Domain.Data
{
    [Serializable]
    public struct ResolutionSaveData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
    }
}